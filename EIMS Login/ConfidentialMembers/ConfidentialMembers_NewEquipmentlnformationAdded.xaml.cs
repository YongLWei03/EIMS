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
    /// ConfidentialMembers_4.xaml 的交互逻辑
    /// </summary>
    public partial class ConfidentialMembers_EquipmentFiling : UserControl
    {
        public ConfidentialMembers_EquipmentFiling()
        {
            InitializeComponent();
            InitTable();
        }
        public void InitTable()
        {
            TableToArmsData.InitTableHeightWidth(480, 880);
            TableToArmsData.AddColumns("DataNo", "资料编号", 100);
            TableToArmsData.AddColumns("DataName", "资料名称", 100);
            TableToArmsData.AddColumns("Typeld", "资料分类编号", 120);
            TableToArmsData.AddColumns("ICount", "数量", 100);
            TableToArmsData.AddColumns("IPrice", "价格", 80);
            TableToArmsData.AddColumns("Memo", "备注", 220);
            TableToArmsData.AddColumns("GreateDate", "创建日期", 120);

        }
    }
}
