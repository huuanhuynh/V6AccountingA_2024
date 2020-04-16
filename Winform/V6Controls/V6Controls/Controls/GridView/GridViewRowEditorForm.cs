using System;
using System.Data;
using System.Windows.Forms;
using V6Controls.Forms;
using V6Tools;
using V6Tools.V6Convert;

namespace V6Controls.Controls.GridView
{
    public partial class GridViewRowEditorForm : V6Form
    {
        public GridViewRowEditorForm()
        {
            InitializeComponent();
        }

        private DataGridView _grid;
        /// <summary>
        /// Field:CVvar - Field:N2
        /// </summary>
        private string[] _columnsInfo;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="grid"></param>
        /// <param name="columnsInfo">Field:CVvar - Field:N2</param>
        public GridViewRowEditorForm(DataGridView grid, params string[] columnsInfo)
        {
            InitializeComponent();
            _grid = grid;
            _columnsInfo = columnsInfo;
            MyInit();
        }

        private void MyInit()
        {
            try
            {
                int top = 10, lblLeft = 10, txtLeft = 250, txtWidth = 150;
                foreach (string columnInfo in _columnsInfo)
                {
                    string[] sss = ObjectAndString.SplitStringBy(columnInfo, ':');
                    string field = sss[0];
                    string cVvar_N2 = "";
                    if (sss.Length > 1)
                    {
                        cVvar_N2 = sss[1];
                    }
                    // Column
                    var column = _grid.Columns[field];
                    if (column == null) continue;
                    // Thêm textBox
                    if (ObjectAndString.IsNumberType(column.ValueType))
                    {
                        var decimalPlaces = 0;
                        if (cVvar_N2.Length > 0)
                        {
                            decimalPlaces = ObjectAndString.ObjectToInt(cVvar_N2.Substring(1));
                        }
                        V6NumberTextBox txt = new V6NumberTextBox()
                        {
                            AccessibleName = field,
                            BorderStyle = BorderStyle.FixedSingle,
                            Name = "txt" + field,
                            Top = top,
                            Left = txtLeft,
                            Width = txtWidth,
                            DecimalPlaces = decimalPlaces,
                        };
                        this.Controls.Add(txt);
                    }
                    else if (cVvar_N2 != "" && cVvar_N2.StartsWith("C"))
                    {
                        V6VvarTextBox txt = new V6VvarTextBox()
                        {
                            AccessibleName = field,
                            BorderStyle = BorderStyle.FixedSingle,
                            Name = "txt" + field,
                            Top = top,
                            Left = txtLeft,
                            Width = txtWidth,
                            VVar = cVvar_N2.Substring(1),
                            //F2 = !string.IsNullOrEmpty(vVar),
                            //CheckOnLeave = checkOnLeave,
                        };
                        this.Controls.Add(txt);
                    }
                    else
                    {
                        V6VvarTextBox txt = new V6VvarTextBox()
                        {
                            AccessibleName = field,
                            BorderStyle = BorderStyle.FixedSingle,
                            Name = "txt" + field,
                            Top = top,
                            Left = txtLeft,
                            Width = txtWidth,
                            
                        };
                        this.Controls.Add(txt);
                    }
                    
                    // Thêm label
                    V6Label lbl = new V6Label()
                    {
                        Name = "lbl" + field,
                        Text = column.HeaderText,
                        Top = top,
                        Left = lblLeft,
                    };
                    
                    this.Controls.Add(lbl);
                    if (btnNhan.Top < lbl.Bottom) Height += 25;
                    top += 25;
                }
                
                V6ControlFormHelper.SetFormDataDictionary(this, _grid.CurrentRow.ToDataDictionary());
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".MyInit", ex);
            }
        }

        private void btnNhan_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

    }
}
