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

namespace EIMS_Login
{
    /// <summary>
    /// ConfidentialMembers_1.xaml 的交互逻辑
    /// </summary>
    public partial class ConfidentialMembers_LoanApplication : UserControl
    {

        Connection Temp = new Connection();
        string ApplyTableSql = "select * from ApplyData";
        public int selecttime = 0;
        public ConfidentialMembers_LoanApplication()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            string[] Str = { "申请编号", "人员编号", "人员姓名", "工作岗位",  "申请日期", "申请资料编号",
                "申请数量", "申请原因", "操作状态" };
            ExportExcel(ApplyTableSql, Str, "装备申请表格.xlsx");
        }
        private void Transferapplicationdatagrid_Loaded(object sender, RoutedEventArgs e)
        {
            DataGrid.connectatabase(stateType.SelectedIndex);
            ApplyTotal.Content = DataGrid.Application.Items.Count;
        }

        private void stateType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (selecttime != 0)
            {
                DataGrid.connectatabase(stateType.SelectedIndex);
                ApplyTotal.Content = stateType.SelectedIndex;
                ApplyTotal.Content = DataGrid.Application.Items.Count;
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

        private void Determine_Click(object sender, RoutedEventArgs e)
        {
            DataGrid.updata();
        }
    }
}
