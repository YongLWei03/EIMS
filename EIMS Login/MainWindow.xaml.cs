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
using System.Windows.Media.Animation;
using System.Data;
using System.Data.SqlClient;

namespace EIMS_Login
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        Connection Temp = new Connection();
        MD5Str md5 = new MD5Str(); 
        //    public static int userIdentity = 0;
        public static userIdentity user;
        public static string CurrentUser;//当前用户
        static int TempInt = 1;

        public enum userIdentity
        {
            Ordinary_users,
            Warehouse_manager,
            System_administrator,
            Confidential_clerk,
            Maintenance_man,
            Finance_department
            
        }
        public MainWindow()
        {
            InitializeComponent();
            DoubleAnimationUsingKeyFrames dak = new DoubleAnimationUsingKeyFrames();
            dak.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))));
            dak.KeyFrames.Add(new LinearDoubleKeyFrame(660, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1))));
            dak.BeginTime = TimeSpan.FromSeconds(0);//从第0秒开始动画
            dak.RepeatBehavior = new RepeatBehavior(1);//动画1次
            //开始动画
            this.login.BeginAnimation(Border.WidthProperty, dak);
            

        }
        //函数功能：鼠标左键按住窗口拖动
        private void DragWindow(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void CloseWindows(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MinimizeWindows(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Account_GotFocus(object sender, RoutedEventArgs e)
        {
            if(Account.Foreground == Brushes.Gray)
            {
                Account.Clear();
                Account.Foreground = Brushes.Black;
            }
        }

        private void Account_LostFocus(object sender, RoutedEventArgs e)
        {
            if(Account.Text == "")
            {
                Account.Text = "请输入您的账号";
                Account.Foreground = Brushes.Gray;
            }
        }


        /*
         * 功能：登陆
         */
        private void Login(object sender, RoutedEventArgs e)
        {
            
            if (Temp.SqlConn()==1 && TempInt == 1)
            {
                MessageBox.Show("服务器连接失败！");
                return;
            }
            TempInt++;//使开启数据库连接只一次
            user = (userIdentity)Identity.SelectedIndex;
            if (user == userIdentity.Ordinary_users)//用户判断，输入账号登陆只对普通用户有效
            {
                try
                {
                    string StrSql_1 = "select * from ArmsUsers where Usersname='" + Account.Text + "'and Userspwd='" + md5.MD5Encoding(PasswordBox.Password.ToString()) + "'";
                    SqlCommand CMD_1 = new SqlCommand(StrSql_1, Temp.GetConn());
                    SqlDataReader Sdr_1 = CMD_1.ExecuteReader();
                    if (Sdr_1.Read())
                    {
                        string TempStr = Sdr_1[2].ToString();
                        Sdr_1.Close();
                        if (TempStr != "普通用户" && user == userIdentity.Ordinary_users)//权限判断，属于普通用户不能选择了其他用户组
                        {
                            MessageBox.Show("当前用户权限不足，请核对权限选择下拉框！");
                            return;
                        }
                        CurrentUser = Account.Text;//满足一切登陆条件后，保存当前账号名，以备各模块使用

                        EimsWindow win1 = new EimsWindow();
                        win1.Show();
                        this.Close();
                    }
                    else
                    {
                        Sdr_1.Close();
                        MessageBox.Show("登陆失败，账号或密码错误！");
                    }
                }
                catch
                {
                    MessageBox.Show("服务器异常,登陆失败！");
                    return;
                }
            }
            else//当选择的不是普通用户，可以直接登陆
            {
                
                EimsWindow win1 = new EimsWindow();
                win1.Show();
                this.Close();
            }
        }

    }
}
