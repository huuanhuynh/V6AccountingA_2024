using System;
using V6Tools;
using V6Tools.V6Convert;
namespace V6Controls.Forms.DanhMuc.Add_Edit.NhanSu
{
    public partial class HINFOR_NS : AddEditControlVirtual
    {
        public HINFOR_NS()
        {
            InitializeComponent();
            MyInit();
        }

        public override void DoBeforeEdit()
        {
            
        }
        public void MyInit()
        {
            
        }

        private void TXTNGAY_SINH_TextChanged(object sender, System.EventArgs e)
        {
            try
            {
                var date = ObjectAndString.ObjectToDate(TXTNGAY_SINH.Text);
                if (date != null)
                {
                    LunarDate lunar = new LunarDate((DateTime)date);
                    lblAmLich.Text = lunar.ToString();
                }
                else
                {
                    lblAmLich.Text = "";
                }
            }
            catch
            {

            }
        }

    }
}
