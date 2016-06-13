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
    /// AddWarehouseDataGrid.xaml 的交互逻辑
    /// </summary>
    public partial class AddWarehouseDataGrid : UserControl
    {
        Connection TempConn = new Connection();
        DataTable waretable = new DataTable();
        public AddWarehouseDataGrid()
        {
            InitializeComponent();
        }

        public void connectataBase()
        {
            string sqll = "Select * From Storehouse";
            SqlDataAdapter sqldata = new SqlDataAdapter(sqll, TempConn.GetConn());
            DataSet ds = new DataSet();
            sqldata.Fill(ds);
            AddWareHouse.ItemsSource = ds.Tables[0].DefaultView;
            waretable = ds.Tables[0];
        }

        public void SearchBase(string sid)
        {
            string strsql = "Select * From Storehouse Where Sid='"+sid+"'";
            SqlDataAdapter sqldata = new SqlDataAdapter(strsql, TempConn.GetConn());
            DataSet ds = new DataSet();
            sqldata.Fill(ds);
            AddWareHouse.ItemsSource = ds.Tables[0].DefaultView;
            waretable = ds.Tables[0];
        }

        private void AddWareHouse_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }
    }
}
