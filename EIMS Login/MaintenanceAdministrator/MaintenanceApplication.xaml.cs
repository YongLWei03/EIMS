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

namespace EIMS_Login.MaintenanceAdministrator
{
    public enum statusEnum
    {
        未操作,
        同意,
        不同意
    }
    /// <summary>
    /// MaintenanceApplication.xaml 的交互逻辑
    /// </summary>
    public partial class MaintenanceApplication : UserControl
    {
        Connection TempConn = new Connection();
        DataTable maintaintable = new DataTable();
        public MaintenanceApplication()
        {
            InitializeComponent();
        }

        public void connectataBase(int myselect)
        {
            statusEnum newstatus = new statusEnum();
            newstatus = (statusEnum)myselect;
            string sqll = "Select * From ApplyMaintain where Status = '" + newstatus + "'";
            SqlDataAdapter sqldata = new SqlDataAdapter(sqll, TempConn.GetConn());
            DataSet ds = new DataSet();
            sqldata.Fill(ds);
            maintenance.ItemsSource = ds.Tables[0].DefaultView;
            maintaintable = ds.Tables[0];
        }

        private void maintenance_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MoreInf moinf = new MoreInf();
            string[] Table_Str = { "ApplyID", "Ryid", "Ryname", "Position", "ApplyDate", "Zbid", "ApplyCount", "FaultReason", "Status" };
            string[] Str = { "申请编号", "申请人编号", "申请人姓名", "申请人职位",  "申请日期", "装备编号",
                "维修数量", "故障原因", "同意状态" };
            if (maintenance.SelectedIndex != -1)
            {
                moinf.SetValues(maintaintable, maintenance.SelectedIndex, maintenance.Items.Count, Table_Str, Str, 15);
                moinf.Show();
            }
            else MessageBox.Show("当前未选中任何行！");
        }

        public bool updata()
        {
            DataTable dt = (maintenance.ItemsSource as DataView).Table;
            DataRow[] Changedata = dt.Select("Status Like '同意'");

            SqlCommand maintain = new SqlCommand();
            maintain.Connection = TempConn.GetConn();

            int i = 0;
            while (Changedata.Length > i)
            {
                try
                {
                    decimal dNum = 0;
                    string ZBsetsql = "update ApplyMaintain set Status = '同意' where ApplyID = '" + Changedata[i][0] + "'";
                    SqlCommand ZBset = new SqlCommand(ZBsetsql, TempConn.GetConn());
                    string insertsql = "insert into ArmsRepair values('" + Changedata[i][1] + "' , '','" + Changedata[i][5] + "','','','"
                        + Changedata[i][7] + "','已送修' , "+dNum+",'','','" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                    maintain.CommandText = insertsql;
                    ZBset.ExecuteNonQuery();
                    maintain.ExecuteNonQuery();
                    i++;
                }
                catch
                {
                    MessageBox.Show("错误！");
                    return false;
                }
            }
            return true;
        }
    }
}
