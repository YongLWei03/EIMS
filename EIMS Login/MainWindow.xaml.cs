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

namespace EIMS_Login
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        //    public static int userIdentity = 0;
        public static userIdentity user;
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

        private void Login(object sender, RoutedEventArgs e)
        {
            user = (userIdentity)Identity.SelectedIndex;
            this.Hide();
            EimsWindow win1 = new EimsWindow();
            win1.Show();
        }
    }
}
