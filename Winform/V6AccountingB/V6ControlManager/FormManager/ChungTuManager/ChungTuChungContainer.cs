using System;
using System.Drawing;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6AccountingBusiness.Invoices;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Structs;
using V6Tools;

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
            Invoice = V6BusinessHelper.CreateInvoice(maCt);
            
            if (Invoice.Alct == null || Invoice.Alct.Rows.Count == 0)
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
                //var BaoGia = new BaoGiaControl(m_itemId);
                var panel = new Panel
                {
                    Size = new Size(800,600), Dock = DockStyle.Fill
                };
                var ChungTu = ChungTuF3.GetChungTuControl(MaCt, ItemID, "");
                
                var LeftControl = new ChungTuQuickView(Invoice)
                {
                    Width = 200, Height = panel.Height,
                    Anchor = AnchorStyles.Top|AnchorStyles.Left|AnchorStyles.Bottom
                    //Dock = DockStyle.Left
                };
                

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

                    panel.Controls.Add(LeftControl);
                }

                
                ChungTu.Left = _showQuickViewControl ? LeftControl.Width + 3 : 3;
                ChungTu.Width = panel.Width - (_showQuickViewControl ? LeftControl.Width : 0) - 6;
                ChungTu.Height = panel.Height;
                ChungTu.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

                panel.Controls.Add(ChungTu);

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

                    }
                };
                tabControl1.TabPages.Add(tab);
                tabControl1.SelectTab(tab);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".AddTab: " + ex.Message, Invoice.Name);
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
                    V6FormControl v6c = null;
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
            var container = Parent;
            var child = this;
            if (container is Form)
            {
                ((Form)container).Close();
            }
            else
            {
                
                var f  = new V6Form
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
                    toolTip1.SetToolTip(tsFull, V6Text.ZoomIn);
                };
                
                tsFull.Image = Properties.Resources.ZoomOut24;
                toolTip1.SetToolTip(tsFull, V6Text.ZoomOut);

                f.ShowDialog(container);
            }
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
    }
}
