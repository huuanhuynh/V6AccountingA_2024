using System;
using System.Drawing;
using System.Windows.Forms;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Structs;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ChungTuManager.PhaiThu.HoaDonCafe
{
    public partial class TableButton : V6Control
    {
        public TableButton()
        {
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            lblName.Click += (s,e)=>
            {
                OnClick(e);
            };
            lblStatus.Click += (s, e) =>
            {
                OnClick(e);
            };
            _backColor = BackColor;
        }


        private Color _backColor;
        public event EventHandler Select1;
        protected virtual void OnSelect1()
        {
            IsSelect = true;
            var handler = Select1;
            if (handler != null) handler(this, new EventArgs());
        }

        public event EventHandler MyDoubleClick;
        protected virtual void OnMyDoubleClick()
        {
            var handler = MyDoubleClick;
            if (handler != null) handler(this, new EventArgs());
        }

        public event EventHandler ChangeStatusEvent;
        protected virtual void OnChangeStatus()
        {
            var handler = ChangeStatusEvent;
            if (handler != null) handler(this, new EventArgs());
        }

        private bool iselect;

        public bool IsSelect
        {
            get
            {
                return iselect;
            }
            set
            {
                iselect = value;
                ChangeStatus(_mode, Status, Stt_Rec, CurrentAMIndex_Nouse, TongTT, GhiChu, _time0, _time2);
            }
        }

        public string Stt_Rec;
        private decimal _tongTT;
        private string _ghiChu = "";
        private string _time0 = "";
        private string _time2 = "";

        public decimal TongTT
        {
            get { return _tongTT; }
            set
            {
                _tongTT = value;
                lblTTT.Text = ObjectAndString.NumberToString(_tongTT, 2, V6Options.M_NUM_POINT, ".");
            }
        }

        
        public string GhiChu
        {
            get { return _ghiChu; }
            set
            {
                _ghiChu = value;
                lblGhiChu.Text = _ghiChu;
            }
        }
        
        public string Time0
        {
            get { return _time0; }
            set
            {
                _time0 = value;
                ShowLblStatus();
            }
        }

        public string Time2
        {
            get { return _time2; }
            set
            {
                _time2 = value;
                ShowLblStatus();
            }
        }


        private void SelectView()
        {
            //lblName.ForeColor = Color.Red;
            //BackColor = Color.Gold;
            panel1.BorderStyle = BorderStyle.FixedSingle;
        }
        private void StyleView()
        {
            if (iselect)
            {
                //lblName.ForeColor = Color.Red;
                //BackColor = Color.Gold;
                //panel1.BorderStyle = BorderStyle.FixedSingle;
                var borderColor = Color.Gold;
                labelTop.BackColor = borderColor;
                labelBottom.BackColor = borderColor;
                labelLeft.BackColor = borderColor;
                labelRight.BackColor = borderColor;
            }
            else
            {
                //lblName.ForeColor = Color.Black;
                //BackColor = _backColor;
                //panel1.BorderStyle = BorderStyle.None;
                labelTop.BackColor = Color.Transparent;
                labelBottom.BackColor = Color.Transparent;
                labelLeft.BackColor = Color.Transparent;
                labelRight.BackColor = Color.Transparent;
            }
            
            switch (Status)
            {
                case "0":
                    BackColor = _backColor;
                    break;
                case "1":
                case "2":
                    BackColor = Color.GreenYellow;
                    break;
                case "3":
                    BackColor = Color.Red;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Tên bàn, ma_vitri
        /// </summary>
        public string Ma_vitri
        {
            get { return lblName.Text; }
            set { lblName.Text = value; }
        }

        private V6Mode _mode = V6Mode.Init;
        public string Status = "";
        /// <summary>
        /// Không dùng
        /// </summary>
        public int CurrentAMIndex_Nouse;
        public V6Mode Mode { get { return _mode; } set { _mode = value; } }

        /// <summary>
        /// Như click vào
        /// </summary>
        public void SelectOne()
        {
            OnSelect1();
        }

        private void TableButton_Click(object sender, EventArgs e)
        {
            SelectOne();
        }

        /// <summary>
        /// Đổi trạng thá IsSelect = false.
        /// </summary>
        public void DeSelect()
        {
            IsSelect = false;
        }



        public void ChangeStatus(V6Mode mode, string status, string stt_rec, int currentAmIndex,
            decimal tongTT, string ghiChu, string time0, string time2)
        {
            try
            {
                _mode = mode;
                Status = status;
                Stt_Rec = stt_rec;
                TongTT = tongTT;
                GhiChu = ghiChu;
                _time0 = time0;
                _time2 = time2;
                ShowLblStatus();

                CurrentAMIndex_Nouse = currentAmIndex;
                StyleView();
                

                OnChangeStatus();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".ChangeStatus", ex);
            }
        }

        private void ShowLblStatus()
        {
            switch (Status)
            {
                case "0":
                    lblStatus.Text = "Trống";
                    break;
                case "1":
                    lblStatus.Text = _time0.Left(5);
                    break;
                case "2":
                    lblStatus.Text = _time0.Left(5);
                    break;
                case "3":
                    lblStatus.Text = _time0.Left(5) + "-" + _time2.Left(5);
                    break;
            }
        }

        private void lblGhiChu_TextChanged(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(lblGhiChu, lblGhiChu.Text);
        }

        private void TableButton_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            OnMyDoubleClick();
        }
    }

    //Select-Have
    public enum TableStatus
    {
        KhongChon_KhongKhach = 0,
        DangChon_KhongKhach = 1,
        KhongChon_CoKhach = 2,
        DangChon_CoKhach = 3,
    }
}
