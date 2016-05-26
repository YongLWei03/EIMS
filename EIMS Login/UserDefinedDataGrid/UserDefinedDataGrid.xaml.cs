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

namespace EIMS_Login.UserDefinedDataGrid
{
    /// <summary>
    /// UserDefinedDataGrid.xaml 的交互逻辑
    /// </summary>
    public partial class UserDefinedDataGrid : UserControl
    {
        Connection Temp = new Connection();
        public UserDefinedDataGrid()
        {
            InitializeComponent();
            dataGrid.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
            dataGrid.VerticalAlignment = System.Windows.VerticalAlignment.Center;
        }
        public void InitTableHeightWidth(int Height,int Width)
        {
            dataGrid.Height = Height;
            dataGrid.Width = Width;
            dataGrid.FontSize = 16;
            
        }
        public void AddColumns(string Binding,string Header,int Width)
        {
            dataGrid.AutoGenerateColumns = false;
            System.Windows.Data.Binding binding = new System.Windows.Data.Binding(Binding);
            binding.Mode = System.Windows.Data.BindingMode.OneWay;
            DataGridTextColumn Temp = new DataGridTextColumn();
            Temp.Header = Header;
            Temp.Width = Width;
            Temp.Binding = binding;
            Temp.ElementStyle = Resources["MyDataGrid"] as Style;
            dataGrid.Columns.Add(Temp);
        }
        public void DataTableSelect(string SQL)
        {
            DataTable Tabel0 = new DataTable();
            SqlDataAdapter sda = new SqlDataAdapter(SQL, Temp.GetConnStr());
            DataSet ds = new DataSet();
            ds.Clear();
            sda.Fill(Tabel0);
            dataGrid.ItemsSource = Tabel0.DefaultView;       
        }

        private void dataGrid_LoadingRow(object sender, DataGridRowEventArgs e)
        {
            e.Row.Header = e.Row.GetIndex() + 1;
        }
    }
}
