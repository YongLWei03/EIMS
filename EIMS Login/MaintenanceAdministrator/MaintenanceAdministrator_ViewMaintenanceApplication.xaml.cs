﻿using EIMS_Login.MaintenanceAdministrator;
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
    /// MaintenanceAdministrator_1.xaml 的交互逻辑
    /// </summary>
    public partial class MaintenanceAdministrator_ViewMaintenanceApplication : UserControl
    {
        public int selecttime = 0;
        public MaintenanceAdministrator_ViewMaintenanceApplication()
        {
            InitializeComponent();
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("    确认要提交？", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Warning, MessageBoxResult.OK) != MessageBoxResult.OK)
            {
                return;
            }
            if(comboBox.SelectedIndex != 0)
            {
                MessageBox.Show("该选项下的表格无法提交");
                return;
            }
            if (maintaindatagrid.updata())
            {
                MessageBox.Show("提交成功");
            }
            maintaindatagrid.connectataBase(comboBox.SelectedIndex);
            labe2.Content = maintaindatagrid.maintenance.Items.Count;
        }

        private void maintaindatagrid_Loaded(object sender, RoutedEventArgs e)
        {
            maintaindatagrid.connectataBase(comboBox.SelectedIndex);
            labe2.Content = maintaindatagrid.maintenance.Items.Count;
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (selecttime != 0)
            {
                maintaindatagrid.connectataBase(comboBox.SelectedIndex);
                labe2.Content = maintaindatagrid.maintenance.Items.Count;
            }
            selecttime++;
        }
    }
}
