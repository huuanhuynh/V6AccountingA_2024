using System;
using System.Reflection;
using V6Controls;
using V6Controls.Forms;

namespace V6ControlManager.FormManager.NhanSu.ThongTin
{
    public partial class CacKhoanThuongControl : V6Control
    {
        private string _ma_ns;
        public CacKhoanThuongControl()
        {
            InitializeComponent();
        }

        public CacKhoanThuongControl(string maNhanSu)
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

        public override void LoadData(string code)
        {
            
        }
    }
}
