using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace V6RptEditor
{
    public class vSectionGroup : Panel
    {
        public void ApplyEvents()
        {
            this.SizeChanged += vSectionGroup_SizeChanged;
        }

        void vSectionGroup_SizeChanged(object sender, EventArgs e)
        {
            ResetSectionLocation(((Control)sender).Parent);
        }

        private void ResetSectionLocation(Control mainPanel)
        {
            int x = mainPanel.DisplayRectangle.Left,
                y = mainPanel.DisplayRectangle.Top;
            foreach (Control item in mainPanel.Controls)
            {
                item.Location = new Point(x, y);
                y += item.Height;
            }
        }
    }
}
