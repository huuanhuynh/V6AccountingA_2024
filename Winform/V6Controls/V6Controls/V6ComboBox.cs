using System;
using System.Drawing;
using System.Windows.Forms;

namespace V6Controls
{
    public class V6ComboBox : ComboBox
    {
        public V6ComboBox()
        {
            KeyDown += V6ColorTextBox_KeyDown;
            VisibleChanged += DisTextBox_VisibleChanged;
        }
        public void PerformClick()
        {
            OnClick(new EventArgs());//InvokeOnClick(this,new EventArgs());
        }
        protected virtual void V6ColorTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                SendKeys.Send("{TAB}");
            }
        }

        #region ==== Change Disable BackColor and ForeColor ====

        private Color _ForeColorBackup;
        private Color _BackColorBackup;
        private bool _ColorsSaved = false;

        private bool _SettingColors = false;
        private Color _BackColorDisabled = SystemColors.Control;

        private Color _ForeColorDisabled = SystemColors.WindowText;

        private const int WM_ENABLE = 0xa;

        //public DisTextBox()
        //{
        //    VisibleChanged += DisTextBox_VisibleChanged;
        //}

        private void DisTextBox_VisibleChanged(object sender, System.EventArgs e)
        {
            if (!this._ColorsSaved && this.Visible)
            {
                // Save the ForeColor/BackColor so we can switch back to them later
                _ForeColorBackup = this.ForeColor;
                _BackColorBackup = this.BackColor;
                _ColorsSaved = true;

                // If the window starts out in a Disabled state...
                if (!this.Enabled)
                {
                    // Force the TextBox to initialize properly in an Enabled state,
                    // then switch it back to a Disabled state
                    this.Enabled = true;
                    this.Enabled = false;
                }

                SetColors();
                // Change to the Enabled/Disabled colors specified by the user
            }
        }

        protected override void OnForeColorChanged(System.EventArgs e)
        {
            base.OnForeColorChanged(e);

            // If the color is being set from OUTSIDE our control,
            // then save the current ForeColor and set the specified color
            if (!_SettingColors)
            {
                _ForeColorBackup = this.ForeColor;
                SetColors();
            }
        }

        protected override void OnBackColorChanged(System.EventArgs e)
        {
            base.OnBackColorChanged(e);

            // If the color is being set from OUTSIDE our control,
            // then save the current BackColor and set the specified color
            if (!_SettingColors)
            {
                _BackColorBackup = this.BackColor;
                SetColors();
            }
        }

        private void SetColors()
        {
            // Don't change colors until the original ones have been saved,
            // since we would lose what the original Enabled colors are supposed to be
            if (_ColorsSaved)
            {
                _SettingColors = true;
                if (this.Enabled)
                {
                    this.ForeColor = this._ForeColorBackup;
                    this.BackColor = this._BackColorBackup;
                }
                else
                {
                    this.ForeColor = this.ForeColorDisabled;
                    this.BackColor = this.BackColorDisabled;
                }
                _SettingColors = false;
            }
        }

        protected override void OnEnabledChanged(System.EventArgs e)
        {
            base.OnEnabledChanged(e);

            SetColors();
            // change colors whenever the Enabled() state changes
        }

        public System.Drawing.Color BackColorDisabled
        {
            get { return _BackColorDisabled; }
            set
            {
                if (!value.Equals(Color.Empty))
                {
                    _BackColorDisabled = value;
                }
                SetColors();
            }
        }

        public System.Drawing.Color ForeColorDisabled
        {
            get { return _ForeColorDisabled; }
            set
            {
                if (!value.Equals(Color.Empty))
                {
                    _ForeColorDisabled = value;
                }
                SetColors();
            }
        }

        protected override System.Windows.Forms.CreateParams CreateParams
        {
            get
            {
                System.Windows.Forms.CreateParams cp = null;
                // If the window starts out in a disabled state...
                if (!this.Enabled)
                {
                    // Prevent window being initialized in a disabled state:
                    this.Enabled = true;
                    // temporary ENABLED state
                    cp = base.CreateParams;
                    // create window in ENABLED state
                    this.Enabled = false;
                    // toggle it back to DISABLED state 
                }
                else
                {
                    cp = base.CreateParams;
                }
                return cp;
            }
        }

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            switch (m.Msg)
            {
                case WM_ENABLE:
                    // Prevent the message from reaching the control,
                    // so the colors don't get changed by the default procedure.
                    return; // <-- suppress WM_ENABLE message

            }

            base.WndProc(ref m);
        }

        #endregion change disable backcolor and forecolor
    }
}
