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
    /// InventoryDataGrid.xaml 的交互逻辑
    /// </summary>
    public partial class InventoryDataGrid : UserControl
    {
        Connection TempConn = new Connection();
        DataTable invertorytable = new DataTable();
        public InventoryDataGrid()
        {
            InitializeComponent();
        }

        public void connectataBase()
        {
            string sqll = "Select * From ArmsSurplus";
            SqlDataAdapter sqldata = new SqlDataAdapter(sqll, TempConn.GetConn());
            DataSet ds = new DataSet();
            sqldata.Fill(ds);
            takeout.ItemsSource = ds.Tables[0].DefaultView;
            invertorytable = ds.Tables[0];
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MoreInf moinf = new MoreInf();
            string[] Table_Str = { "SpId", "ZbId", "Zbprice", "Zbnum", "MakeDate", "Sid", "Memo" };
            string[] Str = { "库存编号", "装备编号", "装备单价", "库存装备数量", "生产日期", "所属仓库编号", "备注" };
            if (takeout.SelectedIndex != -1)
            {
                moinf.SetValues(invertorytable, takeout.SelectedIndex, takeout.Items.Count, Table_Str, Str, 11);
                moinf.Show();
            }
            else MessageBox.Show("当前未选中任何行！");
        }

        private void takeout_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }
    }
}
