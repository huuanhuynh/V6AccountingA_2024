using System;
using System.Collections.Generic;
using System.Drawing;
using System.Data;
using System.Reflection;
using V6Controls;
using V6Controls.Forms;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.KhoHangManager
{
    /// <summary>
    /// Kệ hàng
    /// </summary>
    public partial class KeHangControl : V6Control
    {
        private Point _p = new Point(0, 0);
        public KhoParams KhoParams { get; set; }
        /// <summary>
        /// 2 KÝ TỰ ĐẦU CỦA CODE
        /// </summary>
        public string CODE_KE { get; set; }
        private Dictionary<string, ViTriControl> _listVitri = new Dictionary<string, ViTriControl>();

        public event HandleData V6Click;
        protected virtual void OnV6Click(IDictionary<string, object> data)
        {
            var handler = V6Click;
            if (handler != null) handler(data);
        }

        public KeHangControl()
        {
            InitializeComponent();
        }

        public KeHangControl(KhoParams kparas, DataRow row)
        {
            InitializeComponent();
            KhoParams = kparas;
            CODE_KE = KhoHangHelper.GetCodeKe_FromCode(row["CODE"].ToString());
            AddViTri(row);
        }

        public void AddRow(DataRow row)
        {
            try
            {
                var ma_vitri = row["MA_VITRI"].ToString().Trim().ToUpper();
                var maVitri_Short = KhoHangHelper.GetMaVitriShort(ma_vitri);
                
                if (_listVitri.ContainsKey(maVitri_Short))
                {
                    var vitriDetail = new ViTriDetail(row);
                    _listVitri[maVitri_Short].AddDetail(vitriDetail);
                }
                else
                {
                    AddViTri(row);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".AddRow", ex);
            }
        }

        private void AddViTri(DataRow row)
        {
            try
            {
                ViTriControl vitri = new ViTriControl(KhoParams, row);
                var vtd = vitri._listVitriDetail[0];

                var index = ObjectAndString.ObjectToInt(vtd.HANG);
                vitri.Left = vitri.Width*index - vitri.Width;
                vitri.Text = vitri.Name;
                //vitri.Click += vitri_Click;
                vitri.V6Click += vitri_V6Click;
                _listVitri.Add(vtd.MA_VITRI_SHORT, vitri);
                vitri.Location = new Point(_p.X, _p.Y);
                _p = new Point(_p.X + vitri.Width, _p.Y);
                Controls.Add(vitri);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".AddViTri", ex);
            }
        }

        void vitri_V6Click(IDictionary<string, object> data)
        {
            OnV6Click(data);
        }

        //void vitri_Click(object sender, EventArgs e)
        //{
        //    try
        //    {

        //    }
        //    catch (Exception ex)
        //    {
        //        this.WriteExLog(GetType() + ".vitri_Click", ex);
        //    }
        //}

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
                this.WriteExLog(string.Format("{0}.{1}", GetType(), MethodBase.GetCurrentMethod().Name), ex);
            }
        }

        public void SetDataVitriVatTu(DataRow row, string cVitri, string cMavt)
        {
            try
            {
                var ma_vitri_short = KhoHangHelper.GetMaVitriShort(cVitri);
                _listVitri[ma_vitri_short].SetDataVitriVatTu(row, cVitri, cMavt);
            }
            catch (Exception ex)
            {
                this.WriteExLog(string.Format("{0}.{1}", GetType(), MethodBase.GetCurrentMethod().Name), ex);
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
                this.WriteExLog(string.Format("{0}.{1}", GetType(), MethodBase.GetCurrentMethod().Name), ex);
            }
        }

        
    }
}
