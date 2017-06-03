using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Init;

namespace V6ControlManager.FormManager.HeThong
{
    public partial class ThongTinChuongTrinh : V6FormControl
    {
        public ThongTinChuongTrinh()
        {
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            try
            {
                LoadInfo();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".Init", ex);
            }
        }

        private void LoadInfo()
        {
            var data = V6BusinessHelper.Select("V6About", "*", "", "", "STT");
            if (data.Data != null && data.TotalRows > 0)
            {
                var old_job = "";
                var top = lblTitle.Top;
                var jobHeight = lblContent1.Top - top;
                var nameHeight = lblContent2.Top - lblContent1.Top;
                foreach (DataRow row in data.Data.Rows)
                {
                    var new_job = row["job2"].ToString().Trim();
                    var name = row["name"].ToString();
                    if (new_job != old_job)
                    {
                        if (old_job != "") top += 5;
                        old_job = new_job;
                        var job = row[V6Setting.IsVietnamese? "job":"job2"].ToString().Trim();
                        var jobLabel = new Label();
                        jobLabel.AutoSize = true;
                        jobLabel.Font = lblTitle.Font;//.Clone();
                        jobLabel.ForeColor = lblTitle.ForeColor;
                        jobLabel.Text = job;
                        jobLabel.Top = top;
                        jobLabel.Left = lblTitle.Left;
                        panel1.Controls.Add(jobLabel);
                        top += jobHeight;
                    }
                    var nameLable = new Label();
                    nameLable.AutoSize = true;
                    nameLable.Font = (Font) lblContent1.Font.Clone();
                    nameLable.ForeColor = lblContent1.ForeColor;
                    nameLable.Text = name;
                    nameLable.Top = top;
                    nameLable.Left = lblContent1.Left;
                    panel1.Controls.Add(nameLable);
                    top += nameHeight;
                }
                panel1.Height = top;
            }
        }

        private void ThongTinChuongTrinh_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Specify that the link was visited.
            linkLabelV6Soft.LinkVisited = true;

            // Navigate to a URL.
            System.Diagnostics.Process.Start("http://www.v6soft.com.vn");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            MoveUp();
        }

        private void MoveUp()
        {
            if (panel1.Bottom < 0) panel1.Top = panelContent.Height;
            panel1.Top--;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void ThongTinChuongTrinh_VisibleChanged(object sender, EventArgs e)
        {
            if(Visible) timer1.Start();
            else timer1.Stop();
        }
    }
}
