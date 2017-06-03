using System;
using System.Reflection;
using V6Controls;
using V6Controls.Forms;

namespace V6ControlManager.FormManager.NhanSu.ThongTin
{
    public partial class CacKhoanGiamTruControl : V6Control
    {

        private string _ma_ns;
        public CacKhoanGiamTruControl()
        {
            InitializeComponent();
        }

        public CacKhoanGiamTruControl(string maNhanSu)
        {
            InitializeComponent();
            _ma_ns = maNhanSu;
            MyInit();
        }

        private void MyInit()
        {
            try
            {

            }
            catch (Exception ex)
            {
                this.WriteExLog(string.Format("{0}.{1}", GetType(), MethodBase.GetCurrentMethod().Name), ex);
            }
        }
    }
}
