﻿using System;
using V6Controls;
using V6Controls.Forms;
using V6Init;

namespace V6ControlManager.FormManager.ToolManager
{
    public partial class FormAmLich : V6Form
    {

        public FormAmLich()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                Text = V6Setting.IsVietnamese ? "Âm lịch Việt Nam" : "Vietnam lunar calendar";
                var now = DateTime.Now;
                //lichViewControl1.FocusDate = now;
                lichViewControl1.SetData(now.Year, now.Month, now, null, null, V6Setting.IsVietnamese ? "Lịch âm" : "Lunar calenda");
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".Load", ex);
            }
        }

        private void lichViewControl1_ClickNextEvent(V6Controls.Controls.LichView.LichViewEventArgs obj)
        {
            try
            {
                var nextMonthDate = lichViewControl1.FocusDate.AddMonths(1);
                lichViewControl1.SetData(nextMonthDate.Year, nextMonthDate.Month, nextMonthDate, null, null, "Lịch âm");
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".Next", ex);
            }
        }

        private void lichViewControl1_ClickPreviousEvent(V6Controls.Controls.LichView.LichViewEventArgs obj)
        {
            try
            {
                var nextMonthDate = lichViewControl1.FocusDate.AddMonths(-1);
                lichViewControl1.SetData(nextMonthDate.Year, nextMonthDate.Month, nextMonthDate, null, null, "Lịch âm");
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".Previous", ex);
            }
        }
    }
}