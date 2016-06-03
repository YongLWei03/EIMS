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

namespace EIMS_Login.SystemAdministrator.SystemAdministrator_AllInformationView_Children
{
    /// <summary>
    /// Data.xaml 的交互逻辑
    /// </summary>
    public partial class Data : UserControl
    {
        MoreInf DaTymoinf;
        MoreInf DaInmoinf;
        InfTotal infnum = new InfTotal();
        string[] DataTypeStr = { "类型编号", "类型名称", "标记"};
        string[] DataInfStr = { "资料编号", "资料名称","资料分类编号","数量","价格","备注","创建日期"};
        public Data()
        {
            InitializeComponent();
            InitDataTypeTable();
            InitDataInfTable();
            DataTypeTable.DataTableSelect("select * from Types", "更新");
            InfType.Content = infnum.InfTotalSet("Types");
            DataInfTable.DataTableSelect("select * from ArmsData", "更新");
            Inf.Content = infnum.InfTotalSet("ArmsData");
            InitRightBm();
        }
        private void InitDataTypeTable()
        {
            DataTypeTable.InitTableHeightWidth(420, 318);
            DataTypeTable.SetCanUserAddRows(false);
            DataTypeTable.AddColumns("TypeId", "类型编号", 100);
            DataTypeTable.AddColumns("TypeName", "类型名称", 100);
            DataTypeTable.AddColumns("Flag", "标记", 100);
        }
        private void InitDataInfTable()
        {
            DataInfTable.InitTableHeightWidth(420, 518);
            DataInfTable.SetCanUserAddRows(false);
            DataInfTable.AddColumns("DataNo", "资料编号", 100);
            DataInfTable.AddColumns("DataName", "资料名称", 100);
            DataInfTable.AddColumns("TypeId", "分类编号", 100);
            DataInfTable.AddColumns("ICount", "数量", 50);
            DataInfTable.AddColumns("IPrice", "价格", 50);
            DataInfTable.AddColumns("Memo", "创建日期", 100);
        }

        private void DataTypeExport_Click(object sender, RoutedEventArgs e)
        {
            DataTypeTable.ExportExcel("select * from Types", DataTypeStr, "资料类型表格.xlsx");
        }

        private void DataInfExport_Click(object sender, RoutedEventArgs e)
        {
            DataInfTable.ExportExcel("select * from ArmsData", DataInfStr, "资料信息表格.xlsx");
        }
        public void InitRightBm()
        {
            MenuItem DataTypeMenu = DataTypeTable.AddMenuItem("更多信息");
            MenuItem DataInfMenu = DataInfTable.AddMenuItem("更多信息");

            DataTypeMenu.Click += DataTypeEventContent;
            DataInfMenu.Click += DataInfEventContent;
            DataTypeTable.dgMenu.Items.Add(DataTypeMenu);
            DataInfTable.dgMenu.Items.Add(DataInfMenu);
        }

        public void DataTypeEventContent(object sender, RoutedEventArgs e)
        {
            DaTymoinf = new MoreInf();
            string[] Table_Str = { "TypeId", "TypeName", "Flag" };
            if (DataTypeTable.dataGrid.SelectedIndex != -1)
            {
                DaTymoinf.SetValues(DataTypeTable.Getdt(), DataTypeTable.dataGrid.SelectedIndex, DataTypeTable.Rows, Table_Str, DataTypeStr, 3);
                DaTymoinf.Show();
            }
            else MessageBox.Show("当前未选中任何行！");
        }
        public void DataInfEventContent(object sender, RoutedEventArgs e)
        {
            DaInmoinf = new MoreInf();
            string[] Table_Str = { "DataNo", "DataName", "TypeId", "ICount", "IPrice", "Memo", "GreateDate" };
            if (DataInfTable.dataGrid.SelectedIndex != -1)
            {
                DaInmoinf.SetValues(DataInfTable.Getdt(), DataInfTable.dataGrid.SelectedIndex, DataInfTable.Rows, Table_Str, DataInfStr, 7);
                DaInmoinf.Show();
            }
            else MessageBox.Show("当前未选中任何行！");
        }
    }
}
