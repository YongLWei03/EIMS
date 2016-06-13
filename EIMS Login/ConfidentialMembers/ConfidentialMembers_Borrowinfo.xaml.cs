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
    /// ConfidentialMembers_2.xaml 的交互逻辑
    /// </summary>
    public partial class ConfidentialMembers_Borrowinfo : UserControl
    {
        string SQL = "select * from DataLend";
        string SelectId;
        Connection Temp = new Connection();
        public ConfidentialMembers_Borrowinfo()
        {
            InitializeComponent();
            InitTable();
            TableToBorrow.DataTableSelect(SQL, "更新");
            TableToBorrow.dataGrid.SelectionChanged += TableToBorrow_SelectionChanged;
            BorrowTotal.Content = TableToBorrow.dt.Rows.Count;
        }
        public void InitTable()
        {
            TableToBorrow.InitTableHeightWidth(480, 880);
            TableToBorrow.SetCanUserAddRows(false);
            TableToBorrow.AddColumns("Id", "借阅号", 80);
            TableToBorrow.AddColumns("DataNo", "资料编号", 80);
            TableToBorrow.AddColumns("LendDate", "借阅日期", 240);
            TableToBorrow.AddColumns("RyId", "借阅人编号", 120);
            TableToBorrow.AddColumns("LendCount", "借阅数量",120 );
            TableToBorrow.AddColumns("Ryname", "审批人",120 );
            TableToBorrow.AddColumns("Flag", "借阅状态",120 );
            Inittthlbm();
        }
        public void Inittthlbm()
        {
            string Str = "无操作";
            TableToBorrow.dgMenu.Items.Add(TableToBorrow.AddMenuItem(Str));
        }
        private void TableToBorrow_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int row = TableToBorrow.dataGrid.SelectedIndex;//获取选中的行数。第一行为0

            if (row == -1) return;

            try
            {
                SelectId =  Id.Text = TableToBorrow.dt.Rows[row]["Id"].ToString();
                DataNo.Text = TableToBorrow.dt.Rows[row]["DataNo"].ToString();
                LendDate.Text = TableToBorrow.dt.Rows[row]["LendDate"].ToString();
                RyId.Text = TableToBorrow.dt.Rows[row]["RyId"].ToString();
                LendCount.Text = TableToBorrow.dt.Rows[row]["LendCount"].ToString();
                Ryname.Text = TableToBorrow.dt.Rows[row]["Ryname"].ToString();
                if (TableToBorrow.dt.Rows[row]["Flag"].ToString() == "借阅")
                {
                    Flag.SelectedIndex = 0;
                }
                else if (TableToBorrow.dt.Rows[row]["Flag"].ToString() == "借阅确认")
                {
                    Flag.SelectedIndex = 1;
                }
                else if (TableToBorrow.dt.Rows[row]["Flag"].ToString() == "归还")
                {
                    Flag.SelectedIndex = 2;
                }
                else if (TableToBorrow.dt.Rows[row]["Flag"].ToString() == "归还确认")
                {
                    Flag.SelectedIndex = 3;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("错误：\n\n"+ex);
            }
        }

        private void CommitChanges_Click(object sender, RoutedEventArgs e)
        {
            string Str = FlagString();
            string SQL1 = "update DataLend set Flag = '"+ Str + "' where Id ='"+ SelectId + "'";
            try
            {
                SqlCommand CMD_1 = new SqlCommand(SQL1, Temp.GetConn());
                CMD_1.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误：\n\n" + ex);
                return ;
            }
            TableToBorrow.DataTableSelect(SQL, "更新");
            MessageBox.Show("修改成功！");
        }
        private string FlagString()
        {
            string Str = " ";
            if (Flag.SelectedIndex == 0)
            {
                Str = "借阅";
            }
            else if (Flag.SelectedIndex == 1)
            {
                Str = "借阅确认";
            }
            else if (Flag.SelectedIndex == 2)
            {
                Str = "归还";
            }
            else if (Flag.SelectedIndex == 3)
            {
                Str = "归还确认";
            }
            return Str;
        }

        private void SearchTwo_Click(object sender, RoutedEventArgs e)
        {
            string SQL2 = "select * from DataLend where Id = '"+ SearchText.Text+ "'";
            try
            {
                SqlCommand CMD_1 = new SqlCommand(SQL2, Temp.GetConn());
                SqlDataReader Sdr_1 = CMD_1.ExecuteReader();
                if (!Sdr_1.Read()) MessageBox.Show("错误：暂无编号为 " + SearchText.Text + " 的资料！");
                else
                {
                    SelectId = Id.Text = Sdr_1[0].ToString();
                    DataNo.Text = Sdr_1[1].ToString();
                    LendDate.Text = Sdr_1[2].ToString();
                    RyId.Text = Sdr_1[3].ToString();
                    LendCount.Text = Sdr_1[4].ToString();
                    Ryname.Text = Sdr_1[5].ToString();
                    if (Sdr_1[6].ToString() == "借阅")
                    {
                        Flag.SelectedIndex = 0;
                    }
                    else if (Sdr_1[6].ToString() == "借阅确认")
                    {
                        Flag.SelectedIndex = 1;
                    }
                    else if (Sdr_1[6].ToString() == "归还")
                    {
                        Flag.SelectedIndex = 2;
                    }
                    else if (Sdr_1[6].ToString() == "归还确认")
                    {
                        Flag.SelectedIndex = 3;
                    }
                }
                Sdr_1.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误：" + ex);
            }
        }

        private void ExportTwo_Click(object sender, RoutedEventArgs e)
        {
            string[] Str = {"借阅号","借阅资料编号", "借阅日期", "借阅人编号", "借阅数量", "借阅人名字", "借阅标记" };
            TableToBorrow.ExportExcel(SQL, Str, "借阅表.xlsx");
        }
    }
}
