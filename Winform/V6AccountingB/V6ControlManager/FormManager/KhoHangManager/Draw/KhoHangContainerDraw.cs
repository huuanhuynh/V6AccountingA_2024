using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using HaUtility.Helper;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Controls.Forms.DanhMuc.Add_Edit;
using V6Init;
using V6Structs;
using V6Tools;

namespace V6ControlManager.FormManager.KhoHangManager.Draw
{
    public partial class KhoHangContainerDraw : V6FormControl
    {

        public event HandleData V6Click;
        protected virtual void OnV6Click(IDictionary<string, object> data)
        {
            var handler = V6Click;
            if (handler != null) handler(data);
        }

        public KhoHangContainerDraw()
        {
            InitializeComponent();
        }

        public KhoHangContainerDraw(KhoParams kparas)
        {
            InitializeComponent();
            _khoParams = kparas;
            _khoHang = new KhoHangControlDraw();
            _khoHang.AddControlsFinish += delegate
            {
                pictureBox1.Invalidate();
            };
            txtTime.Visible = kparas.RunTimer;
            LoadComboboxSource();
            LoadDefaultData(4, "", kparas.Program, _khoParams.ItemId);
        }

        #region ==== Properties ====
        private Point _p = new Point(0, 0);
        private DataTable _data;
        private DataTable MauInData;
        public DataRow MauInSelectedRow
        {
            get
            {
                return MauInData == null || MauInData.Rows.Count == 0 ? null : MauInData.Rows[0];
            }
        }
        public DateTime _cuoiNgay { get; set; }
        public string _mavt { get; set; }

        private KhoHangControlDraw _khoHang;

        public KhoParams _khoParams { get; set; }

        public string Report_GRDSV1
        {
            get
            {
                var result = "";
                if (MauInSelectedRow != null)
                {

                    result = MauInSelectedRow["GRDS_V1"].ToString().Trim();
                }
                return result;
            }
        }
        public string Report_GRDSV2
        {
            get
            {
                var result = "";
                if (MauInSelectedRow != null)
                {

                    result = MauInSelectedRow["GRDS_V2"].ToString().Trim();
                }
                return result;
            }
        }
        public string Report_GRDFV1
        {
            get
            {
                var result = "";
                if (MauInSelectedRow != null)
                {

                    result = MauInSelectedRow["GRDF_V1"].ToString().Trim();
                }
                return result;
            }
        }
        public string Report_GRDFV2
        {
            get
            {
                var result = "";
                if (MauInSelectedRow != null)
                {

                    result = MauInSelectedRow["GRDF_V2"].ToString().Trim();
                }
                return result;
            }
        }
        public string Report_GRDHV_V1
        {
            get
            {
                var result = "";
                if (MauInSelectedRow != null)
                {

                    result = MauInSelectedRow["GRDHV_V1"].ToString().Trim();
                }
                return result;
            }
        }
        public string Report_GRDHE_V1
        {
            get
            {
                var result = "";
                if (MauInSelectedRow != null)
                {

                    result = MauInSelectedRow["GRDHE_V1"].ToString().Trim();
                }
                return result;
            }
        }
        public string Report_GRDHV_V2
        {
            get
            {
                var result = "";
                if (MauInSelectedRow != null)
                {

                    result = MauInSelectedRow["GRDHV_V2"].ToString().Trim();
                }
                return result;
            }
        }
        public string Report_GRDHE_V2
        {
            get
            {
                var result = "";
                if (MauInSelectedRow != null)
                {

                    result = MauInSelectedRow["GRDHE_V2"].ToString().Trim();
                }
                return result;
            }
        }

        #endregion properties

        private void LoadComboboxSource()
        {
            MauInData = Albc.GetMauInData(_khoParams.Program, "", "", "");
            //GetSumCondition();
        }

        public void GetAndSetData()
        {
            try
            {
                FormManagerHelper.HideMainMenu();
                _cuoiNgay = dateCuoiNgay.Date;
                _mavt = txtMavt.Text.Trim();

                DataTable data;
                if (txtMaKho.Text == "")
                {
                    data = V6BusinessHelper.Select("APALETT").Data;
                }
                else
                {
                    data = V6BusinessHelper.Select("APALETT", "*", "ma_kho=@ma_kho", "", "", new SqlParameter("@ma_kho", txtMaKho.Text)).Data;
                }
                
                SetData(data);
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
                if (_khoHang != null)
                {
                    txtMavt.Text = ma_vt;
                    txtMavt.ExistRowInTable();
                    _khoHang.SetColorMavt(ma_vt, color);
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".SetColorMavt", ex);
            }
        }

        private void SetData(DataTable data)
        {
            try
            {
                _data = data;
                CreatePicture();
                CreateObjects();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".SetData", ex);
            }
        }

        private void CreateObjects()
        {
            _khoHang.SetData(_data);
            //_khoHang.SetDataViTriVatTu(_data);
        }

        private void CreatePicture()
        {
            if (pictureBox1.Image != null) return;

            int image_width = 800;
            int image_height = 800;

            //Image image = new Bitmap(image_width, image_height);
            //pictureBox1.Image = image;
        }

        void _khoHang_V6Click(IDictionary<string, object> data)
        {
            OnV6Click(data);
        }

        private void ClearDataVitriVaTu()
        {
            try
            {
                if (_khoHang == null) return;
                _khoHang.ClearDataVitriVaTu();
            }
            catch (Exception ex)
            {
                this.WriteExLog(string.Format("{0}.{1}", GetType(), MethodBase.GetCurrentMethod().Name), ex);
            }
        }

        private void SetDataViTriVatTu()
        {
            if (_khoHang == null) return;
            var condition = string.Format("MA_KHO='{0}'", txtMaKho.Text.Replace("'", "''"));
            //txtMavt.Text.Trim() == "" ? "" : string.Format("and MA_VT='{0}'", txtMavt.Text.Replace("'", "''")));
            //tuanmh 18/06/2017
            if (_khoParams.Program == "AINVITRI03")
            {
                if (txtMavt.Text.Trim() != "")
                {
                    condition = condition + string.Format(" and MA_VT='{0}'", txtMavt.Text.Replace("'", "''"));
                }
            }

            SqlParameter[] plist =
            {
                new SqlParameter("@EndDate", _cuoiNgay.ToString("yyyyMMdd")),
                new SqlParameter("@Condition", condition),
                new SqlParameter("@Vttonkho", "1"),
                new SqlParameter("@Kieu_in", "1"),
                new SqlParameter("@Makho",  txtMaKho.Text.Trim()),
                new SqlParameter("@Mavt", txtMavt.Text.Trim()),
            };
            var data_vitri_vattu = V6BusinessHelper.ExecuteProcedure(_khoParams.Program, plist).Tables[0];
            _khoHang.SetDataViTriVatTu(data_vitri_vattu);
            //_khoHang.Focus();

            if (txtMavt.Text != "") SetColorMavt(txtMavt.Text, Color.Blue);
        }


        private bool running, success;
        private int count, total;


        public override bool DoHotKey0(Keys keyData)
        {
            try
            {
                if (keyData == Keys.Escape)
                {
                    Dispose();
                    return true;
                }
            }
            catch (Exception)
            {
                // ignored
            }
            return false;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            GetAndSetData();
        }

        private void btnSuaTTMauBC_Click(object sender, EventArgs e)
        {
            try
            {
                if (MauInData == null || MauInData.Rows.Count == 0) return;
                var row0 = MauInSelectedRow;
                var keys = new SortedDictionary<string, object>
                    {
                        {"MA_FILE", row0["MA_FILE"].ToString().Trim()},
                        {"MAU", row0["MAU"].ToString().Trim()},
                        {"LAN", row0["LAN"].ToString().Trim()},
                        {"REPORT", row0["REPORT"].ToString().Trim()}
                    };
                var f2 = new FormAddEdit(V6TableName.Albc, V6Mode.Edit, keys, null);
                f2.AfterInitControl += f_AfterInitControl;
                f2.InitFormControl();
                f2.SetFather(this);
                f2.ShowDialog(this);
                if (f2.UpdateSuccess)
                {
                    LoadComboboxSource();
                };
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".btnSuaTTMauBC_Click: " + ex.Message);
            }
            SetStatus2Text();
        }

        void f_AfterInitControl(object sender, EventArgs e)
        {
            LoadAdvanceControls((Control)sender, "Albc");
        }

        protected void LoadAdvanceControls(Control form, string ma_ct)
        {
            try
            {
                FormManagerHelper.CreateAdvanceFormControls(form, ma_ct, new Dictionary<string, object>());
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".LoadAdvanceControls " + _sttRec, ex);
            }
        }

        private void btnThemMauBC_Click(object sender, EventArgs e)
        {
            try
            {
                if (MauInSelectedRow != null) return;

                ConfirmPasswordV6 f_v6 = new ConfirmPasswordV6();
                if (f_v6.ShowDialog(this) == DialogResult.OK)
                {
                    SortedDictionary<string, object> data0 = null;
                    //var viewt = new DataView(MauInData);
                    //viewt.RowFilter = "mau='" + MAU + "'" + " and lan='" + LAN + "'";
                    var keys = new SortedDictionary<string, object>
                    {
                        {"MA_FILE", _khoParams.Program},
                        {"MAU", "VN"},
                        {"LAN", "V"},
                        {"REPORT", _khoParams.Program}
                    };
                    if (MauInData == null || MauInData.Rows.Count == 0)
                    {
                        data0 = new SortedDictionary<string, object>();
                        data0.AddRange(keys);
                        data0["CAPTION"] = _khoParams.Program;
                        data0["CAPTION2"] = _khoParams.Program;
                        data0["TITLE"] = _khoParams.Program;
                        data0["FirstAdd"] = "1";
                    }

                    var f2 = new FormAddEdit(V6TableName.Albc, V6Mode.Add, keys, data0);
                    f2.AfterInitControl += f_AfterInitControl;
                    f2.InitFormControl();
                    f2.SetFather(this);
                    f2.ShowDialog(this);
                    if (f2.InsertSuccess)
                    {
                        LoadComboboxSource();
                    };
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".ThemMauBC_Click: " + ex.Message);
            }
            SetStatus2Text();
        }

        private void txtMavt_V6LostFocus(object sender)
        {
            _mavt = txtMavt.Text.Trim();
            SetColorMavt(txtMavt.Text, Color.Blue);
        }

        private int timeCount = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            timeCount++;

            if (timeCount >= txtTime.Value)
            {
                timeCount = 0;
                ClearDataVitriVaTu();
                SetDataViTriVatTu();
            }
        }

        private void dateCuoiNgay_ValueChanged(object sender, EventArgs e)
        {
            //Tuanmh 17/06/2017
            _cuoiNgay = dateCuoiNgay.Date;
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            //if (loaded)
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                DrawOnPictureBoxPaint(e.Graphics);
            }
        }

        private void DrawOnPictureBoxPaint(Graphics graphics)
        {
            //throw new NotImplementedException();
            Point[] polygon =
            {
                new Point(1,1),
                new Point(100,1),
                new Point(100,100),
            };
            HDrawing.DrawPolygon(graphics, polygon, Color.Blue, 5);
            // Nên làm danh sách objec chứa các thông tin sẵn để vẽ.
            _khoHang.DrawToGraphics(graphics, new Point(0, 0));
            // no // Mỗi lần load lại sẽ có ds object mới. so cái nào mới thì vẽ?
        }
    }
}
