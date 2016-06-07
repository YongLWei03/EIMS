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

namespace EIMS_Login
{
    /// <summary>
    /// ConfidentialMembers_3.xaml 的交互逻辑
    /// </summary>
    public partial class ConfidentialMembers_NewEquipmentInformationAdded : UserControl
    {
        //Connection Temp = new Connection();
        //string SQLStr = "select * from ArmsData, Types where ArmsData.TypeId = Types.TypeId ";
        //DataTable dt = new DataTable();
        //DataTable dtTypes = new DataTable();
        public ConfidentialMembers_NewEquipmentInformationAdded()
        {
            InitializeComponent();
            InitTable();
        }

        public void InitTable()
        {
            TableToDataFile.InitTableHeightWidth(480, 880);
            TableToDataFile.AddColumns("Id", "归档号", 80);
            TableToDataFile.AddColumns("InDate", "归档日期", 80);
            TableToDataFile.AddColumns("DateNo", "资料编号", 80);
            TableToDataFile.AddColumns("InCount", "归档数量", 80);
            TableToDataFile.AddColumns("Usersname", "经办人", 80);
            TableToDataFile.AddColumns("Ryname", "审批人", 80);
            TableToDataFile.AddColumns("Detail", "说明", 300);
            TableToDataFile.AddColumns("Flag", "审核标记", 75);
        }

        //public void UpdateLeft(int row)
        //{
        //    FileNo.Text = dt.Rows[row]["Id"].ToString();
        //    Materialnumber.Text = dt.Rows[row]["InDate"].ToString();
        //    FileNumber1.Text = dt.Rows[row]["DateNo"].ToString();
        //    Agent.Text = dt.Rows[row]["InCount"].ToString();
        //    Approver.Text = dt.Rows[row]["Usersname"].ToString();
        //    FileDate.Text = dt.Rows[row]["Ryname"].ToString();
        //    Detail.Text = dt.Rows[row]["Detail"].ToString();
        //    FileStustas.Text = dt.Rows[row]["Flag"].ToString();
        //}

        //private void SearchButton_Click(object sender, RoutedEventArgs e)
        //{
        //    string sql = "select * from DataIn where Id = '" + FileNo.Text + "'";
        //    try
        //    {
        //        SqlCommand CMD_1 = new SqlCommand(sql, Temp.GetConn());
        //        SqlDataReader Sdr_1 = CMD_1.ExecuteReader();
        //        if (!Sdr_1.Read()) MessageBox.Show("错误：暂无编号为 " + FileNo.Text + " 的资料！");
        //        else
        //        {
        //            FileNo.Text = Sdr_1[0].ToString();
        //            Materialnumber.Text =  Sdr_1[1].ToString() ;
        //            FileNumber1.Text = Sdr_1[2].ToString();
        //            Agent.Text = Sdr_1[3].ToString();
        //            Approver.Text = Sdr_1[4].ToString();
        //            FileDate.Text = Sdr_1[5].ToString();
        //            Detail.Text = Sdr_1[6].ToString();
        //            FileStustas.Text = Sdr_1[7].ToString();

        //        }
        //        Sdr_1.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("错误：" + ex);
        //    }
        //}

        //private void ApplicationSubmit_Click(object sender, RoutedEventArgs e)
        //{
        //    if (Materialnumber.Text.Length == 0)
        //    {
        //        MessageBox.Show("错误：资料编号或申请数量为空！");
        //    }
        //    else if (Convert.ToInt32(ApplicationDataCount.Text) < 0)
        //    {
        //        MessageBox.Show("错误：申请数量不能负数！");
        //    }
        //    else if (ApplicationReasons.Text.Length > 100)
        //    {
        //        MessageBox.Show("错误：申请原因字数不能超过100！");
        //    }
        //    else if (UITemp.GetRyidStatus)
        //    {
        //        string Date = DateTime.Now.ToString("yyyy-MM-dd") + " " + DateTime.Now.ToLongTimeString().ToString(); ;//获取当前时间
        //        string StrSQL1 = "insert into ApplyData values('" + UITemp.UserInfoTemp.Ryid + "','" + UITemp.UserInfoTemp.RyName + "','" + UITemp.UserInfoTemp.Position +
        //             "','" + Date + "','" + ApplicationDataNumber.Text + "',"
        //            + ApplicationDataCount.Text + ",'" + ApplicationReasons.Text + "','未操作')";
        //        string StrSQL2 = "select * from ArmsData where DataNo ='" + ApplicationDataNumber.Text + "'";
        //        SqlCommand CMD_1 = new SqlCommand(StrSQL2, Temp.GetConn());
        //        SqlDataReader Sdr_1 = CMD_1.ExecuteReader();
        //        if (!Sdr_1.Read()) MessageBox.Show("错误：暂无编号为 " + ApplicationDataNumber.Text + " 的资料！");
        //        else
        //        {
        //            if (Convert.ToInt32(Sdr_1[3].ToString()) < Convert.ToInt32(ApplicationDataCount.Text))
        //            {
        //                MessageBox.Show("编号为 " + ApplicationDataNumber.Text + " 的资料，暂不能满足申请数量！");
        //                return;
        //            }
        //            Sdr_1.Close();
        //            try
        //            {
        //                CMD_1.CommandText = StrSQL1;
        //                CMD_1.ExecuteNonQuery();
        //            }
        //            catch
        //            {
        //                MessageBox.Show("申请失败！");
        //                return;
        //            }
        //            MessageBox.Show("申请成功，请耐心等待批准结果。。。");
        //            TableToApply.DataTableSelect(ApplyTableSql, "更新");//申请成功更新：申请历史表格
        //            UpDataLog();//更新操作日志
        //            UpDateRLR(0);//更新左下部操作提示
        //            ApplicationHistoryCount.Content = TableToApply.Rows;//更新申请总计
        //            if (admiw_OpenSign == 1)
        //                admiw.updata(TableToApply.Getdt(), TableToApply.Rows);//更新查看详细信息窗口的总行数
        //        }
        //        Sdr_1.Close();
        //    }
        //    else
        //    {
        //        MessageBox.Show("个人信息拉取失败或个人信息未填写充分，无法提交申请!");
        //    }
        //}
    }
}

    
    

