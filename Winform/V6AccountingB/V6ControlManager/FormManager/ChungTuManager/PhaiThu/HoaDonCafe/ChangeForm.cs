using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6AccountingBusiness.Invoices;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Structs;

namespace V6ControlManager.FormManager.ChungTuManager.PhaiThu.HoaDonCafe
{
    public partial class ChangeForm : V6Form
    {
        public ChangeForm()
        {
            InitializeComponent();
        }

        private int _mode;
        /// <summary>
        /// Đổi bàn (1), Gộp bàn (2), tách (?)
        /// </summary>
        /// <param name="mode"></param>
        public ChangeForm(int mode)
        {
            _mode = mode;
            InitializeComponent();
        }

        private string Khu_ID_Field = "MA_KHO";
        private string Khu_NameField = "TEN_KHO";
        private string Vitri_ID_Field = "MA_VITRI";
        private string Vitri_NameField = "TEN_VITRI";
        private DataTable Data_Kho = null;
        //private DataTable Data_Vitri = null;
        private DataView DataView_Vitri = null;
        private DataTable Data_Vitri_Full = null;

        public string SelectedKhoID { get; set; }
        public string SelectedKhoName { get; set; }
        public string SelectedVitriID { get; set; }
        public string SelectedVitriName { get; set; }
        public string NotInList { get; set; }

        public string Khu1 { get { return txtKhu1.Text; } set { txtKhu1.Text = value; } }
        public string Ban1 { get { return txtBan1.Text; } set { txtBan1.Text = value; } }
        public string BusyTables { get; set; }
        public string EmptyTables { get; set; }

        private void ChangeForm_Load(object sender, System.EventArgs e)
        {
            try
            {
                if (!V6Setting.IsVietnamese) Khu_NameField = "TEN_KHO2";

                var and_not_in = string.IsNullOrEmpty(NotInList) ? "" : " AND MA_KHO NOT IN (" + NotInList + ")";
                Data_Kho = V6BusinessHelper.Select("Alkho", "*",
                    V6Login.IsAdmin ? "1=1" : " dbo.VFA_Inlist_MEMO(MA_KHO,'" + V6Login.UserRight.RightKho +"')=1"+
                    and_not_in).Data;
                if (Data_Kho.Rows.Count == 0)
                {
                    this.ShowWarningMessage("NO KHO");
                    return;
                }

                cboKhu2.DisplayMember = Khu_NameField;
                cboKhu2.ValueMember = Khu_ID_Field;
                cboKhu2.DataSource = Data_Kho;
                cboKhu2.DisplayMember = Khu_NameField;
                cboKhu2.ValueMember = Khu_ID_Field;

                
                cboKhu2.SelectedValue = txtKhu1.Text;

                LoadVitriDataFull();
                var not_in_list = "";
                if (_mode == 1)//chuyen ban,
                {
                    not_in_list = BusyTables;
                }
                else if (_mode == 2)//Gop ban.
                {
                    not_in_list = EmptyTables;
                }
                ViewListVitri(not_in_list);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".Load", ex);
            }
            Ready();
        }

        private void LoadVitriDataFull()
        {
            SqlParameter[] plist = { new SqlParameter("@MA_KHO", SelectedKhoID), };
            Data_Vitri_Full = V6BusinessHelper.Select("Alvitri", "*", "MA_KHO = @MA_KHO",
               "", "MA_VITRI", plist).Data;
        }

        private void Nhan()
        {
            try
            {
                if (string.IsNullOrEmpty(SelectedVitriID))
                {
                    this.ShowWarningMessage(SelectedKhoName + " - 0 Vitri");
                    return;
                }
                
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".Nhan", ex);
            }
        }

        /// <summary>
        /// Hiển thị
        /// </summary>
        /// <param name="not_in_list">'a','b'...</param>
        private void ViewListVitri(string not_in_list)
        {
            
            //var where_not_in = string.IsNullOrEmpty(not_in_list) ? "" : " and Ma_vitri not in ("+not_in_list+")";
            var where_not_in0 = string.IsNullOrEmpty(not_in_list) ? "" : "Ma_vitri not in ("+not_in_list+")";
            //Data_Vitri_Full = V6BusinessHelper.Select("Alvitri", "*", "MA_KHO = @MA_KHO",
            //    "", "MA_VITRI", plist).Data;
            DataView_Vitri = new DataView(Data_Vitri_Full);
            DataView_Vitri.RowFilter = where_not_in0;
            //Data_Vitri = V6BusinessHelper.Select("Alvitri", "*", "MA_KHO = @MA_KHO" + where_not_in,
            //    "", "MA_VITRI", plist).Data;
            if (DataView_Vitri.Count == 0)
            {
                this.ShowWarningMessage(SelectedKhoName + " - 0 Vitri");
                cboBan2.DataSource = null;
                SelectedVitriID = "";
                return;
            }

            cboBan2.DisplayMember = Vitri_NameField;
            cboBan2.ValueMember = Vitri_ID_Field;
            cboBan2.DataSource = DataView_Vitri;
            cboBan2.DisplayMember = Vitri_NameField;
            cboBan2.ValueMember = Vitri_ID_Field;
        }


        private void cboKhu2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboKhu2.DataSource != null)
                {
                    DataRowView row = cboKhu2.SelectedItem as DataRowView;
                    if (row != null && row.Row.Table.Columns.Contains(Khu_ID_Field))
                    {
                        SelectedKhoID = row[Khu_ID_Field].ToString().Trim();
                        SelectedKhoName = row[Khu_NameField].ToString().Trim();
                    }
                    else
                    {
                        SelectedKhoID = null;
                        SelectedKhoName = "";
                    }
                }

                if (IsReady)
                {
                    LoadVitriDataFull();

                    //Lọc dữ liệu chưa hoàn tất trong ngày.
                    var day = DateTime.Today;
                    var hoadonCafe_Invoice = new V6Invoice83();
                    var whereAM = string.Format("Ngay_Ct='{0}' and MA_KHOPH = '{1}' and status in ('1','2') ",
                        day.ToString("yyyyMMdd"), SelectedKhoID);
                    var whereAD = "";
                    var data_hoadon = hoadonCafe_Invoice.SearchAM("", whereAM, whereAD, "", "");
                    var dic_vitri = data_hoadon.ToDataSortedDictionary("Ma_vitriPH", "Stt_rec");

                    var not_in_list = "";
                    if (_mode == 1)//chuyen ban,
                    {
                        //not_in_list = BusyTables;//Khong dung khi chon lai khu khac tren form chon
                        foreach (DataRow row in Data_Vitri_Full.Rows)
                        {
                            var c_vitri = row["Ma_vitri"].ToString().Trim();
                            if (dic_vitri.ContainsKey(c_vitri))
                            {
                                not_in_list += ",'" + c_vitri + "'";
                            }
                        }
                    }
                    else if (_mode == 2)//Gop ban.
                    {
                        //not_in_list = EmptyTables;//Khong dung khi chon lai khu khac tren form chon
                        foreach (DataRow row in Data_Vitri_Full.Rows)
                        {
                            var c_vitri = row["Ma_vitri"].ToString().Trim();
                            if (!dic_vitri.ContainsKey(c_vitri))
                            {
                                not_in_list += ",'" + c_vitri + "'";
                            }
                        }
                    }
                    if (not_in_list.Length > 0) not_in_list = not_in_list.Substring(1);
                    ViewListVitri(not_in_list);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".cboKhu2", ex);
            }
        }

        private void cboBan2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboBan2.DataSource != null)
            {
                DataRowView row = cboBan2.SelectedItem as DataRowView;
                if (row != null && row.Row.Table.Columns.Contains(Vitri_ID_Field))
                {
                    SelectedVitriID = row[Vitri_ID_Field].ToString().Trim();
                    SelectedVitriName = row[Vitri_NameField].ToString().Trim();
                }
                else
                {
                    SelectedVitriID = null;
                    SelectedVitriName = "";
                }
            }
        }

        private void btnNhan_Click(object sender, EventArgs e)
        {
            Nhan();
        }
    }
}
