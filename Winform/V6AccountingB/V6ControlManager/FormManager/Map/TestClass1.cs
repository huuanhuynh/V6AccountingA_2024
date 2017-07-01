using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using V6Controls;

namespace V6ControlManager.FormManager.Map
{
    class TestClass1
    {
        public static void CheckMaxValue(V6NumberTextBox sender)
        {
            if (sender.Value > 255) { sender.Value = 255; }
        }
    }
}
