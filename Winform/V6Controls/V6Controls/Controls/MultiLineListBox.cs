/* 
 * This class was written by Nishant S [nishforever@vsnl.com]
 * You may freely use this class in your freeware, shareware or even
 * commercial applications. But you must retain this header information.
 * Feel free to report any bugs or suggestions you have :-)  
*/

using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using V6Tools.V6Convert;

namespace V6Controls.Controls
{
    public class MultiLineListBox : ListBox
    {
        public MultiLineListBox()
        {
            this.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.ScrollAlwaysVisible = true;
            tbox.Hide();
            tbox.mllb = this;
            Controls.Add(tbox);

        }

        /// <summary>
        /// 0: không dùng, 1: dùng data COLOR
        /// </summary>
        [DefaultValue(0)]
        [Description("0: không dùng, 1: dùng data COLOR, 2: dùng BackColor")]
        public int UseBackColor { get; set; }

        protected override void OnMeasureItem(MeasureItemEventArgs e)
        {
            if (Site != null)
                return;
            if (e.Index > -1)
            {
                string s = "";
                var lineItem = Items[e.Index];
                if (lineItem is DataRowView)
                {
                    s = ((DataRowView)lineItem)[DisplayMember] + "";
                }
                else
                {
                    s = Items[e.Index] + "";
                }
                SizeF sf = e.Graphics.MeasureString(s, Font, Width);
                //int htex = (e.Index == 0) ? 15 : 10;
                e.ItemHeight = (int)sf.Height;// + htex;
                e.ItemWidth = Width;
            }
        }

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (Site != null)
                return;
            if (e.Index > -1)
            {
                string s = "";
                var lineItem = Items[e.Index];
                var lineColor = SystemColors.Window;
                if (lineItem is DataRowView)
                {
                    DataRowView rv = lineItem as DataRowView;
                    s = rv[DisplayMember] + "";
                    if (UseBackColor == 1 && rv.Row.Table.Columns.Contains("COLOR"))
                    {
                        lineColor = ObjectAndString.StringToColor(rv["COLOR"] + "");
                    }
                    else if (UseBackColor == 2)
                    {
                        lineColor = BackColor;
                    }
                }
                else
                {
                    s = Items[e.Index] + "";
                }

                if ((e.State & DrawItemState.Focus) == 0)
                {
                    e.Graphics.FillRectangle(new SolidBrush(lineColor), e.Bounds);
                    e.Graphics.DrawString(s, Font, new SolidBrush(SystemColors.WindowText),
                        e.Bounds);
                    e.Graphics.DrawRectangle(new Pen(SystemColors.Highlight), e.Bounds);
                }
                else
                {
                    e.Graphics.FillRectangle(new SolidBrush(SystemColors.Highlight), e.Bounds);
                    e.Graphics.DrawString(s, Font, new SolidBrush(SystemColors.HighlightText),
                        e.Bounds);
                }
            }
        }

        protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs e)
        {
            int index = IndexFromPoint(e.X, e.Y);

            if (index != ListBox.NoMatches &&
                index != 65535)
            {


                if (e.Button == MouseButtons.Right)
                {

                    string s = Items[index].ToString();
                    Rectangle rect = GetItemRectangle(index);

                    tbox.Location = new Point(rect.X, rect.Y);
                    tbox.Size = new Size(rect.Width, rect.Height);
                    tbox.Text = s;
                    tbox.index = index;
                    tbox.SelectAll();
                    tbox.Show();
                    tbox.Focus();
                }
            }

            base.OnMouseUp(e);
        }

        NTextBox tbox = new NTextBox();

        class NTextBox : TextBox
        {
            public MultiLineListBox mllb;
            public int index = -1;

            bool errshown = false;
            bool brementer = false;

            public NTextBox()
            {
                Multiline = true;
                ScrollBars = ScrollBars.Vertical;
            }

            protected override void OnKeyUp(KeyEventArgs e)
            {
                if (brementer)
                {
                    Text = "";
                    brementer = false;
                }
                base.OnKeyUp(e);
            }

            protected override void OnKeyPress(KeyPressEventArgs e)
            {
                base.OnKeyPress(e);

                if (e.KeyChar == 13)
                {
                    if (Text.Trim() == "")
                    {
                        errshown = true;
                        brementer = true;

                        MessageBox.Show(
                            "Cannot enter NULL string as item!",
                            "Fatal error!", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                    else
                    {
                        errshown = false;
                        mllb.Items[index] = Text;
                        Hide();
                    }

                }

                if (e.KeyChar == 27)
                {
                    Text = mllb.Items[index].ToString();
                    Hide();
                    mllb.SelectedIndex = index;
                }

            }

            protected override void OnLostFocus(System.EventArgs e)
            {

                if (Text.Trim() == "")
                {
                    if (!errshown)
                    {
                        MessageBox.Show(
                            "Cannot enter NULL string as item!",
                            "Fatal error!", MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                    errshown = false;
                }
                else
                {
                    errshown = false;
                    mllb.Items[index] = Text;
                    Hide();
                }
                base.OnLostFocus(e);
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            if (e.KeyData == Keys.F2)
            {
                int index = SelectedIndex;
                if (index == ListBox.NoMatches ||
                    index == 65535)
                {
                    if (Items.Count > 0)
                        index = 0;
                }
                if (index != ListBox.NoMatches &&
                    index != 65535)
                {

                    string s = Items[index].ToString();
                    Rectangle rect = GetItemRectangle(index);

                    tbox.Location = new Point(rect.X, rect.Y);
                    tbox.Size = new Size(rect.Width, rect.Height);
                    tbox.Text = s;
                    tbox.index = index;
                    tbox.SelectAll();
                    tbox.Show();
                    tbox.Focus();
                }
            }
            base.OnKeyDown(e);
        }
    }

}
