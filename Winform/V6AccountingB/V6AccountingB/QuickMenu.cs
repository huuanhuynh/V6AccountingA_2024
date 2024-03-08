using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6ControlManager.FormManager.ChungTuManager;
using V6ControlManager.FormManager.ChungTuManager.PhaiThu.BaoGia;
using V6ControlManager.FormManager.ChungTuManager.PhaiThu.HoaDon;
using V6ControlManager.FormManager.MenuManager;
using V6ControlManager.FormManager.ReportManager.ReportR;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Tools;
using V6Tools.V6Convert;

namespace V6AccountingB
{
    public partial class QuickMenu : UserControl
    {
        public QuickMenu()
        {
            InitializeComponent();
            textBox1.GrayText = V6Setting.IsVietnamese ? "Tìm" : "Search";
        }

        private DataTable data;
        private DataView view;
        public void LoadMenuData()
        {
            try
            {
                data = V6Menu.GetMenuQuickRun();
                view = new DataView(data);
                view.RowFilter = "1=0";
                listBox1.DisplayMember = V6Setting.IsVietnamese ? "vbar" : "vbar2";
                listBox1.DataSource = view;
                listBox1.DisplayMember = V6Setting.IsVietnamese ? "vbar" : "vbar2";
            }
            catch (Exception ex)
            {
                Logger.WriteToLog(V6Login.ClientName + GetType() +".LoadMenuData " + ex.Message, Application.ProductName);
            }
        }

        private void Choose()
        {
            try
            {
                if (listBox1.SelectedItems.Count != 1) return;
                DataRowView rowView = listBox1.SelectedItem as DataRowView;
                if (rowView == null) return;
                //view.RowFilter = "1=0";
                textBox1.Text = "";

                MenuButton mButton = new MenuButton(rowView.Row.ToDataDictionary());

                var c = MenuManager.GenControl(this, mButton, null);
                if (c is ChungTuChungContainer)
                {
                    ((ChungTuChungContainer)c).DisableZoomButton();
                }
                else if (c is BaoGiaContainer)
                {
                    ((BaoGiaContainer)c).DisableZoomButton();
                }
                
                if (c != null)
                {
                    c.ShowToForm(this, mButton.Text, true, false);
                }

            }
            catch (Exception ex)
            {
                Logger.WriteToLog(V6Login.ClientName + GetType() + ".Choose " + ex.Message, Application.ProductName);
            }
        }

        private void Choose2()
        {
            try
            {
                if (listBox2.SelectedItems.Count != 1) return;
                DataRowView rowView = listBox2.SelectedItem as DataRowView;
                if (rowView == null) return;
                
                var data = rowView.Row.ToDataDictionary();
                var code_type = data["CODE_TYPE"].ToString().Trim();
                var PPROCEDURE = data["PPROCEDURE"].ToString().Trim();
                var PPARANAME = ObjectAndString.SplitStringBy(data["PPARANAME"].ToString(), '|', false);
                var PPARAVALUE = ObjectAndString.SplitStringBy(data["PPARAVALUE"].ToString(), '|', false);
                var PPARATYPE = ObjectAndString.SplitStringBy(data["PPARATYPE"].ToString(), '|', false);
                var plist = new List<SqlParameter>();
                for (int i = 0; i < PPARANAME.Length; i++)
                {
                    switch (PPARATYPE[i])
                    {
                        case "N":
                            plist.Add(new SqlParameter(PPARANAME[i], ObjectAndString.ObjectToDecimal(PPARAVALUE[i])));
                            break;
                        case "D":
                            plist.Add(new SqlParameter(PPARANAME[i], ObjectAndString.StringToDate(PPARAVALUE[i])));
                            break;
                        default:
                            plist.Add(new SqlParameter(PPARANAME[i], PPARAVALUE[i]));
                            break;
                    }
                }

                var ds = V6BusinessHelper.ExecuteProcedure(PPROCEDURE, plist.ToArray());

                // TT Trạng thái
                if (code_type == "TT")
                {
                    HistoryStatusForm form = new HistoryStatusForm(ds.Tables[0]);
                    form.Text = data["MESS2"].ToString();
                    form.ShowDialog(this);
                }
                else if (code_type == "45")
                {   
                    var REP_FILE = data["REP_FILE"].ToString();
                    var TITLE = data["TITLE"].ToString();
                    var TITLE2 = data["TITLE2"].ToString();
                    var PROGRAM = data["PROGRAM"].ToString();
                    var view = new ReportR45ViewBase("m_itemId", PROGRAM, PPROCEDURE, REP_FILE, TITLE, TITLE2, "", "", "");
                    //view.MAU = MAU;
                    //view.LAN = LAN;
                    view.Dock = DockStyle.Fill;
                    view.DataSet = ds;
                    view.btnNhan.Enabled = false;
                    view.panel1.Visible = false;
                    view.ShowToForm(this, "Chi tiết", true);
                }
                else if (code_type == "44")
                {
                    var REP_FILE = data["REP_FILE"].ToString();
                    var TITLE = data["TITLE"].ToString();
                    var TITLE2 = data["TITLE2"].ToString();
                    var PROGRAM = data["PROGRAM"].ToString();
                    var view = new ReportR44_DX("m_itemId", PROGRAM, PPROCEDURE, REP_FILE, TITLE, TITLE2, "", "", "");
                    //view.MAU = MAU;
                    //view.LAN = LAN;
                    view.Dock = DockStyle.Fill;
                    view.DataSet = ds;
                    view.btnNhan.Enabled = false;
                    view.panel1.Visible = false;
                    view.ShowToForm(this, "Chi tiết", true);
                }

            }
            catch (Exception ex)
            {
                Logger.WriteToLog(V6Login.ClientName + GetType() + ".Choose2 " + ex.Message, Application.ProductName);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (view == null) return;
            if (textBox1.Text.Trim() == "")
            {
                view.RowFilter = "1=0";
                return;
            }

            string value = textBox1.Text;
            value = value.Replace("'", "''");
            value = value.Replace("[", "[[]");
            value = value.Replace("*", "[*]");


            view.RowFilter = string.Format("vbar like '%{0}%' or vbar2 like '%{0}%' or ma_ct like '%{0}%'", value);
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            Choose();
        }

        private void listBox2_DoubleClick(object sender, EventArgs e)
        {
            Choose2();
        }

        private void listBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && listBox1.SelectedItems.Count == 1)
            {
                Choose();
            }
            else
            {
                textBox1.Focus();
                if (('a' <= e.KeyValue && e.KeyValue <= 'z') | ('A' <= e.KeyValue && e.KeyValue <= 'Z'))
                {
                    char k = (char) e.KeyValue;
                    textBox1.Text = k.ToString();
                    textBox1.SelectionStart = textBox1.TextLength;
                }
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (listBox1.Items.Count == 0)
            {
                e.Handled = true;
                return;
            }
            if (e.KeyCode == Keys.Enter && listBox1.SelectedItems.Count == 1)
            {
                Choose();
            }
            else
            {
                if (e.KeyCode == Keys.Up)
                {
                    e.Handled = true;
                    if (listBox1.SelectedIndex > 0)
                        listBox1.SelectedIndex--;
                    else
                        listBox1.SelectedIndex = listBox1.Items.Count - 1;
                }
                else if (e.KeyCode == Keys.Down)
                {
                    e.Handled = true;
                    if (listBox1.SelectedIndex >= listBox1.Items.Count-1)
                        listBox1.SelectedIndex = 0;
                    else
                        listBox1.SelectedIndex++;
                }
            }
        }

        /// <summary>
        /// Tạo listbox mess2
        /// </summary>
        /// <param name="data"></param>
        internal void SetMess2Data(DataTable data)
        {
            try
            {
                listBox2.DisplayMember = V6Setting.IsVietnamese ? "mess2" : "mess2";
                listBox2.DataSource = data;
                listBox2.DisplayMember = V6Setting.IsVietnamese ? "mess2" : "mess2";
            }
            catch (Exception)
            {
                
            }
        }

        private void QuickMenu_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                listBox2.Height = (Height - textBox1.Height)/2;
                listBox1.Top = listBox2.Bottom;
                listBox1.Height = listBox2.Height;
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
