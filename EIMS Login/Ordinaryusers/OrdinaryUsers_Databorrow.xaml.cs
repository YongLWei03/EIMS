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
using EIMS_Login.Ordinaryusers;//用户信息命名空间申明

namespace EIMS_Login.Ordinary_users
{
    /// <summary>
    /// OrdinaryUsers_Databorrow.xaml 的交互逻辑
    /// </summary>

    public partial class OrdinaryUsers_Databorrow : UserControl
    {
        Connection Temp = new Connection();
        OrdinaryUserInfo UITemp = new OrdinaryUserInfo();
        string ApplyTableSql;
        string HistoryTableSql;
        string ApplyType = "jy_sq";
        ApplyDataMoreInfoWindows admiw;//借阅详细信息窗口
        int admiw_OpenSign;//详细信息窗口标记，0为未打开过！
        public OrdinaryUsers_Databorrow()
        {
            InitializeComponent();
            ApplyTableSql = "select * from ApplyData where Ryid='" + UITemp.UserInfoTemp.Ryid + "'";
            HistoryTableSql = "select * from DataLend where RyId='" + UITemp.UserInfoTemp.Ryid + "'";


            InitTabelToApply();//申请历史表格初始化 
            InitTableToHistory();//借阅历史表格初始化
            TableToApply.DataTableSelect(ApplyTableSql, "更新");
            Initttalbm();
            ApplicationHistoryCount.Content = TableToApply.Rows;//申请总计
            TableToHistory.DataTableSelect(HistoryTableSql, "更新");
            Inittthlbm();
            BorrowHistoryCount.Content = TableToHistory.Rows;//借阅历史总计
            UpDateRLR(1);//更新左下侧操作历史

        }
        //初始化申请历史表格
        private void InitTabelToApply()
        {
            TableToApply.InitTableHeightWidth(210, 810);
            TableToApply.SetCanUserAddRows(false);
            TableToApply.AddColumns("Ryname", "姓名", 80);
            TableToApply.AddColumns("ApplyDataID", "资料编号", 100);
            TableToApply.AddColumns("ApplyCount", "数量", 40);
            TableToApply.AddColumns("ApplyDate", "申请日期", 170);
            TableToApply.AddColumns("ApplyID", "申请编号", 80);
            TableToApply.AddColumns("Status", "同意状态", 80);
            TableToApply.AddColumns("ApplyReason", "申请原因",240);
        }
        public void Initttalbm()
        {
            MenuItem TempMenu = TableToApply.AddMenuItem("查看更多");
            TempMenu.Click += LookMore;
            TableToApply.dgMenu.Items.Add(TempMenu);
        }
        public void LookMore(object sender, RoutedEventArgs e)
        {
            admiw = new ApplyDataMoreInfoWindows();
            admiw.SetValues(TableToApply.Getdt(), TableToApply.dataGrid.SelectedIndex, TableToApply.Rows);
            admiw.Show();
            admiw_OpenSign = 1;
        }
        //初始化借阅历史表格
        private void InitTableToHistory()
        {
            TableToHistory.InitTableHeightWidth(210, 810);
            TableToHistory.SetCanUserAddRows(false);
            TableToHistory.AddColumns("Id", "借阅号", 80);
            TableToHistory.AddColumns("DataNo", "资料编号", 100);
            TableToHistory.AddColumns("LendDate", "借阅日期", 220);
            TableToHistory.AddColumns("RyId", "借阅人编号", 120);
            TableToHistory.AddColumns("LendCount", "借阅数量", 80);
            TableToHistory.AddColumns("Ryname", "借阅人名字", 110);
            TableToHistory.AddColumns("Flag", "状态", 80);
        }
        public void Inittthlbm()
        {
            string Str = "无操作";
            TableToHistory.dgMenu.Items.Add(TableToHistory.AddMenuItem(Str));
        }

        //申请提交按钮功能
        private void ApplicationSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (UITemp.GetRyidStatus)
            {
                string Date = DateTime.Now.ToString("yyyy-MM-dd") + " " + DateTime.Now.ToLongTimeString().ToString(); ;//获取当前时间
                string StrSQL1 = "insert into ApplyData values('" + UITemp.UserInfoTemp.Ryid + "','" + UITemp.UserInfoTemp.RyName + "','" + UITemp.UserInfoTemp.Position +
                     "','" + Date + "','" + ApplicationDataNumber.Text + "',"
                    + ApplicationDataCount.Text + ",'" + ApplicationReasons.Text + "','未操作')";
                string StrSQL2 = "select * from ArmsData where DataNo ='" + ApplicationDataNumber.Text + "'";
                SqlCommand CMD_1 = new SqlCommand(StrSQL2, Temp.GetConn());
                SqlDataReader Sdr_1 = CMD_1.ExecuteReader();
                if (!Sdr_1.Read()) MessageBox.Show("错误：暂无编号为 " + ApplicationDataNumber.Text + " 的资料！");
                else
                {
                    if (Convert.ToInt32(Sdr_1[3].ToString()) < Convert.ToInt32(ApplicationDataCount.Text))
                    {
                        MessageBox.Show("编号为 " + ApplicationDataNumber.Text + " 的资料，暂不能满足申请数量！");
                        return;
                    }
                    Sdr_1.Close();
                    try
                    {
                        CMD_1.CommandText = StrSQL1;
                        CMD_1.ExecuteNonQuery();
                    }
                    catch
                    {
                        MessageBox.Show("申请失败！");
                        return;
                    }
                    MessageBox.Show("申请成功，请耐心等待批准结果。。。");
                    TableToApply.DataTableSelect(ApplyTableSql, "更新");//申请成功更新：申请历史表格
                    UpDataLog();//更新操作日志
                    UpDateRLR(0);//更新左下部操作提示
                    ApplicationHistoryCount.Content = TableToApply.Rows;//更新申请总计
                    if (admiw_OpenSign == 1)
                        admiw.updata(TableToApply.Getdt(), TableToApply.Rows);//更新查看详细信息窗口的总行数
                }
                Sdr_1.Close();
            }
            else
            {
                MessageBox.Show("个人信息拉取失败或个人信息未填写充分，无法提交申请!");
            }
        }



        private void UpDateRLR(int Sign)
        {
            if (Sign == 1)
                ApplyRLR.SetValues(UITemp.UserInfoTemp.Ryid, ApplyType);
            ApplyRLR.UpdateShowLables(TableToApply.Getdt());
        }

        private void UpDataLog()
        {
            UpDataSysLog TempLog = new UpDataSysLog();

            TempLog.SetLogValues(ApplyType, UITemp.UserInfoTemp.RyName + "提交借阅申请",
                UITemp.UserInfoTemp.RyName + "于" + DateTime.Now.ToString("yyyy年MM月dd日") + " " + DateTime.Now.ToLongTimeString().ToString()
                + "提交借阅申请，申请借阅编号为：" + ApplicationDataNumber.Text +
                "；申请借阅数量为：" + ApplicationDataCount.Text, UITemp.UserInfoTemp.Ryid);
        }
        //申请数量TextBox设置为只能输入数字！
        private void ApplicationDataCount_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.Key >= Key.NumPad0 && e.Key <= Key.NumPad9) ||
                (e.Key >= Key.D0 && e.Key <= Key.D9) ||
                 e.Key == Key.Back ||
                 e.Key == Key.Left || e.Key == Key.Right)
            {
                if (e.KeyboardDevice.Modifiers != ModifierKeys.None)
                {
                    e.Handled = true;
                }
            }
            else
            {
                e.Handled = true;
            }
        }

        

        private void ExportTable_AH_Click(object sender, RoutedEventArgs e)
        {
            string[] Str = { "申请编号", "申请人编号", "申请人名字", "工作岗位",  "申请日期", "申请资料编号",
                "申请数量", "申请原因", "操作状态" };
            TableToApply.ExportExcel(ApplyTableSql, Str, "借阅申请历史表格.xlsx");
        }

        private void ExportTable_BH_Click(object sender, RoutedEventArgs e)
        {
            string[] Str = { "借阅号", "资料编号", "借阅日期", "借阅人编号", "借阅数量", "借阅人名字", "状态" };
            TableToApply.ExportExcel(HistoryTableSql, Str, "借阅历史表格.xlsx"); 
        }
    }
}
