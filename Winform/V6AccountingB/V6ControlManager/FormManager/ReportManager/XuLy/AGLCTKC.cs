using System;
using System.Collections.Generic;
using System.Windows.Forms;
using V6AccountingBusiness;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Structs;
using V6Tools;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public class AGLCTKC : XuLyBase
    {
        public AGLCTKC(string itemId, string program, string reportProcedure, string reportFile, string reportCaption, string reportCaption2)
            : base(itemId, program, reportProcedure, reportFile, reportCaption, reportCaption2, false)
        {
            
        }

        public override void SetStatus2Text()
        {
            V6ControlFormHelper.SetStatusText2("F4: Tạo bút toán kết chuyển,F8: Xóa bút toán kết chuyển");
        }

        protected override void MakeReport2()
        {
            Load_Data = true;//Thay đổi cờ.
            base.MakeReport2();
        }

        protected override void XuLyF7()
        {
            try
            {
               
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLyF7", ex);
            }
        }

        #region ==== Xử lý F8 ====


        protected override void XuLyF8()
        {
            try
            {
                if (_tbl == null || dataGridView1.CurrentRow == null)
                {
                    this.ShowWarningMessage("Chưa chọn dữ liệu!");
                    return;
                }

                if (!V6Login.UserRight.AllowDelete(Name, "GL3")) return;

                var currentRow = dataGridView1.CurrentRow;
                if (dataGridView1.Columns.Contains("NAM") && dataGridView1.Columns.Contains("STT"))
                {
                    var selectedStt = currentRow.Cells
                        ["STT"].Value;
                    int selectedNam = ObjectAndString.ObjectToInt(currentRow.Cells
                        ["NAM"].Value);

                    var _numlist = "";

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.IsSelect())
                        {
                            var rowdata = row.ToDataDictionary();
                            _numlist = _numlist + "," + rowdata["STT"].ToString().Trim();
                        }
                    }

                    if (_numlist.Length <= 0) return;

                    _numlist = _numlist.Substring(1);
                    var f = new V6Form
                    {
                        Text = "Xóa kết chuyển tự động ",
                        AutoSize = true,
                        FormBorderStyle = FormBorderStyle.FixedSingle
                    };

                    var ketchuyenForm = new AGLCTKC_F8(_numlist, selectedNam, _program);


                    ketchuyenForm.UpdateSuccessEvent += delegate
                    {
                        foreach (DataGridViewRow row in dataGridView1.Rows)
                        {
                            if (row.IsSelect())
                            {
                                row.UnSelect();
                            }
                        }
                    };

                    f.Controls.Add(ketchuyenForm);
                    ketchuyenForm.Disposed += delegate
                    {
                        f.Close();
                    };

                    f.ShowDialog(this);
                    SetStatus2Text();
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLyF8", ex);
            }
        }

        #endregion xử lý F8

        #region ==== Xử lý F9 ====
        protected override void XuLyF9()
        {
            try
            {
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLyF9", ex);
            }
        }
        
        #endregion xulyF9

        protected override void XuLyHienThiFormSuaChungTuF3()
        {
            try
            {
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLyF3", ex);
            }
        }

        protected override void XuLyBoSungThongTinChungTuF4()
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    if (V6Login.UserRight.AllowAdd(Name, "GL3"))
                    {

                        var currentRow = dataGridView1.CurrentRow;
                        if (dataGridView1.Columns.Contains("NAM") && dataGridView1.Columns.Contains("STT"))
                        {
                            var selectedStt = currentRow.Cells["STT"].Value;
                            int selectedNam = ObjectAndString.ObjectToInt
                                (currentRow.Cells["NAM"].Value);

                            var _numlist = "";

                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                if (row.IsSelect())
                                {
                                    var rowdata = row.ToDataDictionary();
                                    _numlist = _numlist + "," + rowdata["STT"].ToString().Trim();

                                }
                            }

                            if (_numlist.Length > 0)
                            {
                                _numlist = _numlist.Substring(1);


                                var fText = "Kết chuyển tự động ";
                                var ketchuyenForm = new AGLCTKC_F4(_numlist, selectedNam, _reportProcedure);

                                ketchuyenForm.UpdateSuccessEvent += delegate
                                {
                                    foreach (DataGridViewRow row in dataGridView1.Rows)
                                    {
                                        if (row.IsSelect())
                                        {
                                            row.UnSelect();
                                        }
                                    }
                                    V6ControlFormHelper.ShowMainMessage(V6Text.Finish);
                                };

                                ketchuyenForm.ShowToForm(this, fText);
                                SetStatus2Text();
                            }
                        }
                    }
                }
                else
                {
                    this.ShowWarningMessage("Chưa có dữ liệu!");
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".XuLyBase XuLyBoSungThongTinChungTuF4", ex);
            }
        }

        public override void FormatGridViewExtern()
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;
            dataGridView1.SetEditColumn(("SO_CT01,SO_CT02,SO_CT03,SO_CT04,SO_CT05,SO_CT06," +
                                     "SO_CT07,SO_CT08,SO_CT09,SO_CT10,SO_CT11,SO_CT12").Split(','));
            V6TableStruct table_struct = V6BusinessHelper.GetTableStruct("ALKC");
            dataGridView1.FormatEditTextBox(table_struct, "UPPER");
        }

        public override void GridView1CellEndEdit(DataGridViewRow row, string FIELD, object fieldData)
        {
            try
            {
                var data = new SortedDictionary<string, object> {{FIELD, fieldData}};
                var keys = new SortedDictionary<string, object>
                {
                    {"NAM", row.Cells["NAM"].Value},
                    {"STT", row.Cells["STT"].Value}
                };
                if (V6BusinessHelper.UpdateSimple("ALKC", data, keys) > 0)
                {
                    V6ControlFormHelper.ShowMainMessage(V6Text.Text("UPDATED"));
                }
                else
                {
                    V6ControlFormHelper.ShowMainMessage(V6Text.Text("NOUPDATE"));
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".GridView1CellEndEdit", ex);
            }
        }
    }
}
