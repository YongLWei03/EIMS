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
    /// FinancialAdministrator_1.xaml 的交互逻辑
    /// </summary>
    public partial class FinancialAdministrator_BookingAndHistory : UserControl
    {

        string TableSql;
        DataTable dt = new DataTable();
        Connection Temp = new Connection();

        public FinancialAdministrator_BookingAndHistory()
        {
            TableSql = "select * from OutlayIn,Types where ItemId = TypeId";

            InitializeComponent();


            InitTabelToOutLayIn();
            TableToOutLayIn.DataTableSelect(TableSql, "更新");
            TableToOutLayIn.dataGrid.SelectionChanged += TableToOutLayIn_SelectionChanged;
            dt = TableToOutLayIn.Getdt();
            Total.Content = dt.Rows.Count;//获取总行数
        }
        private void InitTabelToOutLayIn()
        {
            TableToOutLayIn.InitTableHeightWidth(460, 878);
            TableToOutLayIn.SetCanUserAddRows(false);
            TableToOutLayIn.AddColumns("Id", "入账编号", 80);
            TableToOutLayIn.AddColumns("Source", "入账来源", 148);
            TableToOutLayIn.AddColumns("TypeName", "项目名称", 100);
            TableToOutLayIn.AddColumns("InSum", "入账金额", 80);
            TableToOutLayIn.AddColumns("Ryname", "经办人", 80);
            TableToOutLayIn.AddColumns("InDate", "入账日期", 170);
            TableToOutLayIn.AddColumns("Memo", "备注", 220);
        }
        private void TableToOutLayIn_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int row = TableToOutLayIn.dataGrid.SelectedIndex;//获取选中的行数。以0开头
            if (row == -1) return;
            Id.Text = dt.Rows[row]["Id"].ToString();
            Source.Text = dt.Rows[row]["Source"].ToString();
            Itemld.Text = dt.Rows[row]["ItemId"].ToString();
            TypeName.Text = dt.Rows[row]["TypeName"].ToString();
            InSum.Text = dt.Rows[row]["InSum"].ToString();
            Ryname.Text = dt.Rows[row]["Ryname"].ToString();
            InDate.Text = dt.Rows[row]["InDate"].ToString();
            Memo.Text = dt.Rows[row]["Memo"].ToString();


        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            string Date = DateTime.Now.ToString("yyyy-MM-dd") + " " + DateTime.Now.ToLongTimeString().ToString(); ;//获取当前时间
            string SQL1 = "insert into OutlayIn values ('" + Source.Text + "','" + Itemld.Text + "','" + InSum.Text + "','" + Ryname.Text + "','" +
                Date + "','" + Memo.Text + "')";
            string SQL2 = "insert into Types values('" + Itemld.Text + "','" + TypeName.Text + "',2)";
            try
            {
                SqlCommand CMD_1 = new SqlCommand(SQL2, Temp.GetConn());
                CMD_1.ExecuteNonQuery();

                CMD_1.CommandText = SQL1;
                CMD_1.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("错误：类型编号重复，或不可重复添加！");
                return;
            }
            TableToOutLayIn.DataTableSelect(TableSql, "更新");
            dt = TableToOutLayIn.Getdt();
        }

        private void InSum_KeyDown(object sender, KeyEventArgs e)
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

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            string[] Str = { "入账编号", "经费来源", "项目编号", "金额",  "经办人", "入账时间",
                "备注", "项目编号", "项目名称","项目标记" };
            TableToOutLayIn.ExportExcel("select * from OutlayIn", Str, "入账表格.xlsx");
        }
    }
}
