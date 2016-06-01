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

namespace EIMS_Login.Ordinaryusers
{
    /// <summary>
    /// ReminderLableRegion.xaml 的交互逻辑
    /// </summary>
    public partial class ReminderLableRegion : UserControl
    {
        DataTable Logdt = new DataTable();
        DataTable Applydt = new DataTable();
        Connection TempConn = new Connection();
        string UserID, LogType,TempStr;
        int TotalRows,LogTypeSetBool = 1;
        public ReminderLableRegion()
        {
            InitializeComponent();
        }
        public void SetValues(string User, string Str)
        {
            UserID = User;
            LogType = Str;
            switch (LogType)
            {
                case "jy_sq": TempStr = "借阅申请"; break;
                case "zb_sq": TempStr = "装备申请"; break;
                case "wx_sq": TempStr = "维修申请"; break;
                default: LogTypeSetBool = 0; MessageBox.Show("日志类型设置错误！"); LogType=null; break;
            }

        }
        public void UpdateShowLables(DataTable dt)
        {
            Applydt = dt;//保存申请表到本地缓存
            if (Applydt.Rows.Count == 0)
            {
                label1.Content = "你暂时没有提交过申请！";
                label1.Foreground = Brushes.Red;
                return;
            }  
            UpdateLogTable();
            LableShow(label1);
            LableShow(labe12);
            LableShow(labe13);
            LableShow(labe14);
            LableShow(labe15);

        }
        private void LableShow(Label Temp)
        {
            if (LogType != null)
            {
                TotalRows--;
                if (TotalRows < 0) return;
                else if (Applydt.Rows[TotalRows]["Status"].ToString() == "未操作")
                {
                    Temp.Content = "你在" + Logdt.Rows[TotalRows]["LogDate"].ToString() + " " + Logdt.Rows[TotalRows]["LogTime"].ToString()
                        + "提交新的" + TempStr + ",并且暂未被审核！";
                    Temp.Foreground = Brushes.Black;
                }
                else if (Applydt.Rows[TotalRows]["Status"].ToString() == "不同意")
                {
                    Temp.Content = "你在" + Logdt.Rows[TotalRows]["LogDate"].ToString() + " " + Logdt.Rows[TotalRows]["LogTime"].ToString()
                        + "申请的资料-(" + Applydt.Rows[TotalRows]["ApplyDataID"].ToString() + "),管理员不同意！";
                    Temp.Foreground = Brushes.Red;
                }
                else if (Applydt.Rows[TotalRows]["Status"].ToString() == "同意")
                {
                    Temp.Content = "你在" + Logdt.Rows[TotalRows]["LogDate"].ToString() + " " + Logdt.Rows[TotalRows]["LogTime"].ToString()
                        + "申请的资料-(" + Applydt.Rows[TotalRows]["ApplyDataID"].ToString() + "),管理员已同意！";
                    Temp.Foreground = Brushes.Green;
                }
            }
        }
        public void UpdateLogTable()
        {
            if (LogTypeSetBool == 1)
            {
                try
                {
                    DataTable Table0 = new DataTable();
                    Logdt.Clear();
                    string SQL = "select * from SysLog where UserName='" + UserID + "' and LogType = '" + LogType + "'";
                    SqlDataAdapter sda = new SqlDataAdapter(SQL, TempConn.GetConnStr());
                    sda.Fill(Table0);
                    Logdt = Table0;
                    TotalRows = Logdt.Rows.Count;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("出现异常！\n\n" + ex);
                }
            }
            else
            {
                label1.Content = "日志类型设置错误！";
                label1.Foreground = Brushes.Red;
            }
        }
    }
}
