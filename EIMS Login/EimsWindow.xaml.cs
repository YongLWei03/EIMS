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

namespace EIMS_Login
{
    /// <summary>
    /// EimsWindow.xaml 的交互逻辑
    /// </summary>
    public partial class EimsWindow : Window
    {
        public EimsWindow()
        {
            InitializeComponent();
        }

        private void DragWindow(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MenuLoad();
        }
        private void MenuLoad()
        {
            int column = 0;
            switch (MainWindow.user)
            {
                case MainWindow.userIdentity.Ordinary_users://普通用户
                    Button Databorrow = new Button();
                    Button EquipmentApplication = new Button();
                    Button MaintenanceApplication = new Button();
                    Button DataSearch = new Button();
                    Button EquipmentInventory = new Button();
                    Databorrow.Width = 100;
                    Databorrow.Height = 100;
                    Databorrow.Style = Resources["MyButton"] as Style;
                    EquipmentApplication.Width = 100;
                    EquipmentApplication.Height = 100;
                    EquipmentApplication.Style = Resources["MyButton"] as Style;
                    MaintenanceApplication.Width = 100;
                    MaintenanceApplication.Height = 100;
                    MaintenanceApplication.Style = Resources["MyButton"] as Style;
                    DataSearch.Width = 100;
                    DataSearch.Height = 100;
                    DataSearch.Style = Resources["MyButton"] as Style;
                    EquipmentInventory.Width = 100;
                    EquipmentInventory.Height = 100;
                    EquipmentInventory.Style = Resources["MyButton"] as Style;
                    application.Children.Add(Databorrow);
                    application.Children.Add(EquipmentApplication);
                    application.Children.Add(MaintenanceApplication);
                    application.Children.Add(DataSearch);
                    application.Children.Add(EquipmentInventory);
                    Databorrow.SetValue(Grid.ColumnProperty, 0);
                    Databorrow.SetValue(Grid.RowProperty, 0);
                    EquipmentApplication.SetValue(Grid.ColumnProperty, 1);
                    EquipmentApplication.SetValue(Grid.RowProperty, 0);
                    MaintenanceApplication.SetValue(Grid.ColumnProperty, 2);
                    MaintenanceApplication.SetValue(Grid.RowProperty, 0);
                    DataSearch.SetValue(Grid.ColumnProperty, 3);
                    DataSearch.SetValue(Grid.RowProperty, 0);
                    EquipmentInventory.SetValue(Grid.ColumnProperty, 4);
                    EquipmentInventory.SetValue(Grid.RowProperty, 0);
                    column = 5;
                    break;
                case MainWindow.userIdentity.Warehouse_manager://仓库管理员
                    Button TransferApplication = new Button();
                    Button AllocationStatus = new Button();
                    Button OutOfStorage = new Button();
                    Button Inventory = new Button();
                    Button warehouse = new Button();
                    TransferApplication.Width = 100;
                    TransferApplication.Height = 100;
                    TransferApplication.Style = Resources["MyButton"] as Style;
                    AllocationStatus.Width = 100;
                    AllocationStatus.Height = 100;
                    AllocationStatus.Style = Resources["MyButton"] as Style;
                    OutOfStorage.Width = 100;
                    OutOfStorage.Height = 100;
                    OutOfStorage.Style = Resources["MyButton"] as Style;
                    Inventory.Width = 100;
                    Inventory.Height = 100;
                    Inventory.Style = Resources["MyButton"] as Style;
                    warehouse.Width = 100;
                    warehouse.Height = 100;
                    warehouse.Style = Resources["MyButton"] as Style;
                    application.Children.Add(TransferApplication);
                    application.Children.Add(AllocationStatus);
                    application.Children.Add(OutOfStorage);
                    application.Children.Add(Inventory);
                    application.Children.Add(warehouse);
                    TransferApplication.SetValue(Grid.ColumnProperty, 0);
                    TransferApplication.SetValue(Grid.RowProperty, 0);
                    AllocationStatus.SetValue(Grid.ColumnProperty, 1);
                    AllocationStatus.SetValue(Grid.RowProperty, 0);
                    OutOfStorage.SetValue(Grid.ColumnProperty, 2);
                    OutOfStorage.SetValue(Grid.RowProperty, 0);
                    Inventory.SetValue(Grid.ColumnProperty, 3);
                    Inventory.SetValue(Grid.RowProperty, 0);
                    warehouse.SetValue(Grid.ColumnProperty, 4);
                    warehouse.SetValue(Grid.RowProperty, 0);
                    column = 5;
                    break;
                case MainWindow.userIdentity.System_administrator://系统管理员
                    Button AddUser = new Button();
                    Button NetUser = new Button();
                    Button Logging = new Button();
                    Button EquipmentInformationAdded = new Button();
                    Button AllInformationView = new Button();
                    AddUser.Width = 100;
                    AddUser.Height = 100;
                    AddUser.Style = Resources["MyButton"] as Style;
                    NetUser.Width = 100;
                    NetUser.Height = 100;
                    NetUser.Style = Resources["MyButton"] as Style;
                    Logging.Width = 100;
                    Logging.Height = 100;
                    Logging.Style = Resources["MyButton"] as Style;
                    EquipmentInformationAdded.Width = 100;
                    EquipmentInformationAdded.Height = 100;
                    EquipmentInformationAdded.Style = Resources["MyButton"] as Style;
                    AllInformationView.Width = 100;
                    AllInformationView.Height = 100;
                    AllInformationView.Style = Resources["MyButton"] as Style;
                    application.Children.Add(AddUser);
                    application.Children.Add(NetUser);
                    application.Children.Add(Logging);
                    application.Children.Add(EquipmentInformationAdded);
                    application.Children.Add(AllInformationView);
                    AddUser.SetValue(Grid.ColumnProperty, 0);
                    AddUser.SetValue(Grid.RowProperty, 0);
                    NetUser.SetValue(Grid.ColumnProperty, 1);
                    NetUser.SetValue(Grid.RowProperty, 0);
                    Logging.SetValue(Grid.ColumnProperty, 2);
                    Logging.SetValue(Grid.RowProperty, 0);
                    EquipmentInformationAdded.SetValue(Grid.ColumnProperty, 3);
                    EquipmentInformationAdded.SetValue(Grid.RowProperty, 0);
                    AllInformationView.SetValue(Grid.ColumnProperty, 4);
                    AllInformationView.SetValue(Grid.RowProperty, 0);
                    column = 5;
                    break;
                case MainWindow.userIdentity.Confidential_clerk://保密员
                    Button LoanApplication = new Button();
                    Button Borrowinfo = new Button();
                    Button NewEquipmentInformationAdded = new Button();
                    Button EquipmentFiling = new Button();
                    LoanApplication.Width = 100;
                    LoanApplication.Height = 100;
                    LoanApplication.Style = Resources["MyButton"] as Style;
                    Borrowinfo.Width = 100;
                    Borrowinfo.Height = 100;
                    Borrowinfo.Style = Resources["MyButton"] as Style;
                    NewEquipmentInformationAdded.Width = 100;
                    NewEquipmentInformationAdded.Height = 100;
                    NewEquipmentInformationAdded.Style = Resources["MyButton"] as Style;
                    EquipmentFiling.Width = 100;
                    EquipmentFiling.Height = 100;
                    EquipmentFiling.Style = Resources["MyButton"] as Style;
                    application.Children.Add(LoanApplication);
                    application.Children.Add(Borrowinfo);
                    application.Children.Add(NewEquipmentInformationAdded);
                    application.Children.Add(EquipmentFiling);
                    LoanApplication.SetValue(Grid.ColumnProperty, 0);
                    LoanApplication.SetValue(Grid.RowProperty, 0);
                    Borrowinfo.SetValue(Grid.ColumnProperty, 1);
                    Borrowinfo.SetValue(Grid.RowProperty, 0);
                    NewEquipmentInformationAdded.SetValue(Grid.ColumnProperty, 2);
                    NewEquipmentInformationAdded.SetValue(Grid.RowProperty, 0);
                    EquipmentFiling.SetValue(Grid.ColumnProperty, 3);
                    EquipmentFiling.SetValue(Grid.RowProperty, 0);
                    column = 4;
                    break;
                case MainWindow.userIdentity.Maintenance_man://维修员
                    Button ViewMaintenanceApplication = new Button();
                    Button MaintenanceStatus = new Button();
                    ViewMaintenanceApplication.Width = 100;
                    ViewMaintenanceApplication.Height = 100;
                    ViewMaintenanceApplication.Style = Resources["MyButton"] as Style;
                    MaintenanceStatus.Width = 100;
                    MaintenanceStatus.Height = 100;
                    MaintenanceStatus.Style = Resources["MyButton"] as Style;
                    application.Children.Add(ViewMaintenanceApplication);
                    application.Children.Add(MaintenanceStatus);
                    ViewMaintenanceApplication.SetValue(Grid.ColumnProperty, 0);
                    ViewMaintenanceApplication.SetValue(Grid.RowProperty, 0);
                    MaintenanceStatus.SetValue(Grid.ColumnProperty, 1);
                    MaintenanceStatus.SetValue(Grid.RowProperty, 0);
                    column = 2;
                    break;
                case MainWindow.userIdentity.Finance_department://财务员
                    Button BookingAndHistory = new Button();
                    Button FinancialUse = new Button();
                    BookingAndHistory.Width = 100;
                    BookingAndHistory.Height = 100;
                    BookingAndHistory.Style = Resources["MyButton"] as Style;
                    FinancialUse.Width = 100;
                    FinancialUse.Height = 100;
                    FinancialUse.Style = Resources["MyButton"] as Style;
                    application.Children.Add(BookingAndHistory);
                    application.Children.Add(FinancialUse);
                    BookingAndHistory.SetValue(Grid.ColumnProperty, 0);
                    BookingAndHistory.SetValue(Grid.RowProperty, 0);
                    FinancialUse.SetValue(Grid.ColumnProperty, 1);
                    FinancialUse.SetValue(Grid.RowProperty, 0);
                    column = 2;
                    break;
            }
            Button PersonalInformation = new Button();
            Button Renovate = new Button();
            Button Cancel = new Button();
            PersonalInformation.Width = 100;
            PersonalInformation.Height = 100;
            PersonalInformation.Style = Resources["MyButton"] as Style;
            Renovate.Width = 100;
            Renovate.Height = 100;
            Renovate.Style = Resources["MyButton"] as Style;
            Cancel.Width = 100;
            Cancel.Height = 100;
            Cancel.Style = Resources["MyButton"] as Style;
            application.Children.Add(PersonalInformation);
            application.Children.Add(Renovate);
            application.Children.Add(Cancel);
            PersonalInformation.SetValue(Grid.ColumnProperty, column);
            PersonalInformation.SetValue(Grid.RowProperty, 0);
            Renovate.SetValue(Grid.ColumnProperty, column + 1);
            Renovate.SetValue(Grid.RowProperty, 0);
            Cancel.SetValue(Grid.ColumnProperty, column + 2);
            Cancel.SetValue(Grid.RowProperty, 0);
        }
    }
}
