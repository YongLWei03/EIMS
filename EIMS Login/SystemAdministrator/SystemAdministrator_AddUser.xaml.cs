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
    /// SystemAdministratorl_1.xaml 的交互逻辑
    /// </summary>
    public partial class SystemAdministrator_AddUser : UserControl
    {
        Connection Temp = new Connection();
        public static userIdentity user;

        public enum userIdentity
        {
            Ordinary_users,
            System_administrator,
            Maintenance_man,
            Warehouse_manager,
            Confidential_clerk,
            Finance_department

        }

        public SystemAdministrator_AddUser()
        {
            InitializeComponent();
        }

        private void Users_submit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(!Status())    
                throw (new Exception());
            }
            catch(Exception s)
            {
                MessageBox.Show("提交页面信息错误！");
                return;
            }

            user = (userIdentity)addgroup_registerText_cb.SelectedIndex;
            string userstring = user.ToString();
            string sexstring = addgroup_registerText_sex.SelectedIndex.ToString();
            string username = register_account.Text;
            string SearchSQL = "select * from ArmsUsers where Usersname='" + username + "'";
            try
            {
                SqlCommand searchcmd = new SqlCommand(SearchSQL,Temp.GetConn());
                SqlDataReader sread = searchcmd.ExecuteReader();
                if(sread.Read())
                {
                    throw new Exception();                    
                }
                sread.Close();
            }
            catch(Exception)
            {
                MessageBox.Show("账号已注册！");
                return;
            }
            //注意账号所属人姓名，编号，性别，部门编号需要与ArmsPerson一致
            string StrSQL = "insert into ArmsUsers values('" + username + "','" + register_pwd.Text + "','" + userstring + "','" + addgroup_registerText_rennum.Text + "','" + register_AffiliationPeople.Text + "','" + sexstring + "','" + addgroup_registerText_departnum.Text + "')";
            
            try
            {
                SqlCommand cmd = new SqlCommand(StrSQL, Temp.GetConn());
                cmd.ExecuteNonQuery();
            }
            catch(Exception se)
            {
                MessageBox.Show("提交失败！"+ se);
                return;
            }
            
        }
        private bool Status()
        {
            if (register_account.Text.Length < 6 || register_account.Text.Length > 15)
                return false;
            else if (register_pwd.Text.Length < 6 || register_pwd.Text.Length > 10)
                return false;
            else if (register_pwd.Text != register_repwd.Text)
                return false;
            else return true;
            
        }

        private void register_repwd_LostFocus(object sender, RoutedEventArgs e)
        {
            if (register_pwd.Text != register_repwd.Text)
            {
                repwdText.Content = "两次密码请输入相同！";
            }
            else repwdText.Content = null;
        }

        private void Departments_submit_Click(object sender, RoutedEventArgs e)
        {
            string DepartSQL = "insert into Departments values('"+ AddDepartment_registorText_newdepartnum.Text +"','"+ AddDepartment_registorText_newdepartid.Text +"','"+ AddDepartment_registorText_newdepartdesc.Text +"','"+ AddDepartment_registorText_parentdepartnum.Text +"')";
            
            try
            {
                SqlCommand AddDepart = new SqlCommand(DepartSQL, Temp.GetConn());
                AddDepart.ExecuteNonQuery();
            }
            catch
            {
                MessageBox.Show("提交失败！");
                return;
            }
        }

        private bool OrResetStatus()
        {
            if (Ordinaryuser_intputpwd.Text.Length < 6 || Ordinaryuser_intputpwd.Text.Length > 10)
                return false;
            else if (Ordinaryuser_intputpwd.Text != Ordinaryuser_reintputpwd.Text)
                return false;
            else return true;
        }
        private void Ordinaryuser_submitleft_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!OrResetStatus())
                    throw (new Exception());
            }
            catch (Exception s)
            {
                MessageBox.Show("提交页面信息错误！");
                return;
            }
            string DetectOrdSQL = "select RyId from ArmsUsers where Usersname ='" + Ordinaryuser_id.Text + "'";
            string OrdRsetSQL = "update ArmsUsers set Userspwd = '" + Ordinaryuser_intputpwd.Text + "' where Usersname = '" + Ordinaryuser_id.Text + "'";
            try
            {
                SqlCommand SamenameCmd = new SqlCommand(DetectOrdSQL, Temp.GetConn());
                string backstring = SamenameCmd.ExecuteScalar().ToString();              
                if (backstring.Substring(0, 1) != "1")
                {
                    MessageBox.Show("提交用户不为普通用户！");
                    return;
                }
                else
                {                  
                    SqlCommand Updatepwd = new SqlCommand(OrdRsetSQL, Temp.GetConn());
                    Updatepwd.ExecuteNonQuery();
                }
            }
            catch
            {
                MessageBox.Show("提交失败！");
                return;
            }
           
           
        }
        private bool SpResetStatus()
        {
            if (Specialuser_intputpwd.Text.Length < 6 || Specialuser_intputpwd.Text.Length > 10)
                return false;
            else if (Specialuser_intputpwd.Text != Specialuser_reintputpwd.Text)
                return false;
            else return true;
        }

        private void Specialuser_submitright_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!SpResetStatus())
                    throw (new Exception());
            }
            catch (Exception s)
            {
                MessageBox.Show("提交页面信息错误！");
                return;
            }
            string DetectSpSQL = "select RyId from ArmsUsers where Usersname ='" + Specialuser_id.Text + "'";
            string DetectdRsetSQL = "update ArmsUsers set Userspwd = '" + Specialuser_intputpwd.Text + "' where Usersname = '" + Specialuser_id.Text + "'";
            try
            {
                SqlCommand SamenameCmd = new SqlCommand(DetectSpSQL, Temp.GetConn());
                string backstring = SamenameCmd.ExecuteScalar().ToString();
                if (backstring.Substring(0, 1) == "1")
                {
                    MessageBox.Show("提交用户不为特殊用户！");
                    return;
                }
                else
                {
                    SqlCommand Updatepwd = new SqlCommand(DetectdRsetSQL, Temp.GetConn());
                    Updatepwd.ExecuteNonQuery();
                }
            }
            catch
            {
                MessageBox.Show("提交失败！");
                return;
            }
        }
   
    }
}
