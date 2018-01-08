using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using V6Controls;
using V6Controls.Forms;
using V6Tools;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.KhoHangManager
{
    public partial class KhoHangControl : V6FormControl
    {
        private Point _p = new Point(0, 0);
        private DataTable _data;
        private SortedList<string, DayHangControl> _listDay;
        public KhoParams KhoParams { get; set; }
        public KhoHangControl()
        {
            InitializeComponent();
        }

        public KhoHangControl(KhoParams kparas)
        {
            InitializeComponent();
            KhoParams = kparas;
            lblKho.Text = KhoParams.MA_KHO;
            SetData(KhoParams.Data);
        }

        /// <summary>
        /// Dùng data để tạo các dãy
        /// </summary>
        /// <param name="data"></param>
        private void SetData(DataTable data)
        {
            try
            {
                _data = data;
                _listDay = new SortedList<string, DayHangControl>();
                Thread T = new Thread(SetDataThread);
                Timer timer = new Timer();
                timer.Tick += timer_Tick;
                total = _data.Rows.Count;
                count = 0;
                running = true;
                T.Start();
                timer.Start();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".SetData", ex);
            }
        }

        void timer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (running)
                {
                    progressBar1.Value = count*100/total;
                }
                else
                {
                    ((Timer)sender).Stop();
                    progressBar1.Value = 100;
                    if (success)
                    {
                        progressBar1.Visible = false;
                        foreach (KeyValuePair<string, DayHangControl> item in _listDay)
                        {
                            panel1.Controls.Add(item.Value);
                        }
                    }
                    else
                    {
                        this.ShowErrorMessage(GetType() + ".Error");
                    }
                    OnAddControlsFinish();
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".timer_Tick", ex);
            }
        }

        private bool running, success;
        private int count, total;

        private void SetDataThread()
        {
            running = true;
            try
            {
                foreach (DataRow row in _data.Rows)
                {
                    AddRow(row);
                    count ++;
                }
                success = true;
            }
            catch (Exception ex)
            {
                success = false;
                this.WriteExLog(GetType() + ".SetDataThread", ex);
            }
            running = false;
        }

        /// <summary>
        /// Thêm hàng cho các dãy...
        /// </summary>
        /// <param name="row"></param>
        private void AddRow(DataRow row)
        {
            try
            {
                var ID = row["CODE"].ToString().Substring(0, 1);
                if (_listDay.ContainsKey(ID))
                {
                    var kho = _listDay[ID];
                    kho.AddRow(row);
                }
                else
                {
                    AddDayHang(row);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".AddRow", ex);
            }
        }

        private void AddDayHang(DataRow row)
        {
            try
            {
                DayHangControl day = new DayHangControl(KhoParams, row);
                _listDay.Add(day.ID, day);
                day.Location = new Point(_p.X, _p.Y);
                _p = new Point(_p.X, _p.Y + day.Height);
                //panel1.Controls.Add(kho);
                day.SizeChanged += day_SizeChanged;
                Resort();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".AddDayHang", ex);
            }
        }

        private void Resort()
        {
            try
            {
                var cTop = 0;
                for (int i = 0; i < _listDay.Count; i++)
                {
                    var day_hang = _listDay.Values[i];
                    day_hang.Top = cTop;
                    cTop += day_hang.Height + 1;
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".Resort", ex);
            }
        }

        void day_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                Resort();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        //public override bool DoHotKey0(Keys keyData)
        //{
        //    try
        //    {
        //        if (keyData == Keys.Escape)
        //        {
        //            Dispose();
        //            return true;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        // ignored
        //    }
        //    return false;
        //}
        public event Action AddControlsFinish;

        protected virtual void OnAddControlsFinish()
        {
            var handler = AddControlsFinish;
            if (handler != null) handler();
        }

        public void ClearDataVitriVaTu()
        {
            try
            {
                foreach (KeyValuePair<string, DayHangControl> item in _listDay)
                {
                    item.Value.ClearDataVitriVaTu();
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(string.Format("{0}.{1}", GetType(), MethodBase.GetCurrentMethod().Name), ex);
            }
        }

        public void SetDataViTriVatTu(DataTable dataVitriVattu)
        {
            try
            {
                Dictionary<string, string> errors = new Dictionary<string, string>();
                foreach (DataRow row in dataVitriVattu.Rows)
                {
                    var cVitri = row["MA_VITRI"].ToString().Trim();
                    var cMavt = row["MA_VT"].ToString().Trim();
                    var ma_day = (cVitri == null || cVitri.Length < 3) ? "" : cVitri.Substring(2, 1);//////
                    
                    if (_listDay.ContainsKey(ma_day))
                    {
                        _listDay[ma_day].SetDataVitriVatTu(row, cVitri, cMavt);
                    }
                    else
                    {
                        errors[ma_day] = "Khong co day: " + ma_day;
                    }
                }

                if (errors.Count > 0)
                {
                    ShowMainMessage(errors.Aggregate("", (current, error) => current + error.Value + "\n"));
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(string.Format("{0}.{1}", GetType(), MethodBase.GetCurrentMethod().Name), ex);
            }
        }

        /// <summary>
        /// Gán màu gạch chân cho tất cả các vị trí có cùng mã vật tư.
        /// </summary>
        /// <param name="ma_vt"></param>
        /// <param name="color"></param>
        public void SetColorMavt(string ma_vt, Color color)
        {
            try
            {
                foreach (KeyValuePair<string, DayHangControl> item in _listDay)
                {
                    item.Value.SetColorMavt(ma_vt, color);
                }
                panel1.Focus();
            }
            catch (Exception ex)
            {
                this.WriteExLog(string.Format("{0}.{1}", GetType(), MethodBase.GetCurrentMethod().Name), ex);
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            panel1.Focus();
        }

        public void DrawToGraphics(Graphics graphics, Point basePoint)
        {
            Rectangle rec = new Rectangle(basePoint, new Size(Width, Height));
        }
    }
}
