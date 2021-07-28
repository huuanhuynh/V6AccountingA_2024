using System;
using System.Data;
using System.Windows.Forms;
using V6ControlManager.FormManager.ChungTuManager;
using V6ControlManager.FormManager.ChungTuManager.PhaiThu.BaoGia;
using V6ControlManager.FormManager.ChungTuManager.PhaiThu.HoaDon;
using V6ControlManager.FormManager.MenuManager;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Tools;

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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (view == null) return;
            if (textBox1.Text.Trim() == "")
            {
                view.RowFilter = "1=0";
                return;
            }

            view.RowFilter = string.Format("vbar like '%{0}%' or vbar2 like '%{0}%' or ma_ct like '%{0}%'", textBox1.Text);
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            Choose();
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
    }
}
