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
        public MainWindow()
        {
            InitializeComponent();
        }

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
            ;
        }
    }
}
