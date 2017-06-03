using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace V6RptEditor
{
    public class vSectionTitle : Label
    {
        vSectionPanel _vSectionPanel;
        public vSectionTitle(vSectionPanel vPanel)
        {
            _vSectionPanel = vPanel;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;

            ApplyEvent();
        }

        public void ApplyEvent()
        {
            this.Click += vSectionTitle_Click;
            this.DoubleClick += vSectionTitle_DoubleClick;
        }

        void vSectionTitle_DoubleClick(object sender, EventArgs e)
        {
            //Thu nho hoac phong to Group
            Control t = (Control)sender;
            Control group = t.Parent;
            if (group.Height == t.Height)
            {
                int x = 0;
                foreach (Control item in group.Controls)
                {
                    x += item.Height;
                }
                group.Height = x;
            }
            else
            {
                group.Height = t.Height;
            }
        }

        void vSectionTitle_Click(object sender, EventArgs e)
        {
            FormRptEditor.SetPropertySelectedObject(_vSectionPanel, _vSectionPanel.Name);
            //_vSectionPanel.Focus();
        }

    }
}
