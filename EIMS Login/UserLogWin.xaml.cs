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
using System.Windows.Shapes;

namespace EIMS_Login
{
    /// <summary>
    /// UserLogWin.xaml 的交互逻辑
    /// </summary>
    public partial class UserLogWin : Window
    {
        string SQL; 
        public UserLogWin()
        {
            InitializeComponent();
        }
        public UserLogWin(string RyId)
        {
            InitializeComponent();
            SQL = "select * from SysLog where UserName ='" + RyId + "'";
            SetCulnms();
            TableToUserLog.DataTableSelect(SQL, "更新");
            label1.Content = TableToUserLog.dt.Rows.Count;
        }

        public void SetCulnms()
        {
            TableToUserLog.InitTableHeightWidth(498, 960);
            TableToUserLog.SetCanUserAddRows(false);
            TableToUserLog.AddColumns("UserName", "用户编号", 80);
            TableToUserLog.AddColumns("LogId", "日志编号", 80);
            TableToUserLog.AddColumns("LogDate", "发生日期", 140);
            TableToUserLog.AddColumns("LogTime", "发生时间", 140);
            TableToUserLog.AddColumns("LogType", "日志类型", 80);
            TableToUserLog.AddColumns("Title", "日志标题", 200);
            TableToUserLog.AddColumns("Body", "日志内容", 260);
        }
        public void SetRyId(string RyId)
        {
            SQL = "select * from SysLog UserName = '" + RyId + "'";
        }
        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            string[] Str = { "日志编号", "发生日期", "发生时间", "日志类型",  "日志标题", "日志内容",
                "操作编号" };
            TableToUserLog.ExportExcel(SQL, Str, "操作日志.xlsx");
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
