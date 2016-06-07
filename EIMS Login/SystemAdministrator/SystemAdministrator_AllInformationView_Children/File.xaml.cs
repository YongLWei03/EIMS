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

namespace EIMS_Login.SystemAdministrator.SystemAdministrator_AllInformationView_Children
{
    /// <summary>
    /// File.xaml 的交互逻辑
    /// </summary>
    public partial class File : UserControl
    {
        MoreInf Flmoinf;
        InfTotal infnum = new InfTotal();
        Connection temp = new Connection();
        string[] Str = { "归档号", "归档日期", "资料编号", "归档数量", "经办人", "审批人", "说明","审核标记" };
        public File()
        {
            InitializeComponent();
            InitFileTable();
            FileNum.Content = infnum.InfTotalSet("DataIn");
            FileTable.DataTableSelect("select * from DataIn", "更新");
            InitRightBm();
        }
        private void InitFileTable()
        {
            FileTable.InitTableHeightWidth(420, 898);
            FileTable.SetCanUserAddRows(false);
            FileTable.AddColumns("Id", "归档号", 100);
            FileTable.AddColumns("DataNo", "资料编号", 150);
            FileTable.AddColumns("InCount", "归档数量", 120);
            FileTable.AddColumns("Username", "经办人", 95);
            FileTable.AddColumns("Username", "审批人", 95);
            FileTable.AddColumns("Ryname", "归档日期", 110);
            FileTable.AddColumns("Detail", "审批状态", 80);
            FileTable.AddColumns("Flag", "备注", 130);
        }

        private void FileTableExport_Click(object sender, RoutedEventArgs e)
        {
            FileTable.ExportExcel("select * from DataIn", Str, "归档信息表格.xlsx");
        }
        public void InitRightBm()
        {
            MenuItem FileMenu = FileTable.AddMenuItem("更多信息");
            MenuItem DeletRowMenu1 = FileTable.AddMenuItem("删除选中的行");
            FileMenu.Click += FileEventContent;
            DeletRowMenu1.Click += DeletRows1;
            FileTable.dgMenu.Items.Add(FileMenu);
            FileTable.dgMenu.Items.Add(DeletRowMenu1);
        }
        public void FileEventContent(object sender, RoutedEventArgs e)
        {
            Flmoinf = new MoreInf();
            string[] Table_Str = { "Id", "InDate", "DataNo", "InCount", "Username", "Ryname", "Detail", "Flag" };
            if (FileTable.dataGrid.SelectedIndex != -1)
            {
                Flmoinf.SetValues(FileTable.Getdt(), FileTable.dataGrid.SelectedIndex, FileTable.Rows, Table_Str, Str, 8);
                Flmoinf.Show();
            }
            else MessageBox.Show("当前未选中任何行！");
        }
        public void DeletRows1(object sender, RoutedEventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = temp.GetConn();
            if (MessageBox.Show("您确定要删除吗？", "系统提示：", MessageBoxButton.OKCancel, MessageBoxImage.Warning) == MessageBoxResult.OK)
            {
                try
                {
                    for (int i = FileTable.dataGrid.SelectedItems.Count - 1; i >= 0; i--)
                    {
                        cmd.CommandText = "delete from DataIn where Id= '" + ((DataRowView)FileTable.dataGrid.SelectedItems[i]).Row["Id"].ToString() + "'";
                        cmd.ExecuteNonQuery();
                        ((DataRowView)FileTable.dataGrid.SelectedItems[i]).Delete();
                    }
                }
                catch (Exception ae)
                {
                    MessageBox.Show("删除失败！");
                }
            }

        }
    }
}
