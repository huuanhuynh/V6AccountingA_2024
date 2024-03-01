using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using V6Controls;
using V6Controls.Forms;
using V6Tools;
using V6Tools.V6Convert;

namespace V6ControlManager.FormManager.ChungTuManager
{
    public partial class HistoryStatusForm : V6Form
    {
        DataTable _data = null;
        public HistoryStatusForm()
        {
            InitializeComponent();
            //MyInit();
        }

        public HistoryStatusForm(DataTable data)
        {
            _data = data.Copy();
            InitializeComponent();
            MyInit();
        }

        //public IDictionary<string, object> Data { get; set; }
        private void MyInit()
        {
            try
            {
                //dataGridView1.DataSource = _data;
                GenViews();
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".MyInit", ex);
            }
        }

        private void HistoryStatusForm_Load(object sender, EventArgs e)
        {
            //GenDataToGridView();
        }

        private void GenViews()
        {
            try
            {
                int top = 3;
                foreach (DataRow row in _data.Rows)
                {
                    if (top > 3)
                    {
                        var u = GenUpArrow(row);
                        u.Left = 3;
                        u.Top = top;
                        top += u.Height;
                        panel1.Controls.Add(u);
                    }

                    var v = GenRow(row);
                    v.Left = 3;
                    v.Top = top;
                    top += v.Height;

                    panel1.Controls.Add(v);
                }
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".GenDataToGridView", ex);
            }
        }

        Panel GenRow(DataRow row)
        {
            Panel p = new Panel();
            p.Size = new System.Drawing.Size(panel1.Width - 2, 22);
            p.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            var color = ObjectAndString.StringToColor(row["COLOR"].ToString());

            Label lblDateTime = new Label(); lblDateTime.AutoSize = false;
            lblDateTime.Size = new System.Drawing.Size(120, 20);
            lblDateTime.Text = ObjectAndString.ObjectToString(row["DATE2"], "yyyy-MM-dd ") + row["TIME2"];
            lblDateTime.Top = 8; lblDateTime.Left = 3;
            p.Controls.Add(lblDateTime);

            Label lblColor = new Label(); lblColor.AutoSize = false;
            lblColor.Size = new System.Drawing.Size(30, 20);
            lblColor.Text = "●"; lblColor.Font = new System.Drawing.Font(lblColor.Font.FontFamily, lblColor.Font.Size + 8);
            lblColor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lblColor.ForeColor = color;
            lblColor.Top = 1; lblColor.Left = 3 + lblDateTime.Width;
            p.Controls.Add(lblColor);

            Label lblName = new Label(); lblName.AutoSize = true;
            lblName.Size = new System.Drawing.Size(panel1.Width - lblDateTime.Width - lblColor.Width - 9, 20);
            lblName.Text = row["NAME"].ToString();// lblName.Font = new System.Drawing.Font(lblName.Font.FontFamily, lblName.Font.Size + 3);
            lblName.ForeColor = color;
            lblName.Top = 8; lblName.Left = 153;
            p.Controls.Add(lblName);

            return p;
        }

        Panel GenUpArrow(DataRow row)
        {
            Panel p = new Panel();
            p.Size = new System.Drawing.Size(panel1.Width - 2, 20);
            p.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            var color = ObjectAndString.StringToColor(row["COLOR"].ToString());
            
            Label lblColor = new Label(); lblColor.AutoSize = false;
            lblColor.Size = new System.Drawing.Size(30, 20);
            lblColor.Text = "↑"; lblColor.Font = new System.Drawing.Font(lblColor.Font.FontFamily, lblColor.Font.Size + 5, System.Drawing.FontStyle.Bold);
            lblColor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            lblColor.ForeColor = color;
            lblColor.Top = 1; lblColor.Left = 3 + 120;
            p.Controls.Add(lblColor);

            return p;
        }

    }
}
