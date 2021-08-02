using System;
using System.Collections.Generic;
using V6Init;
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

        public override void SetData(IDictionary<string, object> d)
        {
            base.SetData(d);
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

                    lblTuoi.Text = TinhTuoi((DateTime)date) + " T";
                }
                else
                {
                    lblAmLich.Text = "";
                    lblTuoi.Text = "";
                }
            }
            catch
            {

            }
        }

        private int TinhTuoi(DateTime date)
        {
            return V6Setting.M_SV_DATE.Year - date.Year;
        }
    }
}
