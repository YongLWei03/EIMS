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
    /// SystemAdministrator_2.xaml 的交互逻辑
    /// </summary>
    public partial class SystemAdministrator_NetUser : UserControl
    {
        Connection Temp = new Connection();
        SqlCommand UserSearch = new SqlCommand();
        SqlCommand AccountSearch = new SqlCommand();
        SqlDataAdapter ado = new SqlDataAdapter();
        SqlDataAdapter bdo = new SqlDataAdapter();
        DataSet Mydataset = new DataSet();
        DataSet AccountSet = new DataSet();
        DataTable account = new DataTable();
        string TempSearch = null;
        public SystemAdministrator_NetUser()
        {
            InitializeComponent();
            AccountSet.Tables.Add(account);
        }

        private void Search_button_Click(object sender, RoutedEventArgs e)
        {
            TempSearch = SearchText.Text;
            string usersearchPerson = "select * from ArmsPerson where RyId ='" + SearchText.Text + "'";
            string usersearchAccount = "select * from ArmsUsers where RyId ='" + SearchText.Text + "'";
            try
            {
                UserSearch.Connection = Temp.GetConn();
                AccountSearch.Connection = Temp.GetConn();
                AccountSearch.CommandText = usersearchAccount;
                bdo.SelectCommand = AccountSearch;
                account.Clear();
                bdo.Fill(AccountSet, "Table1");
                Accounts.DisplayMemberPath = "Usersname";
                Accounts.ItemsSource = AccountSet.Tables[0].DefaultView;
                Accounts.SelectedIndex = 0;
                UserSearch.CommandText = usersearchPerson;           
                ado.SelectCommand = UserSearch;               
                ado.Fill(Mydataset,"person");
               
                MessageBox.Show(Mydataset.Tables["person"].Rows[0]["RyId"].ToString());
                foreach(DataRow row in Mydataset.Tables["person"].Rows)
                {
                    if (row["RyId"].Equals(null))
                    {
                        throw (new Exception());
                    }
                }
                number.Text = Mydataset.Tables["person"].Rows[0]["RyId"].ToString();
                name.Text = Mydataset.Tables["person"].Rows[0]["RyName"].ToString();
                if (Mydataset.Tables["person"].Rows[0]["Sex"].ToString() == "男")
                {
                    sex.SelectedIndex = 0;
                }
                else sex.SelectedIndex = 1;               
                post.Text = Mydataset.Tables["person"].Rows[0]["Title"].ToString();
                nation.Text = Mydataset.Tables["person"].Rows[0]["Nationalty"].ToString();
                if(Mydataset.Tables["person"].Rows[0]["Marital_Condition"].ToString() == "已婚")
                {
                    maritalstatus.SelectedIndex = 0;
                }
                else if (Mydataset.Tables["person"].Rows[0]["Marital_Condition"].ToString() == "未婚")
                {
                    maritalstatus.SelectedIndex = 1;
                }               
                else maritalstatus.SelectedIndex = 2;
                birthday.Text = Mydataset.Tables["person"].Rows[0]["Birth"].ToString();
                rank.Text = Mydataset.Tables["person"].Rows[0]["Rank"].ToString();
                Education_level.Text = Mydataset.Tables["person"].Rows[0]["Culture_Level"].ToString();
                Family_Place.Text = Mydataset.Tables["person"].Rows[0]["Family_Place"].ToString();
                Politicalstatus.Text = Mydataset.Tables["person"].Rows[0]["Political_Party"].ToString();
                id.Text = Mydataset.Tables["person"].Rows[0]["Id_Card"].ToString();
                DepartmentNum.Text = Mydataset.Tables["person"].Rows[0]["Dep_Id"].ToString();
                jobs.Text = Mydataset.Tables["person"].Rows[0]["Position"].ToString();
                LeadershipNum.Text = Mydataset.Tables["person"].Rows[0]["UpperId"].ToString();

                
                
            }
            catch(Exception se)
            {
                MessageBox.Show("搜索失败！" + se);
                return;
            }
            
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlCommandBuilder sqlcb = new SqlCommandBuilder(ado);
                SqlCommandBuilder sqlcb1 = new SqlCommandBuilder(bdo);
                Mydataset.Tables["person"].Rows[0]["RyId"] = number.Text;
                Mydataset.Tables["person"].Rows[0]["RyName"] = name.Text;
                Mydataset.Tables["person"].Rows[0]["Sex"] = sex.SelectionBoxItem.ToString();
                Mydataset.Tables["person"].Rows[0]["Title"] = post.Text;
                Mydataset.Tables["person"].Rows[0]["Nationalty"] = nation.Text;
                Mydataset.Tables["person"].Rows[0]["Marital_Condition"] = maritalstatus.SelectionBoxItem.ToString();
                Mydataset.Tables["person"].Rows[0]["Birth"] = birthday.Text;
                Mydataset.Tables["person"].Rows[0]["Rank"] = rank.Text;
                Mydataset.Tables["person"].Rows[0]["Culture_Level"] = Education_level.Text;
                Mydataset.Tables["person"].Rows[0]["Family_Place"] = Family_Place.Text;
                Mydataset.Tables["person"].Rows[0]["Political_Party"] = Politicalstatus.Text;
                Mydataset.Tables["person"].Rows[0]["Id_Card"] = id.Text;
                Mydataset.Tables["person"].Rows[0]["Dep_Id"] = DepartmentNum.Text;
                Mydataset.Tables["person"].Rows[0]["Position"] = jobs.Text;
                Mydataset.Tables["person"].Rows[0]["UpperId"] = LeadershipNum.Text;

                AccountSet.Tables[0].Rows[Accounts.SelectedIndex]["User_type"] = Competence.SelectionBoxItem.ToString();
                bdo.Update(AccountSet, "Table1");
                AccountSet.Tables["Table1"].AcceptChanges();
                ado.Update(Mydataset,"person");
                Mydataset.Tables["person"].AcceptChanges();

            }
            catch(Exception se)
            {
                MessageBox.Show("提交失败！"+se);
                return;
            }
            MessageBox.Show("修改成功！");
            Search_button.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));








        }

        private void Accounts_DropDownClosed(object sender, EventArgs e)
        {
            if (Accounts.SelectedIndex == -1) return;
            string type = AccountSet.Tables[0].Rows[Accounts.SelectedIndex]["User_type"].ToString();
            if (type == "普通用户") Competence.SelectedIndex = 0;
            if (type == "系统管理员") Competence.SelectedIndex = 1;
            if (type == "维修管理员") Competence.SelectedIndex = 2;
            if (type == "仓库管理员") Competence.SelectedIndex = 3;
            if (type == "财务管理员") Competence.SelectedIndex = 4;
            if (type == "保密员") Competence.SelectedIndex = 5;
  
        }

        private void DeletAccount_Click(object sender, RoutedEventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Temp.GetConn();
            if (MessageBox.Show("您确定要删除该账号吗？", "系统提示：", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
            {
                try
                {
                    cmd.CommandText = "delete from ArmsUsers where Usersname= '" + Accounts.Text.ToString() + "'";
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ae)
                {
                    MessageBox.Show("删除失败！");
                }
            }
            SearchText.Text = TempSearch;
            Accounts.ItemsSource = null;
            Search_button.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            
        }
    }
}
