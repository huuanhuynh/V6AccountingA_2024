using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6AccountingBusiness.Invoices;
using V6ControlManager.FormManager.ChungTuManager.PhaiThu.HoaDonCafe;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Structs;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.VitriManager
{
    public partial class VitriKhuCafeControl : V6Control
    {
        public VitriKhuCafeControl()
        {
            InitializeComponent();
            MyInit();
        }

        public VitriKhuCafeControl(string ma_kho)
        {
            InitializeComponent();
            Ma_kho = ma_kho;
            MyInit();
        }

        public string Ma_kho;

        private void MyInit()
        {
            try
            {
                if (V6Setting.IsDesignTime) return;
                if (string.IsNullOrEmpty(Ma_kho))
                {
                    this.ShowWarningMessage("No kho");
                }
                InitHoaDonCafe_Invoice();
                InitTables(V6BusinessHelper.GetServerDateTime());
                Resize += VitriKhuCafeControl_Resize;
            }
            catch (Exception ex)
            {
                this.ShowErrorException("Init", ex);
            }
        }

        void VitriKhuCafeControl_Resize(object sender, EventArgs e)
        {
            FixVitriKhuResize();
        }


        
        //protected SortedDictionary<string, V6Control> ControlsDictionary = new SortedDictionary<string, V6Control>();
        protected SortedDictionary<string, TableButton> ButtonsDictionary = new SortedDictionary<string, TableButton>();

        public delegate void ChuyenKhuEventHandler(object sender, ChuyenKhuEventArgs e);
        public event ChuyenKhuEventHandler ChuyenKhu;
        protected virtual void OnChuyenKhu(string stt_rec, IDictionary<string,object> am_data, string maKhoTo, string maVitriTo)
        {
            var handler = ChuyenKhu;
            if (handler != null) handler(this, new ChuyenKhuEventArgs(stt_rec, am_data, maKhoTo, maVitriTo));
        }

        private void InitTables(DateTime day)
        {
            //Load ma_vitri theo ma_kho
            IDictionary<string, object> keys = new SortedDictionary<string, object>();
            keys.Add("MA_KHO", Ma_kho);
            var data_vitri = V6BusinessHelper.Select(V6TableName.Alvitri, keys, "*").Data;
            if (data_vitri.Rows.Count == 0)
            {
                this.ShowWarningMessage(V6Text.NoData + " Vitri");
                return;
            }

            var data_hoadon = GetDataHoaDon(day);
            //hoadonCafe.AM = data_hoadon;

            var MaVitri_SttRec_dic = data_hoadon.ToDataSortedDictionary("Ma_vitriPH", "Stt_rec");
            var row_dictionary = data_hoadon.ToRowDictionary("Stt_rec");
            //var key_array = key_dic.Keys.ToArray();

            var base_top = panelTable.AutoScrollPosition.Y;
            var base_left = panelTable.AutoScrollPosition.X;
            int count = 0;
            int col_count = 0;
            int row_count = 0;
            int c_top = 0;
            int width = 93, height = 73;
            int max_col = panelTable.Width / width;
            foreach (DataRow row in data_vitri.Rows)
            {
                var ma_vitri = row["MA_VITRI"].ToString().Trim();
                TableButton button = new TableButton();
                button.Ma_vitri = ma_vitri;
                //button.Left = button.Width * count;
                button.Top = row_count * height + base_top;
                button.Left = col_count * width + base_left;
                col_count++;
                if (col_count >= max_col)
                {
                    col_count = 0;
                    row_count++;
                }
                //button_Click
                button.Select1 += button_Select1;
                button.ChangeStatusEvent += button_ChangeStatusEvent;
                //
                //if (count < key_array.Length)
                //    button.Stt_Rec = key_array[count];

                //Nếu vị trí có trong dữ liệu lấy lên
                if (MaVitri_SttRec_dic.ContainsKey(ma_vitri))
                {
                    // Nếu có dữ liệu ban đầu
                    button.Stt_Rec = MaVitri_SttRec_dic[ma_vitri].ToString().Trim();
                    var row1 = row_dictionary[button.Stt_Rec];
                    button.Status = row1["Status"].ToString().Trim();
                    button.TongTT = ObjectAndString.ObjectToDecimal(row1["T_TT_NT"]);
                    button.Time0 = row1["TIME0"].ToString().Left(5);
                    button.Time2 = row1["TIME2"].ToString().Left(5);
                    button.GhiChu = row1["GC_UD1"].ToString().Trim();
                }
                else
                {
                    button.Stt_Rec = "";
                    button.Status = "0";

                    button.Time0 = "";
                    button.Time2 = "";
                    button.GhiChu = "";
                }

                ButtonsDictionary[ma_vitri] = button;
                panelTable.Controls.Add(button);
                count++;
            }
            // Chọn vị trí đầu tiên
            if (count > 0)
            {
                var b = panelTable.Controls[0] as TableButton;
                if(b != null) b.SelectOne();
            }

        }

        /// <summary>
        /// Lọc dữ liệu chưa hoàn tất theo Option
        /// </summary>
        /// <param name="day"></param>
        /// <returns></returns>
        private DataTable GetDataHoaDon(DateTime day)
        {
            var whereAM = string.Format("Ngay_Ct='{0}' and MA_KHOPH = '{1}' and status in ('1','2') ",
                day.ToString("yyyyMMdd"), Ma_kho);
            if (V6Options.V6OptionValues.ContainsKey("M_SOC_GET_ALL_DATE"))
            {
                switch (V6Options.V6OptionValues["M_SOC_GET_ALL_DATE"].Trim())
                {
                    case "0":
                        // Lọc tất cả trong ngày
                        whereAM = string.Format("Ngay_Ct='{0}' and MA_KHOPH = '{1}' and status in ('1','2') ",
                        day.ToString("yyyyMMdd"), Ma_kho);
                        break;
                    case "1":
                        // Lọc tất cả
                        whereAM = string.Format("MA_KHOPH = '{1}' and status in ('1','2') ",
                            null, Ma_kho);
                        break;
                    case "2":
                        // Tuanmh Lọc chứng từ có ngày lớn nhất cho 1 vị trí - lấy status=1,2
                        whereAM = string.Format("MA_KHOPH = '{1}' and status in ('1','2') " +
                                                "and dbo.VFV_D2S(ngay_ct)+MA_KHOPH in (select dbo.VFV_D2S(max(ngay_ct))+MA_KHOPH from AM83 " +
                                                " where (MA_KHOPH = '{1}' and status in ('1','2')) group by MA_KHOPH) ",
                            null, Ma_kho);
                        break;
                    case "3":
                        // Lọc chứng từ có ngày lớn nhất cho 1 vị trí - lấy status=1,2,3
                        whereAM = string.Format("MA_KHOPH = '{1}' and status in ('1','2','3') " +
                                                "and dbo.VFV_D2S(ngay_ct)+MA_KHOPH in (select dbo.VFV_D2S(max(ngay_ct))+MA_KHOPH from AM83 " +
                                                " where (MA_KHOPH = '{1}' and status in ('1','2','3')) group by MA_KHOPH) ",
                            null, Ma_kho);
                        break;
                    case "4":
                        //  Lọc tất cả trong ngày
                        whereAM = string.Format("Ngay_Ct='{0}' and MA_KHOPH = '{1}' and status in ('1','2','3') ",
                        day.ToString("yyyyMMdd"), Ma_kho);
                        break;
                    case "5":
                        //  Lọc tất cả 
                        whereAM = string.Format("MA_KHOPH = '{1}' and status in ('1','2','3') ",
                        null, Ma_kho);
                        break;
                }
            }

            var whereAD = "";
            var data_hoadon = hoadonCafe_Invoice.SearchAM("", whereAM, whereAD, "", "");
            return data_hoadon;
        }

        void button_ChangeStatusEvent(object sender, EventArgs e)
        {
            var button = (TableButton) sender;
            if (button.IsSelect)
            {
                //txtMaVitri.Text = button.Ma_vitri;
            }
        }

        void button_Select1(object sender, EventArgs e)
        {
            _listen_change_mode = false;
            var click_button = (TableButton)sender;
            //if (selected_button != null && selected_button != click_button && (selected_button.Status == "1" || selected_button.Status == "2"))
            //{
            //    hoadonCafe.Luu(Ma_kho, selected_button.Ma_vitri, false);
            //}

            //if (selected_button != null) selected_button.DeSelect();
            selected_button = click_button;
            DeSelectAll(selected_button);
            //ShowInvoice(selected_button);
            //txtMaVitri.Text  = selected_button.Ma_vitri;
        }


        internal V6Invoice83 hoadonCafe_Invoice;
        private TableButton selected_button;
        private bool _listen_change_mode;
        private void InitHoaDonCafe_Invoice()
        {
            hoadonCafe_Invoice = new V6Invoice83();
            //hoadonCafe = new HoaDonCafeControl(ItemID, "Stt_rec???");
            //hoadonCafe.Dock = DockStyle.Fill;
            //hoadonCafe.BillChanged += hoadonCafe_BillChanged;
            //hoadonCafe.ChangeTable += hoadonCafe_ChangeTable;
            ////hoadonCafe.ViewNext += hoadonCafe_ViewNext;
            //hoadonCafe.ResetAllVar += hoadonCafe_ResetAllVar;
            //panelInvoice.Controls.Add(hoadonCafe);
            //hoadonCafe.Focus();
        }

        /// <summary>
        /// Bỏ chọn các nút khác
        /// </summary>
        /// <param name="except"></param>
        private void DeSelectAll(TableButton except)
        {
            foreach (KeyValuePair<string, TableButton> item in ButtonsDictionary)
            {
                var tb = item.Value;
                if (tb != null && tb != except)
                {
                    tb.DeSelect();
                }
            }
        }

        public override bool DoHotKey0(Keys keyData)
        {
            if (keyData == Keys.F6)
            {
                if (selected_button.Status != "1") return false;
                //hoadonCafe_ChangeTable(hoadonCafe, EventArgs.Empty);
                return true;
            }
            return base.DoHotKey0(keyData);
        }
        
        
        private void FixVitriKhuResize()
        {
            var base_top = panelTable.AutoScrollPosition.Y;
            var base_left = panelTable.AutoScrollPosition.X;
            
            //panelTable.Height = Height - 10;
            int col_count = 0;
            int row_count = 0;
            int c_top = 0;
            int width = 93, height = 73;
            int max_col = panelTable.Width/width;
            foreach (KeyValuePair<string, TableButton> item in ButtonsDictionary)
            {
                item.Value.Top = row_count*height + base_top;
                item.Value.Left = col_count*width + base_left;
                col_count++;
                if (col_count >= max_col)
                {
                    col_count = 0;
                    row_count++;
                }
            }
            var newHeight = (row_count + 1)*height;
            if (newHeight > this.Height) newHeight = this.Height;
            panelTable.Height = newHeight;
            //tsFull.Image = Properties.Resources.ZoomOut24;
            //_tsFullText = V6Text.ZoomIn;
        }

        public void RefreshData()
        {
            try
            {
                var new_data = GetDataHoaDon(V6BusinessHelper.GetServerDateTime());
                var MaVitri_SttRec_dic = new_data.ToDataSortedDictionary("Ma_vitriPH", "Stt_rec");
                var row_dictionary = new_data.ToRowDictionary("Stt_rec");
                foreach (KeyValuePair<string, TableButton> item in ButtonsDictionary)
                {
                    var vitri = item.Key;
                    var button = item.Value;
                    //Nếu có dữ liệu cho nút này
                    if (MaVitri_SttRec_dic.ContainsKey(button.Ma_vitri))
                    {
                        var stt_rec = MaVitri_SttRec_dic[button.Ma_vitri.ToUpper()].ToString().Trim();
                        var row_data = row_dictionary[stt_rec];
                        string status = row_data["Status"].ToString().Trim();
                        decimal tongTT = ObjectAndString.ObjectToDecimal(row_data["T_TT_NT"]);
                        string ghiChu = row_data["GC_UD1"].ToString().Trim();
                        var time0 = row_data["Time0"].ToString();
                        var time2 = row_data["Time2"].ToString();

                        button.ChangeStatus(V6Mode.Init, status, stt_rec, 0,
                            tongTT, ghiChu, time0, time2);
                    }
                    else
                    {
                        button.ChangeStatus(V6Mode.Init, "0", "", 0,
                            0, "", "", "");
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".RefreshData", ex);
            }
        }
    }

    //public class ChuyenKhuEventArgs : EventArgs
    //{
    //    public ChuyenKhuEventArgs(string stt_rec, IDictionary<string,object> am_data, string makhoTo, string ma_vitriTo)
    //    {
    //        Stt_rec = stt_rec;
    //        AM_Data = am_data;
    //        Ma_Kho_Den = makhoTo;
    //        Ma_Vitri_Den = ma_vitriTo;
    //    }
    //    public string Ma_Kho_Den;
    //    public string Ma_Vitri_Den;
    //    public IDictionary<string, object> AM_Data;
    //    public string Stt_rec;
    //}
}
