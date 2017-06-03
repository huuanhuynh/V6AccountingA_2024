using V6Controls;
using V6Controls.Forms;

namespace V6ControlManager.FormManager.MenuManager
{
    partial class Menu3Control
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.lblShowHide = new V6Controls.V6VeticalLable();
            this.panelMenu = new System.Windows.Forms.Panel();
            this.menuControl1 = new V6Controls.MenuControl();
            this.panelMenuShow = new System.Windows.Forms.Panel();
            this.panelView = new System.Windows.Forms.Panel();
            this.panelMenu.SuspendLayout();
            this.panelMenuShow.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblShowHide
            // 
            this.lblShowHide.AccessibleName = "ShowHideMenu";
            this.lblShowHide.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblShowHide.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblShowHide.Location = new System.Drawing.Point(254, 0);
            this.lblShowHide.Name = "lblShowHide";
            this.lblShowHide.RenderingMode = System.Drawing.Text.TextRenderingHint.SystemDefault;
            this.lblShowHide.Size = new System.Drawing.Size(27, 203);
            this.lblShowHide.TabIndex = 1;
            this.lblShowHide.Tag = "Ẩn hoặc hiện menu";
            this.lblShowHide.Text = "Hide";
            this.lblShowHide.TextDrawMode = V6Controls.DrawMode.TopBottom;
            this.toolTip1.SetToolTip(this.lblShowHide, "Ẩn hoặc hiện menu");
            this.lblShowHide.TransparentBackground = false;
            this.lblShowHide.TextChanged += new System.EventHandler(this.lblShowHide_TextChanged);
            this.lblShowHide.Click += new System.EventHandler(this.lblShowHide_Click);
            // 
            // panelMenu
            // 
            this.panelMenu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panelMenu.BackColor = System.Drawing.Color.AliceBlue;
            this.panelMenu.Controls.Add(this.menuControl1);
            this.panelMenu.Location = new System.Drawing.Point(0, 0);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(261, 459);
            this.panelMenu.TabIndex = 3;
            // 
            // menuControl1
            // 
            this.menuControl1.BackColor = System.Drawing.SystemColors.Highlight;
            this.menuControl1.ButtonHeight = 30;
            this.menuControl1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.menuControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.menuControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menuControl1.GradientButtonHoverDark = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(192)))), ((int)(((byte)(91)))));
            this.menuControl1.GradientButtonHoverLight = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(220)))));
            this.menuControl1.GradientButtonNormalDark = System.Drawing.Color.FromArgb(((int)(((byte)(178)))), ((int)(((byte)(193)))), ((int)(((byte)(140)))));
            this.menuControl1.GradientButtonNormalLight = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(240)))), ((int)(((byte)(207)))));
            this.menuControl1.GradientButtonSelectedDark = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(150)))), ((int)(((byte)(21)))));
            this.menuControl1.GradientButtonSelectedLight = System.Drawing.Color.FromArgb(((int)(((byte)(251)))), ((int)(((byte)(230)))), ((int)(((byte)(148)))));
            this.menuControl1.Location = new System.Drawing.Point(0, 0);
            this.menuControl1.Name = "menuControl1";
            this.menuControl1.SelectedButton = null;
            this.menuControl1.Size = new System.Drawing.Size(261, 48);
            this.menuControl1.TabIndex = 0;
            this.menuControl1.Click += new V6Controls.MenuControl.ButtonClickEventHandler(this.menuControl_Click);
            this.menuControl1.DoubleClick += new System.EventHandler(this.menuControl1_DoubleClick);
            // 
            // panelMenuShow
            // 
            this.panelMenuShow.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.panelMenuShow.Controls.Add(this.lblShowHide);
            this.panelMenuShow.Location = new System.Drawing.Point(0, 0);
            this.panelMenuShow.Name = "panelMenuShow";
            this.panelMenuShow.Size = new System.Drawing.Size(281, 203);
            this.panelMenuShow.TabIndex = 4;
            // 
            // panelView
            // 
            this.panelView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelView.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(243)))), ((int)(((byte)(226)))));
            this.panelView.Location = new System.Drawing.Point(281, 3);
            this.panelView.Name = "panelView";
            this.panelView.Size = new System.Drawing.Size(422, 456);
            this.panelView.TabIndex = 5;
            // 
            // Menu3Control
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.LightSkyBlue;
            this.Controls.Add(this.panelMenu);
            this.Controls.Add(this.panelMenuShow);
            this.Controls.Add(this.panelView);
            this.Name = "Menu3Control";
            this.Size = new System.Drawing.Size(706, 462);
            this.Load += new System.EventHandler(this.Form_Load);
            this.VisibleChanged += new System.EventHandler(this.Menu3Control_VisibleChanged);
            this.panelMenu.ResumeLayout(false);
            this.panelMenuShow.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Panel panelMenuShow;
        private V6VeticalLable lblShowHide;
        private System.Windows.Forms.Panel panelView;
        private MenuControl menuControl1;
        private System.Windows.Forms.ToolTip toolTip1;

        //private System.Windows.Forms.Panel panelAllView;
    }
}
