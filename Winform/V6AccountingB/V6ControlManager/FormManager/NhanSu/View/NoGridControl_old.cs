using System;
using System.Reflection;
using V6Controls;
using V6Controls.Forms;

namespace V6ControlManager.FormManager.NhanSu.View
{
    public partial class NoGridControl_old : V6Control
    {
        private readonly string _formname;
        private V6Control ThongTinControl;
        private V6Control ThongTinControl2;

        public NoGridControl_old()
        {
            InitializeComponent();
        }

        public NoGridControl_old(string itemID, string formname)
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
                ThongTinControl = NhanSuManager.GetControl(ItemID, _formname) as V6Control;
                if (ThongTinControl != null)
                {
                    //ThongTinControl.Dock = DockStyle.Fill;
                    panelFull.Controls.Add(ThongTinControl);
                }
                ThongTinControl2 = NhanSuManager.GetControl(ItemID, "HINFOR_NS") as V6Control;
                if (ThongTinControl2 != null)
                {

                    panelTop.Controls.Add(ThongTinControl2);
                }

                // SetData...
            }
            catch (Exception ex)
            {
                this.WriteExLog(string.Format("{0}.{1} {2}", GetType(), MethodBase.GetCurrentMethod().Name, _formname), ex);
            }
        }

        public override void LoadData(string code)
        {
            if (ThongTinControl != null)
            {
                ThongTinControl.LoadData(code);
            }
            if (ThongTinControl2 != null)
            {
                ThongTinControl2.LoadData(code);
            }
        }
    }
}
