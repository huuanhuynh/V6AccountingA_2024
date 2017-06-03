using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6AccountingBusiness.Invoices;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ToolManager
{
    public partial class FormTestInvoice : V6Form
    {
        public FormTestInvoice()
        {
            InitializeComponent();
        }

        List<string> _file, _file_result;
        Dictionary<string,string> _replace1, _replace2;
        OpenFileDialog o = new OpenFileDialog();
        
        private void OpenReplace(string file, ref Dictionary<string, string> dic)
        {
            dic = new Dictionary<string, string>();
            try
            {
                if (File.Exists(file))
                {
                    FileStream fs = new FileStream(file, FileMode.Open);
                    StreamReader sr = new StreamReader(fs);
                    string s = "";
                    while (!sr.EndOfStream)
                    {
                        s = sr.ReadLine();
                        if (!s.StartsWith(";"))
                        {
                            if (s.Contains('='))
                            {
                                string[] ss = s.Split('=');
                                dic.Add(ss[0], ss[1]);
                            }
                        }
                    }

                    sr.Close();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex.Message);
            }
        }

        private void FormHuuanEditText_Load(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex.Message);
            }
        }

        
        private void btnTestInsert81_Click(object sender, EventArgs e)
        {
            try
            {
                btnTestInsert81.Enabled = false;
                sttRec81 = txtSttRec81.Text.Trim();
                flag81 = false;
                num81 = (int) txtNum81.Value;
                Thread T = new Thread(InsertInvoice81);
                T.Start();
                Timer timer = new Timer();
                timer.Tick += (sender1, args) =>
                {
                    if (flag81)
                    {
                        timer.Stop();
                        btnTestInsert81.Enabled = true;
                    }

                    int length = log81.Length;
                    txtList.AppendText(log81.Substring(0, length));
                    log81 = log81.Substring(length);
                };
                timer.Start();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex.Message);
            }
        }
        
        private string log81 = "", sttRec81 = "";
        private bool flag81;
        private int num81 = 10;
        private void InsertInvoice81()
        {
            try
            {
                V6Invoice81 invoice = new V6Invoice81();
                var where = string.IsNullOrEmpty(sttRec81) ? "" : "Stt_rec='"+sttRec81+"'";
                var loadAM = V6BusinessHelper.Select(invoice.AM, "Top 1 *", where).Data;
                if (loadAM != null && loadAM.Rows.Count == 1)
                {
                    var amData = loadAM.Rows[0].ToDataDictionary();
                    amData["NGAY_CT"] = DateTime.Now;
                    var oldSttRec = amData["STT_REC"].ToString().Trim();
                    var loadAD = invoice.LoadAd81(oldSttRec);

                    for (int i = 0; i < num81; i++)
                    {
                        var newSttRec = V6BusinessHelper.GetNewSttRec(invoice.Mact);
                        amData["STT_REC"] = newSttRec;
                        amData["SO_CT"] = newSttRec.Right(12);
                        var adData = loadAD.ToListDataDictionary(newSttRec);

                        if (invoice.InsertInvoice(amData, adData, new List<SortedDictionary<string, object>>()))
                        {
                            log81 += newSttRec + "\r\n";
                        }
                        else
                        {
                            log81 += newSttRec + " " + V6Text.AddFail + " " + invoice.V6Message + "\r\n";
                        }
                    }
                }
                else
                {
                    log81 += "Nodata.";
                }
            }
            catch (Exception ex)
            {
                log81 += ex.Message;
            }
            flag81 = true;
        }


        private void btnTestUpdate81_Click(object sender, EventArgs e)
        {
            try
            {
                btnTestUpdate81.Enabled = false;
                sttRec81u = txtSttRec81u.Text.Trim();
                flag81u = false;
                num81u = (int)txtNum81u.Value;
                Thread T = new Thread(UpdateInvoice81);
                T.Start();
                Timer timer = new Timer();
                timer.Tick += (sender1, args) =>
                {
                    if (flag81u)
                    {
                        timer.Stop();
                        btnTestUpdate81.Enabled = true;
                    }

                    int length = log81u.Length;
                    txtList.AppendText(log81u.Substring(0, length));
                    log81u = log81u.Substring(length);
                };
                timer.Start();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(string.Format("{0} {1} {2} {3} {4}", V6Login.ClientName, GetType(), MethodBase.GetCurrentMethod().Name, sttRec81u, ex.Message));
            }
        }

        private string log81u = "", sttRec81u = "";
        private bool flag81u;
        private int num81u = 10;
        private void UpdateInvoice81()
        {
            try
            {
                V6Invoice81 invoice = new V6Invoice81();
                var where = string.IsNullOrEmpty(sttRec81u) ? "" : "Stt_rec='" + sttRec81u + "'";
                var loadAM = V6BusinessHelper.Select(invoice.AM, "Top 1 *", where).Data;
                if (loadAM != null && loadAM.Rows.Count == 1)
                {
                    var amData = loadAM.Rows[0].ToDataDictionary();
                    amData["NGAY_CT"] = DateTime.Now;
                    var oldSttRec = amData["STT_REC"].ToString().Trim();
                    var loadAD = invoice.LoadAd81(oldSttRec);

                    for (int i = 0; i < num81u; i++)
                    {
                        //var newSttRec = V6BusinessHelper.GetNewSttRec(invoice.Mact);
                        //amData["STT_REC"] = newSttRec;
                        var adData = loadAD.ToListDataDictionary();
                        var keys = new SortedDictionary<string, object> { { "STT_REC", oldSttRec } };
                        if (invoice.UpdateInvoice(amData, adData, new List<SortedDictionary<string, object>>(), keys))
                        {
                            log81u += "Update ok " + oldSttRec + "\r\n";
                        }
                        else
                        {
                            log81u += "Update fail " + oldSttRec + " " + invoice.V6Message + "\r\n";
                        }
                    }
                }
                else
                {
                    log81u = "Nodata.";
                }
            }
            catch (Exception ex)
            {
                log81u += ex.Message;
            }
            flag81u = true;
        }

        private void btnOpenReplace1_Click(object sender, EventArgs e)
        {
            try
            {
                if (o.ShowDialog() == DialogResult.OK)
                {
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex.Message);
            }
        }

        private void btnOpenReplace2_Click(object sender, EventArgs e)
        {
            try
            {
                if (o.ShowDialog() == DialogResult.OK)
                {
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                _file = txtList.Lines.ToList();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(ex.Message);
            }
        }

        
        
    }
}
