using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

namespace EIMS_Login.WarehouseManager.warehousedatagrid
{
    /// <summary>
    /// TakeOutDataGrid.xaml 的交互逻辑
    /// </summary>
    public partial class TakeOutDataGrid : UserControl
    {
        Connection TempConn = new Connection();
        DataTable taketable = new DataTable();
        public TakeOutDataGrid()
        {
            InitializeComponent();
        }

        public void connectataBase()
        {
            string sqll = "Select * From Takeout";
            SqlDataAdapter sqldata = new SqlDataAdapter(sqll, TempConn.GetConn());
            DataSet ds = new DataSet();
            sqldata.Fill(ds);
            takeout.ItemsSource = ds.Tables[0].DefaultView;
            taketable = ds.Tables[0];
        }
        private void takeout_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            MoreInf moinf = new MoreInf();
            string[] Table_Str = { "ToId", "Ttype", "ZbId", "Zbprice", "Zbnum", "Sid", "Ryname1", "Ryname", "OptDate", "Memo" };
            string[] Str = { "出库编号", "出库编号", "出库装备编号", "装备单价",  "出库装备数量", "仓库编号",
                "批准人", "经办人", "出库日期", "备注" };
            if (takeout.SelectedIndex != -1)
            {
                moinf.SetValues(taketable, takeout.SelectedIndex, takeout.Items.Count, Table_Str, Str, 11);
                moinf.Show();
            }
            else MessageBox.Show("当前未选中任何行！");
        }
    }
}
