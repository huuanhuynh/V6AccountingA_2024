using System;
using System.Data;
using System.IO;
using System.Net;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Init;
using V6SqlConnect;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;

namespace V6Controls.Forms.DanhMuc.Add_Edit
{
    public partial class CorpLanAddEditForm : AddEditControlVirtual
    {
        public CorpLanAddEditForm()
        {
            InitializeComponent();
            MyInit1();
        }

        private void MyInit1()
        {
            try
            {
                if (V6Setting.Language != "V")
                {
                    lblTextE.Text = "Text " + V6Setting.Language;
                    txtTextE.AccessibleName = V6Setting.Language;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteToLog(Name + " Init1 " + ex.Message, Application.ProductName);
            }
        }

        private void Form_Load(object sender, EventArgs e)
        {
            //if (TableName != V6TableName.CorpLan)
            //{
            //    txtCtype.Visible = false;
            //    lblCtype.Visible = false;
                
            //    txtTen.Visible = false;
            //    txtTen2.Visible = false;

            //    lblTen.Visible = false;
            //    lblTen2.Visible = false;

            //    txtSname.Visible = false;
            //    lblSName.Visible = false;
            //}
        }

        public override void DoBeforeAdd()
        {
            try
            {
                FixID();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".DoBeforeAdd", ex);
            }
        }

        public override void DoBeforeEdit()
        {
            try
            {
                num = ObjectAndString.ObjectToInt(txtID.Text.Right(5));
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".DoBeforeEdit", ex);
            }
        }

        //private bool must_have_name;
        public override void ValidateData()
        {
            var errors = "";
            if (txtID.Text.Trim() == "")
                errors += V6Text.Text("CHUANHAPMA");
            if (txtTextD.Text.Trim() == "")
                errors += "Chưa nhập mặc định !\r\n";
            //if (txtTextV.Text.Trim() == "")
            //    errors += "Chưa nhập TextV !\r\n";
            //if (txtTextE.Text.Trim() == "")
            //    errors += "Chưa nhập TextE !\r\n";

            //if (Mode == V6Mode.Add && txtTen.Text.Trim() == "")
            //    errors += "Chưa nhập Tên !\r\n";
            //if (Mode == V6Mode.Add && txtTen2.Text.Trim() == "")
            //    errors += "Chưa nhập Tên 2 !\r\n";

            if (Mode == V6Mode.Edit)
            {
               
            }
            else if (Mode == V6Mode.Add)
            {
                bool b = V6BusinessHelper.IsValidOneCode_Full(TableName.ToString(), 1, "ID", txtID.Text.Trim(), txtID.Text.Trim());
                if (!b)
                {
                    throw new Exception("Không được thêm mã đã tồn tại: ID = " + txtID.Text.Trim());
                }
            }

            if (errors.Length > 0) throw new Exception(errors);
        }

        public override void V6F3Execute()
        {
            txtSfile.ReadOnly = false;
            //FCRJK
            lblF.Visible = true;
            txtF.Visible = true;
            lblC.Visible = true;
            txtC.Visible = true;
            lblR.Visible = true;
            txtR.Visible = true;
            lblJ.Visible = true;
            txtJ.Visible = true;
            lblK.Visible = true;
            txtK.Visible = true;
        }

        private int num;
        private void FixID()
        {
            try
            {
                if (!IsReady) return;

                var CType = txtCtype.Text.Trim();
                var SFile = txtSfile.Text.Trim();
                
                if (Mode == V6Mode.Add)
                {
                    var sql = "Select Max(Right(Rtrim(ID),5)) as max_num From Corplan Where SFile='" + SFile
                              + "' and ctype='" + CType + "'";
                    var max_num = ObjectAndString.ObjectToInt(SqlConnect.ExecuteScalar(CommandType.Text, sql));
                    num = max_num + 1;
                }
                var id = SFile + CType + "" + ("00000" + num).Right(5);
                txtID.Text = id;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".FixID", ex);
            }
        }

        public string RequestWeather(string word, string toLanguage)
        {
            string url0 = "https://translate.google.com/#view=home&op=translate&sl=en&tl=zh-CN&text=like";
            string fromLanguage = "en";
            var ur1 = string.Format("https://translate.google.com/translate_a/single?client=webapp&sl=en&tl=zh-CN&hl=vi&dt=at&dt=bd&dt=ex&dt=ld&dt=md&dt=qca&dt=rw&dt=rm&dt=ss&dt=t&source=bh&ssel=0&tsel=0&kc=1&tk=318115.151379&q=amount", fromLanguage, toLanguage, (word));
            var ur2 = string.Format("https://translate.google.com/translate_a/single?client=webapp&sl=en&tl=zh-CN&hl=vi&dt=at&dt=bd&dt=ex&dt=ld&dt=md&dt=qca&dt=rw&dt=rm&dt=ss&dt=t&source=bh&ssel=0&tsel=0&kc=1&tk=480477.114989&q=love", fromLanguage, toLanguage, (word));
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url0);
            request.Method = "GET";

            var webResponse = request.GetResponse();
            var webStream = webResponse.GetResponseStream();
            var responseReader = new StreamReader(webStream);
            var response = responseReader.ReadToEnd();
            Console.WriteLine("Response: " + response);
            responseReader.Close();
            return response;
        }

        private void GoogleTranslate()
        {
            try
            {
                if (txtTextE.Text.Trim() == "") return;
                txtC.Text = RequestWeather(txtTextE.Text, LanguagePair.China);
                txtC.Text = TranslateText(txtTextE.Text, LanguagePair.China);
                txtF.Text = SingleTranslate(txtTextE.Text, LanguagePair.France);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".GoogleTranslate", ex);
            }
        }

        public string TranslateText(string input, string languagePair)
        {
            //string url = String.Format("http://www.google.com/translate_t?hl=en&ie=UTF8&text={0}&langpair={1}", input, languagePair);
            var url = string.Format("https://translate.google.com/translate_a/single?client=webapp&sl={0}&tl={1}&hl=vi&dt=at&dt=bd&dt=ex&dt=ld&dt=md&dt=qca&dt=rw&dt=rm&dt=ss&dt=t&source=bh&ssel=0&tsel=0&kc=1&tk=318115.151379&q={2}",
                    "en", languagePair, (input));
            WebClient webClient = new WebClient();
            webClient.Encoding = System.Text.Encoding.UTF8;
            string result = webClient.DownloadString(url);
            result = result.Substring(result.IndexOf("<span title=\"") + "<span title=\"".Length);
            result = result.Substring(result.IndexOf(">") + 1);
            result = result.Substring(0, result.IndexOf("</span>"));
            return result.Trim();
        }

        public string SingleTranslate(string word, string toLanguage)
        {
            //var toLanguage = "en"; //English
            var fromLanguage = "en"; //Deutsch
            var url =
                //String.Format("https://translate.googleapis.com/translate_a/single?client=gtx&sl={0}&tl={1}&dt=t&q={2}",
                String.Format("https://translate.google.com/translate_a/single?client=webapp&sl={0}&tl={1}&hl=vi&dt=at&dt=bd&dt=ex&dt=ld&dt=md&dt=qca&dt=rw&dt=rm&dt=ss&dt=t&source=bh&ssel=0&tsel=0&kc=1&tk=318115.151379&q={2}",
                    fromLanguage, toLanguage, (word));
            var webClient = new WebClient
            {
                Encoding = System.Text.Encoding.UTF8
            };
            var result = webClient.DownloadString(url);
            try
            {
                result = result.Substring(4, result.IndexOf("\"", 4, StringComparison.Ordinal) - 4);
                return result;
            }
            catch
            {
                return "Error";
            }
        }

        private class LanguagePair
        {
            public static string China = "zh-CN";
            public static string English = "en";
            public static string France = "FR";
        }

        private void txtCtype_TextChanged(object sender, EventArgs e)
        {
            FixID();
        }

        private void txtSfile_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F3)
                {
                    txtSfile.ReadOnly = false;
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".txtSfile_KeyDown", ex);
            }
        }

        private void txtSfile_TextChanged(object sender, EventArgs e)
        {
            FixID();
        }

        private void btnKhongDau_Click(object sender, EventArgs e)
        {
            txtTextE.Text = ChuyenMaTiengViet.ToUnSign(txtTextE.Text);
        }

        private void btnGoogleTranslate_Click(object sender, EventArgs e)
        {
            GoogleTranslate();
        }

        
    }
}
