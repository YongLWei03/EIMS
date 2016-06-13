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
using EIMS_Login.Ordinaryusers;

namespace EIMS_Login
{
    /// <summary>
    /// FinancialAdministrator_2.xaml 的交互逻辑
    /// </summary>
    public partial class FinancialAdministrator_FinancialUse : UserControl
    {
        DataTable dt = new DataTable();
        Connection Temp = new Connection();
        string TableSql;
        public FinancialAdministrator_FinancialUse()
        {
            TableSql = "select * from OutlayCost,Types where ItemId = TypeId";
            InitializeComponent();
            InitTabelToOutLayIn();
            FinancialUseTable.DataTableSelect(TableSql, "更新");
            FinancialUseTable.dataGrid.SelectionChanged += FinancialUseTable_SelectionChanged;
            dt = FinancialUseTable.Getdt();
            Total.Content = dt.Rows.Count;//获取总行数
        }
        private void InitTabelToOutLayIn()
        {
            FinancialUseTable.InitTableHeightWidth(460, 878);
            FinancialUseTable.SetCanUserAddRows(false);
            FinancialUseTable.AddColumns("Id", "出账编号", 80);
            FinancialUseTable.AddColumns("CDate", "出账日期", 148);
            FinancialUseTable.AddColumns("ItemId", "项目编号", 100);
            FinancialUseTable.AddColumns("CSum", "使用金额", 80);
            FinancialUseTable.AddColumns("Ryname", "经办人", 80);
            FinancialUseTable.AddColumns("Ryname1", "批准人", 170);
            FinancialUseTable.AddColumns("CDescribe", "费用摘要", 170);
            FinancialUseTable.AddColumns("Memo", "备注", 220);
        }

        private void FinancialUseTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int row = FinancialUseTable.dataGrid.SelectedIndex;//获取选中的行数。以0开头
            if (row == -1) return;
            Id.Text = dt.Rows[row]["Id"].ToString();
            CDate.Text = dt.Rows[row]["CDate"].ToString();
            Itemld.Text = dt.Rows[row]["ItemId"].ToString();
            CSum.Text = dt.Rows[row]["CSum"].ToString();
            Ryname.Text = dt.Rows[row]["Ryname"].ToString();
            Ryname1.Text = dt.Rows[row]["Ryname1"].ToString();
            CDescribe.Text = dt.Rows[row]["CDescribe"].ToString();
            Memo.Text = dt.Rows[row]["Memo"].ToString();

        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            string Date = DateTime.Now.ToString("yyyy-MM-dd") + " " + DateTime.Now.ToLongTimeString().ToString(); ;//获取当前时间
            string Submitsql = "insert into OutlayCost values('" + Date + "','" + Itemld.Text + "','" + CSum.Text + "','" + Ryname.Text + "','" + Ryname1.Text + "','" + CDescribe.Text + "','" + Memo.Text + "')";
            string SQL2 = "insert into Types values('" + Itemld.Text + "','" + TypeName.Text + "',2)";
            try
            {
                SqlCommand cmd = new SqlCommand(Submitsql, Temp.GetConn());
                cmd.ExecuteNonQuery();

                cmd.CommandText = SQL2;
                cmd.ExecuteNonQuery();
            }
            catch(Exception se)
            {
                MessageBox.Show("提交失败!"+ se);
                return;
            }
            FinancialUseTable.DataTableSelect(TableSql, "更新");
            dt = FinancialUseTable.Getdt();
           
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            int numId = Convert.ToInt32(Search.Text);
            string sql = "select * from OutlayCost where Id = '" + numId + "'";
            try
            {
                FinancialUseTable.DataTableSelect(sql, "更新");
            }
            catch
            {
                MessageBox.Show("搜索失败!");
                return;
            }
           
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            string[] Str = { "出账编号", "使用日期", "经费明细编号", "金额",  "经办人", "批准人",
                "费用摘要", "备注" };
            FinancialUseTable.ExportExcel("select * from OutlayCost", Str, "出账表格.xlsx");
        }

        private void Itemld_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void CSum_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) ||
                (e.Key >= Key.D0 && e.Key <= Key.D9) ||
                 e.Key == Key.Back ||
                 e.Key == Key.Left || e.Key == Key.Right)
            {
                if (e.KeyboardDevice.Modifiers != ModifierKeys.None)
                {
                    e.Handled = true;
                }
            }
            else
            {
                e.Handled = true;
            }
        }

        private void Itemld_KeyDown_1(object sender, KeyEventArgs e)
        {
            if ((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) ||
               (e.Key >= Key.D0 && e.Key <= Key.D9) ||
                e.Key == Key.Back ||
                e.Key == Key.Left || e.Key == Key.Right)
            {
                if (e.KeyboardDevice.Modifiers != ModifierKeys.None)
                {
                    e.Handled = true;
                }
            }
            else
            {
                e.Handled = true;
            }
        }

    }
}
