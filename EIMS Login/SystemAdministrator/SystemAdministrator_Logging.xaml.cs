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
using EIMS_Login;

namespace EIMS_Login
{
    /// <summary>
    /// SystemAdministrator_3.xaml 的交互逻辑
    /// </summary>
    public partial class SystemAdministrator_Logging : UserControl
    {
        Connection Temp = new Connection();
        MoreInf moinf;
        string[] Str = { "日志编号", "事件发生日期", "事件发生时间", "事件类型", "事件标题", "操作用户账号","事件内容" };
        
        public SystemAdministrator_Logging()
        {
            InitializeComponent();
            
            InitLoggingTable();
            LoggingTable.DataTableSelect("select * from SysLog where UserName in( select RyId from ArmsUsers where User_type = '普通用户') ", "更新");
            InitRightBm();
        }
        private string loggingRy(int SelectUser)
        {
            string Oridinaryusers = "select * from SysLog where UserName in( select RyId from ArmsUsers where User_type = '普通用户') ";
            string System_administrator = "select * from SysLog where UserName in( select RyId from ArmsUsers where User_type = '系统管理员') ";
            string Maintenance_man = "select * from SysLog where UserName in( select RyId from ArmsUsers where User_type = '维修管理员') ";
            string Warehouse_manager = "select * from SysLog where UserName in( select RyId from ArmsUsers where User_type = '仓库管理员') ";
            string Finance_department = "select * from SysLog where UserName in( select RyId from ArmsUsers where User_type = '财务管理员') ";
            string Confidential_clerk = "select * from SysLog where UserName in( select RyId from ArmsUsers where User_type = '保密员') ";
            switch(SelectUser)
            {
                case -1:
                    
                    return System_administrator;
                case 0:

                    return Oridinaryusers;
                case 1:
                    
                    return System_administrator;
                case 2:
                    
                    return Maintenance_man;
                case 3:
                    
                    return Warehouse_manager;
                case 4:
                    
                    return Finance_department;
                case 5:
                    return Confidential_clerk;
            }
            return null;
        }
        private void loggingscan()
        {
            
        }
        private void InitLoggingTable()
        {
            LoggingTable.InitTableHeightWidth(449, 1250);
            LoggingTable.SetCanUserAddRows(false);
            LoggingTable.AddColumns("LogId", "日志编号", 80);
            LoggingTable.AddColumns("LogDate", "事件发生日期", 120);
            LoggingTable.AddColumns("LogTime", "事件发生时间", 120);
            LoggingTable.AddColumns("LogType", "事件类型", 120);
            LoggingTable.AddColumns("Title", "事件标题", 160);           
            LoggingTable.AddColumns("UserName", "操作用户编号", 169);
            LoggingTable.AddColumns("Body", "事件内容", 580);

        }

        private void user_DropDownClosed(object sender, EventArgs e)
        {
            LoggingTable.DataTableSelect(loggingRy(user.SelectedIndex), "更新");
        }

        private void OrdinarySearch_button_Click(object sender, RoutedEventArgs e)
        {
            string StrSql = "select * from SysLog where UserName ='" + Search.Text + "'";
            try
            {
                SqlCommand Ori = new SqlCommand(StrSql, Temp.GetConn());
                if (Ori.ExecuteScalar() == null)
                {
                    MessageBox.Show("无此人员！");
                }
                else
                {
                    LoggingTable.DataTableSelect(StrSql, "更新");
                }

            }
            catch (Exception a)
            {
                MessageBox.Show("提交失败！" + a);
                return;
            }
        }

        private void OrdinaryExport_button_Click(object sender, RoutedEventArgs e)
        {
           
            LoggingTable.ExportExcel(loggingRy(user.SelectedIndex), Str, "事件日志表格.xlsx"); 
        }

        public void InitRightBm()
        {
            MenuItem TempMenu = LoggingTable.AddMenuItem("事件内容");
            TempMenu.Click += EventContent;
            LoggingTable.dgMenu.Items.Add(TempMenu);
        }

        public void EventContent(object sender,RoutedEventArgs e)
        {
            moinf = new MoreInf();
            string[] Table_Str = { "LogId", "LogDate", "LogTime", "LogType", "Title", "UserName", "Body" };
            moinf.SetValues(LoggingTable.Getdt(), LoggingTable.dataGrid.SelectedIndex, LoggingTable.Rows, Table_Str, Str, 7);
            moinf.Show();
        }
    }
}
