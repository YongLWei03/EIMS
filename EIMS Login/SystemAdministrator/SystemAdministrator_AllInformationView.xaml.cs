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
using EIMS_Login.SystemAdministrator.SystemAdministrator_AllInformationView_Children;

namespace EIMS_Login
{
    /// <summary>
    /// SystemAdministrator_5.xaml 的交互逻辑
    /// </summary>
    public partial class SystemAdministrator_AllInformationView : UserControl
    {
        public SystemAdministrator_AllInformationView()
        {
            InitializeComponent();
        }

        private void DepartmentStaffClick(object sender, MouseButtonEventArgs e)
        {
            List.Children.Clear();
            DepartmentStaff DepartmentStaff = new DepartmentStaff();
            List.Children.Add(DepartmentStaff);
            DepartmentStaff.SetValue(Grid.RowProperty, 0);
            DepartmentStaff.SetValue(Grid.ColumnProperty, 3);
            DepartmentStaff.SetValue(Grid.ColumnSpanProperty, 12);
        }

    }
}
