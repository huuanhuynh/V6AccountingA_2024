using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace V6RptEditor
{
    public class vSectionPanel:Panel
    {
        
        private static ReportObject _selectedObj;
        
        private static Point _cursorStartPoint;
        private static bool _resizing;
        private static Size _currentControlStartSize;

        #region ==== Object Name ====
        [Category("_crProperty")]
        [Description("Tên đối tượng")]
        public string ObjectName
        {
            get
            {
                return _section.Name;
            }
        }
        #endregion
        #region ==== BackGroundColor ====
        private Color backGroundColor;
        [Category("_crProperty")]
        [Description("Màu nền")]
        public Color BackGroundColor
        {
            get { return backGroundColor; }
            set
            {
                if (value != backGroundColor)
                {
                    backGroundColor = value;
                    BackColor = value;//Đổi hiển thị màu
                    Tag = value;//Đổi biến lưu dùng cho hover
                    try
                    {
                        BackGroundColorChanged(this, new EventArgs());
                    }
                    catch { }
                }
            }
        }
        public delegate void OnBackGroundColorChanged(object sender, EventArgs e);
        public event OnBackGroundColorChanged BackGroundColorChanged;
        #endregion

        #region ==== Suppress (Ẩn) ====
        //Color oldColor;
        private bool enableSuppress;
        [Category("_crProperty")]
        [Description("Ẩn đi")]
        public bool EnableSuppress
        {
            get { return enableSuppress; }
            set
            {

                if (value != enableSuppress)
                {
                    enableSuppress = value;
                    if (value)
                    {
                        //oldColor = this.ForeColor;
                        this.BackColor = Color.Gray;
                    }
                    else
                    {
                        this.BackColor = backGroundColor;
                    }
                    try
                    {
                        EnableSuppressChanged(this, new EventArgs());
                    }
                    catch { }
                }
            }
        }
        public delegate void OnEnableSuppressChanged(object sender, EventArgs e);
        public event OnEnableSuppressChanged EnableSuppressChanged;
        #endregion

        Section _section;
        public vSectionPanel(Section section, Point location)
        {
            _section = section;
            Name = section.Name;
            Location = location;
            Size = new Size(FormRptEditor._rpt.PrintOptions.PageContentWidth / (int)Twip.twipPerPixelX,
                section.Height / (int)Twip.twipPerPixelY);
            backGroundColor = section.SectionFormat.BackgroundColor;
            enableSuppress = section.SectionFormat.EnableSuppress;
            if (enableSuppress)
            {
                BackColor = Color.Gray;
            }
            else
            {
                BackColor = backGroundColor;
            }
            TabStop = true;

            ApplyEvent();
        }

        public void ApplyEvent()
        {
            //this.SizeChanged += rptSectionPanel_SizeChanged;
            this.MouseDown += (s, e) => _MouseDown(this, e);
            this.MouseUp += (s, e) => _MouseUp(this,e);
            this.MouseMove += (s, e) => _MouseMove(this.Parent, e);
            this.SizeChanged += vSectionPanel_SizeChanged;

            this.MouseClick += vSectionPanel_MouseClick;
            this.PreviewKeyDown += vSectionPanel_PreviewKeyDown;
        }

        void vSectionPanel_MouseClick(object sender, MouseEventArgs e)
        {
            this.Focus();
        }

        void vSectionPanel_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            //MessageBox.Show("Test: "+this.Name);
            //throw new NotImplementedException();
            if (FormRptEditor._classStatic.ObjForPGrid is rptText)
            {
                rptText moveObj = (rptText)FormRptEditor._classStatic.ObjForPGrid;
                if (e.KeyCode == Keys.Up)
                {
                    moveObj.Top--;
                }
                else if (e.KeyCode == Keys.Down)
                {
                    moveObj.Top++;
                }
                else if (e.KeyCode == Keys.Left)
                {
                    moveObj.Left--;
                }
                else if (e.KeyCode == Keys.Right)
                {
                    moveObj.Left++;
                }
            }
            else if (FormRptEditor._classStatic.ObjForPGrid is rptField)
            {
                rptField moveObj = (rptField)FormRptEditor._classStatic.ObjForPGrid;
                if (e.KeyCode == Keys.Up)
                {
                    moveObj.Top--;
                }
                else if (e.KeyCode == Keys.Down)
                {
                    moveObj.Top++;
                }
                else if (e.KeyCode == Keys.Left)
                {
                    moveObj.Left--;
                }
                else if (e.KeyCode == Keys.Right)
                {
                    moveObj.Left++;
                }
            }
            else if (FormRptEditor._classStatic.ObjForPGrid is rptLine0)
            {
                rptLine0 moveObj = (rptLine0)FormRptEditor._classStatic.ObjForPGrid;
                if (e.KeyCode == Keys.Up)
                {
                    moveObj.Y1--; moveObj.Y2--;
                }
                else if (e.KeyCode == Keys.Down)
                {
                    moveObj.Y1++; moveObj.Y2++;
                }
                else if (e.KeyCode == Keys.Left)
                {
                    moveObj.X1--; moveObj.X2--;
                }
                else if (e.KeyCode == Keys.Right)
                {
                    moveObj.X1++; moveObj.X2++;
                }
            }
            else if (FormRptEditor._classStatic.ObjForPGrid is rptLineDC)
            {
                rptLineDC moveObj = (rptLineDC)FormRptEditor._classStatic.ObjForPGrid;
                if (e.KeyCode == Keys.Up)
                {
                    moveObj.Y1--; moveObj.Y2--;
                }
                else if (e.KeyCode == Keys.Down)
                {
                    moveObj.Y1++; moveObj.Y2++;
                }
                else if (e.KeyCode == Keys.Left)
                {
                    moveObj.X1--; moveObj.X2--;
                }
                else if (e.KeyCode == Keys.Right)
                {
                    moveObj.X1++; moveObj.X2++;
                }
            }
            else if (FormRptEditor._classStatic.ObjForPGrid is rptPicture)
            {
                rptPicture moveObj = (rptPicture)FormRptEditor._classStatic.ObjForPGrid;
                if (e.KeyCode == Keys.Up)
                {
                    moveObj.Top--;
                }
                else if (e.KeyCode == Keys.Down)
                {
                    moveObj.Top++;
                }
                else if (e.KeyCode == Keys.Left)
                {
                    moveObj.Left--;
                }
                else if (e.KeyCode == Keys.Right)
                {
                    moveObj.Left++;
                }
            }
        }

        void vSectionPanel_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                _section.Height = Twip.PixelToTwipY(this.Height);
                ResetSectionGroupSize(((Control)sender).Parent);
            }
            catch (Exception ex)
            {   
                //MessageBox.Show(ex.Message);
                this.Height = Twip.TwipToPixelY(FormRptEditor._rpt.ReportDefinition.Sections[this.Name].Height);
            }
        }
        void ResetSectionGroupSize(Control group)
        {
            int x = 0;
            foreach (Control item in group.Controls)
            {
                x += item.Height;
            }
            group.Height = x;
        }

        private void _MouseMove(Control control, MouseEventArgs e)
        {
            int temp = this.Height - e.Y;
            if (temp <= 2)
                this.Cursor = Cursors.SizeNS;
            else this.Cursor = Cursors.Default;
            

            if (_resizing)
            {
                int h = (e.Y - _cursorStartPoint.Y) + _currentControlStartSize.Height;
                if (h <= 0) h = 1;
                this.Height = h;
                
            }
        }

        private void _MouseDown(Control control, MouseEventArgs e)
        {
            int temp = this.Height - e.Y;
            if (temp <= 2)
            {
                _resizing = true;
                _currentControlStartSize = control.Size;
            }
            
            _cursorStartPoint = new Point(e.X, e.Y);
            control.Capture = true;
        }

        private void _MouseUp(Control control, MouseEventArgs e)
        {
            if (_resizing)
            {
                _resizing = false;
            }
        }

        //void UpdateValue()
        //{
        //    if (Form1._rpt != null)
        //    {
        //        try
        //        {
        //            Form1._rpt.ReportDefinition.Sections[this.Name].Height
        //                = Twip.PixelToTwipY(this.Height);
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.Message);
        //            this.Height = Twip.TwipToPixelY(Form1._rpt.ReportDefinition.Sections[this.Name].Height);
        //        }
        //    }
        //}
    }
}
