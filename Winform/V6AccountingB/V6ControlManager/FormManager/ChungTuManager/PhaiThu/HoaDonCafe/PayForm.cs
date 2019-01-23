using System;
using System.Windows.Forms;
using V6Controls;
using V6Controls.Forms;

namespace V6ControlManager.FormManager.ChungTuManager.PhaiThu.HoaDonCafe
{
    public partial class PayForm : V6Form
    {
        public PayForm()
        {
            InitializeComponent();
        }
        
        public PayForm(decimal tt, int num_format)
        {
            InitializeComponent();

            ThanhToan = tt;
            KhachDua = ThanhToan;
            txtTongTienNt2.DecimalPlaces = num_format;
            txtKhachDua.DecimalPlaces = num_format;
            txtTraLai.DecimalPlaces = num_format;
        }


        public decimal ThanhToan { get { return txtTongTienNt2.Value; } set { txtTongTienNt2.Value = value; } }
        public decimal KhachDua { get { return txtKhachDua.Value; } set { txtKhachDua.Value = value; } }
        public decimal TraLai { get { return txtTraLai.Value; } set { txtTraLai.Value = value; } }


        private void PayForm_Load(object sender, System.EventArgs e)
        {
            try
            {
                

            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".Load", ex);
            }
            Ready();
        }

        private bool _first_input = true;
        private void InputMoney(int input)
        {
            if (_first_input)
            {
                _first_input = false;
                KhachDua = input;
            }
            else
            {
                KhachDua = KhachDua + input;
            }
        }
        
        private void Nhan(DialogResult drs)
        {
            try
            {
                DialogResult = drs;
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".Nhan", ex);
            }
        }

        public void TinhTienTraLai()
        {
            try
            {
                if(IsReady) TraLai = KhachDua - ThanhToan;
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".TinhTienTraLai", ex);
            }
        }
        
        private void btnNhan_Click(object sender, EventArgs e)
        {
            Nhan(DialogResult.OK);
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            Nhan(DialogResult.Yes);
        }

        private void txtKhachDua_StringValueChange(object sender, StringValueChangeEventArgs e)
        {
            TinhTienTraLai();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InputMoney(50000);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            InputMoney(100000);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            InputMoney(200000);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            InputMoney(500000);
        }

        private void txtKhachDua_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                button1.PerformClick();
            }
            else if (e.KeyCode == Keys.F2)
            {
                button2.PerformClick();
            }
            else if (e.KeyCode == Keys.F3)
            {
                button3.PerformClick();
            }
            else if (e.KeyCode == Keys.F4)
            {
                button4.PerformClick();
            }
            else if (e.KeyCode == Keys.F5)
            {
                KhachDua = ThanhToan;
                _first_input = true;
            }
        }
    }
}
