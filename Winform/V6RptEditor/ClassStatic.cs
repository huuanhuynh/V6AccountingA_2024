using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace V6RptEditor
{
    public class ClassStatic
    {
        public string lblSelectedControlNameText = "";
        private object objForPGrid = null;

        public object ObjForPGrid
        {
            get { return objForPGrid; }
            set
            {
                object tempObject = objForPGrid;

                objForPGrid = value;
                if (onObjChanged != null)
                    onObjChanged(this, new EventArgs());

                if (tempObject != null)
                {
                    try
                    {
                        if (tempObject is Control)
                        {
                            ((Control)tempObject).Invalidate();
                        }
                    }
                    catch (Exception ex)
                    {
                        // ignored
                    }
                }
            }
        }
        public delegate void objChange(object sender, EventArgs e);
        public event objChange onObjChanged;
    }
}
