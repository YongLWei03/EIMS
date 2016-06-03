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

namespace EIMS_Login.WarehouseManager.warehousedatagrid
{
    public enum statusEnum
    {
        未操作,
        不同意,
        同意
    }
    /// <summary>
    /// transferapplicationdatagrid.xaml 的交互逻辑
    /// </summary>
    public partial class transferapplicationdatagrid : UserControl
    {
        public statusEnum Mystatus
        {
            get;
            set;
        }
        public transferapplicationdatagrid()
        {
            InitializeComponent();
            connectatabase();
        }
        public void connectatabase()
        {
            string sqll = "Select * From ApplyEquip";
            SqlDataAdapter sqldata = new SqlDataAdapter(sqll,Connection.lo_conn);
            DataSet ds = new DataSet();
            sqldata.Fill(ds);
            Application.ItemsSource = ds.Tables[0].DefaultView;
        }

        private void Application_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }
    }
}
