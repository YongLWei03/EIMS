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
using System.IO;
using Microsoft.Win32;
using System.Data.SqlClient;
using System.Windows.Interop;

namespace EIMS_Login
{
    /// <summary>
    /// PersonalInformation.xaml 的交互逻辑
    /// </summary>
    public partial class PersonalInformation : UserControl
    {
        private byte[] _imageBinary;
        private string _imgLocalPath;
        public EIMS_Login.Ordinaryusers.OrdinaryUserInfo NowUser = new Ordinaryusers.OrdinaryUserInfo();
        Connection TempConn = new Connection();

        struct TextStatus
        {
            public bool sId_Card;
            public bool sNationalty;
            public bool sBirth;
            public bool sPosition;
            public bool sRank;
            public bool sPolitical_Party;
            public bool sCulture_Level;
            public bool sMarital_Condition;
            public bool sFamily_Place;
            public bool sTitle;
            public bool sUpperId;
            public TextStatus(bool Status)
            {
                sId_Card = Status;
                sNationalty = Status;
                sBirth = Status;
                sPosition = Status;
                sRank = Status;
                sPolitical_Party = Status;
                sCulture_Level = Status;
                sMarital_Condition = Status;
                sFamily_Place = Status;
                sTitle = Status;
                sUpperId = Status;
            }
        };

        TextStatus TempStatus = new TextStatus(false);
        TextStatus TS = new TextStatus(false);


        public PersonalInformation()
        {
            
            InitializeComponent();
            ReSet();
        }

        private void SelectImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "*jpg|*.JPG|*.GIF|*.GIF|*.BMP|*.BMP|*.png|*.PNG";
            dialog.Multiselect = false;

            if (dialog.ShowDialog() == true)
            {
                FileStream fs = new FileStream(dialog.FileName, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                this._imageBinary = br.ReadBytes((int)fs.Length);
                this._imgLocalPath = dialog.FileName;
                br.Close();
                fs.Close();
                this.MyImage.Source = ByteArrayToBitmapImage(this._imageBinary);
            }
        }
        BitmapImage ByteArrayToBitmapImage(byte[] byteArray)
        {
            BitmapImage bmp = null;

            try
            {
                bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.StreamSource = new MemoryStream(byteArray);
                bmp.EndInit();
            }
            catch
            {
                bmp = null;
            }
            return bmp;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            
            Connection Temp = new Connection();
            this.Account.Text = MainWindow.CurrentUser;
            this.Name.Text = NowUser.UserInfoTemp.RyName;
            this.Serialnumber.Text = NowUser.UserInfoTemp.Ryid;
            if(NowUser.UserInfoTemp.Sex == "男")
            {
                this.Gender.Text = "男";
            }
            else if(NowUser.UserInfoTemp.Sex == "女")
            {
                this.Gender.Text = "女";
            }
            else
            {
                this.Gender.Text = "未知";
            }

            this.Departmentnumber.Text = NowUser.UserInfoTemp.Dep_Id.ToString();
            this.IDcard.Text = NowUser.UserInfoTemp.Id_Card;
            this.National.Text = NowUser.UserInfoTemp.Nationalty;
            this.Birthday.Text = NowUser.UserInfoTemp.Birth;
            this.Position.Text = NowUser.UserInfoTemp.Title;
            this.Rank.Text = NowUser.UserInfoTemp.Rank;
            this.Politicallandscape.Text = NowUser.UserInfoTemp.Political_Party;
            this.Leveleducation.Text = NowUser.UserInfoTemp.Culture_Level;
            this.Maritalstatus.Text = NowUser.UserInfoTemp.Marital_Condition.ToString();
            this.Nativeplace.Text = NowUser.UserInfoTemp.Family_Place;
            this.Post.Text = NowUser.UserInfoTemp.Position;
            this.LeaderNumber.Text = NowUser.UserInfoTemp.UpperId;
            if(NowUser.UserInfoTemp.Photo == null)
            {
                //MessageBox.Show("没有图片");
            }
            else
            {
                this.MyImage.Source = Imaging.CreateBitmapSourceFromHBitmap(NowUser.UserInfoTemp.Photo.GetHbitmap(),IntPtr.Zero,Int32Rect.Empty,BitmapSizeOptions.FromEmptyOptions());
            }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool UpdateCon = false ;
            if (TempStatus.sId_Card)
            {
                UpdateCon = UpdateId_Card();
            }


            if (UpdateCon) MessageBox.Show("提示：修改成功！");
            ReSet();
        }
        private bool UpdateId_Card()
        {
            string SQL = "update ArmsPerson set Id_Card = '" + IDcard.Text + "' where Ryid = '" + NowUser.UserInfoTemp.Ryid + "'";
            try
            {
                SqlCommand CMD_1 = new SqlCommand(SQL, TempConn.GetConn());
                CMD_1.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误：\n\n"+ex);
                return false;
            }
            return true;
        }

        private void IDcard_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TS.sId_Card)
            {
                TempStatus.sId_Card = true;
                label_1.Foreground = Brushes.Red;
            }
            TS.sId_Card = true;
        }
        public void ReSet()
        {
            TempStatus = new TextStatus(false);
            TS = new TextStatus(false);
            label_1.Foreground = Brushes.Black;
        }
    }
}
