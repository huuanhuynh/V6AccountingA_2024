﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6AccountingBusiness.Invoices;
using V6ControlManager.FormManager.ChungTuManager.PhaiThu.HoaDonCafe;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Structs;

namespace V6ControlManager.FormManager.VitriManager
{
    public partial class VitriCafeContainer : V6FormControl
    {
        private Timer _timerHideMessage;
        private Timer _timerAutoRefresh;
        private int _timeCount;
        private int currentTabIndex = -1;
        private string MaCt;
        //private bool _showQuickViewControl;
        private List<VitriKhuCafeControl> KhuList = new List<VitriKhuCafeControl>();
        private V6InvoiceBase Invoice { get; set; }

        public VitriCafeContainer(string maCt, string itemId, string program)
        {
            MaCt = maCt;
            m_itemId = itemId;
            //_showQuickViewControl = showQuickView;
            Invoice = V6BusinessHelper.CreateInvoice(maCt);
            
            if (Invoice.Alct == null)
            {
                this.ShowWarningMessage("Kiểm tra mã chứng từ!");
                Hide();
            }
            else
            {
                InitializeComponent();
                MyInit();
                AddTab0();
            }
            LoadDefaultData(4, "", program, itemId);
        }

        private void MyInit()
        {
            _timerHideMessage = new Timer();
            _timerHideMessage.Interval = 200;
            _timerHideMessage.Tick += _timerHideMessage_Tick;
            _timerAutoRefresh = new Timer();
            _timerAutoRefresh.Interval = 1000;
            _timerAutoRefresh.Tick += _timerAutoRefresh_Tick;

            currentTabIndex = tabControl1.SelectedIndex;
        }

        private void VitriCafeContainer_Load(object sender, EventArgs e)
        {
            if (_timerAutoRefresh != null) _timerAutoRefresh.Start();
        }

        private int timeCount;
        void _timerAutoRefresh_Tick(object sender, EventArgs e)
        {
            timeCount++;

            if (timeCount >= txtTime.Value)
            {
                timeCount = 0;
                RefreshCurrentKhuData();
            }
        }

        private void RefreshCurrentKhuData()
        {
            try
            {
                var currentKhu = GetCurrentKhu();
                if (currentKhu == null) return;
                currentKhu.RefreshData();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".RefreshCurrentKhuData", ex);
            }
        }

        private VitriKhuCafeControl GetCurrentKhu()
        {
            var khu = V6ControlFormHelper.FindChild<VitriKhuCafeControl>(tabControl1.SelectedTab);
            if (khu is VitriKhuCafeControl) return (VitriKhuCafeControl) khu;
            return null;
        }

        /// <summary>
        /// Thêm tab đầu tiên, tự lấy mã kho đầu tiên
        /// </summary>
        private void AddTab0()
        {
            // Thêm một khu mới.
            // Hiển thị danh sách kho để chọn, trừ kho đã có. (kho = khu)
            try
            {
                if (V6Setting.IsDesignTime) return;
                IDictionary<string, object> keys = new SortedDictionary<string, object>();
                keys.Add("LOAI_KHO", "1");
                var kho_data = V6BusinessHelper.Select(V6TableName.Alkho, keys, "top 1 *").Data;
                if (kho_data.Rows.Count != 1)
                {
                    this.ShowWarningMessage("LOAI_KHO ERROR");
                    return;
                }

                var makho = kho_data.Rows[0]["MA_KHO"].ToString().Trim();
                var khu = new VitriKhuCafeControl(makho)
                {
                    Dock = DockStyle.Fill
                };

                var ten_khu = kho_data.Rows[0]["TEN_KHO"].ToString().Trim();
                var tab = new TabPage("Khu " + ten_khu);
                tab.Controls.Add(khu);
                KhuList.Add(khu);

                //khu.ChuyenKhu += khu_ChuyenKhu;
                khu.Disposed += delegate 
                {
                    try
                    {
                        KhuList.Remove(khu);
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

        /// <summary>
        /// Thêm tab khi ấn nút +, chọn kho.
        /// </summary>
        private void AddTab()
        {
            // Thêm một khu mới.
            // Hiển thị danh sách kho để chọn, trừ kho đã có. (kho = khu)
            try
            {
                var not_in = "";
                foreach (VitriKhuCafeControl khu_cafe_control in KhuList)
                {
                    not_in += string.Format(",'{0}'", khu_cafe_control.Ma_kho);
                }
                if (not_in.Length > 1) not_in = not_in.Substring(1);
                
                //SHOW SELECT
                KhuSelectForm selectForm = new KhuSelectForm();
                selectForm.NotInList = not_in;
                if (selectForm.ShowDialog(this) == DialogResult.OK)
                {
                    var makho = selectForm.SelectedID;

                    

                    var tab = new TabPage(selectForm.SelectedName);
                    
                    tabControl1.TabPages.Add(tab);
                    tabControl1.SelectTab(tab);

                    var khu = new VitriKhuCafeControl(makho)
                    {
                        Dock = DockStyle.Fill
                    };
                    tab.Controls.Add(khu);
                    KhuList.Add(khu);

                    //khu.ChuyenKhu += khu_ChuyenKhu;
                    khu.Disposed += delegate
                    {
                        try
                        {
                            KhuList.Remove(khu);
                            tab.Dispose();
                        }
                        catch
                        {

                        }
                    };

                }
                else
                {
                    
                }

                
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
                    Control c = tabControl1.SelectedTab.Controls[0];
                    if (c is VitriKhuCafeControl)
                    {
                        return ((VitriKhuCafeControl)c).DoHotKey0(keyData);
                    }
                    //foreach (Control control in c.Controls)
                    //{
                    //    if (control is V6FormControl)
                    //    {
                    //        return (control as V6FormControl).DoHotKey0(keyData);
                    //    }
                    //}
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
                    toolTipV6FormControl.SetToolTip(tsFull, V6Text.ZoomIn);
                };
                
                tsFull.Image = Properties.Resources.ZoomOut24;
                toolTipV6FormControl.SetToolTip(tsFull, V6Text.ZoomOut);

                f.ShowDialog(container);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            V6ControlsHelper.DisableLookup = true;
            try
            {
                if (tabControl1.TabCount != 0 &&
                    this.ShowConfirmMessage(V6Text.CloseConfirm) == DialogResult.Yes)
                {
                    var p = GetCurrentKhu();
                    p.Dispose();
                }
                else if (tabControl1.TabCount == 0 &&
                    this.ShowConfirmMessage(V6Text.CloseConfirm) == DialogResult.Yes)
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
