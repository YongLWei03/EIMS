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
        DataTable Applydt = new DataTable();
        Connection TempConn = new Connection();
        string UserID, ApplyType, TempStr,TempId;
        int TotalRows;
        public ReminderLableRegion()
        {
            InitializeComponent();
        }
        public void SetValues(string User, string Str)
        {
            UserID = User;
            ApplyType = Str;
            switch (ApplyType)
            {
                case "jy_sq": TempStr = "借阅"; TempId = "ApplyDataID"; break;
                case "zb_sq": TempStr = "装备"; TempId = "Zbid"; break;
                case "wx_sq": TempStr = "维修"; TempId = "Zbid"; break;
                default: MessageBox.Show("日志类型设置错误！"); ApplyType = null; break;
            }

        }
        public void UpdateShowLables(DataTable dt)
        {
            Applydt = dt;//保存申请表到本地缓存
            TotalRows = Applydt.Rows.Count;
            if (Applydt.Rows.Count == 0)
            {
                label1.Content = "你暂时没有提交过申请！";
                label1.Foreground = Brushes.Red;
                return;
            }
            LableShow(label1);
            LableShow(labe12);
            LableShow(labe13);
            LableShow(labe14);
            LableShow(labe15);

        }
        private void LableShow(Label Temp)
        {
            TotalRows--;
            if (TotalRows < 0) return;
            else if (Applydt.Rows[TotalRows]["Status"].ToString() == "未操作")
            {
                Temp.Content = "你在  " + Applydt.Rows[TotalRows]["ApplyDate"].ToString()+ "  提交新的" + TempStr + "申请,并且暂未被审核！";
                Temp.Foreground = new SolidColorBrush(Color.FromRgb(0,176,240));
            }
            else if (Applydt.Rows[TotalRows]["Status"].ToString() == "不同意")
            {
                Temp.Content = "你在  " + Applydt.Rows[TotalRows]["ApplyDate"].ToString()
                    + "  申请的"+ TempStr + "-(" + Applydt.Rows[TotalRows][TempId].ToString() + "),管理员不同意！";
                Temp.Foreground = Brushes.Red;
            }
            else if (Applydt.Rows[TotalRows]["Status"].ToString() == "同意")
            {
                Temp.Content = "你在  " + Applydt.Rows[TotalRows]["ApplyDate"].ToString()
                    + "  申请的" + TempStr + "-(" + Applydt.Rows[TotalRows][TempId].ToString() + "),管理员已同意！";
                Temp.Foreground = Brushes.Green;
            }
        }
       
    }
}
