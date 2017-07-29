using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using V6Controls;
using V6Controls.Forms;
using V6Tools;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.KhoHangManager
{
    /// <summary>
    /// Kệ hàng
    /// </summary>
    public partial class KeHangControlDraw
    {
        private Point _p = new Point(0, 0);
        public KhoParams KhoParams { get; set; }
        /// <summary>
        /// ABC
        /// </summary>
        public string ID { get; set; }

        public int Top { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        private Dictionary<string, ViTriControl> _listVitri;

        public KeHangControlDraw()
        {
            //InitializeComponent();
            _listVitri = new Dictionary<string, ViTriControl>();
        }

        public KeHangControlDraw(KhoParams kparas, DataRow row)
        {
            //InitializeComponent();
            KhoParams = kparas;
            _listVitri = new Dictionary<string, ViTriControl>();
            ID = row["CODE"].ToString().Substring(0, 2).ToUpper();
            AddViTri(row);
        }

        public void AddRow(DataRow row)
        {
            try
            {
                var ma_vitri = row["MA_VITRI"].ToString().Trim().ToUpper();
                if (_listVitri.ContainsKey(ma_vitri))
                {
                    //var vitri = _listVitri[ID];
                    //vitri.AddRow(row);
                }
                else
                {
                    AddViTri(row);
                }
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.WriteExLog(GetType() + ".AddRow", ex);
            }
        }

        private void AddViTri(DataRow row)
        {
            try
            {
                ViTriControl vitri = new ViTriControl(KhoParams, row);

                var index = ObjectAndString.ObjectToInt(vitri.HANG);
                vitri.Left = vitri.Width*index - vitri.Width;
                vitri.Text = vitri.Name;
                vitri.Click += vitri_Click;
                _listVitri.Add(vitri.Name, vitri);
                vitri.Location = new Point(_p.X, _p.Y);
                _p = new Point(_p.X + vitri.Width, _p.Y);
                //Controls.Add(vitri);
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.WriteExLog(GetType() + ".AddViTri", ex);
            }
        }

        void vitri_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                V6ControlFormHelper.WriteExLog(GetType() + ".vitri_Click", ex);
            }
        }

        public void ClearDataVitriVaTu()
        {
            try
            {
                foreach (KeyValuePair<string, ViTriControl> item in _listVitri)
                {
                    item.Value.ClearDataVitriVaTu();
                }
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.WriteExLog(string.Format("{0}.{1}", GetType(), MethodBase.GetCurrentMethod().Name), ex);
            }
        }

        public void SetDataVitriVatTu(DataRow row, string cVitri, string cMavt)
        {
            try
            {
                _listVitri[cVitri].SetDataVitriVatTu(row, cVitri, cMavt);
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.WriteExLog(string.Format("{0}.{1}", GetType(), MethodBase.GetCurrentMethod().Name), ex);
            }
        }

        public void SetColorMavt(string maVt, Color color)
        {
            try
            {
                foreach (KeyValuePair<string, ViTriControl> item in _listVitri)
                {
                    item.Value.SetColorMavt(maVt, color);
                }
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.WriteExLog(string.Format("{0}.{1}", GetType(), MethodBase.GetCurrentMethod().Name), ex);
            }
        }

        
    }
}
