using System.Collections.Generic;
using V6Controls.Forms;

namespace V6ControlManager.FormManager.DanhMucManager.ChangeCode
{
    public class ChangeCodeBase0 : V6Form
    {
        public delegate void ChangeCodeFinish(IDictionary<string, object> data);

        public event ChangeCodeFinish DoChangeCodeFinish;
        protected virtual void OnDoChangeCodeFinish(IDictionary<string, object> data)
        {
            var handler = DoChangeCodeFinish;
            if (handler != null) handler(data);
        }
    }
}
