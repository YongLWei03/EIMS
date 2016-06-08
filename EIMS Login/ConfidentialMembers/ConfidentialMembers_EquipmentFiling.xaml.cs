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
        Connection Temp = new Connection();

        string SQLStr = "select * from DataIn";

        DataTable dt = new DataTable();
        DataTable dtTypes = new DataTable();
        string[] IdText = new string[10];//要删除的归档号
        int IdCount = 0;//已删除的归档号数量
        public ConfidentialMembers_NewEquipmentInformationAdded()
        {
            InitializeComponent();

            InitTable();
            Initttalbm();
            TableToDataFile.DataTableSelect(SQLStr, "更新");
            TableToDataFile.dataGrid.SelectionChanged += TableToDataFile_SelectionChanged;
            dt = TableToDataFile.Getdt();
            ArchiveTotal.Content = dt.Rows.Count;//获取总行数
        }

        public void InitTable()
        {
            TableToDataFile.InitTableHeightWidth(480, 880);
            TableToDataFile.SetCanUserAddRows(false);
            TableToDataFile.AddColumns("Id", "归档号", 80);
            TableToDataFile.AddColumns("InDate", "归档日期", 80);
            TableToDataFile.AddColumns("DataNo", "资料编号", 80);
            TableToDataFile.AddColumns("InCount", "归档数量", 80);
            TableToDataFile.AddColumns("Username", "经办人", 80);
            TableToDataFile.AddColumns("Ryname", "审批人", 80);
            TableToDataFile.AddColumns("Detail", "说明", 300);
            TableToDataFile.AddColumns("Flag", "审核标记", 75);
        }
        public void Initttalbm()//右键菜单
        {
            MenuItem TempMenu = TableToDataFile.AddMenuItem("删除");
            TempMenu.Click += Delete_Click;
            TableToDataFile.dgMenu.Items.Add(TempMenu);
        }
        public void UpdateLeft(int row)
        {
            Id.Text = dt.Rows[row]["Id"].ToString();
            InDate.Text = dt.Rows[row]["InDate"].ToString();
            DataNo.Text = dt.Rows[row]["DataNo"].ToString();
            InCount.Text = dt.Rows[row]["InCount"].ToString();
            Usersname.Text = dt.Rows[row]["Username"].ToString();
            Ryname.Text = dt.Rows[row]["Ryname"].ToString();
            Detail.Text = dt.Rows[row]["Detail"].ToString();
            Flag.Text = dt.Rows[row]["Flag"].ToString();
        }
        private void TableToDataFile_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int row = TableToDataFile.dataGrid.SelectedIndex;//获取选中的行数。第一行为0

            if (row == -1) return;

            Id.Text = dt.Rows[row]["Id"].ToString();
            DataNo.Text = dt.Rows[row]["DataNo"].ToString();
            InCount.Text = dt.Rows[row]["InCount"].ToString();
            Usersname.Text = dt.Rows[row]["Username"].ToString();
            Ryname.Text = dt.Rows[row]["Ryname"].ToString();
            InDate.Text = dt.Rows[row]["InDate"].ToString();
            Flag.Text = dt.Rows[row]["Flag"].ToString();
            Detail.Text = dt.Rows[row]["Detail"].ToString();
        }

        private void SubmitAdd_Click(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt32(InCount.Text) < 0)
            {
                MessageBox.Show("错误：归档数量不能为负数！");
                return;
            }
            else if (Detail.Text.Length > 100)
            {
                MessageBox.Show("错误：说明不能超过一百字！");
                return;
            }
            string InDate = DateTime.Now.ToString("yyyy-MM-dd") + " " + DateTime.Now.ToLongTimeString().ToString(); ;//获取当前时间
            string SQL1 = "insert into DataIn values ('" + InDate + "','" + DataNo.Text + "','" + InCount.Text + "','" + Usersname.Text + "','"
                + Ryname.Text + "','" + Detail.Text + "','" + Flag.Text + "')";
            try
            {
                SqlCommand CMD_1 = new SqlCommand(SQL1, Temp.GetConn());
                CMD_1.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("错误：类型编号重复，或不可重复添加！");
                return;
            }
            TableToDataFile.DataTableSelect(SQLStr, "更新");
            dt = TableToDataFile.Getdt();
        }

        private void SearchTwo_Click(object sender, RoutedEventArgs e)
        {
            string SQL1 = "select * from DataIn where Id ='" + Search.Text + "'";

            try
            {
                SqlCommand CMD_1 = new SqlCommand(SQL1, Temp.GetConn());
                SqlDataReader Sdr_1 = CMD_1.ExecuteReader();
                if (!Sdr_1.Read()) MessageBox.Show("错误：暂无编号为 " + Search.Text + " 的归档！");
                else
                {
                    Id.Text = Sdr_1[0].ToString();
                    InDate.Text = Sdr_1[1].ToString();
                    DataNo.Text = Sdr_1[2].ToString();
                    InCount.Text = Sdr_1[3].ToString();
                    Usersname.Text = Sdr_1[4].ToString();
                    Ryname.Text = Sdr_1[5].ToString();
                    Detail.Text = Sdr_1[6].ToString();
                    Flag.Text = Sdr_1[7].ToString();
                }
                Sdr_1.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误：" + ex);
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            int Row = TableToDataFile.dataGrid.SelectedIndex;

            if (Row == -1) return;
            if (IdCount > 9)
            {
                MessageBox.Show("不允许删除，已超过允许一次的最大删除量！");
            }
            else
            {
                MessageBoxResult dr = MessageBox.Show("你确定要删除当前这一行吗？", "警告", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                if (dr == MessageBoxResult.OK)
                {
                    Warnning.Content = "你删除了"+ (IdCount+1).ToString()+ "数据，但未提交到服务器，下次更新将继续出现！";
                    Warnning.Foreground = Brushes.Red;

                    IdText[IdCount] = dt.Rows[Row]["Id"].ToString();//获取到删除的编号
                    IdCount++;

                    TableToDataFile.dt.Rows.RemoveAt(Row);
                }
            }
        }

        private void SubmitDateTwo_Click(object sender, RoutedEventArgs e)
        {
            SqlCommand CMD_1 = new SqlCommand();
            CMD_1.Connection = Temp.GetConn();

            for (int i = 0; i<IdCount;i++)
            {
                try
                {
                    CMD_1.CommandText = "delete from DataIn where Id= '" + IdText[i] + "'";
                    CMD_1.ExecuteNonQuery();
                }
                catch(Exception ex)
                {
                    MessageBox.Show("错误：\n"+ex);
                    return;
                }
               
            }
            MessageBox.Show("提交删除成功！");
            Warnning.Content = "";
            IdCount = 0;
        }

        private void ExportThree_Click(object sender, RoutedEventArgs e)
        {
            string[] Str = { "申请编号", "申请人编号", "申请人名字", "工作岗位",  "申请日期", "申请资料编号",
                "申请数量", "申请原因", "操作状态" };
            TableToDataFile.ExportExcel(SQLStr, Str, "归档表.xlsx");
        }

        private void InCount_KeyDown(object sender, KeyEventArgs e)
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
    }
}

    
    

