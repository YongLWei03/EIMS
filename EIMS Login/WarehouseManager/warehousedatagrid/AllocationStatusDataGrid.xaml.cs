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
    public enum ClmpleteStatusEnum
    {
        未完成,
        完成
    }
    /// <summary>
    /// AllocationStatusDataGrid.xaml 的交互逻辑
    /// </summary>
    public partial class AllocationStatusDataGrid : UserControl
    {
        Connection TempConn = new Connection();
        DataTable allotable = new DataTable();
        public AllocationStatusDataGrid()
        {
            InitializeComponent();
        }
        public void connectataBase(int myselect)
        {
            ClmpleteStatusEnum newstatus = new ClmpleteStatusEnum();
            newstatus = (ClmpleteStatusEnum)myselect;
            string sqll = "Select * From ArmsAllo where Status = '" + newstatus + "'";
            SqlDataAdapter sqldata = new SqlDataAdapter(sqll, TempConn.GetConn());
            DataSet ds = new DataSet();
            sqldata.Fill(ds);
            Allocation.ItemsSource = ds.Tables[0].DefaultView;
            allotable = ds.Tables[0];
        }

        private void Allocation_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }

        public bool Updata()
        {
            try
            {
                DataTable dt = (Allocation.ItemsSource as DataView).Table;
                DataRow[] Changedata = dt.Select("Status Like '完成'");
                int i = 0;
                while (Changedata.Length > i)
                {
                    try
                    {
                        string Allosetsql = "update ArmsAllo set Status = '完成',Person = '"+Changedata[i][8]+"',Ryname = '"+Changedata[i][11]+"' where Id = '" + Changedata[i][0] + "'";
                        SqlCommand Alloset = new SqlCommand(Allosetsql, TempConn.GetConn());
                        string insertsql = "insert into Takeout values('" + Changedata[i][7] + "' , '" + Changedata[i][2] + "','" + Changedata[i][4] + "','" + Changedata[i][3] + "','" + Changedata[i][5] + "','"
                            + Changedata[i][10] + "','" + Changedata[i][11] + "','" + Changedata[i][12] + "','" + Changedata[i][13] + "')";
                        Alloset.ExecuteNonQuery();
                        SqlCommand setout = new SqlCommand(insertsql, TempConn.GetConn());
                        setout.ExecuteNonQuery();
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
            catch
            {
                MessageBox.Show("错误！");
                return false;
            }
            
        }

        private void view_Click(object sender, RoutedEventArgs e)
        {
            MoreInf moinf = new MoreInf();
            string[] Table_Str = { "Id", "RyId", "ZbId", "ANum", "Zbprice", "OutDep", "InDep", "AType", "Person", "UsefulDate", "Ryname1", "Ryname", "ADate", "Memo", "Status"  };
            string[] Str = { "调拨单号", "人员编号", "调出装备编号", "申请数量",  "装备单价", "调出单位",
                "调入单位", "调拨方式", "提货人", "有效期", "批准人", "承办人", "批准日期", "备注", "完成状态" };
            if (Allocation.SelectedIndex != -1)
            {
                moinf.SetValues(allotable, Allocation.SelectedIndex, Allocation.Items.Count, Table_Str, Str,15);
                moinf.Show();
            }
            else MessageBox.Show("当前未选中任何行！");
        }
    }

}
