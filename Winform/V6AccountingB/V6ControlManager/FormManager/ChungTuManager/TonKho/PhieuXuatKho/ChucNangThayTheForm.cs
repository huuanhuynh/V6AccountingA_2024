using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using V6Controls;
using V6Controls.Forms;

namespace V6ControlManager.FormManager.ChungTuManager.TonKho.PhieuXuatKho
{
    public partial class ChucNangThayTheForm : V6Form
    {
        private bool _isnum;
        public ChucNangType ChucNangDaChon { get; private set; }
        public string Value { get { return textBox1.Text; } }
        public ChucNangThayTheForm()
        {
            InitializeComponent();
            MyInit();
        }
        public ChucNangThayTheForm(bool isnum)
        {
            _isnum = isnum;
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            try
            {
                if (_isnum)
                {
                    radDaoNguoc.Checked = true;
                    textBox1.Enabled = false;
                }
                else
                {
                    radThayThe.Checked = true;
                    radDaoNguoc.Enabled = false;
                    textBox1.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".MyInit", ex);
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radThayThe.Checked)
            {
                ChucNangDaChon = ChucNangType.ThayThe;
                textBox1.Enabled = true;
            }
            else if (radDaoNguoc.Checked)
            {
                ChucNangDaChon = ChucNangType.DaoNguoc;
                textBox1.Enabled = false;
            }
        }

    }

    public enum ChucNangType
    {
        ThayThe, DaoNguoc
    }
}
