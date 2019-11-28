using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using V6Controls.Forms;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.KhoHangManager.Draw
{
    public class KhoHangControlDraw
    {
        private Point _p = new Point(0, 0);
        private DataTable _data;
        private SortedList<string, DayHangControlDraw> _listDay = new SortedList<string, DayHangControlDraw>();
        public KhoParams KhoParams { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        /// <summary>
        /// Độ cao cố định của mỗi dãy
        /// </summary>
        private int day_height = 150;

        
        /// <summary>
        /// Dùng data để tạo các dãy
        /// </summary>
        /// <param name="data"></param>
        public void SetData(DataTable data)
        {
            try
            {
                _data = data;
                running = true;
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
                V6ControlFormHelper.WriteExLog(GetType() + ".SetData", ex);
            }
        }

        public int progressBar1_Value;
        void timer_Tick(object sender, EventArgs e)
        {
            try
            {
                if (running)
                {
                    progressBar1_Value = count*100/total;
                }
                else
                {
                    ((Timer)sender).Stop();
                    //progressBar1.Value = 100;
                    if (success)
                    {
                        progressBar1_Value = 100;
                        //foreach (KeyValuePair<string, DayHangControlDraw> item in _listDay)
                        //{
                        //    //panel1.Controls.Add(item.Value);
                        //}
                    }
                    else
                    {
                        V6ControlFormHelper.ShowErrorMessage(GetType() + ".Error");
                    }
                    OnAddControlsFinish();
                }
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.WriteExLog(GetType() + ".timer_Tick", ex);
            }
        }

        private bool running, success;
        private int count, total;
        private string lblKho_Text;

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
                V6ControlFormHelper.WriteExLog(GetType() + ".SetDataThread", ex);
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
                var CODE_DAY = KhoHangHelper.GetCodeDay_FromCode(row["CODE"].ToString());
                var MA_KHO = row["MA_KHO"].ToString().Trim();
                string ID_DAY = MA_KHO + CODE_DAY;

                if (_listDay.ContainsKey(ID_DAY))
                {
                    _listDay[ID_DAY].AddRow(row);
                }
                else
                {
                    AddDayHang(row);
                }
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.WriteExLog(GetType() + ".AddRow", ex);
            }
        }

        private void AddDayHang(DataRow row)
        {
            try
            {
                var CODE_DAY = KhoHangHelper.GetCodeDay_FromCode(row["CODE"].ToString());
                var MA_KHO = row["MA_KHO"].ToString().Trim();
                string ID_DAY = MA_KHO + CODE_DAY;

                DayHangControlDraw day = _listDay.ContainsKey(CODE_DAY) ? _listDay[CODE_DAY] : new DayHangControlDraw(ID_DAY, KhoParams, row);
                day.AddRow(row);
                day.Height += 25;
                day.Location = new Point(0, _listDay.Count * day_height);
                if (!_listDay.ContainsKey(ID_DAY))
                {
                    _listDay[ID_DAY] = day;
                }
                
                //Resort();
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.WriteExLog(GetType() + ".AddDayHang", ex);
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
                V6ControlFormHelper.WriteExLog(GetType() + ".Resort", ex);
            }
        }

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
                foreach (KeyValuePair<string, DayHangControlDraw> item in _listDay)
                {
                    item.Value.ClearDataVitriVaTu();
                }
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.WriteExLog(string.Format("{0}.{1}", GetType(), MethodBase.GetCurrentMethod().Name), ex);
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
                    var MA_KHO = row["MA_KHO"].ToString().Trim();
                    //var cMavt = row["MA_VT"].ToString().Trim();
                    var cMavt = cVitri;
                    var CODE_DAY = (cVitri.Length < 3) ? "" : cVitri.Substring(2, 1);//////
                    var ID_DAY = MA_KHO + CODE_DAY;
                    if (_listDay.ContainsKey(ID_DAY))
                    {
                        _listDay[ID_DAY].SetDataVitriVatTu(row, cVitri, cMavt);
                    }
                    else
                    {
                        AddDayHang(row);
                        //errors[ID_DAY] = "Khong co day: " + ID_DAY;
                    }
                }

                if (errors.Count > 0)
                {
                    V6ControlFormHelper.ShowMainMessage(errors.Aggregate("", (current, error) => current + error.Value + "\n"));
                }
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.WriteExLog(string.Format("{0}.{1}", GetType(), MethodBase.GetCurrentMethod().Name), ex);
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
                foreach (KeyValuePair<string, DayHangControlDraw> item in _listDay)
                {
                    item.Value.SetColorMavt(ma_vt, color);
                }
                //panel1.Focus();
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.WriteExLog(string.Format("{0}.{1}", GetType(), MethodBase.GetCurrentMethod().Name), ex);
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            //panel1.Focus();
        }

        /// <summary>
        /// Vẽ các dãy hàng.
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="basePoint"></param>
        public void DrawToGraphics(Graphics graphics, Point basePoint)
        {
            if (running) return;

            Rectangle rec = new Rectangle(basePoint, new Size(Width, Height));
            Pen pen = new Pen(Color.Black);
            graphics.DrawRectangle(pen, rec);

            //Point basePoint = new Point();
            foreach (KeyValuePair<string, DayHangControlDraw> dayItem in _listDay)
            {
                var day = dayItem.Value;
                day.DrawToGraphics(graphics, basePoint);
                basePoint.Y += day.Height; // day.Height có thể bị thay đổi sau khi vẽ xong ở dòng trên.
            }
        }
    }
}
