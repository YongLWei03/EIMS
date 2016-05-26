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


namespace EIMS_Login.Ordinary_users
{
    /// <summary>
    /// OrdinaryUsers_Databorrow.xaml 的交互逻辑
    /// </summary>
    
    public partial class OrdinaryUsers_Databorrow : UserControl
    {
        string ConnStr = "Data Source=橙;Initial Catalog=Temp;User ID=EIMS;Password=1";
        public OrdinaryUsers_Databorrow()
        {
            InitializeComponent();
            InitDataBaseConnection();
        }
        public void InitDataBaseConnection()
        {
            SqlConnection lo_conn = new SqlConnection(ConnStr);
            try
            {
                lo_conn.Open();
            }
            catch
            {
                MessageBox.Show("服务器未连接！");
                return;
            }
            if (ConnectionState.Open == lo_conn.State)
            {
                InitDataToAppHis();
            }
        }
        public void InitDataToAppHis()
        {
            string StrSql_0 = "select * from Temp_1";
            DataTable Tabel0 = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(StrSql_0, ConnStr);
            DataSet ds = new DataSet();
            ds.Clear();
            sda.Fill(Tabel0);
            dataGrid.ItemsSource = Tabel0.DefaultView;
        }

        private void dataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }
    }
}
