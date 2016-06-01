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
    /// OrdinaryUsers_DataSearch.xaml 的交互逻辑
    /// </summary>
    public partial class OrdinaryUsers_DataSearch : UserControl
    {


        Connection Temp = new Connection();
        string SQLStr1 = "select * from ArmsData, Types where ArmsData.TypeId = Types.TypeId ";
        DataTable dt = new DataTable();
        DataTable dtTypes = new DataTable();
        public OrdinaryUsers_DataSearch()
        {
            InitializeComponent();
            InitTabelToData();
            Inittthlbm();
            TableToData.DataTableSelect(SQLStr1, "更新");
            dt = TableToData.Getdt();
            TableToData.dataGrid.SelectionChanged += TableToData_SelectionChanged;
            DataTotal.Content = TableToData.Rows;
        }
        private void InitTabelToData()
        {
            TableToData.InitTableHeightWidth(494, 884);
            TableToData.SetCanUserAddRows(false);
            TableToData.AddColumns("DataNo", "资料编号", 80);
            TableToData.AddColumns("DataName", "资料名称", 120);
            TableToData.AddColumns("TypeName", "资料类型", 110);
            TableToData.AddColumns("ICount", "数量", 50);
            TableToData.AddColumns("IPrice", "价格", 80);
            TableToData.AddColumns("GreateDate", "创建日期", 180);
            TableToData.AddColumns("Memo", "备注", 260);
        }
        public void Inittthlbm()
        {
            string Str = "无操作";
            TableToData.dgMenu.Items.Add(TableToData.AddMenuItem(Str));
        }
        public void UpdateLeft(int row)
        {
            DataID.Text = dt.Rows[row]["DataNo"].ToString();
            DataName.Text = dt.Rows[row]["DataName"].ToString();
            ClassifyID.Text = dt.Rows[row]["TypeName"].ToString();
            DataCount.Text = dt.Rows[row]["ICount"].ToString();
            DataPrice.Text = dt.Rows[row]["IPrice"].ToString();
            CreationDate.Text = dt.Rows[row]["GreateDate"].ToString();
            DataRemark.Text = dt.Rows[row]["Memo"].ToString();
        }
        private void TableToData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateLeft(TableToData.dataGrid.SelectedIndex);
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string sql = "select * from ArmsData where DataNo = '" + SearchText .Text+ "'";
            string sqlstr2 = "select * from ArmsData, Types where (ArmsData.DataNo ='" + SearchText.Text + "') and (ArmsData.TypeId = Types.TypeId)";
            try
            {
                SqlCommand CMD_1 = new SqlCommand(sql, Temp.GetConn());
                SqlDataReader Sdr_1 = CMD_1.ExecuteReader();
                if (!Sdr_1.Read()) MessageBox.Show("错误：暂无编号为 "+SearchText .Text+" 的资料！");
                else
                {
                    Sdr_1.Close();
                    CMD_1.CommandText = sqlstr2;
                    Sdr_1 = CMD_1.ExecuteReader();
                    Sdr_1.Read();
                    DataID.Text = Sdr_1[0].ToString();
                    DataName.Text = "《" + Sdr_1[1].ToString() + "》";
                    ClassifyID.Text = Sdr_1[8].ToString();
                    DataCount.Text = Sdr_1[3].ToString();
                    DataPrice.Text = "￥" + Sdr_1[4].ToString();
                    CreationDate.Text = Sdr_1[5].ToString();
                    DataRemark.Text = Sdr_1[6].ToString();
                }
                Sdr_1.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误："+ ex);
            }
        }
    }
}
