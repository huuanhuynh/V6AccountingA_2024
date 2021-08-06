using System;
using System.Reflection;
using System.Windows.Forms;
using V6Controls;
using V6Controls.Forms;
using V6Tools;

namespace V6ControlManager.FormManager.NhanSu.View
{
    public partial class TwoGridControl : V6Control
    {
        private readonly string _formname;
        private V6FormControl BottomControl;
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
                BottomControl = NhanSuManager.GetControl(ItemID, _formname) as V6FormControl;
                if (BottomControl != null)
                {
                    //ThongTinControl.Dock = DockStyle.Fill;
                    panelBottom.Controls.Add(BottomControl);
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
                if (BottomControl != null)
                {
                    BottomControl.LoadData(code);
                }
                gridView1.Focus();
            }
            catch (Exception ex)
            {
                this.WriteExLog(GetType() + ".LoadData", ex);
            }
        }

        //public override bool DoHotKey0(Keys keyData)
        //{
        //    if (keyData == Keys.Escape)
        //    {
        //        btnHuy.PerformClick();
        //    }
        //    else
        //    {
        //        return base.DoHotKey0(keyData);
        //    }
        //    return true;
        //}

        private void gridView2_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (gridView2.CurrentRow != null)
                {
                    BottomControl.SetData(gridView2.CurrentRow.ToDataDictionary());
                }
            }
            catch (Exception ex)
            {
                Logger.WriteToLog(string.Format("{0}.{1} {2} {3}", GetType(), MethodBase.GetCurrentMethod().Name, _formname, ex.Message));
            }
        }
    }
}
