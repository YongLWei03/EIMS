using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
    /// WarehouseManager_2.xaml 的交互逻辑
    /// </summary>
    public partial class WarehouseManager_AllocationStatus : UserControl
    {
        Connection Temp = new Connection();
        string ApplyTableSql = "select * from ArmsAllo";
        public int selecttime = 0;
        public WarehouseManager_AllocationStatus()
        {
            InitializeComponent();
        }

        private void allocationstatusdatagrid_Loaded(object sender, RoutedEventArgs e)
        {
            allocationstatusdatagrid.connectataBase(statetype.SelectedIndex);
            totalApplication.Content = allocationstatusdatagrid.Allocation.Items.Count;
        }

        private void confirmSubmission_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("    确认要提交？", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Warning, MessageBoxResult.OK) != MessageBoxResult.OK)
            {
                return;
            }
            if (statetype.SelectedIndex != 0)
            {
                MessageBox.Show("该选项下的表格无法提交");
                return;
            }
            if (allocationstatusdatagrid.Updata())
            {
                MessageBox.Show("提交成功");
            }
            allocationstatusdatagrid.connectataBase(statetype.SelectedIndex);
            totalApplication.Content = allocationstatusdatagrid.Allocation.Items.Count;
        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("即将导出表格，请你稍等。。。");
            string[] Str = { "调拨单号", "人员编号", "调出装备编号", "申请数量",  "装备单价", "调出单位",
                "调入单位", "调拨方式", "提货人", "有效期", "批准人", "承办人", "批准日期", "备注", "完成状态" };
            ExportExcel(ApplyTableSql, Str, "装备调拨表格.xlsx");
        }

        private void statetype_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (selecttime != 0)
            {
                allocationstatusdatagrid.connectataBase(statetype.SelectedIndex);
                totalApplication.Content = allocationstatusdatagrid.Allocation.Items.Count;
            }
            selecttime++;
        }

        public string ExportExcel(string SQL, string[] StrCloumns, string saveFileName)
        {
            try
            {
                DataTable Table0 = new DataTable();
                SqlDataAdapter sda = new SqlDataAdapter(SQL, Temp.GetConnStr());
                sda.Fill(Table0);
                DataSet ds = new DataSet();
                ds.Tables.Add(Table0);
                ChangeColumnName(ref ds, StrCloumns);
                if (ds == null)
                    return "数据库为空";

                bool fileSaved = false;
                Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();
                if (xlApp == null)
                {
                    return "无法创建Excel对象，可能您的机子未安装Excel";
                }
                Microsoft.Office.Interop.Excel.Workbooks workbooks = xlApp.Workbooks;
                Microsoft.Office.Interop.Excel.Workbook workbook = workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
                Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];//取得sheet1
                                                                                                                                      //写入字段
                for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                {
                    worksheet.Cells[1, i + 1] = ds.Tables[0].Columns[i].ColumnName;
                }
                //写入数值
                for (int r = 0; r < ds.Tables[0].Rows.Count; r++)
                {
                    for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
                    {
                        worksheet.Cells[r + 2, i + 1] = ds.Tables[0].Rows[r][i];
                    }
                    System.Windows.Forms.Application.DoEvents();
                }
                worksheet.Columns.EntireColumn.AutoFit();//列宽自适应。
                if (saveFileName != "")
                {
                    try
                    {
                        workbook.Saved = true;
                        workbook.SaveCopyAs(saveFileName);
                        fileSaved = true;
                    }
                    catch (Exception ex)
                    {
                        fileSaved = false;
                        MessageBox.Show("导出文件时出错,文件可能正被打开！\n" + ex.Message);
                        return "";
                    }
                }
                else
                {
                    fileSaved = false;
                }
                xlApp.Quit();
                GC.Collect();//强行销毁
                if (fileSaved && System.IO.File.Exists(saveFileName)) System.Diagnostics.Process.Start(saveFileName); //打开EXCEL
                MessageBox.Show("导出成功，默认保存在：我的电脑/文档");
                return "成功保存到Excel";
            }
            catch (Exception ex)
            {
                return ex.ToString();
            }
        }
        /*对导出的Excel文档表头进行更改
         *参数1：dataset ds；为要进行修改的数据表
         *参数2：string []StrCloumns；修改的目标表头，顺序为从左到右
         */
        public void ChangeColumnName(ref DataSet ds, string[] StrCloumns)
        {
            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
            {
                ds.Tables[0].Columns[i].ColumnName = StrCloumns[i];
            }

        }
    }
}
