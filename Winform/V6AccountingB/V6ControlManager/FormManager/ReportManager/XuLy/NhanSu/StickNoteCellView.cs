using System;
using V6Controls;
using V6Controls.Controls.LichView;
using V6Controls.Forms;

namespace V6ControlManager.FormManager.ReportManager.XuLy.NhanSu
{
    public partial class StickNoteCellView : V6Control
    {
        public StickNoteCellView()
        {
            InitializeComponent();
        }

        public void SetCellData(LichViewCellData cellData)
        {
            label1.Text = "";
            panel1.Height = 140;
            Height = 150;
            try
            {
                label1.Text = GetViewText(cellData);
            }
            catch (Exception ex)
            {
                this.ShowErrorException(GetType() + ".SetCellData", ex);
            }
        }

        private string GetViewText(LichViewCellData cellData)
        {
            string result = "";
            result += cellData.Detail4.Replace("|", "\r\n").Replace("~", "\r\n\r\n");
            return result;
        }
    }
}
