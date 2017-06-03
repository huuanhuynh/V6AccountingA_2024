using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace V6Controls.Controls
{
    public partial class GioiTinhControl : UserControl
    {
        public GioiTinhControl()
        {
            InitializeComponent();
            //TextChanged += GioiTinh_TextChanged;
            radNu.Left = Width/2;
        }

        /// <summary>
        /// Đang chọn Nam?
        /// </summary>
        [DefaultValue(false)]
        public bool IsNam
        {
            get { return radNam.Checked; }
            set { radNam.Checked = value; }
        }

        /// <summary>
        /// Đang chọn Nữ?
        /// </summary>
        [DefaultValue(false)]
        public bool IsNu
        {
            get { return radNu.Checked; }
            set { radNu.Checked = value; }
        }

        [DefaultValue("Nam")]
        public string NamText
        {
            get { return radNam.Text; }
            set { radNam.Text = value; }
        }
        [DefaultValue("Nữ")]
        public string NuText
        {
            get { return radNu.Text; }
            set { radNu.Text = value; }
        }

        /// <summary>
        /// Gán text theo dạng Nam/Nữ
        /// </summary>
        [Browsable(true)]
        [DefaultValue("Nam/Nữ")]
        [Description("Gán text theo dạng Nam/Nữ")]
        public override string Text
        {
            get { return radNam.Text + "/" + radNu.Text; }
            set
            {
                var ss = value.Split(new[] {' ', '/', ',', ';', '-'});
                if (ss.Length > 0) radNam.Text = ss[0].Trim();
                if (ss.Length > 1) radNu.Text = ss[1].Trim();
            }
        }

        [DefaultValue("")]
        public string Value
        {
            get { return radNam.Checked ? "1" : radNu.Checked ? "0" : ""; }
            set
            {
                if (value == "1") radNam.Checked = true;
                else if (value == "0") radNu.Checked = true;
                else
                {
                    radNam.Checked = false;
                    radNu.Checked = false;
                }
            }
        }

    }
}
