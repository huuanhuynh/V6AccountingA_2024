using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Test
{
    public partial class FormTabChungTu : Form
    {
        public FormTabChungTu()
        {
            InitializeComponent();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            //MyControlLibrary.V6TabPage v6tp = new MyControlLibrary.V6TabPage();
            //v6tp.Text = "Chứng từ + "(tabCacChungTu.TabCount + 1);
            //tabCacChungTu.TabPages.Add(v6tp);

            V6TabControlLib.V6TabPage tpe = new V6TabControlLib.V6TabPage();
            tpe.Text = "Chứng từ " + (tabCacChungTu.TabPages.Count + 1);
            V6Controls.ucV6ChungTu ucVct = new V6Controls.ucV6ChungTu();
            ucVct.Dock = DockStyle.Fill;
            tpe.Controls.Add(ucVct);
            tabCacChungTu.TabPages.Add(tpe);
            tabCacChungTu.SelectedTab = tpe;
        }

        private void FormTabChungTu_Load(object sender, EventArgs e)
        {
            btnNew_Click(null, null);            
        }

        private void tabCacChungTu_OnAddClick(V6TabControlLib.V6TabPage v6Tabpage, V6TabControlLib.AddEventArgs a)
        {
            v6Tabpage.Text = "FormTabChungTu" + tabCacChungTu.TabCount;
            V6Controls.ucV6ChungTu uc = new V6Controls.ucV6ChungTu();
            uc.Dock = DockStyle.Fill;
            v6Tabpage.Controls.Add(uc);
        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            tabCacChungTu.Refresh();
        }

        private void tabCacChungTu_Validated(object sender, EventArgs e)
        {
            propertyGrid1.Refresh();
        }
        
    }
}
