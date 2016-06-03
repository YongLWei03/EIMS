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
    /// DepartmentStaff.xaml 的交互逻辑
    /// </summary>
    public partial class DepartmentStaff : UserControl
    {
        public DepartmentStaff()
        {
            InitializeComponent();
            InitDepartmentTable();
            InitStaffTable();
        }
        private void InitDepartmentTable()
        {
            DepartmentTable.InitTableHeightWidth(420, 440);
            DepartmentTable.SetCanUserAddRows(false);
            DepartmentTable.AddColumns("DepId", "部门编号", 100);
            DepartmentTable.AddColumns("DepName", "名称", 100);
            DepartmentTable.AddColumns("Describes", "职能描述", 100);
            DepartmentTable.AddColumns("UpperId", "上级部门编号", 140);
        }
        private void InitStaffTable()
        {
            StaffTable.InitTableHeightWidth(420, 440);
            StaffTable.SetCanUserAddRows(false);
            StaffTable.AddColumns("RyId", "人员编号", 100);
            StaffTable.AddColumns("RyName", "姓名", 100);
            StaffTable.AddColumns("Sex", "性别", 100);
            StaffTable.AddColumns("Dep_Id", "所在部门编号", 140);
        }
    }
}
