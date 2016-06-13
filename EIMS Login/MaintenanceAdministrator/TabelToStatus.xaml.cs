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
    public enum MyStatusEnum
    {
        已送修,
        维修完毕
    }
    /// <summary>
    /// TabelToStatus.xaml 的交互逻辑
    /// </summary>
    public partial class TabelToStatus : UserControl
    {
        string SQL = "select * from ArmsRepair";
        Connection TempConn = new Connection();
        DataTable dt = new DataTable();
        public TabelToStatus()
        {
            InitializeComponent();
        }
        public void connectataBase(int myselect)
        { 
            try
            {
                MyStatusEnum newstatus = new MyStatusEnum();
                newstatus = (MyStatusEnum)myselect;
                string sqll = "Select * From ArmsRepair where Status = '" + newstatus + "'";
                SqlDataAdapter sqldata = new SqlDataAdapter(sqll, TempConn.GetConn());
                DataSet ds = new DataSet();
                sqldata.Fill(ds);
                Allocation.ItemsSource = ds.Tables[0].DefaultView;
                dt = ds.Tables[0];
            }
            catch(Exception ex)
            {
                MessageBox.Show("错误："+ex);
            }
        }
        public bool updata()
        {
            try
            {
                DataTable dt = (Allocation.ItemsSource as DataView).Table;
                SqlDataAdapter Adapter = new SqlDataAdapter();
                Adapter.SelectCommand = new SqlCommand("select * from ArmsRepair", TempConn.GetConn());
                SqlCommandBuilder builder = new SqlCommandBuilder(Adapter);
                Adapter.Update(dt);
                return true;
            }
            catch
            {
                MessageBox.Show("错误！");
                return false;
            }
        }

        private void Allocation_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }
    }
}
