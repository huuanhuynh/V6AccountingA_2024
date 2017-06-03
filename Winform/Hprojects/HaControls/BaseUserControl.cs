using System;
using System.Collections.Generic;
using System.Windows.Forms;
using H_Utility.Helper;

namespace H_Controls
{
    public partial class BaseUserControl : UserControl
    {
        private bool _ready;
        public bool IsReady { get { return _ready; } }

        public void Ready()
        {
            _ready = true;
        }

        protected SortedDictionary<string, BaseUserControl> ControlsDictionary = new SortedDictionary<string, BaseUserControl>();
        protected BaseUserControl CurrentControl;

        public BaseUserControl()
        {
            InitializeComponent();
        }


        private void BaseUserControl_Load(object sender, EventArgs e)
        {
            this.LoadComboboxLookup();
        }

        public void Clear()
        {
            try
            {
                HControlHelper.SetFormDataDic(this, null);
            }
            catch (Exception ex)
            {
                Logger.WriteToLog("BaseUserControl Clear " + ex.Message);
            }
        }

        protected bool do_hot_key;
        public virtual void DoHotKey(Keys keyData)
        {
            try
            {
                do_hot_key = true;
                DoHotKey0(keyData);
            }
            catch
            {
                // ignored
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            try
            {
                if (do_hot_key)
                {
                    do_hot_key = false;
                    return base.ProcessCmdKey(ref msg, keyData);
                }
                if (DoHotKey0(keyData)) return true;
            }
            catch
            {
                return false;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// Thực hiện sự kiện khi bấm phím.
        /// </summary>
        /// <param name="keyData"></param>
        /// <returns></returns>
        public virtual bool DoHotKey0(Keys keyData)
        {
            return HControlHelper.DoKeyCommand(this, keyData);
        }

    }
}
