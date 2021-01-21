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

        public BangCanDoiTaiChinhForm(string initFilter, IDictionary<string, object> parentData)
        {
            _parentData = parentData;
            InitializeComponent();
            MyInit(initFilter);
        }

        private CategoryView view;
        private IDictionary<string, object> _parentData;

        private void MyInit(string initFilter)
        {
            try
            {
                if(string.IsNullOrEmpty(initFilter))
                    throw new ArgumentNullException("initFilter");
                view = new CategoryView("itemID", "title", "ALmaubcct", initFilter, "STT", _parentData);
                //view.EnableFullScreen = false;
                //view.SetParentData();
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

        //public void SetParentData(IDictionary<string, object> parentData)
        //{
        //    view.SetParentData(parentData);
        //}
    }
}
