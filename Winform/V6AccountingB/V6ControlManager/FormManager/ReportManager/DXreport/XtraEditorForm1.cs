using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraReports.UserDesigner;
using DevExpress.XtraReports.UI;
using V6Controls.Forms;
using System.IO;

namespace V6ControlManager.FormManager.ReportManager.DXreport
{
    public partial class XtraEditorForm1 : V6Form// DevExpress.XtraEditors.XtraForm
    {
        public XtraEditorForm1()
        {
            InitializeComponent();
            MyInit();
        }

        public XtraEditorForm1(XtraReport repx, string repxFile)
        {
            IDictionary<string, XtraReport> file_repx = new Dictionary<string, XtraReport>();
            file_repx.Add(repxFile, repx);
            _file_repx = file_repx;
            InitializeComponent();
            MyInit();

        }

        public XtraEditorForm1(IDictionary<string, XtraReport> file_repx)
        {
            _file_repx = file_repx;
            InitializeComponent();
            MyInit();
            
        }
        
        IDictionary<string, XtraReport> _file_repx;
        XRDesignPanel _first_design_panel = null;

        private void MyInit()
        {
            try
            {
                reportDesigner1.DesignPanelLoaded += reportDesigner1_DesignPanelLoaded;
                
                if (_file_repx != null) // Open
                foreach (KeyValuePair<string, XtraReport> item in _file_repx)
                {
                    reportDesigner1.OpenReport(item.Value);
                    reportDesigner1.ActiveDesignPanel.FileName = item.Key;
                    reportDesigner1.ActiveDesignPanel.Text = item.Value.Name;
                    if (_first_design_panel == null) _first_design_panel = reportDesigner1.ActiveDesignPanel;
                }
            }
            catch (Exception ex)
            {
                V6ControlFormHelper.ShowErrorException(GetType() + "MyInit", ex, this);
            }
        }

        /// <summary>
        /// Sự kiện panel xuất hiện.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void reportDesigner1_DesignPanelLoaded(object sender, DesignerLoadedEventArgs e)
        {
            XRDesignPanel panel = (XRDesignPanel)sender;
            string a = panel.FileName;
            //panel.FileName = _repx.
            //panel.AddCommandHandler(new SaveCommandHandler(panel));
            //panel.AddCommandHandler(new SaveAsCommandHandler(panel));
        }

        private void XtraForm1_Load(object sender, EventArgs e)
        {
            try
            {
                 if (_first_design_panel != null) _first_design_panel.Focus();
            }
            catch (Exception ex)
            {

            }
        }
    }

    public class SaveCommandHandler : DevExpress.XtraReports.UserDesigner.ICommandHandler
    {
        XRDesignPanel _designPanel;
        XtraReport _repx;
        //string _repxFile;

        public SaveCommandHandler(XRDesignPanel panel, XtraReport repx)
        {
            this._designPanel = panel;
        }

        public void HandleCommand(DevExpress.XtraReports.UserDesigner.ReportCommand command, object[] args)
        {
            // Save the report. 
            Save();
        }

        public bool CanHandleCommand(DevExpress.XtraReports.UserDesigner.ReportCommand command, ref bool useNextHandler)
        {
            useNextHandler = !(command == ReportCommand.SaveFile || command == ReportCommand.SaveFileAs);
            return !useNextHandler;
        }

        void Save()
        {
            // Write your custom saving here. 
            // ... 
            //_designPanel.SaveReport(_designPanel.Report.SourceUrl);
            string savefile = _designPanel.FileName;
            
            if (string.IsNullOrEmpty(savefile))
                savefile = V6ControlFormHelper.ChooseSaveFile(_designPanel, "Repx|*.repx");

            if (!string.IsNullOrEmpty(savefile))
            {
                // For instance: // Save luôn xuống file.
                _designPanel.Report.SaveLayout(savefile);

                // Prevent the "Report has been changed" dialog from being shown. 
                _designPanel.ReportState = ReportState.Saved;
            }
        }
    }

    public class SaveAsCommandHandler : DevExpress.XtraReports.UserDesigner.ICommandHandler
    {
        XRDesignPanel _designPanel;
        XtraReport _repx;
        //string _repxFile;

        public SaveAsCommandHandler(XRDesignPanel panel)
        {
            this._designPanel = panel;
        }

        public void HandleCommand(DevExpress.XtraReports.UserDesigner.ReportCommand command, object[] args)
        {
            // Save the report. 
            SaveAs();
        }

        public bool CanHandleCommand(DevExpress.XtraReports.UserDesigner.ReportCommand command, ref bool useNextHandler)
        {
            useNextHandler = !(command == ReportCommand.SaveFileAs);
            return !useNextHandler;
        }

        void SaveAs()
        {
            // Write your custom saving here. 
            // ... 
            string savefile = V6ControlFormHelper.ChooseSaveFile(_designPanel, "Repx|*.repx", _designPanel.FileName);

            if (!string.IsNullOrEmpty(savefile))
            {
                // For instance: // Save luôn xuống file.
                _designPanel.Report.SaveLayout(savefile);

                // Prevent the "Report has been changed" dialog from being shown. 
                _designPanel.ReportState = ReportState.Saved;
                _designPanel.FileName = savefile;
                _designPanel.Text = Path.GetFileName(savefile);
            }
        }
    }
}