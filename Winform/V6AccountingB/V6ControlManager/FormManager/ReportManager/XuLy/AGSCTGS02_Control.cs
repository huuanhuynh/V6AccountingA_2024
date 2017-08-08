using System;
using System.Windows.Forms;
using V6Controls;
using V6Controls.Forms;
using V6Init;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ReportManager.XuLy
{
    public class AGSCTGS02_Control : XuLyBase
    {
        public AGSCTGS02_Control(string itemId, string program, string reportProcedure, string reportFile, string reportCaption,string reportCaption2)
            : base(itemId, program, reportProcedure, reportFile, reportCaption, reportCaption2, false)
        {
            _SpaceBar = false;
        }

        public override void SetStatus2Text()
        {
            V6ControlFormHelper.SetStatusText2("F4: Đăng ký chứng từ ghi sổ  ,F8: Xóa đăng ký CTGS");
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
                this.ShowErrorMessage(GetType() + ".XuLyF7: " + ex.Message);
            }
        }

        #region ==== Xử lý F8 ====


        protected override void XuLyF8()
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    if (V6Login.UserRight.AllowDelete(Name, "S07"))
                    {

                        var currentRow = dataGridView1.CurrentRow;
                        if (dataGridView1.Columns.Contains("NAM") &&dataGridView1.Columns.Contains("THANG") && dataGridView1.Columns.Contains("KHOA_CTGS"))
                        {
                            int selectedNam = ObjectAndString.ObjectToInt(currentRow.Cells
                                ["NAM"].Value);
                             int selectedthang = ObjectAndString.ObjectToInt
                               (currentRow.Cells["THANG"].Value);
                            var _numlist = "";

                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                if (row.IsSelect())
                                {
                                    var rowdata = row.ToDataDictionary();
                                    _numlist = _numlist + string.Format(",'{0}'", rowdata["KHOA_CTGS"].ToString().Trim());

                                }
                            }

                            if (_numlist.Length > 0)
                            {
                                _numlist = _numlist.Substring(1);


                                var fText = "Xóa đăng ký chứng từ ghi sổ";
                                var f = new V6Form
                                {
                                    Text = fText,
                                    AutoSize = true,
                                    FormBorderStyle = FormBorderStyle.FixedSingle
                                };

                                var ketchuyenForm = new AGSCTGS02_F8(_numlist, selectedNam,selectedthang,selectedthang, _program);


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
                                    f.Dispose();
                                };

                                f.ShowDialog(this);
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
                this.ShowErrorMessage(GetType() + ".XuLyBase XuLyF8:\n" + ex.Message);
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
                this.ShowErrorMessage(GetType() + ".XuLyF9: " + ex.Message);
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
                this.ShowErrorMessage(GetType() + ".XuLyF3: " + ex.Message);
            }
        }

    protected override void XuLyBoSungThongTinChungTuF4()
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    if (V6Login.UserRight.AllowAdd(Name, "S07"))
                    {

                        var currentRow = dataGridView1.CurrentRow;
                        if (dataGridView1.Columns.Contains("NAM") && dataGridView1.Columns.Contains("THANG") && dataGridView1.Columns.Contains("KHOA_CTGS"))
                        {
                            int selectedNam = ObjectAndString.ObjectToInt
                                (currentRow.Cells["NAM"].Value);

                            int selectedthang = ObjectAndString.ObjectToInt
                               (currentRow.Cells["THANG"].Value);

                            var _numlist = "";

                            foreach (DataGridViewRow row in dataGridView1.Rows)
                            {
                                if (row.IsSelect())
                                {
                                    var rowdata = row.ToDataDictionary();
                                    _numlist = _numlist + string.Format(",'{0}'", rowdata["KHOA_CTGS"].ToString().Trim());
                                }
                            }

                            if (_numlist.Length > 0)
                            {
                                _numlist = _numlist.Substring(1);


                                var fText = "Đăng ký chứng từ ghi sổ";
                                var f = new V6Form
                                {
                                    Text = fText,
                                    AutoSize = true,
                                    FormBorderStyle = FormBorderStyle.FixedSingle
                                };

                                var ketchuyenForm = new AGSCTGS02_F4(_numlist, selectedNam,selectedthang,selectedthang, _program);


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
                                    f.Dispose();
                                };

                                f.ShowDialog(this);
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
                this.ShowErrorMessage(GetType() + ".XuLyBase XuLyBoSungThongTinChungTuF4:\n" + ex.Message);
            }
        }

        protected override void FormatGridViewExtern()
        {
            //dataGridView1.SelectionMode = DataGridViewSelectionMode.CellSelect;
            //dataGridView1.EnableEdit(("SO_CT01,SO_CT02,SO_CT03,SO_CT04,SO_CT05,SO_CT06," +
            //                         "SO_CT07,SO_CT08,SO_CT09,SO_CT10,SO_CT11,SO_CT12").Split(','));
            //V6TableStruct table_struct = V6BusinessHelper.GetTableStruct("ALKC");
            //dataGridView1.FormatEditTextBox(table_struct, "UPPER");
        }

        public override void GridView1CellEndEdit(DataGridViewRow row, string FIELD, object fieldData)
        {
            try
            {
                
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".GridView1CellEndEdit: " + ex.Message);
            }
        }
    }
}
