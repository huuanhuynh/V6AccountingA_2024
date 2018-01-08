using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using HaUtility.Helper;
using H_document.DocumentObjects;

namespace H_document.Forms
{
    public partial class HdocumentEditor : UserControl
    {
        #region ==== const ====
        
        private MouseStatus MouseStatus = MouseStatus.MouseUp; 

        private ActionMode Mode
        {
            get
            {
                return mode;
            }
            set
            {
                mode = value;
                switch (value)
                {
                    case ActionMode.SangSang:
                        
                        break;
                    case ActionMode.ThemMoi:
                        
                        break;
                    case ActionMode.DangChon:
                        
                        break;
                    case ActionMode.SaoChep:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("value", value, null);
                }
            }
        }

        private ActionMode mode = ActionMode.SangSang;
        
        #endregion

        #region ==== PROPERTIES ====

        [DefaultValue(true)]
        [Description("Cho phép tạo mới.")]
        public bool EnableNew
        {
            get { return btnNew.Enabled; }
            set { btnNew.Enabled = value; }
        }
        [DefaultValue(true)]
        [Description("Cho phép lưu.")]
        public bool EnableSave
        {
            get { return btnSave.Enabled; }
            set { btnSave.Enabled = value; }
        }
        [DefaultValue(true)]
        [Description("Cho phép mở.")]
        public bool EnableOpen
        {
            get { return btnOpen.Enabled; }
            set { btnOpen.Enabled = value; }
        }
        #endregion properties

        public HdocumentEditor()
        {
            InitializeComponent();
            MyInit();
        }

        private void MyInit()
        {
            hdocumentViewer1.DocumentChanged += hdocumentViewer1_DocumentChanged;
            hdocumentViewer1.SelectedObjectChanged += HdocumentViewer1OnSelectedObjectChanged;
            hdocumentViewer1.DrawMode = H_document.HMode.Design;
        }

        public void SetDocument(Hdocument document)
        {
            hdocumentViewer1.SetDocument(document);
            document.DrawComplete += document_DrawComplete;
        }

        public void New()
        {
            if (EnableNew)
            {
                hdocumentViewer1.CreateNew();
            }
        }

        public void Save()
        {
            if (EnableSave)
            {
                hdocumentViewer1.HDocumentMaster.Save();
            }
        }

        public void Open()
        {
            if (EnableOpen)
            {
                try
                {
                    OpenFileDialog open = new OpenFileDialog();
                    open.Filter = "Xml|*.xml";
                    open.Title = "Chọn tập tin document xml";
                    if (open.ShowDialog(this) == DialogResult.OK)
                    {
                        hdocumentViewer1.CreateNew();
                        hdocumentViewer1.HDocumentMaster.Load(open.FileName);
                    }
                }
                catch (Exception ex)
                {
                    Logger.WriteToLog("HdocumentEditor Open " + ex.Message);
                }
            }
        }

        private void DoCopy()
        {
            Mode = ActionMode.SaoChep;
            if (hdocumentViewer1.HDocumentMaster.SelectedObjects != null && hdocumentViewer1.HDocumentMaster.SelectedObjects.Length == 1
                && !(hdocumentViewer1.HDocumentMaster.SelectedObjects[0] is DetailsObject))
            {
                hdocumentViewer1.HDocumentMaster.CopyObject = hdocumentViewer1.HDocumentMaster.SelectedObjects[0].Clone();
                hdocumentViewer1.HDocumentMaster.AddObjectType = DocObjectType.Copy;
            }
        }

        private void DoDelete()
        {
            try
            {
                if (hdocumentViewer1.HDocumentMaster != null)
                {
                    hdocumentViewer1.HDocumentMaster.RemoveSelectedDocumentObject();
                    hdocumentViewer1.pictureBox1.Invalidate();
                }
            }
            catch (Exception)
            {
                
            }
        }


        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            try
            {
                if (hdocumentViewer1.Focused)
                {
                    if (keyData == Keys.Delete)
                    {
                        DoDelete();
                        return true;
                    }
                    else if (keyData == (Keys.Control | Keys.C))
                    {
                        DoCopy();
                        return true;
                    }
                    else
                    {
                        var keyString = keyData.ToString();
                        if (keyString == KeyString.Control_C)
                        {
                            DoCopy();
                            return true;
                        }
                    }
                }

                return base.ProcessCmdKey(ref msg, keyData);
            }
            catch
            {
                return false;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        

        void document_DrawComplete(object sender, EventArgs e)
        {
            propertyGrid1.Refresh();
        }

        void hdocumentViewer1_DocumentChanged(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObject = hdocumentViewer1.HDocumentMaster;
        }

        private void HdocumentViewer1OnSelectedObjectChanged(object[] documentObject)
        {
            if (documentObject == null) propertyGrid1.SelectedObject = hdocumentViewer1.HDocumentMaster;
            else propertyGrid1.SelectedObjects = documentObject;
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            DoCopy();
        }

        private void btnAddText_Click(object sender, EventArgs e)
        {
            Mode = ActionMode.ThemMoi;
            hdocumentViewer1.HDocumentMaster.AddObjectType = DocObjectType.Text;
        }

        private void btnAddLineH_Click(object sender, EventArgs e)
        {
            Mode = ActionMode.ThemMoi;
            hdocumentViewer1.HDocumentMaster.AddObjectType = DocObjectType.Line;
        }

        private void btnAddLineV_Click(object sender, EventArgs e)
        {
            Mode = ActionMode.ThemMoi;
            hdocumentViewer1.HDocumentMaster.AddObjectType = DocObjectType.Line;
        }

        private void btnAddPicture_Click(object sender, EventArgs e)
        {
            Mode = ActionMode.ThemMoi;
            hdocumentViewer1.HDocumentMaster.AddObjectType = DocObjectType.Picture;
        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            //propertyGrid1.Refresh();
            hdocumentViewer1.pictureBox1.Invalidate();
        }

        private void ThemDongChuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hdocumentViewer1.HDocumentMaster.AddObjectType = DocObjectType.Text;
        }

        private void themDuongKeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hdocumentViewer1.HDocumentMaster.AddObjectType = DocObjectType.Line;
        }

        private void themKhungChuNhatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //hdocumentViewer1.AddObjectType = DocObjectType.Box;
        }

        private void themHinhAnhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hdocumentViewer1.HDocumentMaster.AddObjectType = DocObjectType.Picture;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DoDelete();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            New();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            Open();
        }

        private void btnBold_Click(object sender, EventArgs e)
        {
            hdocumentViewer1.DoBold();
        }

        private void btnItalic_Click(object sender, EventArgs e)
        {
            hdocumentViewer1.DoItalic();
        }

        private void btnUnderLine_Click(object sender, EventArgs e)
        {
            hdocumentViewer1.DoChangeFontStyle(FontStyle.Underline);
        }

        private void btnStreakLine_Click(object sender, EventArgs e)
        {
            hdocumentViewer1.DoChangeFontStyle(FontStyle.Strikeout);
        }

        private void btnAlignLeft_Click(object sender, EventArgs e)
        {
            hdocumentViewer1.DoChangeTextAlign(ContentAlignment.TopLeft);
        }

        private void btnAlignCenter_Click(object sender, EventArgs e)
        {
            hdocumentViewer1.DoChangeTextAlign(ContentAlignment.TopCenter);
        }

        private void btnAlignRight_Click(object sender, EventArgs e)
        {
            hdocumentViewer1.DoChangeTextAlign(ContentAlignment.TopRight);
        }

        
    }
}
