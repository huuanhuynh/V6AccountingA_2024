using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using H;
using V6Tools;

namespace Tools
{
    public partial class FormFilterTextFiles : Form
    {
        private class MyFileInfo
        {
            public MyFileInfo(string path)
            {
                FullPath = path;
            }

            /// <summary>
            /// Lấy tất cả thông tin MyFileInfos
            /// </summary>
            private void GetAllInfos()
            {
                try
                {
                    Name = Path.GetFileName(_fullPath);
                    Text = File.ReadAllText(_fullPath);
                }
                catch (Exception)
                {
                    //
                }
            }

            // ReSharper disable once MemberCanBePrivate.Local
            public string FullPath
            {
                // ReSharper disable once UnusedMember.Local
                get { return _fullPath; }
                set
                {
                    _fullPath = value;
                    GetAllInfos();
                }
            }

            public string FoundLine { get; set; }

            private string _fullPath;
            public string Name;
            public string Text;

            public override string ToString()
            {
                return Name;
            }
        }

        // ======================================== END CLASS MY_FILE_INFO ==================================

        private class DataMemberItem
        {
            public string text;
            public int startIndex;
            public int endIndex;

            public string GetRefValue()
            {
                int index1 = text.IndexOf("Ref=\"") + 5;
                if (index1 < 5) return null;
                int index2 = text.IndexOf("\"", index1);
                string value = text.Substring(index1, index2-index1);
                return value;
            }

            public string Replace_Ref_and_DataMember(ParameterItem parameterItem)
            {
                string new_Ref = parameterItem.GetRefValue();
                int index1 = text.IndexOf("Parameter=\"#Ref-");
                if (index1 < 0) return null;
                int index2 = text.IndexOf("\" ", index1);
                string value = text.Substring(0, index1);
                value += "Parameter=\"#Ref-" + new_Ref;
                value += text.Substring(index2);

                string newText = value;

                index1 = newText.IndexOf("DataMember=\"");
                if (index1 >= 0)
                {
                    index2 = newText.IndexOf("\" ", index1);
                    value = newText.Substring(0, index1);
                    value += "DataMember=\"" + parameterItem.Name;
                    value += newText.Substring(index2);
                }

                return value;
            }
        }

        private class ParameterItem
        {
            public string Name;
            public string Text;
            public int StartIndex;
            public int EndIndex;

            public string GetRefValue()
            {
                int index1 = Text.IndexOf("Ref=\"") + 5;
                if (index1 < 5) return null;
                int index2 = Text.IndexOf("\"", index1);
                string value = Text.Substring(index1, index2 - index1);
                return value;
            }

            public string Replace_Ref_and_DataMember(string newParameterRefValue)
            {
                int index1 = Text.IndexOf("Parameter=\"#Ref-");
                if (index1 < 0) return null;
                int index2 = Text.IndexOf("\" ", index1);
                string value = Text.Substring(0, index1);
                value += "Parameter=\"#Ref-" + newParameterRefValue;
                value += Text.Substring(index2);
                return value;
            }
        }

        // ======================================== DATAMEMBER ITEM ==================================

        public FormFilterTextFiles()
        {
            InitializeComponent();
        }

        Setting Setting = new Setting("Setting.ini");
        private readonly FolderBrowserDialog f = new FolderBrowserDialog();

        private void GetFolder()
        {
            try
            {
                if (Directory.Exists(txtFolder.Text)) f.SelectedPath = txtFolder.Text;

                if (f.ShowDialog(this) == DialogResult.OK)
                {
                    txtFolder.Text = f.SelectedPath;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteExLog(GetType() + ".GetFolder", ex, "");
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadFiles(bool subFolder)
        {
            try
            {
                var files = Directory.GetFiles(txtFolder.Text, txtExt.Text == "" ? "*.*" : "*." + txtExt.Text,
                    subFolder ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);

                List<MyFileInfo> listFileInfo = new List<MyFileInfo>();
                foreach (string path in files)
                {
                    var info = new MyFileInfo(path);
                    listFileInfo.Add(info);
                }

                listBox1.DataSource = listFileInfo;
            }
            catch (Exception ex)
            {
                Logger.WriteExLog(GetType() + ".LoadFiles", ex, "");
                MessageBox.Show(ex.Message);
            }
        }

        private void Filter()
        {
            try
            {
                listBox2.Items.Clear();
                foreach (object item in listBox1.Items)
                {
                    var myFileInfo = item as MyFileInfo;
                    if (myFileInfo != null)
                    {
                        if (Check(myFileInfo))
                        {
                            listBox2.Items.Add(item);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteExLog(GetType() + ".Filter", ex, "");
                MessageBox.Show(ex.Message);
            }
        }

        private int count;
        private bool running;
        private string error = "";
        private string append_text = "";

        private void Repx_VtoE()
        {
            try
            {
                parameterMappings = parameterMappings_V_to_E;
                replaces = replaces_V_to_E;
                foreach (object item in listBox1.Items)
                {
                    var myFileInfo = item as MyFileInfo;
                    if (myFileInfo != null)
                    {
                        Repx_ParameterMapping_and_Replace(myFileInfo);
                        count++;
                        append_text += "\nHoàn thành " + count + ": " + myFileInfo.FullPath;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteExLog(GetType() + ".Filter", ex, "");
                MessageBox.Show(ex.Message);
            }

            running = false;
        }

        private void Repx_FormatFix()
        {
            try
            {
                replaces = replaces_FormatFix;
                foreach (object item in listBox1.Items)
                {
                    var myFileInfo = item as MyFileInfo;
                    if (myFileInfo != null)
                    {
                        Repx_ParameterMapping_and_Replace(myFileInfo);
                        count++;
                        append_text += "\nHoàn thành " + count + ": " + myFileInfo.FullPath;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteExLog(GetType() + ".Filter", ex, "");
                MessageBox.Show(ex.Message);
            }

            running = false;
        }

        private void Repx_ParameterMapping_and_Replace(MyFileInfo myFileInfo)
        {
            try
            {
                myFileInfo.Text = File.ReadAllText(myFileInfo.FullPath);
                // Gán Ref Parameter hoặc Data.\
                foreach (string[] replace in parameterMappings)
                {
                    myFileInfo.Text = ChangeParameterReference(myFileInfo.Text, replace[0], replace[1]);
                }

                // Thay thế text.
                foreach (string[] replace in replaces)
                {
                    myFileInfo.Text = myFileInfo.Text.Replace(replace[0], replace[1]);
                }

                File.WriteAllText(myFileInfo.FullPath, myFileInfo.Text);
            }
            catch (Exception ex)
            {
                error += "\n" + myFileInfo.FullPath + "\n" + ex.Message;
            }
        }

        private string ChangeParameterReference(string text, string oldParameterName, string newParameterName)
        {
            var item = FindDataMemberItem(text, oldParameterName);
            var parameterItem = FindParameterItem(text, newParameterName);
            text = ReplaceParameterReference(text, item, parameterItem);
            return text;
        }

        private string ReplaceParameterReference(string text, DataMemberItem item, ParameterItem parameterItem)
        {
            if (item == null || parameterItem == null) return text;

            string begin = text.Substring(0, item.startIndex);
            string end = text.Substring(item.endIndex);
            string newItemText = item.Replace_Ref_and_DataMember(parameterItem);
            return begin + newItemText + end;
        }

        private DataMemberItem FindDataMemberItem(string text, string name)
        {
            int index1 = text.IndexOf("DataMember=\"" + name + "\"", StringComparison.InvariantCultureIgnoreCase);
            if (index1 < 0) return null;
            int index2 = text.IndexOf("/>", index1, StringComparison.InvariantCulture) + 2;
            int temp = index1 - 10;
            int index0 = text.IndexOf("<Item", index1);
            while (index0 > index1 && temp > 0)
            {
                temp = temp - 10;
                index0 = text.IndexOf("<Item", temp);
            }

            string itemText = text.Substring(index0, index2 - index0);
            DataMemberItem item = new DataMemberItem()
            {
                text = itemText,
                startIndex = index0,
                endIndex = index2
            };
            return item;
        }

        private ParameterItem FindParameterItem(string text, string name)
        {
            int index0 = text.IndexOf("<Parameters>");
            int index2 = text.IndexOf("</Parameters>");
            if (index0 < 0 || index2 < 0)
            {
                throw new Exception("No <Parameters>");
                return null;
            }
            int index1 = text.IndexOf("Name=\"" + name + "\"", index0, StringComparison.InvariantCultureIgnoreCase);
            if (index1 < 0 || index1 > index2)
            {
                return null;
            }
            int index1_1 = text.IndexOf("/>", index1) + 2;
            int temp = index1 - 10;
            int index1_0 = text.IndexOf("<Item", temp);
            while (index1_0 > index1 && temp > 0)
            {
                temp -= 10;
                index1_0 = text.IndexOf("<Item", temp);
            }

            string itemText = text.Substring(index1_0, index1_1 - index1_0);
            ParameterItem item = new ParameterItem()
            {
                Name = name,
                Text = itemText,
                StartIndex = index1_0,
                EndIndex = index1_1
            };
            return item;
        }

        private string[][] parameterMappings = { };
        private string[][] replaces = { };

        private string[][] parameterMappings_V_to_E =
        {
            // COMPANY INFOR
            new[] {"DataTable2.M_TEN_TCTY", "M_TEN_TCTY2"},
            new[] {"M_TEN_TCTY", "M_TEN_TCTY2"},
            new[] {"DataTable2.M_TEN_CTY", "M_TEN_CTY2"},
            new[] {"M_TEN_CTY", "M_TEN_CTY2"},
            new[] {"DataTable2.M_DIA_CHI", "M_DIA_CHI2"},
            new[] {"M_DIA_CHI", "M_DIA_CHI2"},

            new[] {"DataTable2.M_SO_QD_CDKT", "M_SO_QD_CDKT2"},
            new[] {"M_SO_QD_CDKT", "M_SO_QD_CDKT2"},
            new[] {"DataTable2.M_NGAY_QD_CDKT", "M_NGAY_QD_CDKT2"},
            new[] {"M_NGAY_QD_CDKT", "M_NGAY_QD_CDKT2"},
            
            // REPORT FOOTER
            new[] {"DataTable2.M_TEN_GOI_KTT", "M_TEN_GOI_KTT"},
            new[] {"M_TEN_GOI_KTT", "M_TEN_GOI_KTT2"},
            new[] {"DataTable2.M_TEN_KTT", "M_TEN_KTT2"},
            new[] {"M_TEN_KTT", "M_TEN_KTT2"},
            new[] {"DataTable2.M_TEN_NLB", "M_TEN_NLB2"},
            new[] {"M_TEN_NLB", "M_TEN_NLB2"},
            new[] {"DataTable2.M_TEN_GOI_GD", "M_TEN_GOI_GD"},
            new[] {"M_TEN_GOI_GD", "M_TEN_GOI_GD2"},
            new[] {"DataTable2.M_TEN_GD", "M_TEN_GD2"},
            new[] {"M_TEN_GD", "M_TEN_GD2"},

        };

        /// <summary>
        /// Dữ liệu thay thế Repx V to E.
        /// </summary>
        string[][] replaces_V_to_E =
        {
            
            new[] {"Expression=\"'Tài khoản: ' + Trim([tk]) + ' - ' + [RTen_tk]\"", "Expression=\"'Account: ' + Trim([tk]) + ' - ' + [RTen_tk2]\""},
            new[] {"Expression=\"'Tài khoản: ' + Trim([RTK]) + ' - ' + [RTen_tk]\"", "Expression=\"'Account: ' + Trim([RTK]) + ' - ' + [RTen_tk2]\""},
            new[] {"Expression=\"'Tài khoản: ' + trim([RTK]) + ' - ' + [RTen_tk]\"", "Expression=\"'Account: ' + Trim([RTK]) + ' - ' + [RTen_tk2]\""},
            new[] {"Name=\"F2_TUNGAY_DENNGAY\" Expression=\"'Từ ngày ' + ToString([Rngay_ct1], 'dd/MM/yyyy')&#xA;+ ' đến ngày ' + ToString([Rngay_ct2], 'dd/MM/yyyy')\"",
                "Name=\"F2_TUNGAY_DENNGAY\" Expression=\"'From ' + ToString([Rngay_ct1], 'MMMM dd, yyyy')&#xA;+ ' to ' + ToString([Rngay_ct2], 'MMMM dd, yyyy')\""},

            //PAGEHEADER:
            
            new[] {"Text=\"STT\" TextAlignment", "Text=\"No.\" TextAlignment"},
            new[] {"Text=\"ĐVT\" TextAlignment", "Text=\"UNIT\" TextAlignment"},
            new[] {"Text=\"MÃ\" TextAlignment", "Text=\"CODE\" TextAlignment"},
            new[] {"Text=\"MÃ HÀNG\" TextAlignment", "Text=\"PRODUCT CODE\" TextAlignment"},
            new[] {"Text=\"MÃ VẬT TƯ\" TextAlignment", "Text=\"PRODUCT CODE\" TextAlignment"},
            new[] {"Text=\"TÊN\" TextAlignment", "Text=\"NAME\" TextAlignment"},
            new[] {"Text=\"TÊN VẬT TƯ\" TextAlignment", "Text=\"PRODUCT NAME\" TextAlignment"},
            new[] {"Text=\"TÊN HÀNG\" TextAlignment", "Text=\"PRODUCT NAME\" TextAlignment"},
            new[] {"Text=\"MÃ KHÁCH\" TextAlignment", "Text=\"CUSTOMER CODE\" TextAlignment"},
            new[] {"Text=\"TÊN KHÁCH\" TextAlignment", "Text=\"CUSTOMER NAME\" TextAlignment"},
            new[] {"Text=\"TÊN KHÁCH HÀNG\" TextAlignment", "Text=\"CUSTOMER NAME\" TextAlignment"},
            new[] {"Text=\"TÊN TÀI KHOẢN\" TextAlignment", "Text=\"ACCOUNT NAME\" TextAlignment"},
            new[] {"Text=\"MÃ KHO\" TextAlignment", "Text=\"WAREHOUSE\" TextAlignment"},
            new[] {"Text=\"TÊN KHO\" TextAlignment", "Text=\"WAREHOUSE NAME\" TextAlignment"},

            new[] {"Text=\"CHỨNG TỪ\" TextAlignment", "Text=\"VOUCHER\" TextAlignment"},
            new[] {"Text=\"NGÀY CT\" TextAlignment", "Text=\"DATE\" TextAlignment"},
            new[] {"Text=\"SỐ CT\" TextAlignment", "Text=\"NUMBER\" TextAlignment"},

            new[] {"Text=\"SỐ LƯỢNG\" TextAlignment", "Text=\"QUANTITY\" TextAlignment"},
            new[] {"Text=\"SỐ LƯỢNG 1\" TextAlignment", "Text=\"QUANTITY 1\" TextAlignment"},
            new[] {"Text=\"SỐ LƯỢNG 2\" TextAlignment", "Text=\"QUANTITY 2\" TextAlignment"},
            new[] {"Text=\"ĐƠN GIÁ\" TextAlignment", "Text=\"UNIT PRICE\" TextAlignment"},
            new[] {"Text=\"GIÁ BÁN\" TextAlignment", "Text=\"SALES PRICE\" TextAlignment"},
            new[] {"Text=\"GIÁ VỐN\" TextAlignment", "Text=\"COST PRICE\" TextAlignment"},
            new[] {"Text=\"THÀNH TIỀN\" TextAlignment", "Text=\"AMOUNT\" TextAlignment"},
            new[] {"Text=\"THÀNH TIỀN 1\" TextAlignment", "Text=\"AMOUNT 1\" TextAlignment"},
            new[] {"Text=\"THÀNH TIỀN 2\" TextAlignment", "Text=\"AMOUNT 2\" TextAlignment"},
            new[] {"Text=\"TIỀN HÀNG\" TextAlignment", "Text=\"AMOUNT\" TextAlignment"},
            new[] {"Text=\"THÀNH&#xD;&#xA; TIỀN\" TextAlignment", "Text=\"AMOUNT\" TextAlignment"},
            new[] {"Text=\"THANH TOÁN\" TextAlignment", "Text=\"TOTAL AMT.\" TextAlignment"},
            new[] {"Text=\"THANH&#xD;&#xA; TOÁN\" TextAlignment", "Text=\"TOTAL AMOUNT\" TextAlignment"},
            new[] {"Text=\"THUẾ\" TextAlignment", "Text=\"TAX AMOUNT\" TextAlignment"},
            
            new[] {"Text=\"NHÓM 1\" TextAlignment", "Text=\"GROUP 1\" TextAlignment"},
            new[] {"Text=\"NHÓM 2\" TextAlignment", "Text=\"GROUP 2\" TextAlignment"},
            new[] {"Text=\"NHÓM 3\" TextAlignment", "Text=\"GROUP 3\" TextAlignment"},
            new[] {"Text=\"NHÓM 4\" TextAlignment", "Text=\"GROUP 4\" TextAlignment"},
            new[] {"Text=\"NHÓM 5\" TextAlignment", "Text=\"GROUP 5\" TextAlignment"},
            new[] {"Text=\"NHÓM 6\" TextAlignment", "Text=\"GROUP 6\" TextAlignment"},

            new[] {"Text=\"CK+GG\" TextAlignment", "Text=\"DISCOUNT\" TextAlignment"},
            new[] {"Text=\"CK + GG\" TextAlignment", "Text=\"DISCOUNT\" TextAlignment"},
            new[] {"Text=\"CK,GG\" TextAlignment", "Text=\"DISCOUNT\" TextAlignment"},
            new[] {"Text=\"CK, GG\" TextAlignment", "Text=\"DISCOUNT\" TextAlignment"},

            new[] {"Text=\"TK Đ/Ứ\" TextAlignment", "Text=\"CRSP ACCT\" TextAlignment"},
            new[] {"Text=\"TK Đ.Ứ\" TextAlignment", "Text=\"CRSP ACCT\" TextAlignment"},
            //new[] {"Text=\"REF ACCOUNT\" TextAlignment", "Text=\"CRSP ACCT\" TextAlignment"},
            new[] {"Text=\"TK ĐỐI ỨNG\" TextAlignment", "Text=\"CRSP ACCT\" TextAlignment"}, // corresponding ACCOUNT
            new[] {"Text=\"TK ĐỐI ỨNG\" TextAlignment", "Text=\"CRSP ACCT\" TextAlignment"}, // corresponding ACCOUNT
            new[] {"Text=\"TÀI KHOẢN ĐỐI ỨNG\" TextAlignment", "Text=\"CORRESPONDING ACCOUNT\" TextAlignment"}, // CORRESPONDING ACCOUNT
            //new[] {"Text=\"CRSP ACCOUNT\" TextAlignment", "Text=\"CRSP ACCT\" TextAlignment"}, // CORRESPONDING ACCOUNT
            new[] {"Text=\"Diễn giải\" TextAlignment", "Text=\"Description\" TextAlignment"},
            new[] {"Text=\"DIỄN GIẢI\" TextAlignment", "Text=\"DESCRIPTION\" TextAlignment"},
            new[] {"Text=\"SỐ PHÁT SINH\" TextAlignment", "Text=\"AMOUNT\" TextAlignment"},

            new[] {"Text=\"PHÁT SINH NỢ\" TextAlignment", "Text=\"DEBIT AMMOUNT\" TextAlignment"},
            new[] {"Text=\"PHÁT SINH CÓ\" TextAlignment", "Text=\"CREDIT AMMOUNT\" TextAlignment"},
            new[] {"Text=\"PS NỢ\" TextAlignment", "Text=\"DR AMT\" TextAlignment"},
            new[] {"Text=\"PS CÓ\" TextAlignment", "Text=\"CR AMT\" TextAlignment"},
            
            new[] {"Text=\"CHI\" TextAlignment", "Text=\"CREDIT\" TextAlignment"},
            new[] {"Text=\"THU\" TextAlignment", "Text=\"DEBIT\" TextAlignment"},
            new[] {"Text=\"SỐ HIỆU CHỨNG TỪ\" TextAlignment", "Text=\"VOUCHER\" TextAlignment"},
            new[] {"Text=\"TÊN KHÁCH HÀNG\" TextAlignment", "Text=\"CUSTOMER\" TextAlignment"},


            // ============= DATA FIELDS ====================
            new[] {"PropertyName=\"Text\" DataMember=\"DataTable1.ten_kh\" />", "PropertyName=\"Text\" DataMember=\"DataTable1.ten_kh2\" />"},
            // ============= REPORT FOOTER ==================

            new[] {"Format=\"Trang {0}/{1}\" TextAlignment", "Format=\"Page {0}/{1}\" TextAlignment"},
            new[] {"Format=\"Trang {0}/{1}\" TextAlignment", "Format=\"Page {0}/{1}\" TextAlignment"},


            

            new[] {"Từ ngày ' + ToString(", "From ' + ToString("},
            new[] {"đến ngày ' + ToString(", "to ' + ToString("},
            new[] {"ToString([Rngay_ct1], 'dd/MM/yyyy')", "ToString([Rngay_ct1], 'MMMM dd, yyyy')"},
            new[] {"ToString([Rngay_ct2], 'dd/MM/yyyy')", "ToString([Rngay_ct2], 'MMMM dd, yyyy')"},
            new[] {"Text=\"TỔNG CỘNG\"", "Text=\"TOTAL:\""},
            new[] {"Text=\"TỔNG CỘNG:\"", "Text=\"TOTAL:\""},
            new[] {"Text=\"TỔNG CỘNG: \"", "Text=\"TOTAL:\""},
            new[] {"Text=\"Tổng cộng\"", "Text=\"TOTAL:\""},
            new[] {"Text=\"Tổng cộng:\"", "Text=\"TOTAL:\""},
            new[] {"Text=\"Tổng cộng: \"", "Text=\"TOTAL:\""},

            new[] {"Format=\"- Sổ này có {1} trang đánh số từ trang số 1 đến trang số {1}\"", "Format=\"- This book included {1} page(s), from 1 to {1}\""},

            new[] {"Text=\"NGƯỜI LẬP BIỂU\"", "Text=\"PREPARED BY\""},
            new[] {"Text=\"(Ký, ghi rõ họ, tên)\"", "Text=\"(Signature, full name)\""},
            new[] {"Text=\"(Ký, ghi rõ họ tên)\"", "Text=\"(Signature, full name)\""},

            new[] {"Text=\"- Ngày mở sổ: \"", "Text=\"- Opening date of book: \""},
            new[] {"Text=\"- Ngày mở sổ: \"", "Text=\"(Signature, full name)\""},
            
        };

        private string[][] replaces_FormatFix =
        {
            new[] {"ControlType=\"TopMarginBand\" Name=\"TopMargin\" HeightF=\"28\"", "ControlType=\"TopMarginBand\" Name=\"TopMargin\" HeightF=\"20\""},
            new[] {"ControlType=\"TopMarginBand\" Name=\"TopMargin\" HeightF=\"32\"", "ControlType=\"TopMarginBand\" Name=\"TopMargin\" HeightF=\"20\""},
            new[] {"ControlType=\"BottomMarginBand\" Name=\"BottomMargin\" HeightF=\"17\"", "ControlType=\"BottomMarginBand\" Name=\"BottomMargin\" HeightF=\"10\""},
            new[] {"ControlType=\"BottomMarginBand\" Name=\"BottomMargin\" HeightF=\"27\"", "ControlType=\"BottomMarginBand\" Name=\"BottomMargin\" HeightF=\"10\""},
        };

        private bool Check(MyFileInfo myFileInfo)
        {
            var o = chkCase.Checked ? StringComparison.Ordinal : StringComparison.InvariantCultureIgnoreCase;
            var fileText = myFileInfo.Text;

            bool useHead = false, useTail = false;
            string headText = "", tailText = "";
            var searchText = txt11.Text;
            if (searchText.StartsWith("[A]"))
            {
                useHead = true;
                searchText = searchText.Substring(3);
            }
            if (searchText.EndsWith("[B]"))
            {
                useTail = true;
                searchText = searchText.Substring(0, searchText.Length - 3);
            }

            if (searchText != "" &&
                (fileText.IndexOf(searchText, o) < 0 ||
                 (chkx211.Checked && fileText.IndexOf(searchText, o) == fileText.LastIndexOf(searchText, o))))
            {
                return false;
            }
            else
            {
                int searchTextIndex0 = 0;
                while (searchTextIndex0 > -1)
                {
                    searchTextIndex0 = fileText.IndexOf(searchText, searchTextIndex0 + searchText.Length, o);

                    // Lấy head
                    if (useHead)
                    {
                        headText = "";
                        int searchTextIndexA = searchTextIndex0 - 1;
                        while (searchTextIndexA > 0 && fileText[searchTextIndexA] != '\n')
                        {
                            headText = fileText[searchTextIndexA] + headText;
                            searchTextIndexA--;
                        }
                        headText = headText.Trim();
                    }

                    if (useTail)
                    {
                        tailText = "";
                        int searchTextIndexB = searchTextIndex0 + searchText.Length;
                        while (fileText[searchTextIndexB] != '\n')
                        {
                            tailText = tailText + fileText[searchTextIndexB];
                            searchTextIndexB++;
                        }
                        tailText = tailText.Trim();
                    }

                    //Check headText and tailText
                    if (useHead)
                    {
                        if (txtAContains.Text != "")
                        {
                            if (!headText.Contains(txtAContains.Text)) continue;
                        }
                        if (txtAContains2.Text != "")
                        {
                            if (!headText.Contains(txtAContains2.Text)) continue;
                        }
                        if (txtAContains3.Text != "")
                        {
                            if (!headText.Contains(txtAContains3.Text)) continue;
                        }

                        if (txtANoContains.Text != "")
                        {
                            if (headText.Contains(txtANoContains.Text)) continue;
                        }
                        if (txtANoContains2.Text != "")
                        {
                            if (headText.Contains(txtANoContains2.Text)) continue;
                        }
                        if (txtANoContains3.Text != "")
                        {
                            if (headText.Contains(txtANoContains3.Text)) continue;
                        }
                    }

                    if (useTail)
                    {
                        if (txtBContains.Text != "")
                        {
                            if (!tailText.Contains(txtBContains.Text)) continue;
                        }
                        if (txtBContains2.Text != "")
                        {
                            if (!tailText.Contains(txtBContains2.Text)) continue;
                        }
                        if (txtBContains3.Text != "")
                        {
                            if (!tailText.Contains(txtBContains3.Text)) continue;
                        }

                        if (txtBNoContains.Text != "")
                        {
                            if (tailText.Contains(txtBNoContains.Text)) continue;
                        }
                        if (txtBNoContains2.Text != "")
                        {
                            if (tailText.Contains(txtBNoContains2.Text)) continue;
                        }
                        if (txtBNoContains3.Text != "")
                        {
                            if (tailText.Contains(txtBNoContains3.Text)) continue;
                        }
                    }

                    try
                    {
                        // Lấy dòng
                        int endLineIndex = fileText.IndexOf("\n", searchTextIndex0, o);
                        string line = headText +
                                      fileText.Substring(searchTextIndex0, endLineIndex - searchTextIndex0 - 1);
                        myFileInfo.FoundLine = line;
                    }
                    catch (Exception ex)
                    {
                        ;
                    }


                    if (txt12.Text != "" &&
                        (fileText.IndexOf(txt12.Text, o) < 0 ||
                         (chkx212.Checked && fileText.IndexOf(txt12.Text, o) == fileText.LastIndexOf(txt12.Text, o))))
                        continue;
                    if (txt13.Text != "" &&
                        (fileText.IndexOf(txt13.Text, o) < 0 ||
                         (chkx213.Checked && fileText.IndexOf(txt13.Text, o) == fileText.LastIndexOf(txt13.Text, o))))
                        continue;

                    searchText = txt01.Text;
                    if (useHead) searchText = searchText.Replace("[A]", headText);
                    if (useTail) searchText = searchText.Replace("[B]", tailText);

                    // Kiểm tra không chứa, nếu có chứa là sai.
                    if (txt01.Text != "" && (fileText.IndexOf(searchText, o) >= 0)) continue;
                    if (txt02.Text != "" && (fileText.IndexOf(txt02.Text, o) >= 0)) continue;
                    if (txt03.Text != "" && (fileText.IndexOf(txt03.Text, o) >= 0)) continue;
                    //myFileInfo.FoundInfos = fileText.IndexOf()
                    // All pass;
                    return true;
                }
                return false;
            }
        }

        private void FormFilterTextFiles_Load(object sender, EventArgs e)
        {
            try
            {
                Setting._autoSave = true;
                txtExt.Leave += txtExt_Leave;
                foreach (Control control in this.Controls)
                {
                    string s = Setting.GetSetting(control.Name);
                    if (s != "") control.Text = s;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnFolder_Click(object sender, EventArgs e)
        {
            GetFolder();
            LoadFiles(chkSubFolder.Checked);
            txtFolder_Leave(txtFolder, new EventArgs());
        }

        private void txtExt_Leave(object sender, EventArgs e)
        {
            LoadFiles(chkSubFolder.Checked);
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            Filter();
            txtReplace.Text = txt11.Text;
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            MessageBox.Show("no code");
        }

        private int padLength = 80;
        private void btnRepxVtoE_1_Click(object sender, EventArgs e)
        {
            try
            {
                richView.Clear();
                richView.AppendText("\n ===== PARAMETERS MAPPING =====");
                foreach (string[] replace in parameterMappings_V_to_E)
                {
                    richView.AppendText(replace[0].PadRight(padLength < replace[0].Length ? 0 : padLength - replace[0].Length) + " => " + replace[1]);
                    richView.AppendText("\n");
                }
                richView.AppendText("\n ===== REPLACES MAPPING =====");
                foreach (string[] replace in replaces_V_to_E)
                {
                    richView.AppendText(replace[0].PadRight(padLength < replace[0].Length ? 0 : padLength - replace[0].Length) + " => " + replace[1]);
                    richView.AppendText("\n");
                }
                richView.SelectionStart = richView.TextLength;
                richView.ScrollToCaret();

                var myFileInfo = listBox1.SelectedItem as MyFileInfo;
                if (myFileInfo != null)
                {
                    parameterMappings = parameterMappings_V_to_E;
                    replaces = replaces_V_to_E;
                    Repx_ParameterMapping_and_Replace(myFileInfo);
                    count++;
                    richView.AppendText("\nHoàn thành " + count + ": " + myFileInfo.FullPath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        

        private void btnRepxVtoE_All_Click(object sender, EventArgs e)
        {
            try
            {
                richView.Clear();
                richView.AppendText("\n ===== PARAMETERS MAPPING =====");
                foreach (string[] replace in parameterMappings_V_to_E)
                {
                    richView.AppendText(replace[0].PadRight(padLength < replace[0].Length ? 0 : padLength - replace[0].Length) + " => " + replace[1]);
                    richView.AppendText("\n");
                }
                richView.AppendText("\n ===== REPLACES MAPPING =====");
                foreach (string[] replace in replaces_V_to_E)
                {
                    richView.AppendText(replace[0].PadRight(padLength < replace[0].Length ? 0 : padLength - replace[0].Length) + " => " + replace[1]);
                    richView.AppendText("\n");
                }
                richView.SelectionStart = richView.TextLength;
                richView.ScrollToCaret();

                running = true;
                count = 0;
                error = "";
                Thread thread = new Thread(Repx_VtoE);
                thread.IsBackground = true;
                thread.Start();
                timer1.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRepxFormatFix_Click(object sender, EventArgs e)
        {
            try
            {
                richView.Clear();
                richView.AppendText("\n ===== REPLACES MAPPING =====");
                foreach (string[] replace in replaces_FormatFix)
                {
                    richView.AppendText(replace[0].PadRight(padLength < replace[0].Length ? 0 : padLength - replace[0].Length) + " => " + replace[1]);
                    richView.AppendText("\n");
                }
                richView.SelectionStart = richView.TextLength;
                richView.ScrollToCaret();

                running = true;
                count = 0;
                error = "";
                Thread thread = new Thread(Repx_FormatFix);
                thread.IsBackground = true;
                thread.Start();
                timer1.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (running)
            {
                int percent = count * 100 / listBox1.Items.Count;
                if (percent == 0 && count > 0) percent = 1;
                lbl100Percent.Text = percent + "%";
                lbl100Percent.Width = btnRepxVtoE_All.Width * percent / 100;
            }
            else
            {
                ((System.Windows.Forms.Timer)sender).Stop();
                lbl100Percent.Text = "100%";
                lbl100Percent.Width = btnRepxVtoE_All.Width;
                if (!string.IsNullOrEmpty(error))
                {
                    MessageBox.Show(error);
                    richView.AppendText(error);
                }
                else
                {
                    MessageBox.Show("Hoàn thành.");
                }
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (listBox2.SelectedItem != null)
                {
                    MyFileInfo fi = (MyFileInfo) listBox2.SelectedItem;
                    richFoundInfos.Text = fi.FoundLine;
                    richView.Text = fi.Text;
                    lblFilePath.Text = fi.FullPath;
                }
            }
            catch (Exception ex)
            {
                Logger.WriteExLog(GetType() + ".listBox2_SelectedIndexChanged", ex, "");
                MessageBox.Show(ex.Message);
            }
        }

        private void txtFolder_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter && Directory.Exists(txtFolder.Text))
            {
                LoadFiles(chkSubFolder.Checked);
            }
        }

        private void txtFolder_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtFolder_Leave(object sender, EventArgs e)
        {
            Setting.SetSetting(((Control)sender).Name, ((Control)sender).Text);
        }

        private void FormFilterTextFiles_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

    }

    
}
