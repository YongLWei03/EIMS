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

namespace EIMS_Login.MaintenanceAdministrator
{
    /// <summary>
    /// TabelToStatus.xaml 的交互逻辑
    /// </summary>
    public partial class TabelToStatus : UserControl
    {
        string SQL = "select * from ArmsRepair";
        Connection TempConn = new Connection();
        public int Rows;
        public TabelToStatus()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Update();
        }
        public void Update()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            try
            {
                SqlDataAdapter sda = new SqlDataAdapter(SQL, TempConn.GetConnStr());
                sda.Fill(dt);
                Rows = dt.Rows.Count;
                Allocation.ItemsSource = dt.DefaultView;
                ds.Clear();
                ds.Tables.Add(dt);
            }
            catch(Exception ex)
            {
                MessageBox.Show("错误："+ex);
            }
        }
    }
}
