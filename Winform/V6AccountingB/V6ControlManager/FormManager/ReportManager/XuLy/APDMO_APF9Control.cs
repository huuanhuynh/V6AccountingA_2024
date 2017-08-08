using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6AccountingBusiness.Invoices;
using V6ControlManager.FormManager.ChungTuManager.InChungTu;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6SqlConnect;
using V6Tools;
using V6Tools.V6Convert;
using Timer = System.Windows.Forms.Timer;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public class APDMO_APF9Control : XuLyBase
    {
        private void InitializeComponent()
        {
            this.btnPhanBoTuDong = new V6Controls.Controls.V6FormButton();
            this.btnXoaPhanBo = new V6Controls.Controls.V6FormButton();
            this.radPbTheoNgay = new System.Windows.Forms.RadioButton();
            this.radPbTheoHanTT = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radPbTheoHanTT);
            this.panel1.Controls.Add(this.radPbTheoNgay);
            this.panel1.Controls.Add(this.btnXoaPhanBo);
            this.panel1.Controls.Add(this.btnPhanBoTuDong);
            // 
            // btnPhanBoTuDong
            // 
            this.btnPhanBoTuDong.AccessibleDescription = "XULYB00001";
            this.btnPhanBoTuDong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnPhanBoTuDong.Location = new System.Drawing.Point(3, 546);
            this.btnPhanBoTuDong.Name = "btnPhanBoTuDong";
            this.btnPhanBoTuDong.Size = new System.Drawing.Size(127, 23);
            this.btnPhanBoTuDong.TabIndex = 7;
            this.btnPhanBoTuDong.Text = "Phân bổ tự động";
            this.btnPhanBoTuDong.UseVisualStyleBackColor = true;
            this.btnPhanBoTuDong.Click += new System.EventHandler(this.btnPhanBoTuDong_Click);
            // 
            // btnXoaPhanBo
            // 
            this.btnXoaPhanBo.AccessibleDescription = "XULYB00002";
            this.btnXoaPhanBo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnXoaPhanBo.Location = new System.Drawing.Point(136, 546);
            this.btnXoaPhanBo.Name = "btnXoaPhanBo";
            this.btnXoaPhanBo.Size = new System.Drawing.Size(104, 23);
            this.btnXoaPhanBo.TabIndex = 7;
            this.btnXoaPhanBo.Text = "Xóa phân bổ";
            this.btnXoaPhanBo.UseVisualStyleBackColor = true;
            this.btnXoaPhanBo.Click += new System.EventHandler(this.btnXoaPhanBo_Click);
            // 
            // radPbTheoNgay
            // 
            this.radPbTheoNgay.AccessibleDescription = "XULYC00002";
            this.radPbTheoNgay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radPbTheoNgay.AutoSize = true;
            this.radPbTheoNgay.Checked = true;
            this.radPbTheoNgay.Location = new System.Drawing.Point(12, 500);
            this.radPbTheoNgay.Name = "radPbTheoNgay";
            this.radPbTheoNgay.Size = new System.Drawing.Size(158, 17);
            this.radPbTheoNgay.TabIndex = 0;
            this.radPbTheoNgay.TabStop = true;
            this.radPbTheoNgay.Text = "Phân bổ theo ngày hóa đơn";
            this.radPbTheoNgay.UseVisualStyleBackColor = true;
            this.radPbTheoNgay.CheckedChanged += new System.EventHandler(this.radPbTheoNgay_CheckedChanged);
            // 
            // radPbTheoHanTT
            // 
            this.radPbTheoHanTT.AccessibleDescription = "XULYL00026";
            this.radPbTheoHanTT.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radPbTheoHanTT.AutoSize = true;
            this.radPbTheoHanTT.Location = new System.Drawing.Point(12, 523);
            this.radPbTheoHanTT.Name = "radPbTheoHanTT";
            this.radPbTheoHanTT.Size = new System.Drawing.Size(119, 17);
            this.radPbTheoHanTT.TabIndex = 0;
            this.radPbTheoHanTT.TabStop = true;
            this.radPbTheoHanTT.Text = "Phân bổ theo hạn tt";
            this.radPbTheoHanTT.UseVisualStyleBackColor = true;
            this.radPbTheoHanTT.CheckedChanged += new System.EventHandler(this.radPbTheoNgay_CheckedChanged);
            // 
            // APDMO_APF9Control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Name = "APDMO_APF9Control";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        public APDMO_APF9Control(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
            : base(itemId, program, reportProcedure, reportFile, reportCaption, reportCaption2, true)
        {
            InitializeComponent();
            dataGridView1.SelectionChanged += dataGridView1_SelectionChanged;
        }

        void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
             

                if (dataGridView1.DataSource == null || dataGridView2.DataSource == null) return;

                var row1 = dataGridView1.CurrentRow;
                if (row1 == null) return;

                var data1 = row1.ToDataDictionary();
                var stt_rec1 = data1["STT_REC"].ToString();
                var ma_nt1 = data1["MA_NT"].ToString();

                var tki = data1["TK_I"].ToString();
                var sql = "Select STT_REC_TT, Sum(Case when Isnull(ma_nt, @ma_nt0) = @ma_nt0 then Isnull(tt,0)";
                sql += " Else Isnull(tt_nt,0) End) tt_dn_nt, Sum(Isnull(tt,0)) tt_dn, Sum(Isnull(tt_qd,0)) tt_qd";
                sql += " From ARS30 Where stt_rec=@stt_rec1 and tk = @tki ";
                sql += " Group by stt_rec_tt";

                SqlParameter[] plist=
                {
                    new SqlParameter("@ma_nt0", V6Options.M_MA_NT0), 
                    new SqlParameter("@stt_rec1", stt_rec1), 
                    new SqlParameter("@tki", tki), 
                };
                var table = SqlConnect.ExecuteDataset(CommandType.Text, sql, plist).Tables[0];

                decimal sum_ttdnnt = 0, sum_ttdn = 0, sum_ttqd = 0;
                foreach (DataRow row in table.Rows)
                {
                    var stt_rec_tt = row["STT_REC_TT"].ToString().Trim();
                   
                    var tt_dn_nt = ObjectAndString.ObjectToDecimal(row["tt_dn_nt"]);
                    var tt_dn = ObjectAndString.ObjectToDecimal(row["tt_dn"]);
                    var tt_qd = ObjectAndString.ObjectToDecimal(row["tt_qd"]);
                    

                    foreach (DataGridViewRow row2 in dataGridView2.Rows)
                    {
                        var rec = row2.Cells["STT_REC"].Value.ToString().Trim();
                        var ma_nt2 = row2.Cells["MA_NT"].Value.ToString().Trim();

                        if (rec == stt_rec_tt)
                        {
                            if (ma_nt1 == ma_nt2)
                            {
                                sum_ttqd += tt_dn_nt;
                            }
                            else
                            {
                                sum_ttqd += tt_qd;
                                
                            }
                            sum_ttdnnt += tt_dn_nt;
                            sum_ttdn += tt_dn;
                           

                            row2.Cells["tt_dn_nt"].Value = tt_dn_nt;
                            row2.Cells["tt_dn"].Value = tt_dn;
                            row2.Cells["tt_qd"].Value = tt_qd;
                            break;
                        }
                    }
                }

               // row1.Cells["DA_PB"].Value = sum_ttqd;

            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".dataGridView1_SelectionChanged " + ex.Message);
            }
        }

        public override void SetStatus2Text()
        {
            V6ControlFormHelper.SetStatusText2("F3: Phân bổ trực tiếp, F8: Xóa phân bổ");
            //lbl.Text = "";
        }

        protected override void MakeReport2()
        {
            Load_Data = true;//Thay đổi cờ.
            base.MakeReport2();
        }

        #region ==== Xử lý F3

        protected override void XuLyHienThiFormSuaChungTuF3()
        {
            try
            {
                if (dataGridView1.CurrentRow == null) return;
                if (dataGridView2.CurrentRow == null) return;

                if (PhanBo1(dataGridView1.CurrentRow, dataGridView2.CurrentRow, 0) == DialogResult.OK)
                {
                    btnNhan.PerformClick();
                    V6ControlFormHelper.ShowMainMessage(V6Text.Finish);
                }
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + "." + MethodBase.GetCurrentMethod().Name, ex);
            }
        }

        private DialogResult PhanBo1(DataGridViewRow row1, DataGridViewRow row2, int isAuto)
        {
            try
            {
                var data1 = row1.ToDataDictionary();
                var data2 = row2.ToDataDictionary();

                var sttRec = data1["STT_REC"].ToString().Trim();
                var tk = data1["TK_I"].ToString().Trim();
                var dienGiai = data1["DIEN_GIAII"].ToString().Trim();
                var maCt = data1["MA_CT"].ToString().Trim();
                var maNt = data1["MA_NT"].ToString().Trim();
                var sttRecTT = data2["STT_REC"].ToString().Trim();
                //var ty_gia_2 = ObjectAndString.ObjectToDecimal(data2["TY_GIA"]);

                

                string ma_nt_1, ma_nt_2;
                decimal ty_gia_1, ty_gia_2;

                decimal tien_nt_1 = 0;    // Tiền nhận trên phiếu thu
                decimal tien_1 = 0;       // Tiền nhận quy đổi mant0
                decimal da_pb_1 = 0;    // Tiền đã phân bổ
                decimal tien_cl_nt_1 = 0; // Tiền còn lại trên phiếu thu

                decimal cl_tt_nt_2 = 0; // Tiền còn phải trả trên hóa đơn
                
        
                tien_nt_1 = ObjectAndString.ObjectToDecimal(data1["TIEN_NT"]);//Tiền nhận trên phiếu thu
                da_pb_1 = ObjectAndString.ObjectToDecimal(data1["DA_PB"]);//   Tiền đã phân bổ
                tien_cl_nt_1 = tien_nt_1-da_pb_1;                           // Tiền còn lại trên phiếu thu
                if (tien_cl_nt_1 == 0)
                {
                    V6ControlFormHelper.ShowMainMessage("Đã phân bổ hết.");
                    return DialogResult.Cancel;
                }
                tien_1 = ObjectAndString.ObjectToDecimal(data1["TIEN"]);//quy đổi
                
                ma_nt_1 = data1["MA_NT"].ToString().Trim();
                ty_gia_1 = ObjectAndString.ObjectToDecimal(data1["TY_GIA"]);

                cl_tt_nt_2 = ObjectAndString.ObjectToDecimal(data2["CL_TT_NT"]);//Tiền còn phải trả
                ma_nt_2 = data2["MA_NT"].ToString().Trim();
                ty_gia_2 = ObjectAndString.ObjectToDecimal(data2["TY_GIA"]);
                if (ty_gia_2 <= 0 || ma_nt_2 == V6Options.M_MA_NT0)
                {
                    ty_gia_2 = 1;
                }
                if (ty_gia_1 <= 0 || ma_nt_1 == V6Options.M_MA_NT0)
                {
                    ty_gia_1 = 1;
                }
                
                

                //Tiền tt con lai đổi ra theo tỷ giá trên hóa đơn.
                var tt_1_nt_hd = V6BusinessHelper.Vround(tien_cl_nt_1*ty_gia_1 / ty_gia_2, V6Options.M_ROUND_NT);
                if (ma_nt_1 == ma_nt_2)
                {
                    tt_1_nt_hd = tien_cl_nt_1;
                }
                // khoảng tiền thanh toán cho hóa đơn + vào đã phân bổ, ghi theo ngoại tệ phiếu thu
                var tt_nt = tt_1_nt_hd > cl_tt_nt_2 ?
                    V6BusinessHelper.Vround((cl_tt_nt_2*ty_gia_2)/ty_gia_1, V6Options.M_ROUND_NT) :
                    tien_cl_nt_1;
                //quy đổi
                var tt = V6BusinessHelper.Vround(tt_nt * ty_gia_1, V6Options.M_ROUND);
                //Chỗ này có thể sai giá trị quy đổi nhưng vừa đủ = hóa đơn.
                if (tt_1_nt_hd>cl_tt_nt_2 && ma_nt_2 == V6Options.M_MA_NT0)
                {
                    tt = cl_tt_nt_2;
                }
                //quy đổi ngoại tệ hóa đơn
                var tt_nt_hdqd = tt/ty_gia_2;
                // Nếu số tiền còn lại trên phiếu thu > còn phải thu của hóa đơn
                // Gán quy đổi ngoại tệ hóa đơn = còn phải thu
                if (tt_1_nt_hd > cl_tt_nt_2)
                {
                    tt_nt_hdqd = cl_tt_nt_2;
                }

                if (ma_nt_1 == ma_nt_2)
                {
                    tt_nt = tt_1_nt_hd > cl_tt_nt_2 ? cl_tt_nt_2 : tt_1_nt_hd;
                    tt = V6BusinessHelper.Vround(tt_nt * ty_gia_1, V6Options.M_ROUND);
                    tt_nt_hdqd = tt_nt;
                }

                
                decimal ttDnNt = tt_nt;
                decimal ttDn = tt;
                decimal ttQd = tt_nt_hdqd;
                

                //if (isAuto == 1)
                //{
                    
                //}
                //else
                if(isAuto == 0 || (ma_nt_1 != ma_nt_2 && cl_tt_nt_2 > 0))
                { 
                    APDMO_F9F3 form = new APDMO_F9F3(data1, data2);

                    form.isAuto = isAuto;
                    form.ma_nt_1 = ma_nt_1;
                    form.ma_nt_2 = ma_nt_2;
                    form.ty_gia_1 = ty_gia_1;
                    form.ty_gia_2 = ty_gia_2;

                    form.tien_nt_1 = tien_nt_1;
                    form.tien_1 = tien_1;
                    form.da_pb_1 = da_pb_1;
                    form.tien_cl_nt_1 = tien_cl_nt_1;
                    form.cl_tt_nt_2 = cl_tt_nt_2;

                    form.txtTienPhaiTra.Value = cl_tt_nt_2;
                    form.txtMaNtTienPhaiTra.Text = ma_nt_2;

                    form.txtTienThanhToanNt.Value = ttDnNt;
                    form.txtMaNtTienThanhToanNt.Text = ma_nt_1;
                    form.txtTienThanhToan.Value = ttDn;
                    form.txtMaNtTienThanhToan.Text = V6Options.M_MA_NT0;

                    form.txtTienNtQd.Value = ttQd;
                    form.txtMaNtTienNtQd.Text = ma_nt_2;

                    if (form.ShowDialog(this) == DialogResult.OK)
                    {

                        ttDnNt = form.txtTienThanhToanNt.Value;
                        ttDn = form.txtTienThanhToan.Value;
                        ttQd = form.txtTienNtQd.Value;


                        //Gan lai cho gridview // Chú ý copy đoạn này xuống dưới khi sửa lại.
                        //Cộng thêm đã phân bổ
                        var da_phan_bo = ObjectAndString.ObjectToDecimal(data1["DA_PB"]);
                        da_phan_bo += ttDnNt;
                        row1.Cells["DA_PB"].Value = da_phan_bo;
                        
                        //Gán tiền đã phân bổ đợt này.
                        row2.Cells["TT_DN_NT"].Value = ttDnNt;
                        row2.Cells["TT_DN"].Value = V6BusinessHelper.Vround(ttDnNt * ty_gia_1, V6Options.M_ROUND);
                        row2.Cells["TT_QD"].Value = ttQd;

                        SqlParameter[] plist =
                        {
                            new SqlParameter("@Stt_rec", sttRec),
                            new SqlParameter("@Stt_rec_tt", sttRecTT),
                            new SqlParameter("@Tk", tk),
                            new SqlParameter("@Dien_giai", dienGiai),
                            new SqlParameter("@Ma_ct", maCt),
                            new SqlParameter("@Ma_nt", maNt),
                            new SqlParameter("@Tt_qd", ttQd),
                            new SqlParameter("@Tt_dn", ttDn),
                            new SqlParameter("@Tt_dn_nt", ttDnNt),
                            new SqlParameter("@IsAuto", isAuto),
                            new SqlParameter("@User_id", V6Login.UserId),
                        };
                        V6BusinessHelper.ExecuteProcedureNoneQuery("AApttpbF3", plist);

                    }
                    return form.DialogResult;
                }
                else
                {
                    //Gan lai cho gridview
                    //Cộng thêm đã phân bổ
                    var da_phan_bo = ObjectAndString.ObjectToDecimal(data1["DA_PB"]);
                    da_phan_bo += ttDnNt;
                    row1.Cells["DA_PB"].Value = da_phan_bo;

                    //Gán tiền đã phân bổ đợt này.
                    row2.Cells["TT_DN_NT"].Value = ttDnNt;
                    row2.Cells["TT_DN"].Value = V6BusinessHelper.Vround(ttDnNt * ty_gia_1, V6Options.M_ROUND);
                    row2.Cells["TT_QD"].Value = ttQd;

                    SqlParameter[] plist =
                    {
                        new SqlParameter("@Stt_rec", sttRec),
                        new SqlParameter("@Stt_rec_tt", sttRecTT),
                        new SqlParameter("@Tk", tk),
                        new SqlParameter("@Dien_giai", dienGiai),
                        new SqlParameter("@Ma_ct", maCt),
                        new SqlParameter("@Ma_nt", maNt),
                        new SqlParameter("@Tt_qd", ttQd),
                        new SqlParameter("@Tt_dn", ttDn),
                        new SqlParameter("@Tt_dn_nt", ttDnNt),
                        new SqlParameter("@IsAuto", isAuto),
                        new SqlParameter("@User_id", V6Login.UserId),
                    };
                    V6BusinessHelper.ExecuteProcedureNoneQuery("AApttpbF3", plist);
                    return DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                if (isAuto == 0) this.ShowErrorMessage(GetType() + ".PhanBo1 " + ex.Message);
                else _pbtd_errors += "\n" + ex.Message;
                return DialogResult.Abort;
            }
        }

        private string _pbtd_errors = "";
        private void PhanBoTuDong()
        {
            try
            {
                if (dataGridView1.CurrentRow == null) return;

                _pbtd_errors = "";

                SortDataGridView2();

                //SortedDictionary<string, DataGridViewRow> row_list = new SortedDictionary<string, DataGridViewRow>();

                foreach (DataGridViewRow row in dataGridView2.Rows)
                {
                    PhanBo1(dataGridView1.CurrentRow, row, 1);
                    //var date_string = ObjectAndString.ObjectToString(row.Cells[order_field].Value, "yyyyMMdd");
                    //row_list.Add(date_string, row);
                }

                //foreach (KeyValuePair<string, DataGridViewRow> item in row_list)
                //{
                //    PhanBo1(dataGridView1.CurrentRow, item.Value, 1);
                //}

                btnNhan.PerformClick();

                if (_pbtd_errors.Length > 0)
                {
                    this.ShowErrorMessage(GetType() + ".PhanBoTuDong " + _pbtd_errors);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".PhanBoTuDong: " + ex.Message);
            }
        }

        #endregion

        protected override void XuLyF8()
        {
            try
            {
                if (dataGridView1.Focused)
                {
                    var row = dataGridView1.CurrentRow;
                    if (row == null) return;

                    if (this.ShowConfirmMessage(V6Text.DeleteConfirm) == DialogResult.Yes)
                    {
                        SqlParameter[] plist =
                        {
                            new SqlParameter("@Stt_rec", "" + row.Cells["STT_REC"].Value),
                            new SqlParameter("@Ma_ct", "" + row.Cells["MA_CT"].Value),
                            new SqlParameter("@Tk", "" + row.Cells["TK_I"].Value),
                            new SqlParameter("@Ma_kh", "" + row.Cells["MA_KH"].Value),
                        };
                        V6BusinessHelper.ExecuteProcedure("AAPttpbDel", plist);
                        V6ControlFormHelper.ShowMainMessage(V6Text.Finish);
                    }
                }
                else if (dataGridView2.Focused)
                {
                    var row = dataGridView2.CurrentRow;
                    if (row == null) return;

                    var row1 = dataGridView1.CurrentRow;
                    if (row1 == null) return;

                    if (this.ShowConfirmMessage(V6Text.DeleteConfirm) == DialogResult.Yes)
                    {
                        SqlParameter[] plist =
                        {
                            new SqlParameter("@Stt_rec", "" + row1.Cells["STT_REC"].Value),
                            new SqlParameter("@Stt_rec_tt", "" + row.Cells["STT_REC"].Value),
                            new SqlParameter("@Ma_ct", "" + row1.Cells["MA_CT"].Value),
                            new SqlParameter("@Tk", "" + row.Cells["TK"].Value),
                            new SqlParameter("@Ma_nt", "" + row.Cells["MA_NT"].Value),
                            new SqlParameter("@Tt_qd", ObjectAndString.ObjectToDecimal(row.Cells["TT_QD"].Value)),
                            new SqlParameter("@Tt_dn_nt", ObjectAndString.ObjectToDecimal(row.Cells["TT_DN_NT"].Value)),
                            new SqlParameter("@Tt", ObjectAndString.ObjectToDecimal(row.Cells["TT"].Value)),
                            new SqlParameter("@User_id", V6Login.UserId),
                        };
                        V6BusinessHelper.ExecuteProcedure("AApttpbF8", plist);
                        V6ControlFormHelper.ShowMainMessage(V6Text.Finish);
                    }
                }

            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".XuLyF8", ex);
            }
            finally
            {
                btnNhan.PerformClick();
            }
        }

        #region ==== Xử lý F9 ====

        private bool f9Running;
        private string f9Error = "";
        private string f9ErrorAll = "";
        private string _oldDefaultPrinter, _PrinterName;
        private int _PrintCopies;
        private V6Controls.Controls.V6FormButton btnPhanBoTuDong;
        private V6Controls.Controls.V6FormButton btnXoaPhanBo;
        private RadioButton radPbTheoHanTT;
        private RadioButton radPbTheoNgay;
        private bool printting;
        protected override void XuLyF9()
        {
            try
            {
                _oldDefaultPrinter = V6Tools.PrinterStatus.GetDefaultPrinterName();

                PrintDialog p = new PrintDialog();
                p.AllowCurrentPage = false;
                p.AllowPrintToFile = false;
                p.AllowSelection = false;
                p.AllowSomePages = false;
                p.PrintToFile = false;
                p.UseEXDialog = true; //Fix win7
                //if (choosePrinter)
                {
                    DialogResult dr = p.ShowDialog();
                    if (dr == DialogResult.OK)
                    {
                        _PrinterName = p.PrinterSettings.PrinterName;
                        _PrintCopies = p.PrinterSettings.Copies;
                        V6BusinessHelper.WriteOldSelectPrinter(_PrinterName);
                        printting = true;
                        //Print(_PrinterName);
                        remove_list_g = new List<DataGridViewRow>();
                        Timer tF9 = new Timer();
                        tF9.Interval = 500;
                        tF9.Tick += tF9_Tick;
                        Thread t = new Thread(F9Thread);
                        t.SetApartmentState(ApartmentState.STA);
                        CheckForIllegalCrossThreadCalls = false;
                        t.IsBackground = true;
                        t.Start();
                        tF9.Start();

                    }
                    else
                    {
                        printting = false;
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".XuLyF9: " + ex.Message);
            }
        }

        private void F9Thread()
        {
            f9Running = true;
            f9ErrorAll = "";

            int i = 0;
            var Invoice = new V6Invoice81();
            var program = _program + "F9";// Invoice.PrintReportProcedure;
            //var repFile = Invoice.Alct.Rows[0]["FORM"].ToString().Trim();
            var repTitle = Invoice.Alct.Rows[0]["TIEU_DE_CT"].ToString().Trim();
            var repTitle2 = Invoice.Alct.Rows[0]["TIEU_DE2"].ToString().Trim();
            

            while(i<dataGridView1.Rows.Count)
            {
                DataGridViewRow row = dataGridView1.Rows[i];
                i++;
                try
                {
                    if (row.IsSelect())
                    {

                        var sttRec = (row.Cells["Stt_rec"].Value ?? "").ToString();

                        var c = new InChungTuViewBase(Invoice, program, program, _reportFile, repTitle, repTitle2,
                            "", "", "", sttRec);
                        c.Text = "In phiếu xuất kho";
                        c.Report_Stt_rec = sttRec;
                        c.TTT = ObjectAndString.ObjectToDecimal(row.Cells["T_TT"].Value);
                        c.TTT_NT = ObjectAndString.ObjectToDecimal(row.Cells["T_TT_NT"].Value);
                        c.MA_NT = row.Cells["MA_NT"].Value.ToString().Trim();
                        c.Dock = DockStyle.Fill;
                        //c.xong = false;
                        c.PrintSuccess += (sender, stt_rec, hoadon_nd51) =>
                        {
                            if (hoadon_nd51 == 1)
                            {
                                var sql = "Update Am81 Set Sl_in = Sl_in+1 Where Stt_rec=@p";
                                SqlConnect.ExecuteNonQuery(CommandType.Text, sql, new SqlParameter("@p", stt_rec));
                            }
                            sender.Dispose();
                        };

                        c.AutoPrint = FilterControl.Check1;
                        c.PrintCopies = _PrintCopies;
                        c.ShowToForm(this, V6Text.PrintIXA, true);

                        //var f = new V6Form();
                        //f.StartPosition = FormStartPosition.CenterScreen;
                        //f.WindowState = FormWindowState.Maximized;
                        //f.Text = V6Text.PrintIXA;
                        //f.Controls.Add(c);

                        //c.Disposed += delegate
                        //{
                        //    //c.xong = true;
                        //    f.Close();
                        //};

                        //c.AutoPrint = FilterControl.Check1;
                        //c.PrintCopies = _PrintCopies;

                        //f.ShowDialog();
                        
                        remove_list_g.Add(row);
                        //i--;
                    }
                }
                catch (Exception ex)
                {
                    
                    f9Error += ex.Message;
                    f9ErrorAll += ex.Message;
                }

            }
            f9Running = false;
        }
        
        void tF9_Tick(object sender, EventArgs e)
        {
            if (f9Running)
            {
                var cError = f9Error;
                f9Error = f9Error.Substring(cError.Length);
                V6ControlFormHelper.SetStatusText("F9 running "
                    + (cError.Length>0?"Error: ":"")
                    + cError);
            }
            else
            {
                ((Timer)sender).Stop();
                RemoveGridViewRow();
                btnNhan.PerformClick();
                try
                {
                    V6Tools.PrinterStatus.SetDefaultPrinter(_oldDefaultPrinter);
                }
                catch
                {
                }
                V6ControlFormHelper.SetStatusText("F9 finish "
                    + (f9ErrorAll.Length > 0 ? "Error: " : "")
                    + f9ErrorAll);

                V6ControlFormHelper.ShowMainMessage("F9 Xử lý xong!");
            }
        }
        #endregion xulyF9

        V6Invoice81 invoice = new V6Invoice81();
        private string ref_key_field = "Ma_kh";
        //private string
        protected override void ViewDetails(DataGridViewRow row)
        {
            try
            {
                var refKey = row.Cells[ref_key_field].Value.ToString().Trim();
                var data = FilterDetailData(refKey);
                dataGridView2.DataSource = data;
                SortDataGridView2();
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".AAPPR_SOA ViewDetails: " + ex.Message);
            }
        }

        private DataTable FilterDetailData(string refKey)
        {
            DataView view = new DataView(_tbl2);
            view.RowFilter = string.Format("Ma_kh = '{0}'", refKey);
            var filterData = view.ToTable();
            return filterData;
        }

        private void SortDataGridView2()
        {
            string order_field = "";
            if (radPbTheoHanTT.Checked)
            {
                order_field = "NGAY_DH";
            }
            else
            {
                order_field = "NGAY_CT";
            }


            var column = dataGridView2.Columns[order_field];
            if (column == null)
            {
                V6ControlFormHelper.ShowMainMessage("Không tìm thấy trường " + order_field);
                return;
            }
            
            dataGridView2.Sort(column, ListSortDirection.Ascending);
        }

        private void btnPhanBoTuDong_Click(object sender, EventArgs e)
        {
            PhanBoTuDong();
        }

        private void btnXoaPhanBo_Click(object sender, EventArgs e)
        {
            try
            {
                var row = dataGridView1.CurrentRow;
                if (row == null) return;

                if (this.ShowConfirmMessage(V6Text.DeleteConfirm) == DialogResult.Yes)
                {
                    SqlParameter[] plist =
                        {
                            new SqlParameter("@Stt_rec", "" + row.Cells["STT_REC"].Value),
                            new SqlParameter("@Ma_ct", "" + row.Cells["MA_CT"].Value),
                            new SqlParameter("@Tk", "" + row.Cells["TK_I"].Value),
                            new SqlParameter("@Ma_kh", "" + row.Cells["MA_KH"].Value),
                        };
                    V6BusinessHelper.ExecuteProcedure("AApttpbDel", plist);
                    btnNhan.PerformClick();
                    V6ControlFormHelper.ShowMainMessage(V6Text.Finish);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".XoaPhanBo: " + ex.Message);
            }
        }

        private void radPbTheoNgay_CheckedChanged(object sender, EventArgs e)
        {
            if (_tbl2 == null) return;
            SortDataGridView2();
        }
    }
}
