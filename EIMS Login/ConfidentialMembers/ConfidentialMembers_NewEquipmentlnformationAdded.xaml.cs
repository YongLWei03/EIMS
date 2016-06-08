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
    /// ConfidentialMembers_4.xaml 的交互逻辑
    /// </summary>
    public partial class ConfidentialMembers_EquipmentFiling : UserControl
    {
        Connection Temp = new Connection();

        string SQLStr = "select * from ArmsData,Types where ArmsData.TypeId = Types.TypeId";

        DataTable dt = new DataTable();
        DataTable dtTypes = new DataTable();
        string[] IdText = new string[10];//要删除的归档号
        int IdCount = 0;//已删除的归档号数量
        public ConfidentialMembers_EquipmentFiling()
        {
            InitializeComponent();

            InitTable();
            Initttalbm();
            TableToArmsData.DataTableSelect(SQLStr, "更新");
            TableToArmsData.dataGrid.SelectionChanged += TableToArmsData_SelectionChanged;
            dt = TableToArmsData.Getdt();
            DateOftotal.Content = dt.Rows.Count;//获取总行数
        }
        public void InitTable()
        {
            TableToArmsData.InitTableHeightWidth(480, 880);
            TableToArmsData.SetCanUserAddRows(false);
            TableToArmsData.AddColumns("DataNo", "资料编号", 100);
            TableToArmsData.AddColumns("DataName", "资料名称", 100);
            TableToArmsData.AddColumns("TypeName", "资料分类", 120);
            TableToArmsData.AddColumns("ICount", "数量", 100);
            TableToArmsData.AddColumns("IPrice", "价格", 80);
            TableToArmsData.AddColumns("Memo", "备注", 220);
            TableToArmsData.AddColumns("GreateDate", "创建日期", 120);

        }

        private void TableToArmsData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int row = TableToArmsData.dataGrid.SelectedIndex;//获取选中的行数。第一行为0

            if (row == -1) return;

            DataNo.Text = dt.Rows[row]["DataNo"].ToString();
            DataName.Text = dt.Rows[row]["DataName"].ToString();
            Typeld.Text = dt.Rows[row]["TypeId"].ToString();
            ICount.Text = dt.Rows[row]["ICount"].ToString();
            IPrice.Text = dt.Rows[row]["IPrice"].ToString();
            Memo.Text = dt.Rows[row]["Memo"].ToString();
            GreateDate.Text = dt.Rows[row]["GreateDate"].ToString();
            TypeName.Text = dt.Rows[row]["TypeName"].ToString();
        }

       private void SubmitToadd_Click(object sender, RoutedEventArgs e)
        {
            bool TempBool = true;
            if (Convert.ToInt32(ICount.Text) < 0)
            {
                MessageBox.Show("错误：数量不能为负数！");
                return;
            }
            else if (Memo.Text.Length > 100)
            {
                MessageBox.Show("错误：说明不能超过一百字！");
                return;
            }
            string GreateDate = DateTime.Now.ToString("yyyy-MM-dd") + " " + DateTime.Now.ToLongTimeString().ToString(); ;//获取当前时间
            string SQL1 = "insert into ArmsData values ('" + DataNo.Text + "','" + DataName.Text + "'," + Convert.ToInt32(Typeld.Text) + ","
                + Convert.ToInt32(ICount.Text) + "," + Convert.ToDecimal(IPrice.Text) + ",'" + Memo.Text + "','"+ GreateDate + "')";
            string SQL2 = "insert into Types values ('"+ Typeld.Text+ "','"+ TypeName .Text+ "',1)";//插入到类型表
            string SQL3 = "select * from Types where TypeId = '"+ Typeld.Text + "'";
            string SQL4 = "select * from ArmsData where DataNo = '"+ DataNo.Text + "'";

            try
            {
                SqlCommand CMD_1 = new SqlCommand(SQL4, Temp.GetConn());
                SqlDataReader Sdr_1 = CMD_1.ExecuteReader();
                if (Sdr_1.Read())
                {
                    Sdr_1.Close();
                    MessageBox.Show("已有编号为 " + Typeld.Text + " 的资料！", "警告");
                    return;

                }
                Sdr_1.Close();
                CMD_1.CommandText = SQL3;
                Sdr_1 = CMD_1.ExecuteReader();
                if (Sdr_1.Read())
                {
                    Sdr_1.Close();
                    MessageBoxResult dr = MessageBox.Show("已有编号为 " + Typeld.Text + " 的分类,你确定要挂载到此分类编号下吗？", "警告", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                    if (dr == MessageBoxResult.Cancel)
                        return;
                    TempBool = false; 
                }
                if (TempBool)
                {
                    CMD_1.CommandText = SQL2;
                    CMD_1.ExecuteNonQuery();
                }

                CMD_1.CommandText = SQL1;
                CMD_1.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                MessageBox.Show("错误：类型编号重复，或不可重复添加！"+ex);
                return;
            }
            TableToArmsData.DataTableSelect(SQLStr, "更新");
            dt = TableToArmsData.Getdt();
            DateOftotal.Content = dt.Rows.Count;//获取总行数
        }

        private void SearchTwo_Click(object sender, RoutedEventArgs e)
        {
            string SQL1 = "select * from ArmsData where DataNo ='" + Search.Text + "'";

            try
            {
                SqlCommand CMD_1 = new SqlCommand(SQL1, Temp.GetConn());
                SqlDataReader Sdr_1 = CMD_1.ExecuteReader();
                if (!Sdr_1.Read()) MessageBox.Show("错误：暂无编号为 " + Search.Text + " 的资料！");
                else
                {
                    DataNo.Text = Sdr_1[0].ToString();
                    DataName.Text = Sdr_1[1].ToString();
                    Typeld.Text = Sdr_1[2].ToString();
                    ICount.Text = Sdr_1[3].ToString();
                    IPrice.Text = Sdr_1[4].ToString();
                    Memo.Text = Sdr_1[5].ToString();
                    GreateDate.Text = Sdr_1[6].ToString();
                }
                Sdr_1.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误：" + ex);
            }
        }

        public void Initttalbm()//右键菜单
        {
            MenuItem TempMenu = TableToArmsData.AddMenuItem("删除");
            TempMenu.Click += Delete_Click;
            TableToArmsData.dgMenu.Items.Add(TempMenu);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            int Row = TableToArmsData.dataGrid.SelectedIndex;

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
                    Warnning.Content = "你删除了" + (IdCount + 1).ToString() + "数据，但未提交到服务器，下次更新将继续出现！";
                    Warnning.Foreground = Brushes.Red;

                    IdText[IdCount] = dt.Rows[Row]["DataNo"].ToString();//获取到删除的编号
                    IdCount++;

                    TableToArmsData.dt.Rows.RemoveAt(Row);
                }
            }
        }

        private void SubmitDateTwo_Click(object sender, RoutedEventArgs e)
        {
            SqlCommand CMD_1 = new SqlCommand();
            CMD_1.Connection = Temp.GetConn();

            for (int i = 0; i < IdCount; i++)
            {
                try
                {
                    CMD_1.CommandText = "delete from ArmsData where DataNo= '" + IdText[i] + "'";
                    CMD_1.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("错误：\n" + ex);
                    return;
                }

            }
            MessageBox.Show("提交删除成功！");
            Warnning.Content = "";
            IdCount = 0;
        }
    }
}
