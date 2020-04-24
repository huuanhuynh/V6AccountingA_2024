using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Controls.Forms.DanhMuc.Add_Edit;
using V6Init;
using V6Structs;
using V6Tools;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public class AINVITRI05 : XuLyBase0
    {
        private ListBox listBoxAlvitri;
        protected V6ColorDataGridView dataGridViewAlvitri;
        protected V6ColorDataGridView dataGridViewDetail2;
        private RadioButton radBoSung;
        private RadioButton radThemMoi;
        private Button btnFilter2;
        private Button btnFilter1;
        private V6VvarTextBox txtMavitri2;
        private V6VvarTextBox txtMavitri1;
        protected V6ColorDataGridView dataGridViewDetail1;

        public AINVITRI05(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
            : base(itemId, program, reportProcedure, reportFile, reportCaption, reportCaption2, true)
        {
            FilterControl.Visible = false;
            btnSuaTTMauBC.Visible = false;
            btnThemMauBC.Visible = false;
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            GetInfo();
            Ready();
        }

        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewDetail1 = new V6Controls.V6ColorDataGridView();
            this.listBoxAlvitri = new System.Windows.Forms.ListBox();
            this.dataGridViewDetail2 = new V6Controls.V6ColorDataGridView();
            this.dataGridViewAlvitri = new V6Controls.V6ColorDataGridView();
            this.radBoSung = new System.Windows.Forms.RadioButton();
            this.radThemMoi = new System.Windows.Forms.RadioButton();
            this.txtMavitri1 = new V6Controls.V6VvarTextBox();
            this.txtMavitri2 = new V6Controls.V6VvarTextBox();
            this.btnFilter1 = new System.Windows.Forms.Button();
            this.btnFilter2 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDetail1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDetail2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAlvitri)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnFilter2);
            this.panel1.Controls.Add(this.btnFilter1);
            this.panel1.Controls.Add(this.txtMavitri2);
            this.panel1.Controls.Add(this.txtMavitri1);
            this.panel1.Controls.Add(this.listBoxAlvitri);
            this.panel1.Controls.Add(this.dataGridViewAlvitri);
            this.panel1.Controls.Add(this.dataGridViewDetail2);
            this.panel1.Controls.Add(this.dataGridViewDetail1);
            // 
            // dataGridViewDetail1
            // 
            this.dataGridViewDetail1.AllowUserToAddRows = false;
            this.dataGridViewDetail1.AllowUserToDeleteRows = false;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.LightCyan;
            this.dataGridViewDetail1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            this.dataGridViewDetail1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewDetail1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dataGridViewDetail1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDetail1.Control_A = true;
            this.dataGridViewDetail1.Control_Space = true;
            this.dataGridViewDetail1.Location = new System.Drawing.Point(204, 3);
            this.dataGridViewDetail1.Name = "dataGridViewDetail1";
            this.dataGridViewDetail1.ReadOnly = true;
            dataGridViewCellStyle9.BackColor = System.Drawing.Color.LightYellow;
            this.dataGridViewDetail1.RowsDefaultCellStyle = dataGridViewCellStyle9;
            this.dataGridViewDetail1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridViewDetail1.Size = new System.Drawing.Size(353, 169);
            this.dataGridViewDetail1.Space_Bar = true;
            this.dataGridViewDetail1.TabIndex = 3;
            this.dataGridViewDetail1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridViewDetail1_KeyDown);
            // 
            // listBoxAlvitri
            // 
            this.listBoxAlvitri.FormattingEnabled = true;
            this.listBoxAlvitri.Location = new System.Drawing.Point(3, 26);
            this.listBoxAlvitri.Name = "listBoxAlvitri";
            this.listBoxAlvitri.Size = new System.Drawing.Size(195, 147);
            this.listBoxAlvitri.TabIndex = 2;
            this.listBoxAlvitri.SelectedIndexChanged += new System.EventHandler(this.listBoxAlvitri_SelectedIndexChanged);
            // 
            // dataGridViewDetail2
            // 
            this.dataGridViewDetail2.AllowUserToAddRows = false;
            this.dataGridViewDetail2.AllowUserToDeleteRows = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.LightCyan;
            this.dataGridViewDetail2.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewDetail2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewDetail2.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewDetail2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewDetail2.Location = new System.Drawing.Point(204, 177);
            this.dataGridViewDetail2.Name = "dataGridViewDetail2";
            this.dataGridViewDetail2.ReadOnly = true;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.LightYellow;
            this.dataGridViewDetail2.RowsDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewDetail2.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridViewDetail2.Size = new System.Drawing.Size(353, 169);
            this.dataGridViewDetail2.TabIndex = 7;
            // 
            // dataGridViewAlvitri
            // 
            this.dataGridViewAlvitri.AllowUserToAddRows = false;
            this.dataGridViewAlvitri.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.LightCyan;
            this.dataGridViewAlvitri.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewAlvitri.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridViewAlvitri.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewAlvitri.Control_A = true;
            this.dataGridViewAlvitri.Control_Space = true;
            this.dataGridViewAlvitri.Location = new System.Drawing.Point(3, 202);
            this.dataGridViewAlvitri.Name = "dataGridViewAlvitri";
            this.dataGridViewAlvitri.ReadOnly = true;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.LightYellow;
            this.dataGridViewAlvitri.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridViewAlvitri.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridViewAlvitri.Size = new System.Drawing.Size(195, 144);
            this.dataGridViewAlvitri.Space_Bar = true;
            this.dataGridViewAlvitri.TabIndex = 6;
            this.dataGridViewAlvitri.SelectionChanged += new System.EventHandler(this.dataGridViewAlvitri_SelectionChanged);
            // 
            // radBoSung
            // 
            this.radBoSung.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radBoSung.AutoSize = true;
            this.radBoSung.Location = new System.Drawing.Point(210, 389);
            this.radBoSung.Name = "radBoSung";
            this.radBoSung.Size = new System.Drawing.Size(64, 17);
            this.radBoSung.TabIndex = 4;
            this.radBoSung.Text = "Bổ sung";
            this.toolTipV6FormControl.SetToolTip(this.radBoSung, "Bổ sung thêm thông tin được chọn ở trên.");
            this.radBoSung.UseVisualStyleBackColor = true;
            // 
            // radThemMoi
            // 
            this.radThemMoi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.radThemMoi.AutoSize = true;
            this.radThemMoi.Checked = true;
            this.radThemMoi.Location = new System.Drawing.Point(306, 389);
            this.radThemMoi.Name = "radThemMoi";
            this.radThemMoi.Size = new System.Drawing.Size(71, 17);
            this.radThemMoi.TabIndex = 0;
            this.radThemMoi.TabStop = true;
            this.radThemMoi.Text = "Thêm mới";
            this.toolTipV6FormControl.SetToolTip(this.radThemMoi, "Xóa cũ và thêm lại");
            this.radThemMoi.UseVisualStyleBackColor = true;
            // 
            // txtMavitri1
            // 
            this.txtMavitri1.BackColor = System.Drawing.SystemColors.Window;
            this.txtMavitri1.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtMavitri1.CheckOnLeave = false;
            this.txtMavitri1.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtMavitri1.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMavitri1.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtMavitri1.HoverColor = System.Drawing.Color.Yellow;
            this.txtMavitri1.LeaveColor = System.Drawing.Color.White;
            this.txtMavitri1.Location = new System.Drawing.Point(3, 3);
            this.txtMavitri1.Name = "txtMavitri1";
            this.txtMavitri1.Size = new System.Drawing.Size(117, 20);
            this.txtMavitri1.TabIndex = 0;
            this.txtMavitri1.VVar = "MA_VITRI";
            // 
            // txtMavitri2
            // 
            this.txtMavitri2.BackColor = System.Drawing.SystemColors.Window;
            this.txtMavitri2.BackColorDisabled = System.Drawing.SystemColors.Control;
            this.txtMavitri2.CheckOnLeave = false;
            this.txtMavitri2.EnterColor = System.Drawing.Color.PaleGreen;
            this.txtMavitri2.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtMavitri2.ForeColorDisabled = System.Drawing.SystemColors.WindowText;
            this.txtMavitri2.HoverColor = System.Drawing.Color.Yellow;
            this.txtMavitri2.LeaveColor = System.Drawing.Color.White;
            this.txtMavitri2.Location = new System.Drawing.Point(3, 177);
            this.txtMavitri2.Name = "txtMavitri2";
            this.txtMavitri2.Size = new System.Drawing.Size(117, 20);
            this.txtMavitri2.TabIndex = 4;
            this.txtMavitri2.VVar = "MA_VITRI";
            // 
            // btnFilter1
            // 
            this.btnFilter1.Location = new System.Drawing.Point(123, 3);
            this.btnFilter1.Name = "btnFilter1";
            this.btnFilter1.Size = new System.Drawing.Size(75, 23);
            this.btnFilter1.TabIndex = 1;
            this.btnFilter1.Text = "Lọc";
            this.btnFilter1.UseVisualStyleBackColor = true;
            this.btnFilter1.Click += new System.EventHandler(this.btnFilter1_Click);
            // 
            // btnFilter2
            // 
            this.btnFilter2.Location = new System.Drawing.Point(123, 175);
            this.btnFilter2.Name = "btnFilter2";
            this.btnFilter2.Size = new System.Drawing.Size(75, 23);
            this.btnFilter2.TabIndex = 5;
            this.btnFilter2.Text = "Lọc";
            this.btnFilter2.UseVisualStyleBackColor = true;
            this.btnFilter2.Click += new System.EventHandler(this.btnFilter2_Click);
            // 
            // AINVITRI05
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.Controls.Add(this.radThemMoi);
            this.Controls.Add(this.radBoSung);
            this.Name = "AINVITRI05";
            this.SizeChanged += new System.EventHandler(this.AINVITRI05_SizeChanged);
            this.Controls.SetChildIndex(this.btnNhan, 0);
            this.Controls.SetChildIndex(this.btnHuy, 0);
            this.Controls.SetChildIndex(this.radBoSung, 0);
            this.Controls.SetChildIndex(this.radThemMoi, 0);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDetail1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewDetail2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewAlvitri)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        public override void SetStatus2Text()
        {
            V6ControlFormHelper.SetStatusText2(".");
        }

        protected override void Nhan()
        {
            cList = "";
            foreach (DataGridViewRow row in dataGridViewDetail1.Rows)
            {
                if (row.IsSelect())
                {
                    cList += "," + row.Cells["MA_VT"].Value.ToString().Trim();
                }
            }
            if (cList.Length > 1) cList = cList.Substring(1);
            cType = radThemMoi.Checked ? "1" : "0";
            if (string.IsNullOrEmpty(cList))
            {
                this.ShowMessage("Chưa chọn chi tiết!\nSử dụng phím cách(Space_bar) để chọn");
                return;
            }

            base.Nhan();
        }

        //protected override void ExecuteProcedure()
        //{
        //    base.ExecuteProcedure();
        //}
        private string cType = "1";
        /// <summary>
        /// Cộng dồn các mã vt được chọn ở GridViewDetail1
        /// </summary>
        private string cList = "";
        /// <summary>
        /// Mã được chọn ở listBox
        /// </summary>
        private string ma_vitri1 = "";
        protected override void TinhToan()
        {
            try
            {
                _executing = true;
                foreach (DataGridViewRow row in dataGridViewAlvitri.Rows)
                {
                    if (row.IsSelect())
                    {
                        string cMa_vitri = row.Cells["MA_VITRI"].Value.ToString().Trim();
                        _message = V6Text.Executing + " " + cMa_vitri;
                        SqlParameter[] plist =
                        {
                            new SqlParameter("@cMa_vitri", ma_vitri1),
                            new SqlParameter("@cList", cList),
                            new SqlParameter("@cNewMa_Vitri", cMa_vitri),
                            new SqlParameter("@cType", cType),
                        };
                        V6BusinessHelper.ExecuteProcedure(_reportProcedure, plist);
                    }
                }

                _executesuccess = true;
                _executing = false;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".TinhToan", ex);
                _message = ex.Message;
                _executing = false;
                _executesuccess = false;
            }
        }

        private V6TableName CurrentTable = V6TableName.Alvitrict;
        private string _tableName = "Alvitrict";
        private void DoEdit()
        {
            var dataGridView1 = dataGridViewDetail1;
            IDictionary<string, object> _data;
            try
            {
                SaveSelectedCellLocation(dataGridView1);
                if (CurrentTable == V6TableName.None)
                {
                    this.ShowWarningMessage("Hãy chọn danh mục!");
                }
                else
                {
                    DataGridViewRow row = dataGridView1.GetFirstSelectedRow();
                    if (row != null)
                    {
                        var keys = new SortedDictionary<string, object>();
                        if (dataGridView1.Columns.Contains("UID")) //Luôn có trong thiết kế rồi.
                            keys.Add("UID", row.Cells["UID"].Value);

                        //if (KeyFields != null)
                        //    foreach (var keyField in KeyFields)
                        //    {
                        //        if (dataGridView1.Columns.Contains(keyField))
                        //        {
                        //            keys[keyField] = row.Cells[keyField].Value;
                        //        }
                        //    }

                        _data = row.ToDataDictionary();
                        FormAddEdit f;
                        if (CurrentTable == V6TableName.Notable)
                        {
                            f = new FormAddEdit(_tableName, V6Mode.Edit, keys, _data);
                        }
                        else
                        {
                            f = new FormAddEdit(CurrentTable, V6Mode.Edit, keys, _data);
                        }
                        f.AfterInitControl += f_AfterInitControl;
                        f.InitFormControl();
                        f.ParentData = ((DataRowView)listBoxAlvitri.SelectedItem).Row.ToDataDictionary();
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
        private void FCallReloadEvent(object sender, EventArgs eventArgs)
        {
            //ReLoad();
            LoadDataVitriCT1(ma_vitri1);
            dataGridViewAlvitri_SelectionChanged(null, null);
        }

        protected override void DoAfterExecuteSuccess()
        {
            LoadDataVitriCT1(ma_vitri1);
            dataGridViewAlvitri_SelectionChanged(null, null);
        }

        /// <summary>
        /// Khi sửa thành công, cập nhập lại dòng được sửa, chưa kiểm ok cancel.
        /// </summary>
        /// <param name="data">Dữ liệu đã sửa</param>
        private void f_UpdateSuccess(IDictionary<string, object> data)
        {
            try
            {
                if (CurrentTable == V6TableName.Notable
                    && !string.IsNullOrEmpty(_alvitrict_config.TABLE_VIEW)
                    && V6BusinessHelper.IsExistDatabaseTable(_alvitrict_config.TABLE_VIEW))
                {
                    //ReLoad();
                    LoadDataVitriCT1(ma_vitri1);
                    dataGridViewAlvitri_SelectionChanged(null, null);
                }
                else
                {
                    if (data == null) return;
                    DataGridViewRow row = dataGridViewDetail1.GetFirstSelectedRow();
                    V6ControlFormHelper.UpdateGridViewRow(row, data);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".f_UpdateSuccess", ex);
            }
        }

        private void DoAdd()
        {
            var dataGridView1 = dataGridViewDetail1;
            IDictionary<string, object> _data;
            FormAddEdit f;
            try
            {
                SaveSelectedCellLocation(dataGridView1);
                if (CurrentTable == V6TableName.None)
                {
                    this.ShowWarningMessage("Hãy chọn danh mục!");
                }
                else
                {
                    DataGridViewRow row = dataGridView1.GetFirstSelectedRow();

                    if (row != null)
                    {
                        var keys = new SortedDictionary<string, object>();
                        if (dataGridView1.Columns.Contains("UID")) //Luôn có trong thiết kế rồi.
                            keys.Add("UID", row.Cells["UID"].Value);

                        //if (KeyFields != null)
                        //    foreach (var keyField in KeyFields)
                        //    {
                        //        if (dataGridView1.Columns.Contains(keyField))
                        //        {
                        //            keys[keyField] = row.Cells[keyField].Value;
                        //        }
                        //    }

                        _data = row.ToDataDictionary();
                        if (CurrentTable == V6TableName.Notable)
                        {
                            f = new FormAddEdit(_tableName, V6Mode.Add, keys, _data);
                        }
                        else
                        {
                            f = new FormAddEdit(CurrentTable, V6Mode.Add, keys, _data);
                        }
                    }
                    else
                    {
                        if (CurrentTable == V6TableName.Notable)
                        {
                            f = new FormAddEdit(_tableName);
                        }
                        else
                        {
                            f = new FormAddEdit(CurrentTable);
                        }
                    }
                    f.AfterInitControl += f_AfterInitControl;
                    f.InitFormControl();
                    f.ParentData = ((DataRowView)listBoxAlvitri.SelectedItem).Row.ToDataDictionary();
                    f.InsertSuccessEvent += f_InsertSuccess;
                    f.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".DoAdd " + _tableName, ex);
            }
        }

        void f_AfterInitControl(object sender, EventArgs e)
        {
            LoadAdvanceControls((Control)sender, CurrentTable == V6TableName.Notable?_tableName:CurrentTable.ToString());
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

        private void f_InsertSuccess(IDictionary<string, object> data)
        {
            try
            {
                //ReLoad();
                LoadDataVitriCT1(ma_vitri1);
                dataGridViewAlvitri_SelectionChanged(null, null);
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".f_InsertSuccess", ex);
            }
        }
        

        //Biến resize
        private int align_right;
        private int align_bottom;
        /// <summary>
        /// Lấy thông tin cho resize control.
        /// </summary>
        private void GetInfo()
        {
            align_right = panel1.Width - dataGridViewDetail1.Right;
            align_bottom = panel1.Height - dataGridViewAlvitri.Bottom;
        }
        private void AINVITRI05_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                int height1 = panel1.Height/2 - 5;
                listBoxAlvitri.Top = txtMavitri1.Bottom + 5;
                listBoxAlvitri.Height = height1 - listBoxAlvitri.Top;
                dataGridViewDetail1.Top = txtMavitri1.Top;
                dataGridViewDetail1.Height = height1;

                int top2 = dataGridViewDetail1.Bottom + 5;
                
                dataGridViewDetail2.Top = top2;
                dataGridViewDetail2.Height = height1;
                txtMavitri2.Top = top2;
                btnFilter2.Top = top2;
                dataGridViewAlvitri.Top = txtMavitri2.Bottom + 5;
                dataGridViewAlvitri.Height = dataGridViewDetail2.Bottom - dataGridViewAlvitri.Top;
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".SizeChanged", ex);
            }
        }

        private void btnFilter1_Click(object sender, EventArgs e)
        {
            LoadListVitri();
        }

        private void LoadListVitri()
        {
            try
            {
                SqlParameter[] plist =
                {
                    new SqlParameter("@p1", txtMavitri1.Text + "%"),
                };
                var data = V6BusinessHelper.Select("ALVITRI", "*", "MA_VITRI like @p1", "", "MA_VITRI", plist).Data;
                
                listBoxAlvitri.DataSource = data;
                listBoxAlvitri.DisplayMember = V6Setting.IsVietnamese ? "TEN" : "TEN2";
                listBoxAlvitri.ValueMember = "MA_VITRI";
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".LoadListVitri", ex);
            }
        }

        private void btnFilter2_Click(object sender, EventArgs e)
        {
            LoadDataVitri();
        }

        private void LoadDataVitri()
        {
            try
            {
                SqlParameter[] plist =
                {
                    new SqlParameter("@p1", txtMavitri2.Text + "%"),
                };
                var data = V6BusinessHelper.Select("ALVITRI", "*", "MA_VITRI like @p1", "", "MA_VITRI", plist).Data;

                dataGridViewAlvitri.DataSource = data;
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".LoadDataVitri", ex);
            }
        }

        private AldmConfig _alvitrict_config;
        private void LoadDataVitriCT1(string id)
        {
            try
            {
                ma_vitri1 = id;
                SqlParameter[] plist =
                {
                    new SqlParameter("@p1", id),
                };
                var _data = V6BusinessHelper.Select("ALVITRICT", "*", "MA_VITRI = @p1", "", "MA_VITRI", plist).Data;
                
                //if (_viewData == null)
                //    _viewData = new DataView(_data);
                //if (string.IsNullOrEmpty(id))
                //{
                //    _viewData.RowFilter = string.Format("{0} = '' or {0} is null", _field, id);
                //}
                //else
                //{
                //    _viewData.RowFilter = string.Format("{0} = '{1}'", _field, id);
                //}
                //_viewData.Sort = _idField;
                
                dataGridViewDetail1.DataSource = _data;
                dataGridViewDetail1.Refresh();

                _alvitrict_config = ConfigManager.GetAldmConfig("ALVITRICT");

                string showFields = _alvitrict_config.GRDS_V1;
                string formatStrings = _alvitrict_config.GRDF_V1;
                string headerString = V6Setting.IsVietnamese ? _alvitrict_config.GRDHV_V1: _alvitrict_config.GRDHE_V1;
                V6ControlFormHelper.FormatGridViewAndHeader(dataGridViewDetail1, showFields, formatStrings, headerString);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".ViewData " + ex.Message);
            }
        }

        private void LoadDataVitriCT2(string id)
        {
            try
            {
                SqlParameter[] plist =
                {
                    new SqlParameter("@p1", id),
                };
                var _data = V6BusinessHelper.Select("ALVITRICT", "*", "MA_VITRI = @p1", "", "MA_VITRI", plist).Data;

                dataGridViewDetail2.DataSource = _data;
                dataGridViewDetail2.Refresh();

                _alvitrict_config = ConfigManager.GetAldmConfig("ALVITRICT");

                string showFields = _alvitrict_config.GRDS_V1;
                string formatStrings = _alvitrict_config.GRDF_V1;
                string headerString = V6Setting.IsVietnamese ? _alvitrict_config.GRDHV_V1 : _alvitrict_config.GRDHE_V1;
                V6ControlFormHelper.FormatGridViewAndHeader(dataGridViewDetail2, showFields, formatStrings, headerString);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".ViewData " + ex.Message);
            }
        }

        private void listBoxAlvitri_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!IsReady) return;
            LoadDataVitriCT1(listBoxAlvitri.SelectedValue.ToString().Trim());
        }

        private void dataGridViewAlvitri_SelectionChanged(object sender, EventArgs e)
        {
            if (!IsReady || dataGridViewAlvitri.CurrentRow == null) return;
            LoadDataVitriCT2(dataGridViewAlvitri.CurrentRow.Cells["MA_VITRI"].Value.ToString().Trim());
        }

        private void dataGridViewDetail1_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.F3)
                {
                    DoEdit();
                }
                else if (e.KeyCode == Keys.F4)
                {
                    DoAdd();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".KeyDown", ex);
            }
        }
    }
}
