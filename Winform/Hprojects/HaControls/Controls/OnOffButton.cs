using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace H_Controls.Controls
{
    public partial class OnOffButton : UserControl
    {
        public OnOffButton()
        {
            InitializeComponent();
        }

        public event EventHandler SwitchChanged;
        protected virtual void OnSwitchChanged()
        {
            var handler = SwitchChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        [DefaultValue(OnOffState.Off)]
        [Description("Trạng thái tắt (Off) hay mở (on), Khi thay đổi giá trị không gây ra sự kiện.")]
        public OnOffState State
        {
            get { return _state; }
            set
            {
                _state = value;
                if (_state == OnOffState.On)
                {
                    _state = OnOffState.On;
                    @switch.Left = 33;
                    @switch.Text = "on";
                    BackColor = Color.LawnGreen;
                }
                else
                {
                    _state = OnOffState.Off;
                    @switch.Left = 3;
                    @switch.Text = "off";
                    BackColor = Color.LightGray;
                }
            }
        }
        private OnOffState _state = OnOffState.Off;

        private void switch_Click(object sender, EventArgs e)
        {
            if (_state == OnOffState.Off)
            {
                _state = OnOffState.On;
                @switch.Left = 33;
                @switch.Text = "on";
                BackColor = Color.LawnGreen;
                OnSwitchChanged();
            }
            else
            {
                _state = OnOffState.Off;
                @switch.Left = 3;
                @switch.Text = "off";
                BackColor = Color.LightGray;
                OnSwitchChanged();
            }
        }

        
    }

    public enum OnOffState
    {
        On,Off
    }
}
