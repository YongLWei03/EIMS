using EIMS_Login.Ordinary_users;
using EIMS_Login.Ordinaryusers;
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
                    ImageBrush Image = new ImageBrush();
                    Image.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/Ordinaryusers/借阅申请.png", UriKind.RelativeOrAbsolute));
                    Image.Stretch = Stretch.Fill;
                    Databorrow.Background = Image;
                    EquipmentApplication.Width = 100;
                    EquipmentApplication.Height = 100;
                    EquipmentApplication.Style = Resources["MyButton"] as Style;
                    ImageBrush Image1 = new ImageBrush();
                    Image1.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/Ordinaryusers/装备申请.png", UriKind.RelativeOrAbsolute));
                    Image1.Stretch = Stretch.Fill;
                    EquipmentApplication.Background = Image1;
                    MaintenanceApplication.Width = 100;
                    MaintenanceApplication.Height = 100;
                    MaintenanceApplication.Style = Resources["MyButton"] as Style;
                    ImageBrush Image2 = new ImageBrush();
                    Image2.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/Ordinaryusers/维修申请.png", UriKind.RelativeOrAbsolute));
                    Image2.Stretch = Stretch.Fill;
                    MaintenanceApplication.Background = Image2;
                    DataSearch.Width = 100;
                    DataSearch.Height = 100;
                    DataSearch.Style = Resources["MyButton"] as Style;
                    ImageBrush Image3 = new ImageBrush();
                    Image3.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/Ordinaryusers/资料信息.png", UriKind.RelativeOrAbsolute));
                    Image3.Stretch = Stretch.Fill;
                    DataSearch.Background = Image3;
                    EquipmentInventory.Width = 100;
                    EquipmentInventory.Height = 100;
                    EquipmentInventory.Style = Resources["MyButton"] as Style;
                    ImageBrush Image4 = new ImageBrush();
                    Image4.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/Ordinaryusers/装备信息.png", UriKind.RelativeOrAbsolute));
                    Image4.Stretch = Stretch.Fill;
                    EquipmentInventory.Background = Image4;
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

                    Databorrow.Click += new RoutedEventHandler(DataborrowClick);
                    EquipmentApplication.Click += new RoutedEventHandler(EquipmentApplicationClick);
                    MaintenanceApplication.Click += new RoutedEventHandler(MaintenanceApplicationClick);
                    DataSearch.Click += new RoutedEventHandler(DataSearchClick);
                    EquipmentInventory.Click += new RoutedEventHandler(EquipmentInventoryClick);


                    application_children.Children.Clear();
                    OrdinaryUsers_Databorrow OrdinaryUsers_Databorrow = new OrdinaryUsers_Databorrow();
                    application_children.Children.Add(OrdinaryUsers_Databorrow);
                    OrdinaryUsers_Databorrow.SetValue(Grid.RowProperty, 1);
                    OrdinaryUsers_Databorrow.SetValue(Grid.ColumnSpanProperty, 12);
                    break;
                case MainWindow.userIdentity.Warehouse_manager://仓库管理员
                    Button TransferApplication = new Button();
                    Button AllocationStatus = new Button();
                    Button OutOfStorage = new Button();
                    Button Inventory = new Button();
                    Button Addwarehouse = new Button();
                    TransferApplication.Width = 100;
                    TransferApplication.Height = 100;
                    TransferApplication.Style = Resources["MyButton"] as Style;
                    ImageBrush WarehouseImage1 = new ImageBrush();
                    WarehouseImage1.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/WarehouseManager/申请管理.png", UriKind.RelativeOrAbsolute));
                    WarehouseImage1.Stretch = Stretch.Fill;
                    TransferApplication.Background = WarehouseImage1;
                    AllocationStatus.Width = 100;
                    AllocationStatus.Height = 100;
                    AllocationStatus.Style = Resources["MyButton"] as Style;
                    ImageBrush WarehouseImage2 = new ImageBrush();
                    WarehouseImage2.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/WarehouseManager/调拨状态.png", UriKind.RelativeOrAbsolute));
                    WarehouseImage2.Stretch = Stretch.Fill;
                    AllocationStatus.Background = WarehouseImage2;
                    OutOfStorage.Width = 100;
                    OutOfStorage.Height = 100;
                    OutOfStorage.Style = Resources["MyButton"] as Style;
                    ImageBrush WarehouseImage3 = new ImageBrush();
                    WarehouseImage3.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/WarehouseManager/出入库管理.png", UriKind.RelativeOrAbsolute));
                    WarehouseImage3.Stretch = Stretch.Fill;
                    OutOfStorage.Background = WarehouseImage3;
                    Inventory.Width = 100;
                    Inventory.Height = 100;
                    Inventory.Style = Resources["MyButton"] as Style;
                    ImageBrush WarehouseImage4 = new ImageBrush();
                    WarehouseImage4.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/WarehouseManager/库存信息.png", UriKind.RelativeOrAbsolute));
                    WarehouseImage4.Stretch = Stretch.Fill;
                    Inventory.Background = WarehouseImage4;
                    Addwarehouse.Width = 100;
                    Addwarehouse.Height = 100;
                    Addwarehouse.Style = Resources["MyButton"] as Style;
                    ImageBrush WarehouseImage5 = new ImageBrush();
                    WarehouseImage5.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/WarehouseManager/增加仓库.png", UriKind.RelativeOrAbsolute));
                    WarehouseImage5.Stretch = Stretch.Fill;
                    Addwarehouse.Background = WarehouseImage5;
                    application.Children.Add(TransferApplication);
                    application.Children.Add(AllocationStatus);
                    application.Children.Add(OutOfStorage);
                    application.Children.Add(Inventory);
                    application.Children.Add(Addwarehouse);
                    TransferApplication.SetValue(Grid.ColumnProperty, 0);
                    TransferApplication.SetValue(Grid.RowProperty, 0);
                    AllocationStatus.SetValue(Grid.ColumnProperty, 1);
                    AllocationStatus.SetValue(Grid.RowProperty, 0);
                    OutOfStorage.SetValue(Grid.ColumnProperty, 2);
                    OutOfStorage.SetValue(Grid.RowProperty, 0);
                    Inventory.SetValue(Grid.ColumnProperty, 3);
                    Inventory.SetValue(Grid.RowProperty, 0);
                    Addwarehouse.SetValue(Grid.ColumnProperty, 4);
                    Addwarehouse.SetValue(Grid.RowProperty, 0);
                    column = 5;

                    TransferApplication.Click += new RoutedEventHandler(TransferApplicationClick);
                    AllocationStatus.Click += new RoutedEventHandler(AllocationStatusClick);
                    OutOfStorage.Click += new RoutedEventHandler(OutOfStorageClick);
                    Inventory.Click += new RoutedEventHandler(InventoryClick);
                    Addwarehouse.Click += new RoutedEventHandler(AddwarehouseClick);

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

                    AddUser.Click += new RoutedEventHandler(AddUserClick);
                    NetUser.Click += new RoutedEventHandler(NetUserClick);
                    Logging.Click += new RoutedEventHandler(LoggingClick);
                    EquipmentInformationAdded.Click += new RoutedEventHandler(EquipmentInformationAddedClick);
                    AllInformationView.Click += new RoutedEventHandler(AllInformationViewClick);
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

                    LoanApplication.Click += new RoutedEventHandler(LoanApplicationClick);
                    Borrowinfo.Click += new RoutedEventHandler(BorrowinfoClick);
                    NewEquipmentInformationAdded.Click += new RoutedEventHandler(NewEquipmentInformationAddedClick);
                    EquipmentFiling.Click += new RoutedEventHandler(EquipmentFilingClick);

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

                    ViewMaintenanceApplication.Click += new RoutedEventHandler(ViewMaintenanceApplicationClick);
                    MaintenanceStatus.Click += new RoutedEventHandler(MaintenanceStatusClick);

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

                    BookingAndHistory.Click += new RoutedEventHandler(BookingAndHistoryClick);
                    FinancialUse.Click += new RoutedEventHandler(FinancialUseClick);
                    break;
            }
            Button PersonalInformation = new Button();
            Button Renovate = new Button();
            Button Cancel = new Button();

            PersonalInformation.Width = 100;
            PersonalInformation.Height = 100;
            PersonalInformation.Style = Resources["MyButton"] as Style;
            ImageBrush Image98 = new ImageBrush();
            Image98.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/Public/个人信息.png", UriKind.RelativeOrAbsolute));
            Image98.Stretch = Stretch.Fill;
            PersonalInformation.Background = Image98;

            Renovate.Width = 100;
            Renovate.Height = 100;
            Renovate.Style = Resources["MyButton"] as Style;
            ImageBrush Image99 = new ImageBrush();
            Image99.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/Public/刷新.png", UriKind.RelativeOrAbsolute));
            Image99.Stretch = Stretch.Fill;
            Renovate.Background = Image99;

            Cancel.Width = 100;
            Cancel.Height = 100;
            Cancel.Style = Resources["MyButton"] as Style;
            ImageBrush Image100 = new ImageBrush();
            Image100.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/Public/注销登录.png", UriKind.RelativeOrAbsolute));
            Image100.Stretch = Stretch.Fill;
            Cancel.Background = Image100;

            application.Children.Add(PersonalInformation);
            application.Children.Add(Renovate);
            application.Children.Add(Cancel);
            PersonalInformation.SetValue(Grid.ColumnProperty, column);
            PersonalInformation.SetValue(Grid.RowProperty, 0);
            Renovate.SetValue(Grid.ColumnProperty, column + 1);
            Renovate.SetValue(Grid.RowProperty, 0);
            Cancel.SetValue(Grid.ColumnProperty, column + 2);
            Cancel.SetValue(Grid.RowProperty, 0);

            Cancel.Click += new RoutedEventHandler(CancelrClick);
        }
        private void CancelrClick(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dr = MessageBox.Show("你确定要注销登陆吗？", "系统提示",MessageBoxButton.OKCancel,MessageBoxImage.Warning);
            if (dr == MessageBoxResult.OK)
            {
                MainWindow TempWindow = new MainWindow();
                TempWindow.Show();
                this.Close();
            }
        }
        private void AddUserClick(object sender, RoutedEventArgs e)
        {
            application_children.Children.Clear();
            SystemAdministrator_AddUser SystemAdministrator_1 = new SystemAdministrator_AddUser();
            application_children.Children.Add(SystemAdministrator_1);
            SystemAdministrator_1.SetValue(Grid.RowProperty, 1);
            SystemAdministrator_1.SetValue(Grid.ColumnSpanProperty, 12);
        }
        private void NetUserClick(object sender, RoutedEventArgs e)
        {
            application_children.Children.Clear();
            SystemAdministrator_NetUser SystemAdministrator_NetUser = new SystemAdministrator_NetUser();
            application_children.Children.Add(SystemAdministrator_NetUser);
            SystemAdministrator_NetUser.SetValue(Grid.RowProperty, 1);
            SystemAdministrator_NetUser.SetValue(Grid.ColumnSpanProperty, 12);
        }
        private void LoggingClick(object sender, RoutedEventArgs e)
        {
            application_children.Children.Clear();
            SystemAdministrator_Logging SystemAdministrator_Logging = new SystemAdministrator_Logging();
            application_children.Children.Add(SystemAdministrator_Logging);
            SystemAdministrator_Logging.SetValue(Grid.RowProperty, 1);
            SystemAdministrator_Logging.SetValue(Grid.ColumnSpanProperty, 12);
        }
        private void EquipmentInformationAddedClick(object sender, RoutedEventArgs e)
        {
            application_children.Children.Clear();
            SystemAdministrator_EquipmentInformationAdded SystemAdministrator_EquipmentInformationAdded = new SystemAdministrator_EquipmentInformationAdded();
            application_children.Children.Add(SystemAdministrator_EquipmentInformationAdded);
            SystemAdministrator_EquipmentInformationAdded.SetValue(Grid.RowProperty, 1);
            SystemAdministrator_EquipmentInformationAdded.SetValue(Grid.ColumnSpanProperty, 12);
        }
        private void AllInformationViewClick(object sender,RoutedEventArgs e)
        {
            application_children.Children.Clear();
            SystemAdministrator_AllInformationView SystemAdministrator_AllInformationView = new SystemAdministrator_AllInformationView();
            application_children.Children.Add(SystemAdministrator_AllInformationView);
            SystemAdministrator_AllInformationView.SetValue(Grid.RowProperty, 1);
            SystemAdministrator_AllInformationView.SetValue(Grid.ColumnSpanProperty, 12);
        }

        private void TransferApplicationClick(object sender,RoutedEventArgs e)
        {
            application_children.Children.Clear();
            WarehouseManager_TransferApplication WarehouseManager_TransferApplication = new WarehouseManager_TransferApplication();
            application_children.Children.Add(WarehouseManager_TransferApplication);
            WarehouseManager_TransferApplication.SetValue(Grid.RowProperty, 1);
            WarehouseManager_TransferApplication.SetValue(Grid.ColumnSpanProperty, 12);
        }

        private void AllocationStatusClick(object sender,RoutedEventArgs e)
        {
            application_children.Children.Clear();
            WarehouseManager_AllocationStatus WarehouseManager_AllocationStatus = new WarehouseManager_AllocationStatus();
            application_children.Children.Add(WarehouseManager_AllocationStatus);
            WarehouseManager_AllocationStatus.SetValue(Grid.RowProperty, 1);
            WarehouseManager_AllocationStatus.SetValue(Grid.ColumnSpanProperty, 12);
        }
        private void OutOfStorageClick(object sender,RoutedEventArgs e)
        {
            application_children.Children.Clear();
            WarehouseManager_OutOfStorage WarehouseManager_OutOfStorage = new WarehouseManager_OutOfStorage();
            application_children.Children.Add(WarehouseManager_OutOfStorage);
            WarehouseManager_OutOfStorage.SetValue(Grid.RowProperty, 1);
            WarehouseManager_OutOfStorage.SetValue(Grid.ColumnSpanProperty, 12);
        }
        private void InventoryClick(object sender,RoutedEventArgs e)
        {
            application_children.Children.Clear();
            WarehouseManager_Inventory WarehouseManager_Inventory = new WarehouseManager_Inventory();
            application_children.Children.Add(WarehouseManager_Inventory);
            WarehouseManager_Inventory.SetValue(Grid.RowProperty, 1);
            WarehouseManager_Inventory.SetValue(Grid.ColumnSpanProperty, 12);
        }
        private void AddwarehouseClick(object sender,RoutedEventArgs e)
        {
            application_children.Children.Clear();
            WarehouseManager_Addwarehouse WarehouseManager_Addwarehouse = new WarehouseManager_Addwarehouse();
            application_children.Children.Add(WarehouseManager_Addwarehouse);
            WarehouseManager_Addwarehouse.SetValue(Grid.RowProperty, 1);
            WarehouseManager_Addwarehouse.SetValue(Grid.ColumnSpanProperty, 12);
        }
        private void LoanApplicationClick(object sender,RoutedEventArgs e)
        {
            application_children.Children.Clear();
            ConfidentialMembers_LoanApplication ConfidentialMembers_LoanApplication = new ConfidentialMembers_LoanApplication();
            application_children.Children.Add(ConfidentialMembers_LoanApplication);
            ConfidentialMembers_LoanApplication.SetValue(Grid.RowProperty, 1);
            ConfidentialMembers_LoanApplication.SetValue(Grid.ColumnSpanProperty, 12);
        }
        private void BorrowinfoClick(object sender,RoutedEventArgs e)
        {
            application_children.Children.Clear();
            ConfidentialMembers_Borrowinfo ConfidentialMembers_Borrowinfo = new ConfidentialMembers_Borrowinfo();
            application_children.Children.Add(ConfidentialMembers_Borrowinfo);
            ConfidentialMembers_Borrowinfo.SetValue(Grid.RowProperty, 1);
            ConfidentialMembers_Borrowinfo.SetValue(Grid.ColumnSpanProperty, 12);
        }
        private void NewEquipmentInformationAddedClick(object sender,RoutedEventArgs e)
        {
            application_children.Children.Clear();
            ConfidentialMembers_NewEquipmentInformationAdded ConfidentialMembers_NewEquipmentInformationAdded = new ConfidentialMembers_NewEquipmentInformationAdded();
            application_children.Children.Add(ConfidentialMembers_NewEquipmentInformationAdded);
            ConfidentialMembers_NewEquipmentInformationAdded.SetValue(Grid.RowProperty, 1);
            ConfidentialMembers_NewEquipmentInformationAdded.SetValue(Grid.ColumnSpanProperty, 12);
        }
        private void EquipmentFilingClick(object sender,RoutedEventArgs e)
        {
            application_children.Children.Clear();
            ConfidentialMembers_EquipmentFiling ConfidentialMembers_EquipmentFiling = new ConfidentialMembers_EquipmentFiling();
            application_children.Children.Add(ConfidentialMembers_EquipmentFiling);
            ConfidentialMembers_EquipmentFiling.SetValue(Grid.RowProperty, 1);
            ConfidentialMembers_EquipmentFiling.SetValue(Grid.ColumnSpanProperty, 12);
        }
        private void ViewMaintenanceApplicationClick(object sender,RoutedEventArgs e)
        {
            application_children.Children.Clear();
            MaintenanceAdministrator_ViewMaintenanceApplication MaintenanceAdministrator_ViewMaintenanceApplication = new MaintenanceAdministrator_ViewMaintenanceApplication();
            application_children.Children.Add(MaintenanceAdministrator_ViewMaintenanceApplication);
            MaintenanceAdministrator_ViewMaintenanceApplication.SetValue(Grid.RowProperty, 1);
            MaintenanceAdministrator_ViewMaintenanceApplication.SetValue(Grid.ColumnSpanProperty, 12);
        }
        private void MaintenanceStatusClick(object sender, RoutedEventArgs e)
        {
            application_children.Children.Clear();
            MaintenanceAdministrator_MaintenanceStatus MaintenanceAdministrator_MaintenanceStatusClick = new MaintenanceAdministrator_MaintenanceStatus();
            application_children.Children.Add(MaintenanceAdministrator_MaintenanceStatusClick);
            MaintenanceAdministrator_MaintenanceStatusClick.SetValue(Grid.RowProperty, 1);
            MaintenanceAdministrator_MaintenanceStatusClick.SetValue(Grid.ColumnSpanProperty, 12);
        }
        private void BookingAndHistoryClick(object sender,RoutedEventArgs e)
        {
            application_children.Children.Clear();
            FinancialAdministrator_BookingAndHistory FinancialAdministrator_BookingAndHistory = new FinancialAdministrator_BookingAndHistory();
            application_children.Children.Add(FinancialAdministrator_BookingAndHistory);
            FinancialAdministrator_BookingAndHistory.SetValue(Grid.RowProperty, 1);
            FinancialAdministrator_BookingAndHistory.SetValue(Grid.ColumnSpanProperty, 12);
        }
        private void FinancialUseClick(object sender,RoutedEventArgs e)
        {
            application_children.Children.Clear();
            FinancialAdministrator_FinancialUse FinancialAdministrator_FinancialUse = new FinancialAdministrator_FinancialUse();
            application_children.Children.Add(FinancialAdministrator_FinancialUse);
            FinancialAdministrator_FinancialUse.SetValue(Grid.RowProperty, 1);
            FinancialAdministrator_FinancialUse.SetValue(Grid.ColumnSpanProperty, 12);
        }
        private void DataborrowClick(object sender,RoutedEventArgs e)
        {
            application_children.Children.Clear();
            OrdinaryUsers_Databorrow OrdinaryUsers_Databorrow = new OrdinaryUsers_Databorrow();
            application_children.Children.Add(OrdinaryUsers_Databorrow);
            OrdinaryUsers_Databorrow.SetValue(Grid.RowProperty, 1);
            OrdinaryUsers_Databorrow.SetValue(Grid.ColumnSpanProperty, 12);
        }
        private void EquipmentApplicationClick(object sender, RoutedEventArgs e)
        {
            application_children.Children.Clear();
            OrdinaryUsers_EquipmentApplication OrdinaryUsers_EquipmentApplication = new OrdinaryUsers_EquipmentApplication();
            application_children.Children.Add(OrdinaryUsers_EquipmentApplication);
            OrdinaryUsers_EquipmentApplication.SetValue(Grid.RowProperty, 1);
            OrdinaryUsers_EquipmentApplication.SetValue(Grid.ColumnSpanProperty, 12);
        }
        private void MaintenanceApplicationClick(object sender,RoutedEventArgs e)
        {
            application_children.Children.Clear();
            OrdinaryUsers_MaintenanceApplication OrdinaryUsers_MaintenanceApplication = new OrdinaryUsers_MaintenanceApplication();
            application_children.Children.Add(OrdinaryUsers_MaintenanceApplication);
            OrdinaryUsers_MaintenanceApplication.SetValue(Grid.RowProperty, 1);
            OrdinaryUsers_MaintenanceApplication.SetValue(Grid.ColumnSpanProperty, 12);
        }
        private void EquipmentInventoryClick(object sender,RoutedEventArgs e)
        {
            application_children.Children.Clear();
            OrdinaryUsers_EquipmentInventory OrdinaryUsers_EquipmentInventory = new OrdinaryUsers_EquipmentInventory();
            application_children.Children.Add(OrdinaryUsers_EquipmentInventory);
            OrdinaryUsers_EquipmentInventory.SetValue(Grid.RowProperty, 1);
            OrdinaryUsers_EquipmentInventory.SetValue(Grid.ColumnSpanProperty, 12);
        }
        private void DataSearchClick(object sender,RoutedEventArgs e)
        {
            application_children.Children.Clear();
            OrdinaryUsers_DataSearch OrdinaryUsers_DataSearch = new OrdinaryUsers_DataSearch();
            application_children.Children.Add(OrdinaryUsers_DataSearch);
            OrdinaryUsers_DataSearch.SetValue(Grid.RowProperty, 1);
            OrdinaryUsers_DataSearch.SetValue(Grid.ColumnSpanProperty, 12);
        }

        private void Min_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}

