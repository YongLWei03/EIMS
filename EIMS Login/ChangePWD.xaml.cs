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
using System.Data.SqlClient;
using EIMS_Login.Ordinaryusers;
namespace EIMS_Login
{
    /// <summary>
    /// ChangePWD.xaml 的交互逻辑
    /// </summary>
    public partial class ChangePWD : Window
    {
        MD5Str md5 = new MD5Str();
        
        Connection Temp = new Connection();
        OrdinaryUserInfo TempUser = new OrdinaryUserInfo();
        bool TempBool = true;
        public ChangePWD()
        {
            InitializeComponent();
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Cancle_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private bool PwdJudge(string str)
        {
            try
            {
                string SQL = "select * from ArmsUsers where Usersname='" + MainWindow.CurrentUser + "' and Userspwd = '"+ str + "'";
                SqlCommand CMD_1 = new SqlCommand(SQL, Temp.GetConn());
                SqlDataReader Sdr_1 = CMD_1.ExecuteReader();
                if (Sdr_1.Read())
                {
                    Sdr_1.Close();
                    return true;
                }
                Sdr_1.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show("错误：\n\n" + ex);
            }
            return false;
         }
        private void Again_LostFocus(object sender, RoutedEventArgs e)
        {
            if (NewPwd.Password != Again.Password)
            {
                label3.Content = "两次密码输入不同！";
                label3.Foreground = Brushes.Red;
                TempBool = false;
            }
            else
            {
                label3.Content = "";
                TempBool = true;
            }
        }

        private void NewPwd_LostFocus(object sender, RoutedEventArgs e)
        {
            if (NewPwd.Password.Length < 6 || NewPwd.Password.Length > 16)
            {
                label2.Content = "密码长度不6--16为之间！";
                label2.Foreground = Brushes.Red;
                TempBool = false;
            }
            else
            {
                label2.Content = "";
                TempBool = true;
            }
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!PwdJudge(md5.MD5Encoding(PasswordBox.Password.ToString())))
            {
                label1.Content = "原始密码错误！";
                label1.Foreground = Brushes.Red;
            }
            else
            {
                label1.Content = "密码验证正确！";
                label1.Foreground = Brushes.Green;
            }
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            bool BoolTemp = PwdJudge(md5.MD5Encoding(PasswordBox.Password.ToString()));
            if (BoolTemp && TempBool)
            {
                string SQL = "update ArmsUsers set Userspwd = '" + md5.MD5Encoding(NewPwd.Password.ToString()) +
                    "' where Usersname = '" + MainWindow.CurrentUser + "'";
                try
                {
                    SqlCommand CMD_1 = new SqlCommand(SQL, Temp.GetConn());
                    CMD_1.ExecuteNonQuery();
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("错误：密码修改失败，可能是服务器出现未知错误！");
                    return;
                }
                MessageBox.Show("密码修改成功，下次登陆请使用新密码！");
                this.Close();
            }

        }
    }
}
