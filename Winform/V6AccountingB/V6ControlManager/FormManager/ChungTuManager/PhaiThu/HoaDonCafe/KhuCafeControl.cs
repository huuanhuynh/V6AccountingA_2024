using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ChungTuManager.PhaiThu.HoaDonCafe
{
    public partial class KhuCafeControl : V6Control
    {
        public KhuCafeControl()
        {
            InitializeComponent();
            //MyInit();
        }

        public KhuCafeControl(string maCt, string ma_kho)
        {
            InitializeComponent();
            Ma_kho = ma_kho;
            MyInit(maCt);
        }

        public string Ma_kho;

        private void MyInit(string maCt)
        {
            try
            {
                if (V6Setting.IsDesignTime) return;
                if (string.IsNullOrEmpty(Ma_kho))
                {
                    this.ShowWarningMessage("No kho");
                }

                txtMaVitri.SetInitFilter(string.Format("Ma_kho='{0}'", Ma_kho));

                InitHoaDonCafeControl(maCt);

                InitTables(V6BusinessHelper.GetServerDateTime());
            }
            catch (Exception ex)
            {
                this.ShowErrorException("Init", ex);
            }
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
                this.ShowMainMessage(V6Text.NoData + " Vitri");
                return;
            }

            //Lọc dữ liệu chưa hoàn tất trong ngày.
            var whereAM = string.Format("Ngay_Ct='{0}' and MA_KHOPH = '{1}' and status in ('1','2') ",
                day.ToString("yyyyMMdd"), Ma_kho);
            if (V6Options.ContainsKey("M_SOC_GET_ALL_DATE"))
            {
                switch (V6Options.GetValue("M_SOC_GET_ALL_DATE").Trim())
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
            var data_hoadon = hoadonCafe.Invoice.SearchAM("", whereAM, whereAD, "", "");
            hoadonCafe.AM = data_hoadon;

            var MaVitri_SttRec_dic = data_hoadon.ToDataSortedDictionary("Ma_vitriPH", "Stt_rec");
            var row_dictionary = data_hoadon.ToRowDictionary("Stt_rec");
            //var key_array = key_dic.Keys.ToArray();
            
            int count = 0;
            foreach (DataRow row in data_vitri.Rows)
            {
                var ma_vitri = row["MA_VITRI"].ToString().Trim();
                TableButton button = new TableButton();
                button.Ma_vitri = ma_vitri;
                button.Left = button.Width * count;
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

        void button_ChangeStatusEvent(object sender, EventArgs e)
        {
            var button = (TableButton) sender;
            if (button.IsSelect)
            {
                txtMaVitri.Text = button.Ma_vitri;
            }
        }

        void button_Select1(object sender, EventArgs e)
        {
            _listen_change_mode = false;
            var click_button = (TableButton)sender;
            if (selected_button != null && selected_button != click_button && (selected_button.Status == "1" || selected_button.Status == "2"))
            {
                hoadonCafe.Luu(Ma_kho, selected_button.Ma_vitri, false);
            }

            //if (selected_button != null) selected_button.DeSelect();
            selected_button = click_button;
            DeSelectAll(selected_button);
            ShowInvoice(selected_button);
            //txtMaVitri.Text  = selected_button.Ma_vitri;
        }


        internal HoaDonCafeControl hoadonCafe;
        private TableButton selected_button;
        private bool _listen_change_mode;
        private void InitHoaDonCafeControl(string maCt)
        {
            hoadonCafe = new HoaDonCafeControl(maCt, ItemID, "Stt_rec???");

            hoadonCafe.Dock = DockStyle.Fill;
            hoadonCafe.BillChanged += hoadonCafe_BillChanged;
            hoadonCafe.ChangeTable += hoadonCafe_ChangeTable;
            //hoadonCafe.ViewNext += hoadonCafe_ViewNext;
            hoadonCafe.ResetAllVar += hoadonCafe_ResetAllVar;

            panelInvoice.Controls.Add(hoadonCafe);
            hoadonCafe.Focus();
        }

        void hoadonCafe_ResetAllVar(object sender, EventArgs e)
        {
            var hdCafe = sender as HoaDonCafeControl;
            if (hdCafe != null)
            {
                hdCafe.MA_KHOPH = Ma_kho;
                if (selected_button != null) hdCafe.MA_VITRIPH = selected_button.Ma_vitri;
            }
        }

        void hoadonCafe_ViewNext(object sender, EventArgs e)
        {
            //var hdcafe = sender as HoaDonCafeControl;
            //if (hdcafe != null)
            //{
            //    if (ButtonsDictionary.ContainsKey(hdcafe.MA_VITRIPH))
            //    {
            //        ButtonsDictionary[hdcafe.MA_VITRIPH].IsSelect = true;
            //    }
            //}
            //var next_button = panelTable.GetNextControl(selected_button, true) as TableButton;
            //if (next_button != null)
            //{
            //    next_button.SelectOne();
            //}
        }

        void hoadonCafe_BillChanged(object sender, EventArgs e)
        {
            try
            {
                var hdcafe = sender as HoaDonCafeControl;
                if (hdcafe != null)
                {
                    if (_listen_change_mode)
                    {
                        if (hdcafe.MA_VITRIPH != selected_button.Ma_vitri)
                        {
                            if (ButtonsDictionary.ContainsKey(hdcafe.MA_VITRIPH))
                            {
                                selected_button.IsSelect = false;
                                selected_button = ButtonsDictionary[hdcafe.MA_VITRIPH];
                                selected_button.IsSelect = true;
                            }
                        }
                        //Hiển thị lại thông tin của bill (form)
                        selected_button.ChangeStatus(hoadonCafe.Mode, hoadonCafe.Status, hoadonCafe._sttRec,
                            hoadonCafe.CurrentIndex, hoadonCafe.TongThanhToanNT, hoadonCafe.GC_UD1, hoadonCafe.Time0, hoadonCafe.Time2);
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".hoadonCafe_BillChanged", ex);
            }
        }

        void hoadonCafe_ChangeTable(object sender, EventArgs e)
        {
            try
            {
                if (selected_button.Status != "1") return;

                var hdcafe = sender as HoaDonCafeControl;
                if (hdcafe != null)
                {
                    var f = new ChangeForm(1);
                    f.Khu1 = hdcafe.MA_KHOPH;
                    f.Ban1 = hdcafe.MA_VITRIPH;
                    var f_BusyTables = "";
                    var f_EmptyTables = "";
                    foreach (KeyValuePair<string, TableButton> item in ButtonsDictionary)
                    {
                        if (item.Value.Status == "1" || item.Value.Status == "2")
                        {
                            f_BusyTables += ",'" + item.Key + "'";
                        }
                        else
                        {
                            f_EmptyTables += ",'" + item.Key + "'";
                        }
                    }
                    if (f_BusyTables.Length > 0) f_BusyTables = f_BusyTables.Substring(1);
                    if (f_EmptyTables.Length > 0) f_EmptyTables = f_EmptyTables.Substring(1);
                    f.BusyTables = f_BusyTables;
                    f.EmptyTables = f_EmptyTables;

                    var dr = f.ShowDialog(this);
                    if (dr == DialogResult.OK)
                    {
                        hdcafe.MA_KHOPH = f.SelectedKhoID;
                        hdcafe.MA_VITRIPH = f.SelectedVitriID;
                        hdcafe.Luu(f.SelectedKhoID, f.SelectedVitriID, false); // luu cho noi den

                        
                        if (_listen_change_mode)
                        {
                            if (hdcafe.MA_KHOPH == Ma_kho && hdcafe.MA_VITRIPH != selected_button.Ma_vitri)
                            {
                                if (ButtonsDictionary.ContainsKey(hdcafe.MA_VITRIPH))
                                {
                                    selected_button.IsSelect = false;
                                    selected_button.Stt_Rec = "";
                                    selected_button.ChangeStatus(V6Mode.Init, "0", "", -1, 0, "", "", "");

                                    selected_button = ButtonsDictionary[hdcafe.MA_VITRIPH];
                                    selected_button.Stt_Rec = hdcafe._sttRec;
                                    //Click chọn qua bàn mới.
                                    //selected_button.SelectOne();
                                    selected_button.IsSelect = true;
                                    selected_button.ChangeStatus(hoadonCafe.Mode, hoadonCafe.Status, hoadonCafe._sttRec,
                                        hoadonCafe.CurrentIndex, hoadonCafe.TongThanhToan, hoadonCafe.GC_UD1, hoadonCafe.Time0, hoadonCafe.Time2);

                                    //txtMaVitri.Text  = hdcafe.MA_VITRIPH;
                                }
                            }
                            else if (hdcafe.MA_KHOPH != Ma_kho)
                            {
                                //Chuyển qua khu khác.
                                //ShowMainMessage("Chuyển khu khác, coding...");
                                OnChuyenKhu(selected_button.Stt_Rec, hoadonCafe.CurrentIndexAM_Data, f.SelectedKhoID, f.SelectedVitriID);
                                selected_button.Stt_Rec = "";

                                hdcafe.ResetForm();
                                selected_button.ChangeStatus(V6Mode.Init, "0", "", -1, 0, "", "", "");
                            }
                        }
                        //ShowParentMessage("OK!");
                    }
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".hoadonCafe_ChangeTable", ex);
            }
        }

        /// <summary>
        /// Hiển thị chứng từ của nút được chọn.
        /// </summary>
        /// <param name="button"></param>
        private void ShowInvoice(TableButton button)
        {
            try
            {
                hoadonCafe.ViewInvoice(button.Stt_Rec, button.Mode);
                hoadonCafe.Status = button.Status;
                hoadonCafe.MA_KHOPH = Ma_kho;
                hoadonCafe.MA_VITRIPH = button.Ma_vitri;

                _listen_change_mode = true;
                
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".ShowInvoice", ex);
            }
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
                hoadonCafe_ChangeTable(hoadonCafe, EventArgs.Empty);
                return true;
            }
            return hoadonCafe.DoHotKey0(keyData);
        }

        private void txtMaVitri_V6LostFocus(object sender)
        {
            ChonBan(txtMaVitri.Text);
        }

        private void ChonBan(string ma_vitri)
        {
            try
            {
                if (ma_vitri == "") return;

                foreach (KeyValuePair<string, TableButton> item in ButtonsDictionary)
                {
                    var button = item.Value;
                    if (button != null)
                    {
                        if (button.Ma_vitri.ToUpper() == ma_vitri.ToUpper())
                        {
                            button.SelectOne();
                            panelTable.ScrollControlIntoView(button);
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".ChonBan", ex);
            }
        }


        public void NhanBan(string stt_rec, IDictionary<string,object> AM_data, string makho_den, string ma_vitri_den)
        {
            try
            {

                AM_data["STT_REC"] = stt_rec;
                var a = Ma_kho;
                AM_data["MA_KHOPH"] = makho_den;
                AM_data["MA_VITRIPH"] = ma_vitri_den;

                hoadonCafe.AM.AddRow(AM_data);// hdCafeFrom.AM.Rows[hdCafeFrom.CurrentIndex].ToDataDictionary());
                //tim button co mavitri =
                if (ButtonsDictionary.ContainsKey(ma_vitri_den))// hdCafeFrom.MA_VITRIPH))
                {
                    var button = ButtonsDictionary[ma_vitri_den];
                    selected_button = button;
                    button.Stt_Rec = stt_rec;// hdCafeFrom._sttRec;
                    hoadonCafe.ViewInvoice(button.Stt_Rec, V6Mode.Edit);
                    hoadonCafe.Status = "1";
                    button.IsSelect = true;
                    DeSelectAll(button);
                    button.ChangeStatus(V6Mode.Edit, "1", button.Stt_Rec, hoadonCafe.CurrentIndex,
                        hoadonCafe.TongThanhToan, hoadonCafe.GC_UD1, hoadonCafe.Time0, hoadonCafe.Time2);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".NhanBan", ex);
            }
        }

        /// <summary>
        /// Cờ cho nút mở rộng các nút bàn.
        /// </summary>
        private string _tsFullText = "";
        private void tsFull_Click(object sender, EventArgs e)
        {
            var base_top = panelTable.AutoScrollPosition.Y;
            var base_left = panelTable.AutoScrollPosition.X;

            if (_tsFullText != V6Text.ZoomIn)
            {
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
                var newHeight = (row_count + 1) * height;
                if (newHeight > this.Height) newHeight = this.Height;
                panelTable.Height = newHeight;
                tsFull.Image = Properties.Resources.ZoomOut24;
                _tsFullText = V6Text.ZoomIn;
            }
            else
            {
                panelTable.Height = 90;
                int count = 0;
                foreach (KeyValuePair<string, TableButton> item in ButtonsDictionary)
                {
                    item.Value.Top = 0 + base_top;
                    item.Value.Left = item.Value.Width*count + base_left;
                    count++;
                }
                tsFull.Image = Properties.Resources.ZoomIn24;
                _tsFullText = V6Text.ZoomOut;
            }
        }
    }

    public class ChuyenKhuEventArgs : EventArgs
    {
        public ChuyenKhuEventArgs(string stt_rec, IDictionary<string,object> am_data, string makhoTo, string ma_vitriTo)
        {
            Stt_rec = stt_rec;
            AM_Data = am_data;
            Ma_Kho_Den = makhoTo;
            Ma_Vitri_Den = ma_vitriTo;
            
        }
        public string Ma_Kho_Den;
        public string Ma_Vitri_Den;
        public IDictionary<string, object> AM_Data;
        public string Stt_rec;
    }
}
