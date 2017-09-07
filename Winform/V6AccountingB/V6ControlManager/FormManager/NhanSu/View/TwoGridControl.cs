using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using V6Controls;
using V6Controls.Forms;
using V6Tools;

namespace V6ControlManager.FormManager.NhanSu.View
{
    public partial class TwoGridControl : V6Control
    {
        private readonly string _formname;
        private V6FormControl ThongTinControl;
        public TwoGridControl()
        {
            InitializeComponent();
        }

        public TwoGridControl(string itemID, string formname)
        {
            m_itemId = itemID;
            _formname = formname;
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            try
            {
                ThongTinControl = NhanSuManager.GetControl(ItemID, _formname) as V6FormControl;
                if (ThongTinControl != null)
                {
                    //ThongTinControl.Dock = DockStyle.Fill;
                    panel1.Controls.Add(ThongTinControl);
                }
                // SetData...
            }
            catch (Exception ex)
            {
                Logger.WriteToLog(string.Format("{0}.{1} {2} {3}", GetType(), MethodBase.GetCurrentMethod().Name, _formname, ex.Message));
            }
        }

        public override void LoadData(string code)
        {
            try
            {
                if (ThongTinControl != null)
                {
                    ThongTinControl.LoadData(code);
                }
                gridView1.Focus();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".LoadData", ex);
            }
        }

        private void gridView2_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (gridView2.CurrentRow != null)
                {
                    ThongTinControl.SetData(gridView2.CurrentRow.ToDataDictionary());
                }
            }
            catch (Exception ex)
            {
                Logger.WriteToLog(string.Format("{0}.{1} {2} {3}", GetType(), MethodBase.GetCurrentMethod().Name, _formname, ex.Message));
            }
        }
    }
}
