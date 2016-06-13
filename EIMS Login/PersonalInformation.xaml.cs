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
            this.Political_Party.Text = NowUser.UserInfoTemp.Political_Party;
            this.Culture_Level.Text = NowUser.UserInfoTemp.Culture_Level;
            this.Marital_Condition.Text = NowUser.UserInfoTemp.Marital_Condition.ToString();
            this.Family_Place.Text = NowUser.UserInfoTemp.Family_Place;
            this.Post.Text = NowUser.UserInfoTemp.Position;
            this.UpperId.Text = NowUser.UserInfoTemp.UpperId;
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
                UpdateCon = UpdateDataBase("Id_Card", IDcard);
            }
            if (TempStatus.sNationalty)
            {
                UpdateCon = UpdateDataBase("Nationalty", National);
            }
            if (TempStatus.sBirth)
            {
                UpdateCon = UpdateDataBase("Birth", Birthday);
            }
            if (TempStatus.sTitle)
            {
                UpdateCon = UpdateDataBase("Title", Position);
            }
            if (TempStatus.sRank)
            {
                UpdateCon = UpdateDataBase("Rank", Rank);
            }
            if (TempStatus.sPolitical_Party)
            {
                UpdateCon = UpdateDataBase("Political_Party", Political_Party);
            }
            if (TempStatus.sCulture_Level)
            {
                UpdateCon = UpdateDataBase("Culture_Level", Culture_Level);
            }
            if (TempStatus.sMarital_Condition)
            {
                UpdateCon = UpdateDataBase("Marital_Condition", Marital_Condition);
            }
            if (TempStatus.sFamily_Place)
            {
                UpdateCon = UpdateDataBase("Family_Place", Family_Place);
            }
            if (TempStatus.sUpperId)
            {
                UpdateCon = UpdateDataBase("UpperId", UpperId);
            }
            if (TempStatus.sPosition)
            {
                UpdateCon = UpdateDataBase("Position", Post);
            }


            if (UpdateCon) MessageBox.Show("提示：修改成功！");
            ReSet();
        }

        private bool UpdateDataBase(string str, TextBox TB)
        {
            string SQL = "update ArmsPerson set " + str + " = '" + TB.Text + "' where Ryid = '" + NowUser.UserInfoTemp.Ryid + "'";
            try
            {
                SqlCommand CMD_1 = new SqlCommand(SQL, TempConn.GetConn());
                CMD_1.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("错误：\n\n" + ex);
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

        private void sNationalty_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TS.sNationalty)
            {
                TempStatus.sNationalty = true;
                label_2.Foreground = Brushes.Red;
            }
            TS.sNationalty = true;
        }

        
        private void sBirth_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TS.sBirth)
            {
                TempStatus.sBirth = true;
                label_3.Foreground = Brushes.Red;
            }
            TS.sBirth = true;
        }
        public void ReSet()
        {
            TempStatus = new TextStatus(false);
            TS = new TextStatus(false);
            label_1.Foreground = Brushes.Black;
            label_2.Foreground = Brushes.Black;
            label_3.Foreground = Brushes.Black;
            label_4.Foreground = Brushes.Black;
            label_5.Foreground = Brushes.Black;
            label_6.Foreground = Brushes.Black;
            label_7.Foreground = Brushes.Black;
            label_10.Foreground = Brushes.Black;
            label_11.Foreground = Brushes.Black;
            label_12.Foreground = Brushes.Black;
            label_13.Foreground = Brushes.Black;

        }

        private void Position_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TS.sTitle)
            {
                TempStatus.sTitle = true;
                label_4.Foreground = Brushes.Red;
            }
            TS.sTitle = true;
        }

        private void Rank_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TS.sRank)
            {
                TempStatus.sRank = true;
                label_5.Foreground = Brushes.Red;
            }
            TS.sRank = true;
        }

        private void Political_Party_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TS.sPolitical_Party)
            {
                TempStatus.sPolitical_Party = true;
                label_6.Foreground = Brushes.Red;
            }
            TS.sPolitical_Party = true;
        }

        private void Culture_Level_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TS.sCulture_Level)
            {
                TempStatus.sCulture_Level = true;
                label_7.Foreground = Brushes.Red;
            }
            TS.sCulture_Level = true;
        }

        private void Marital_Condition_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TS.sMarital_Condition)
            {
                TempStatus.sMarital_Condition = true;
                label_10.Foreground = Brushes.Red;
            }
            TS.sMarital_Condition = true;
        }

        private void Family_Place_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TS.sFamily_Place)
            {
                TempStatus.sFamily_Place = true;
                label_11.Foreground = Brushes.Red;
            }
            TS.sFamily_Place = true;
        }

        private void Post_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TS.sCulture_Level)
            {
                TempStatus.sCulture_Level = true;
                label_12.Foreground = Brushes.Red;
            }
            TS.sCulture_Level = true;
        }

        private void UpperId_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TS.sUpperId)
            {
                TempStatus.sUpperId = true;
                label_13.Foreground = Brushes.Red;
            }
            TS.sUpperId = true;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ChangePWD TempWindow = new ChangePWD();
            TempWindow.Show();
        }
    }
}
