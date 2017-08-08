﻿using System;
using System.Windows.Forms;
using V6AccountingBusiness.Invoices;
using V6Controls;
using V6Controls.Forms;
using V6Init;

namespace V6ControlManager.FormManager.ChungTuManager.PhaiTra.PhieuXuatTraLaiNCC
{
    public partial class PhieuXuatTraLaiNCCContainer : V6Control
    {
        private Timer _timerHideMessage;
        private int _timeCount;
        private int currentTabIndex = -1;
        public PhieuXuatTraLaiNCCContainer()
        {
            V6Invoice86 invoice81 = new V6Invoice86();
            if (invoice81.Alct == null || invoice81.Alct.Rows.Count == 0)
            {
                this.ShowWarningMessage("Kiểm tra mã chứng từ!");
                Hide();
            }
            else
            {
                InitializeComponent();
                MyInit();
                AddTab();
            }
        }
        
        public PhieuXuatTraLaiNCCContainer(string itemId)
        {
            m_itemId = itemId;
            V6Invoice86 invoice81 = new V6Invoice86();
            if (invoice81.Alct == null || invoice81.Alct.Rows.Count == 0)
            {
                this.ShowWarningMessage("Kiểm tra mã chứng từ!");
                Hide();
            }
            else
            {
                InitializeComponent();
                MyInit();
                AddTab();
            }
        }

        private void MyInit()
        {
            _timerHideMessage = new Timer();
            _timerHideMessage.Interval = 200;
            _timerHideMessage.Tick += _timerHideMessage_Tick;

            currentTabIndex = tabControl1.SelectedIndex;
        }

        private void AddTab()
        {
            try
            {
                var hoadon = new PhieuXuatTraLaiNCCControl(m_itemId) { Dock = DockStyle.Fill };
                
                var tab = new TabPage(V6Text.Invoice);
                tab.Controls.Add(hoadon);
                hoadon.Disposed += delegate {
                    tab.Dispose();
                };
                tabControl1.TabPages.Add(tab);
                tabControl1.SelectTab(tab);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".AddTab: " +ex.Message, "PhieuXuatTraLaiNCC");
            }
        }

        public override void DisableZoomButton()
        {
            tsFull.Enabled = false;
        }

        public override void DoHotKey (Keys keyData)
        {
            try
            {
                if (keyData == (Keys.Alt | Keys.Left))
                {
                    currentTabIndex--;
                    if (tabControl1.TabPages.Count > 0 && currentTabIndex < 0)
                        currentTabIndex = tabControl1.TabPages.Count - 1;
                    tabControl1.SelectedIndex = currentTabIndex;
                }
                else if (keyData == (Keys.Alt | Keys.Right))
                {
                    currentTabIndex++;
                    if (tabControl1.TabPages.Count > 0 && currentTabIndex >= tabControl1.TabPages.Count)
                        currentTabIndex = 0;
                    tabControl1.SelectedIndex = currentTabIndex;
                }
                else
                {
                    if (tabControl1.TabPages.Count > 0)
                    {
                        V6FormControl c = tabControl1.SelectedTab.Controls[0] as V6FormControl;
                        if (c != null) c.DoHotKey(keyData);
                    }
                    else if (keyData == Keys.Escape)
                    {
                        tsClose.PerformClick();
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public void ShowMessage(string message)
        {
            try
            {
                tsMessage.Text = message;
                _timeCount = 0;
                _timerHideMessage.Start();
            }
            catch
            {
                // ignored
            }
        }

        void _timerHideMessage_Tick(object sender, EventArgs e)
        {
            _timeCount++;
            if (_timeCount >= 25)
            {
                if (tsMessage.Text.Length > 0)
                    tsMessage.Text = tsMessage.Text.Substring(1);
                else _timerHideMessage.Stop();
            }
        }

        private void btnThemPhieuXuatTraLaiNCC_Click(object sender, EventArgs e)
        {
            AddTab();
        }

        private void btnFullScreen_Click(object sender, EventArgs e)
        {
            var container = Parent;
            var child = this;
            if (container is Form)
            {
                ((Form)container).Close();
            }
            else
            {
                
                var f = new V6Form
                {
                    WindowState = FormWindowState.Maximized,
                    ShowInTaskbar = false,
                    FormBorderStyle = FormBorderStyle.None
                };
                f.Controls.Add(child);
                f.FormClosing += (se, a) =>
                {
                    container.Controls.Add(child);
                    //tsFull.Enabled = true;
                    tsFull.Image = Properties.Resources.ZoomIn24;
                    tsFull.Text = V6Text.ZoomIn;
                };
                //tsFull.Enabled = false;
                tsFull.Image = Properties.Resources.ZoomOut24;
                tsFull.Text = V6Text.ZoomOut;

                f.ShowDialog(this);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            V6ControlsHelper.DisableLookup = true;
            try
            {
                if (this.ShowConfirmMessage(V6Text.BackConfirm) == DialogResult.Yes)
                {
                    var p = Parent;
                    Dispose();
                    if (p is Form) p.Dispose();

                    //FormManagerHelper. ShowCurrentMenu3Menu();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".Close " + ex.Message);
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentTabIndex = tabControl1.SelectedIndex;
        }
    }
}
