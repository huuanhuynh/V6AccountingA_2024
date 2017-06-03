﻿using System;
using System.Windows.Forms;
using V6Controls;
using V6Controls.Forms;

namespace V6ControlManager.FormManager.HeThong.QuanLyHeThong.NgonNgu
{
    public partial class CorplanContainer  : V6FormControl
    {
        public CorplanContainer()
        {
            InitializeComponent();
            MyInit();
        }

        public CorplanContainer(string itemId)
        {
            m_itemId = itemId;
            InitializeComponent();
            MyInit();
        }
        
        private void MyInit()
        {
            try
            {
                tabPage1.Controls.Add(new CorplanControl(m_itemId) {Dock = DockStyle.Fill});
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".Init", ex);
            }
        }

        private bool loadTab2, loadTab3;

        private void btnESC_Click(object sender, EventArgs e)
        {
            try
            {
                var p = Parent;
                Dispose();
                if (p is Form) p.Dispose();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".Close " + ex.Message);
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (tabControl1.SelectedTab == tabPage2 && !loadTab2)
                {
                    tabPage2.Controls.Add(new CorplanControl1(m_itemId) { Dock = DockStyle.Fill });
                    loadTab2 = true;
                }
                else if (tabControl1.SelectedTab == tabPage3 && !loadTab3)
                {
                    tabPage3.Controls.Add(new CorplanControl2(m_itemId) { Dock = DockStyle.Fill });
                    loadTab3 = true;
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".tabControl1_SelectedIndexChanged", ex);
            }
        }
    }
}
