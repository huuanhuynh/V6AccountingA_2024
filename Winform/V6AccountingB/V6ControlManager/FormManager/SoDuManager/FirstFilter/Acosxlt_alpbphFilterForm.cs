using System;
using System.Windows.Forms;
using V6Controls;
using V6Controls.Forms;
using V6Structs;

namespace V6ControlManager.FormManager.SoDuManager.FirstFilter
{
    public partial class Acosxlt_alpbphFilterForm : V6Form
    {
        //public delegate void FilterOkHandle(string query);
        //public event FilterOkHandle FilterOkClick;
        
        public string QueryString { get; set; }
        private V6TableStruct _structTable;
        private string[] _fields;

        public Acosxlt_alpbphFilterForm()
        {
            InitializeComponent();
            MyInit();
        }
        
        private void MyInit()
        {
            
        }

        private void CallFilterOkClick()
        {
            try
            {
                var where = "";
                if (txtMA_BPHT.Text != "")
                {
                    where += string.Format(" and MA_BPHT like '{0}%'", txtMA_BPHT.Text);
                }
                if (txtMa_ytcp.Text != "")
                {
                    where += string.Format(" and MA_YTCP like '{0}%'", txtMa_ytcp.Text);
                }
                
                if (where.Length > 4)
                {
                    where = where.Substring(4);
                    //QueryString = string.Format("ma_sp in (Select ma_vt from Alvt where {0})", where);
                    QueryString = where;
                }
                
                DialogResult = DialogResult.OK;
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".CallFilterOkClick", ex);
            }
        }
        
        public override bool DoHotKey0(Keys keyData)
        {
            try
            {
                if (keyData == (Keys.Control | Keys.Enter))
                {
                    CallFilterOkClick();
                    return true;
                }
            }
            catch (Exception)
            {
                // ignored
            }
            return false;
        }

        private void FilterForm_Load(object sender, EventArgs e)
        {

        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            CallFilterOkClick();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
