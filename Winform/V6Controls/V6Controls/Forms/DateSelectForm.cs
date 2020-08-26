using System;
using System.Windows.Forms;
using V6Init;

namespace V6Controls.Forms
{
    public partial class DateSelectForm : V6Form
    {

        public DateSelectForm()
        {
            InitializeComponent();
        }

        public DateTime SelectedDate
        {
            get
            {
                return lichViewControl1.FocusDate;
            }
            set
            {
                lichViewControl1.FocusDate = value;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                Text = V6Setting.IsVietnamese ? "Âm lịch Việt Nam" : "Vietnam lunar calendar";
                lichViewControl1.SetData(SelectedDate.Year, SelectedDate.Month, SelectedDate, null, null, V6Setting.IsVietnamese ? "Lịch âm" : "Lunar calenda");
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".Load", ex);
            }
        }

        private void lichViewControl1_ClickNextEvent(Controls.LichView.LichViewEventArgs obj)
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

        private void lichViewControl1_ClickPreviousEvent(Controls.LichView.LichViewEventArgs obj)
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

        private void btnNhan_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void btnToday_Click(object sender, EventArgs e)
        {
            try
            {
                var nextMonthDate = DateTime.Now;
                lichViewControl1.SetData(nextMonthDate.Year, nextMonthDate.Month, nextMonthDate, null, null, "Lịch âm");
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".Next", ex);
            }
        }
    }
}
