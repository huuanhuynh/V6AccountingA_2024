﻿using System;
using System.Drawing;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6AccountingBusiness.Invoices;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Structs;

namespace V6ControlManager.FormManager.ChungTuManager
{
    public partial class ChungTuChungContainer : V6FormControl
    {
        private Timer _timerHideMessage;
        private int _timeCount;
        private int currentTabIndex = -1;
        private string MaCt;
        private bool _showQuickViewControl;
        private V6InvoiceBase Invoice { get; set; }

        public ChungTuChungContainer(string maCt, string itemId, bool showQuickView)
        {
            MaCt = maCt;
            m_itemId = itemId;
            _showQuickViewControl = showQuickView;
            Invoice = V6BusinessHelper.CreateInvoice(MaCt);
            
            if (Invoice.Alct == null)
            {
                this.ShowWarningMessage(V6Text.Text("KTMACT"));
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

        private void ChungTuChungContainer_Load(object sender, EventArgs e)
        {
            ApplyButtonToolTip();
        }

        private void ApplyButtonToolTip()
        {
            try
            {
                toolTipV6FormControl.SetToolTip(tsNew, V6Text.Add);
                toolTipV6FormControl.SetToolTip(tsFull, V6Text.ZoomIn);
                toolTipV6FormControl.SetToolTip(tsClose, V6Text.Close);
            }
            catch (Exception)
            {
                //
            }
        }

        private void AddTab()
        {
            string method_log = "";
            try
            {
                //var BaoGia = new BaoGiaControl(m_itemId);
                method_log += "var panel;";
                var panel = new Panel
                {
                    Size = new Size(800,600), Dock = DockStyle.Fill
                };
                method_log += "var ChungTu;";
                var ChungTu = ChungTuF3.GetChungTuControl(MaCt, ItemID, "");
                method_log += "LeftControl;";
                var LeftControl = new ChungTuQuickView(Invoice)
                {
                    Width = 200, Height = panel.Height,
                    Anchor = AnchorStyles.Top|AnchorStyles.Left|AnchorStyles.Bottom
                    //Dock = DockStyle.Left
                };

                method_log += "if (_showQuickViewControl);";
                if (_showQuickViewControl)
                {
                    LeftControl.SetDataSource(ChungTu.AM);
                    LeftControl.SelectedIndexChanged += data =>
                    {
                        if (LeftControl.EnableChangeInvoice)
                        {
                            if (ChungTu.Mode == V6Mode.View)
                            {
                                if (data != null && data.ContainsKey("STT_REC"))
                                {
                                    ChungTu.ViewInvoice(data["STT_REC"].ToString().Trim(), V6Mode.View);
                                }
                            }
                            else
                            {
                                LeftControl.SetSelectedRow(ChungTu._sttRec);
                            }
                        }
                    };
                    ChungTu.AmChanged += data =>
                    {
                        LeftControl.SetDataSource(data);
                    };
                    ChungTu.InvoiceChanged += sttRec =>
                    {
                        LeftControl.SetSelectedRow(sttRec);
                    };
                    method_log += "panel.Controls.Add(LeftControl);";
                    panel.Controls.Add(LeftControl);
                }

                method_log += "ChungTu.Left = _showQu...;";
                ChungTu.Left = _showQuickViewControl ? LeftControl.Width + 3 : 3;
                ChungTu.Width = panel.Width - (_showQuickViewControl ? LeftControl.Width : 0) - 6;
                ChungTu.Height = panel.Height;
                ChungTu.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

                panel.Controls.Add(ChungTu);
                method_log += "var tab;";
                var tab = new TabPage(V6Text.Invoice);
                ChungTu.ParentTabPage = tab;
                tab.Controls.Add(panel);
                
                ChungTu.Disposed += delegate 
                {
                    try
                    {
                        //tabControl1.Controls.Remove(tab);
                        tab.Dispose();
                    }
                    catch
                    {
                        //
                    }
                };
                method_log += "tabControl1.TabPages.Add(tab);";
                tabControl1.TabPages.Add(tab);
                method_log += "tabControl1.SelectTab(tab);";
                tabControl1.SelectTab(tab);
                Button btnMoi = V6ControlFormHelper.GetControlByName(ChungTu, "btnMoi") as Button;
                if (btnMoi != null) btnMoi.Focus();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".AddTab\nmethod log: " +method_log + "\n", ex);
                //this.ShowErrorMessage(GetType() + ".AddTab: " + ex.Message, Invoice.Name);
            }
        }

        public override void DisableZoomButton()
        {
            tsFull.Enabled = false;
        }

        public override bool DoHotKey0(Keys keyData)
        {
            try
            {
                if (keyData == Keys.F11)
                {
                    tsFull.PerformClick();
                    return true;
                }
                else if (keyData == (Keys.Control | Keys.T))
                {
                    tsNew.PerformClick();
                    return true;
                }
                else if (keyData == (Keys.Alt | Keys.Left))
                {
                    currentTabIndex--;
                    if (tabControl1.TabPages.Count > 0 && currentTabIndex < 0)
                        currentTabIndex = tabControl1.TabPages.Count - 1;
                    tabControl1.SelectedIndex = currentTabIndex;
                    return true;
                }
                else if (keyData == (Keys.Alt | Keys.Right))
                {
                    currentTabIndex++;
                    if (tabControl1.TabPages.Count > 0 && currentTabIndex >= tabControl1.TabPages.Count)
                        currentTabIndex = 0;
                    tabControl1.SelectedIndex = currentTabIndex;
                    return true;
                }
                else if(tabControl1.TabCount > 0)
                {
                    Control c = tabControl1.SelectedTab.Controls[0];
                    foreach (Control control in c.Controls)
                    {
                        if (control is V6FormControl)
                        {
                            return (control as V6FormControl).DoHotKey0(keyData);
                        }
                    }
                }

                if (keyData == Keys.Escape)
                {
                    tsClose.PerformClick();
                    return true;
                }
            }
            catch (Exception)
            {
                // ignored
            }
            return false;
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

        public void DoFullScreen()
        {
            try
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
                        if (child.IsDisposed) return;
                        container.Controls.Add(child);
                        tsFull.Image = Properties.Resources.ZoomIn24;
                        toolTipV6FormControl.SetToolTip(tsFull, V6Text.ZoomIn);
                    };

                    tsFull.Image = Properties.Resources.ZoomOut24;
                    toolTipV6FormControl.SetToolTip(tsFull, V6Text.ZoomOut);

                    f.ShowDialog(container);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".DoFullScreen", ex);
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddTab();
        }

        private void btnFullScreen_Click(object sender, EventArgs e)
        {
            DoFullScreen();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            V6ControlsHelper.DisableLookup = true;
            try
            {
                if (tabControl1.TabCount == 0 ||
                    this.ShowConfirmMessage(V6Text.BackConfirm) == DialogResult.Yes)
                {
                    var p = Parent;
                    
                    if (p is Form) ((Form)p).Dispose();
                    else Dispose();
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".btnClose_Click", ex);
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            currentTabIndex = tabControl1.SelectedIndex;
        }

        private void tsMessage_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            DoFullScreen();
        }
        
    }
}
