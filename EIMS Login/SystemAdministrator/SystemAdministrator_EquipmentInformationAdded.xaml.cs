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

namespace EIMS_Login
{
    /// <summary>
    /// SystemAdministrator_4.xaml 的交互逻辑
    /// </summary>
    public partial class SystemAdministrator_EquipmentInformationAdded : UserControl
    {
        Connection Temp = new Connection();
        MoreInf moinf;
        SqlCommand ZbSearch = new SqlCommand();
        SqlDataAdapter ado = new SqlDataAdapter();       
        DataSet Mydataset = new DataSet();
        string[] Str = { "装备编号", "装备名称", "规格型号", "装备类别", "计量单位"};
        public SystemAdministrator_EquipmentInformationAdded()
        {
            InitializeComponent();
            InitEquipmentTable();
            EquipmentTable.DataTableSelect("select * from ArmsInfo", "更新");
            InitRightBm();
        }

        private void Search_button_Click(object sender, RoutedEventArgs e)
        {
           string Search = "select * from ArmsInfo where ZbId = '" + SearchText.Text + "'";
           try
           {
               ZbSearch.Connection = Temp.GetConn();
               ZbSearch.CommandText = Search;
               ado.SelectCommand = ZbSearch;
               ado.Fill(Mydataset, "equipinfo");
               foreach(DataRow row in Mydataset.Tables["equipinfo"].Rows)
               {
                   if (row["ZbId"].Equals(null))
                   {
                       throw (new Exception());
                   }
               }
               EquipmentNumText.Text = Mydataset.Tables["equipinfo"].Rows[0]["ZbId"].ToString();
               EquipmentIdText.Text = Mydataset.Tables["equipinfo"].Rows[0]["Zbname"].ToString();
               SpecificationsModelText.Text = Mydataset.Tables["equipinfo"].Rows[0]["Zbspec"].ToString();
               Equipment_categoryText.Text = Mydataset.Tables["equipinfo"].Rows[0]["Zbkind"].ToString();
               MeasurementUnitText.Text = Mydataset.Tables["equipinfo"].Rows[0]["Zbunit"].ToString();
           }
           catch
           {
               MessageBox.Show("搜索失败！");
               return;
           }
           

            
        }


        private void Submit_image_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (AddEquipment.SelectedIndex == 0)
                {
                    string AddEquip = "insert into ArmsInfo values('" + EquipmentNumText.Text + "','" + EquipmentIdText.Text + "','" + SpecificationsModelText.Text + "','" + Equipment_categoryText.Text + "','" + MeasurementUnitText.Text + "')";
                    ZbSearch.Connection = Temp.GetConn();
                    ZbSearch.CommandText = AddEquip;
                    ZbSearch.ExecuteNonQuery();
                }
                if (AddEquipment.SelectedIndex == 1)
                {
                    MessageBox.Show("该模式下无法提交！");
                }
                if (AddEquipment.SelectedIndex == 2)
                {
                    SqlCommandBuilder sqlcb = new SqlCommandBuilder(ado);
                    Mydataset.Tables["equipinfo"].Rows[0]["ZbId"] = EquipmentNumText.Text;
                    Mydataset.Tables["equipinfo"].Rows[0]["Zbname"] = EquipmentIdText.Text;
                    Mydataset.Tables["equipinfo"].Rows[0]["Zbspec"] = SpecificationsModelText.Text;
                    Mydataset.Tables["equipinfo"].Rows[0]["Zbkind"] = Equipment_categoryText.Text;
                    Mydataset.Tables["equipinfo"].Rows[0]["Zbunit"] = MeasurementUnitText.Text;
                }
            }
            catch
            {
                MessageBox.Show("提交失败！");
            }
          
        }
        private void InitEquipmentTable()
        {

            EquipmentTable.InitTableHeightWidth(240, 1250);
            EquipmentTable.SetCanUserAddRows(false);
            EquipmentTable.AddColumns("ZbId", "装备编号", 270);
            EquipmentTable.AddColumns("Zbname", "装备名称", 270);
            EquipmentTable.AddColumns("Zbspec", "规格型号", 270);
            EquipmentTable.AddColumns("Zbkind", "装备类别", 270);
            EquipmentTable.AddColumns("Zbunit", "计量单位", 152);           
        }

        private void EquipmentExport_button_Click(object sender, RoutedEventArgs e)
        {
            EquipmentTable.ExportExcel("select * from ArmsInfo", Str, "装备信息表.xlsx"); 
        }
        public void InitRightBm()
        {
            MenuItem EquipmentMenu = EquipmentTable.AddMenuItem("更多信息");
            MenuItem DeletRowMenu1 = EquipmentTable.AddMenuItem("删除选中的行");
            EquipmentMenu.Click += EventContent;
            DeletRowMenu1.Click += DeletRows1;
            EquipmentTable.dgMenu.Items.Add(EquipmentMenu);
            EquipmentTable.dgMenu.Items.Add(DeletRowMenu1);
        }

        public void EventContent(object sender, RoutedEventArgs e)
        {
            moinf = new MoreInf();
            string[] Table_Str = { "ZbId", "Zbname", "Zbspec", "Zbkind", "Zbunit" };
            if (EquipmentTable.dataGrid.SelectedIndex != -1)
            {
                moinf.SetValues(EquipmentTable.Getdt(), EquipmentTable.dataGrid.SelectedIndex, EquipmentTable.Rows, Table_Str, Str, 5);
                moinf.Show();
            }
            else MessageBox.Show("当前未选中任何行！");
        }
        public void DeletRows1(object sender, RoutedEventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Temp.GetConn();
            if (MessageBox.Show("您确定要删除吗？", "系统提示：", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
            {
                try
                {
                    for (int i = EquipmentTable.dataGrid.SelectedItems.Count - 1; i >= 0; i--)
                    {
                        cmd.CommandText = "delete from ArmsInfo where ZbId= '" + ((DataRowView)EquipmentTable.dataGrid.SelectedItems[i]).Row["ZbId"].ToString() + "'";
                        cmd.ExecuteNonQuery();
                        ((DataRowView)EquipmentTable.dataGrid.SelectedItems[i]).Delete();
                    }
                }
                catch (Exception ae)
                {
                    MessageBox.Show("删除失败！");
                }
            }

        }
    }
}
