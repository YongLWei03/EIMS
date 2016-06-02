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
        public Data()
        {
            InitializeComponent();
            InitDataTypeTable();
            InitDataInfTable();
        }
        private void InitDataTypeTable()
        {
            DataTypeTable.InitTableHeightWidth(420, 300);
            DataTypeTable.SetCanUserAddRows(false);
            DataTypeTable.AddColumns("TypeId", "类型编号", 100);
            DataTypeTable.AddColumns("TypeName", "类型名称", 100);
            DataTypeTable.AddColumns("Flag", "标记", 100);
        }
        private void InitDataInfTable()
        {
            DataInfTable.InitTableHeightWidth(420, 500);
            DataInfTable.SetCanUserAddRows(false);
            DataInfTable.AddColumns("DataNo", "资料编号", 100);
            DataInfTable.AddColumns("DataName", "资料名称", 100);
            DataInfTable.AddColumns("TypeId", "分类编号", 100);
            DataInfTable.AddColumns("ICount", "数量", 50);
            DataInfTable.AddColumns("IPrice", "价格", 50);
            DataInfTable.AddColumns("Memo", "创建日期", 100);
        }
    }
}
