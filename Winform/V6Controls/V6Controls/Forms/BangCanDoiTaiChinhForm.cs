using System;
using System.Collections.Generic;
using System.Windows.Forms;
using V6Controls.Controls;

namespace V6Controls.Forms
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
                CategoryView view = new CategoryView("itemID", "title", "ALmaubcct", initFilter, "STT", new Dictionary<string, object>());
                //view.EnableFullScreen = false;
                view.Dock = DockStyle.Fill;
                Controls.Add(view);
                view.Disposed += view_Disposed;
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".MyInit", ex);
            }
        }

        void view_Disposed(object sender, EventArgs e)
        {
            Dispose(true);
        }
    }
}
