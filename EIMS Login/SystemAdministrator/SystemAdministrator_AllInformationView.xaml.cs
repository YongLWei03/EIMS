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

        private void DepartmentStaff_Click(object sender, RoutedEventArgs e)
        {
            List.Children.Clear();
            DepartmentStaff DepartmentStaff = new DepartmentStaff();
            List.Children.Add(DepartmentStaff);
            DepartmentStaff.SetValue(Grid.RowProperty, 0);
            DepartmentStaff.SetValue(Grid.ColumnProperty, 3);
            DepartmentStaff.SetValue(Grid.ColumnSpanProperty, 12);
        }

        private void Maintenance_Click(object sender, RoutedEventArgs e)
        {
            List.Children.Clear();
            Maintenance Maintenance = new Maintenance();
            List.Children.Add(Maintenance);
            Maintenance.SetValue(Grid.RowProperty, 0);
            Maintenance.SetValue(Grid.ColumnProperty, 3);
            Maintenance.SetValue(Grid.ColumnSpanProperty, 12);
        }

        private void WarehouseStocks_Click(object sender, RoutedEventArgs e)
        {
            List.Children.Clear();
            WarehouseStocks WarehouseStocks = new WarehouseStocks();
            List.Children.Add(WarehouseStocks);
            WarehouseStocks.SetValue(Grid.RowProperty, 0);
            WarehouseStocks.SetValue(Grid.ColumnProperty, 3);
            WarehouseStocks.SetValue(Grid.ColumnSpanProperty, 12);
        }

        private void Allocation_Click(object sender, RoutedEventArgs e)
        {
            List.Children.Clear();
            Allocation Allocation = new Allocation();
            List.Children.Add(Allocation);
            Allocation.SetValue(Grid.RowProperty, 0);
            Allocation.SetValue(Grid.ColumnProperty, 3);
            Allocation.SetValue(Grid.ColumnSpanProperty, 12);
        }

        private void OutputLibrary_Click(object sender, RoutedEventArgs e)
        {
            List.Children.Clear();
            OutputLibrary OutputLibrary = new OutputLibrary();
            List.Children.Add(OutputLibrary);
            OutputLibrary.SetValue(Grid.RowProperty, 0);
            OutputLibrary.SetValue(Grid.ColumnProperty, 3);
            OutputLibrary.SetValue(Grid.ColumnSpanProperty, 12);
        }

        private void InputLibrary_Click(object sender, RoutedEventArgs e)
        {
            List.Children.Clear();
            InputLibrary InputLibrary = new InputLibrary();
            List.Children.Add(InputLibrary);
            InputLibrary.SetValue(Grid.RowProperty, 0);
            InputLibrary.SetValue(Grid.ColumnProperty, 3);
            InputLibrary.SetValue(Grid.ColumnSpanProperty, 12);
        }

        private void Data_Click(object sender, RoutedEventArgs e)
        {
            List.Children.Clear();
            Data Data = new Data();
            List.Children.Add(Data);
            Data.SetValue(Grid.RowProperty, 0);
            Data.SetValue(Grid.ColumnProperty, 3);
            Data.SetValue(Grid.ColumnSpanProperty, 12);
        }

        private void Borrow_Click(object sender, RoutedEventArgs e)
        {
            List.Children.Clear();
            Borrow Borrow = new Borrow();
            List.Children.Add(Borrow);
            Borrow.SetValue(Grid.RowProperty, 0);
            Borrow.SetValue(Grid.ColumnProperty, 3);
            Borrow.SetValue(Grid.ColumnSpanProperty, 12);
        }

        private void File_Click(object sender, RoutedEventArgs e)
        {
            List.Children.Clear();
            File File = new File();
            List.Children.Add(File);
            File.SetValue(Grid.RowProperty, 0);
            File.SetValue(Grid.ColumnProperty, 3);
            File.SetValue(Grid.ColumnSpanProperty, 12);
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            List.Children.Clear();
            Help Help = new Help();
            List.Children.Add(Help);
            Help.SetValue(Grid.RowProperty, 0);
            Help.SetValue(Grid.ColumnProperty, 3);
            Help.SetValue(Grid.ColumnSpanProperty, 12);
        }

    }
}
