using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;

namespace EIMS_Login.Ordinaryusers
{
    /// <summary>
    /// OrdinaryUsers_EquipmentInventory.xaml 的交互逻辑
    /// </summary>
    public partial class OrdinaryUsers_EquipmentInventory : UserControl
    {

        Connection Temp = new Connection();
        string SQLStr1 = "select * from ArmsSurplus,ArmsInfo where ArmsSurplus.ZbId = ArmsInfo.ZbId";
        DataTable dt = new DataTable();
        DataTable dtTypes = new DataTable();
        public OrdinaryUsers_EquipmentInventory()
        {
            InitializeComponent();
            InitTabelToData();
            Inittthlbm();
            TableToInventory.DataTableSelect(SQLStr1, "更新");
            dt = TableToInventory.Getdt();
            TableToInventory.dataGrid.SelectionChanged += TableToData_SelectionChanged;
            EquipmentTotal.Content = TableToInventory.Rows;
        }
        private void InitTabelToData()
        {
            TableToInventory.InitTableHeightWidth(494, 884);
            TableToInventory.SetCanUserAddRows(false);
            TableToInventory.AddColumns("ZbId", "装备编号", 80);
            TableToInventory.AddColumns("Zbname", "装备名称", 100);
            TableToInventory.AddColumns("Zbspec", "规格型号", 100);
            TableToInventory.AddColumns("Zbkind", "装备类别", 80);
            TableToInventory.AddColumns("Zbprice", "价格", 80);
            TableToInventory.AddColumns("MakeDate", "生产日期", 100);
            TableToInventory.AddColumns("Zbnum", "库存数量", 80);
            TableToInventory.AddColumns("Memo", "备注", 260);
        }
        public void Inittthlbm()
        {
            string Str = "无操作";
            TableToInventory.dgMenu.Items.Add(TableToInventory.AddMenuItem(Str));
        }
        public void UpdateLeft(int row)
        {
            EquipmentID.Text = dt.Rows[row]["ZbId"].ToString();
            EquipmentName.Text = dt.Rows[row]["Zbname"].ToString();
            StandardType.Text = dt.Rows[row]["Zbspec"].ToString();
            EquipmentType.Text = dt.Rows[row]["Zbkind"].ToString();
            EquipmentPrice.Text = dt.Rows[row]["Zbprice"].ToString();
            ProductionDate.Text = dt.Rows[row]["MakeDate"].ToString();
            InventoryCount.Text = dt.Rows[row]["Zbnum"].ToString();
            EquipmentRemark.Text = dt.Rows[row]["Memo"].ToString();

        }
        private void TableToData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateLeft(TableToInventory.dataGrid.SelectedIndex);
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string sqlstr1 = "select * from ArmsSurplus where ZbId = '" + textBox .Text+"'";
            string sqlstr2 = "select * from ArmsSurplus, ArmsInfo where (ArmsSurplus.ZbId ='" + textBox.Text + "') and (ArmsSurplus.ZbId = ArmsInfo.ZbId)";
            try
            {
                SqlCommand CMD_1 = new SqlCommand(sqlstr1, Temp.GetConn());
                SqlDataReader Sdr_1 = CMD_1.ExecuteReader();
                if (!Sdr_1.Read()) MessageBox.Show("错误：仓库暂无编号为 "+ textBox.Text + " 的装备！");
                else
                { 
                    Sdr_1.Close();
                    CMD_1.CommandText = sqlstr2;
                    Sdr_1= CMD_1.ExecuteReader();
                    Sdr_1.Read();
                    EquipmentID.Text = Sdr_1[1].ToString();
                    EquipmentName.Text = Sdr_1[8].ToString();
                    StandardType.Text = Sdr_1[9].ToString();
                    EquipmentType.Text = Sdr_1[10].ToString();
                    EquipmentPrice.Text = "￥" + Sdr_1[2].ToString();
                    ProductionDate.Text = Sdr_1[4].ToString();
                    InventoryCount.Text = Sdr_1[3].ToString();
                    EquipmentRemark.Text = Sdr_1[6].ToString();
                }
                Sdr_1.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("错误："+ex);
            }
        }
    }
}
