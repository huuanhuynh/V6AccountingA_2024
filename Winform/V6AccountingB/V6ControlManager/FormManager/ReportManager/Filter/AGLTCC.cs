﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Controls.Forms.DanhMuc.Add_Edit;
using V6Init;
using V6Structs;
using V6Tools;

namespace V6ControlManager.FormManager.ReportManager.Filter
{
    public partial class AGLTCC : FilterBase
    {
        public AGLTCC()
        {
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            try
            {
                F3 = false;
                F5 = false;
                txtma_maubc.Text = "GLTCC";
                txtNgay_ct1.SetValue(V6Setting.M_ngay_ct1);
                txtNgay_ct2.SetValue(V6Setting.M_ngay_ct2);
                txtNgay_ct3.SetValue(V6Setting.M_ngay_ct1.AddMonths(-1));
                txtNgay_ct4.SetValue(V6Setting.M_ngay_ct2.AddMonths(-1));

                txtMaDvcs.VvarTextBox.Text = V6Login.Madvcs;
                if (V6Login.MadvcsCount <= 1)
                {
                    txtMaDvcs.Enabled = false;
                }

                SetHideFields("V");
                LoadAlmaubc();
                if (V6Login.IsAdmin) chkHienTatCa.Enabled = true;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".Init", ex);
            }
        }

        public void SetHideFields(string lang)
        {
            GridViewHideFields = new SortedDictionary<string, string>();
            if (lang == "V")
            {
                GridViewHideFields = new SortedDictionary<string, string> {{"TAG", "TAG"}};
            }
            else
            {
                
            }
        }

        private DataTable maubcData;
        private void LoadAlmaubc()
        {
            try
            {
                maubcData = V6BusinessHelper.Select("ALMAUBC",
                    "*", (chkHienTatCa.Checked ? "" : "[status]='1' and ") + "ma_maubc='" + txtma_maubc.Text.ToUpper() + "'",
                    "", "[ORDER]").Data;

                cboMaubc.ValueMember = "file_maubc";
                cboMaubc.DisplayMember = V6Setting.IsVietnamese ? "ten_maubc" : "ten_maubc2";
                cboMaubc.DataSource = maubcData;
                cboMaubc.ValueMember = "file_maubc";
                cboMaubc.DisplayMember = V6Setting.IsVietnamese ? "ten_maubc" : "ten_maubc2";
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(string.Format("{0} {1} {2} {3}", V6Login.ClientName, GetType(), MethodBase.GetCurrentMethod().Name, ex.Message));
            }
        }

        /// <summary>
        /// Lay cac tham so cho procedure
        /// </summary>
        /// <returns>cKey</returns>
        public override List<SqlParameter> GetFilterParameters()
        {

            //@Ngay_ct1 SMALLDATETIME,
            //@Ngay_ct2 SMALLDATETIME,
            //@Ngay_ct3 SMALLDATETIME,
            //@Ngay_ct4 SMALLDATETIME,
            //@Ma_dvcs VARCHAR(50),
            //@Luyke BIT,
            //@Mau VARCHAR(50),
            //@Advance VARCHAR(8000) = '',
            //@OutputCmd VARCHAR(8000) = ''      

            string maubc = "";
            if (cboMaubc.Items.Count > 0 && cboMaubc.SelectedIndex >= 0)
            {
                maubc = cboMaubc.SelectedValue.ToString().Trim();
            }

            if (maubc == "")
            {
                maubc = "GLTCC";
            }
            
            int luyke =chk_Luy_ke.Checked ? 1 : 0;
            var ma_dvcs = txtMaDvcs.IsSelected ? txtMaDvcs.StringValue.Trim() + "%" : "%";

        var result = new List<SqlParameter>
            {
                 
                 
                new SqlParameter("@Ngay_ct1", txtNgay_ct1.YYYYMMDD),
                new SqlParameter("@Ngay_ct2", txtNgay_ct2.YYYYMMDD),
                new SqlParameter("@Ngay_ct3", txtNgay_ct3.YYYYMMDD),
                new SqlParameter("@Ngay_ct4", txtNgay_ct4.YYYYMMDD),
                new SqlParameter("@Ma_dvcs", ma_dvcs),
                new SqlParameter("@Luyke", luyke),
                new SqlParameter("@Mau", maubc),
                new SqlParameter("@Advance", ""),
                new SqlParameter("@OutputCmd","")


                
            };
            
            return result;
        }


        V6TableName CurrentTable = V6TableName.Almaubc;
        private void DoAdd()
        {
            try
            {
                if (CurrentTable == V6TableName.None)
                {
                    this.ShowWarningMessage("Hãy chọn danh mục!");
                }
                else
                {


                    if (maubcData != null)
                    {
                        var row0 = maubcData.Rows[cboMaubc.SelectedIndex];

                        var keys = new SortedDictionary<string, object>();
                        if (maubcData.Columns.Contains("UID"))
                            keys.Add("UID", row0["UID"]);

                        //if (KeyFields != null)
                        //    foreach (var keyField in KeyFields)
                        //    {
                        //        if (dataGridView1.Columns.Contains(keyField))
                        //        {
                        //            keys[keyField] = row.Cells[keyField].Value;
                        //        }
                        //    }

                        var _data = row0.ToDataDictionary();
                        var f = new FormAddEdit(CurrentTable.ToString(), V6Mode.Add, keys, _data);
                        f.AfterInitControl += f_AfterInitControl;
                        f.InitFormControl(FindParent<V6FormControl>());
                        f.InsertSuccessEvent += f_InsertSuccess;
                        f.ShowDialog(this);
                    }
                    else
                    {
                        var f = new FormAddEdit(CurrentTable.ToString(), V6Mode.Add, null, null);
                        f.AfterInitControl += f_AfterInitControl;
                        f.InitFormControl(FindParent<V6FormControl>());
                        f.InsertSuccessEvent += f_InsertSuccess;
                        f.ShowDialog(this);
                    }
                }
            }
            catch (Exception ex)
            {
                V6Message.Show(ex.Message);
            }
        }

        void f_AfterInitControl(object sender, EventArgs e)
        {
            LoadAdvanceControls((Control)sender, CurrentTable.ToString());
        }

        protected void LoadAdvanceControls(Control form, string ma_ct)
        {
            try
            {
                FormManagerHelper.CreateAdvanceFormControls(form, ma_ct, new Dictionary<string, object>());
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".LoadAdvanceControls " + ma_ct, ex);
            }
        }

        void f_InsertSuccess(IDictionary<string, object> dataDic)
        {
            try
            {
                var newRow = maubcData.NewRow();
                foreach (KeyValuePair<string, object> item in dataDic)
                {
                    if (maubcData.Columns.Contains(item.Key))
                        newRow[item.Key] = item.Value;
                }
                maubcData.Rows.Add(newRow);
                cboMaubc.SelectedIndex = maubcData.Rows.Count - 1;
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".InsertSuccessHandler: " + ex.Message);
            }
        }

        private void DoEdit()
        {
            try
            {
                if (CurrentTable == V6TableName.None)
                {
                    this.ShowWarningMessage("Hãy chọn danh mục!");
                }
                else
                {
                    //DataGridViewRow row = dataGridView1.GetFirstSelectedRow();

                    if (cboMaubc.SelectedIndex >= 0)
                    {
                        var row0 = maubcData.Rows[cboMaubc.SelectedIndex];
                        var keys = new SortedDictionary<string, object>();
                        if (maubcData.Columns.Contains("UID")) //Luôn có trong thiết kế rồi.
                            keys.Add("UID", row0["UID"]);

                        //if (KeyFields != null)
                        //    foreach (var keyField in KeyFields)
                        //    {
                        //        if (dataGridView1.Columns.Contains(keyField))
                        //        {
                        //            keys[keyField] = row.Cells[keyField].Value;
                        //        }
                        //    }

                        var _data = row0.ToDataDictionary();
                        var f = new FormAddEdit(CurrentTable.ToString(), V6Mode.Edit, keys, _data);
                        f.AfterInitControl += f_AfterInitControl;
                        f.InitFormControl(FindParent<V6FormControl>());
                        f.UpdateSuccessEvent += f_UpdateSuccess;
                        f.CallReloadEvent += FCallReloadEvent;
                        f.ShowDialog(this);
                    }
                    else
                    {
                        this.ShowWarningMessage(V6Text.NoSelection);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".DoEdit", ex);
            }
        }

        private void FCallReloadEvent(object sender, EventArgs e)
        {

        }

        private void f_UpdateSuccess(IDictionary<string, object> dataDic)
        {
            try
            {
                var editRow = maubcData.Rows[cboMaubc.SelectedIndex];
                foreach (KeyValuePair<string, object> item in dataDic)
                {
                    if (maubcData.Columns.Contains(item.Key))
                        editRow[item.Key] = item.Value;
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".UpdateSuccessHandler: " + ex.Message);
            }
        }

        private void DoEditDetails()
        {
            try
            {
                if (cboMaubc.SelectedIndex >= 0)
                {
                    var row0 = maubcData.Rows[cboMaubc.SelectedIndex];
                    var ma_maubc = row0["file_maubc"].ToString().Trim();
                    var filter = "mau_bc='" + ma_maubc + "'";
                    var parentData = row0.ToDataDictionary();
                    parentData["MAU_BC"] = parentData["FILE_MAUBC"];
                    BangCanDoiTaiChinhForm form = new BangCanDoiTaiChinhForm(filter, parentData);
                    form.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".DoEditDetails: " + ex.Message);
            }
        }

        private void btnThemMau_Click(object sender, EventArgs e)
        {
            if (V6Login.UserRight.AllowAdd("", "Almaubc".ToUpper() + "6"))
            {
                DoAdd();
            }
            else
            {
                V6ControlFormHelper.NoRightWarning();
            }
        }

        private void btnSuaTTMau_Click(object sender, EventArgs e)
        {
            if (V6Login.UserRight.AllowEdit("", CurrentTable.ToString().ToUpper() + "6"))
            {
                DoEdit();
            }
            else
            {
                V6ControlFormHelper.NoRightWarning();
            }
        }

        private void btnSuaCTMau_Click(object sender, EventArgs e)
        {
            if (V6Login.UserRight.AllowEdit("", CurrentTable.ToString().ToUpper() + "6"))
            {
                DoEditDetails();
            }
            else
            {
                V6ControlFormHelper.NoRightWarning();
            }
        }

        private void chkHienTatCa_CheckedChanged(object sender, EventArgs e)
        {
            LoadAlmaubc();
        }

        
    }
}
