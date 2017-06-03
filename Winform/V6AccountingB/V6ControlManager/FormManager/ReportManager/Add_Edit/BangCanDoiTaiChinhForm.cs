using System;
using System.Windows.Forms;
using V6ControlManager.FormManager.DanhMucManager;
using V6Controls;
using V6Controls.Forms;

namespace V6ControlManager.FormManager.ReportManager.Add_Edit
{
    public partial class BangCanDoiTaiChinhForm : V6Form
    {
        public BangCanDoiTaiChinhForm()
        {
            InitializeComponent();
        }

        public BangCanDoiTaiChinhForm(string initFilter)
        {
            InitializeComponent();
            MyInit(initFilter);
        }

        private void MyInit(string initFilter)
        {
            try
            {
                if(string.IsNullOrEmpty(initFilter))
                    throw new ArgumentNullException("initFilter");
                DanhMucView view = new DanhMucView("itemID", "title", "ALmaubcct", initFilter, "STT", false);
                view.EnableFullScreen = false;
                view.Dock = DockStyle.Fill;
                Controls.Add(view);
            }
            catch (Exception ex)
            {
                this.ShowErrorMessage(GetType() + ".MyInit: " + ex.Message);
            }
        }
    }
}
