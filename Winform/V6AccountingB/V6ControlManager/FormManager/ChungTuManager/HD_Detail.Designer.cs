using V6Controls;
using V6Controls.Controls;
using V6Controls.Forms;

namespace V6ControlManager.FormManager.ChungTuManager
{
    partial class HD_Detail
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
            this.panelControls = new System.Windows.Forms.Panel();
            this.panel0 = new System.Windows.Forms.Panel();
            this.btnChucNang = new V6Controls.Controls.DropDownButton();
            this.lblName = new V6Controls.V6Label();
            this.btnSua = new V6Controls.Controls.V6FormButton();
            this.btnXoa = new V6Controls.Controls.V6FormButton();
            this.btnMoi = new V6Controls.Controls.V6FormButton();
            this.btnNhan = new V6Controls.Controls.V6FormButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.hScrollBar1 = new System.Windows.Forms.HScrollBar();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel0.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelControls
            // 
            this.panelControls.AutoSize = true;
            this.panelControls.Location = new System.Drawing.Point(1, 0);
            this.panelControls.Name = "panelControls";
            this.panelControls.Size = new System.Drawing.Size(730, 33);
            this.panelControls.TabIndex = 2;
            this.panelControls.SizeChanged += new System.EventHandler(this.panel1and2_SizeChanged);
            // 
            // panel0
            // 
            this.panel0.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.panel0.AutoSize = true;
            this.panel0.Controls.Add(this.btnChucNang);
            this.panel0.Controls.Add(this.lblName);
            this.panel0.Location = new System.Drawing.Point(77, 0);
            this.panel0.Name = "panel0";
            this.panel0.Size = new System.Drawing.Size(69, 52);
            this.panel0.TabIndex = 1;
            this.panel0.SizeChanged += new System.EventHandler(this.panel0_SizeChanged);
            // 
            // btnChucNang
            // 
            this.btnChucNang.AccessibleDescription = "AINCTIXAB00033";
            this.btnChucNang.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChucNang.Location = new System.Drawing.Point(35, 24);
            this.btnChucNang.Name = "btnChucNang";
            this.btnChucNang.Size = new System.Drawing.Size(31, 23);
            this.btnChucNang.TabIndex = 38;
            this.btnChucNang.TabStop = false;
            this.btnChucNang.Text = "...";
            this.btnChucNang.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnChucNang.UseVisualStyleBackColor = true;
            this.btnChucNang.Visible = false;
            // 
            // lblName
            // 
            this.lblName.AccessibleName = "ten_vt";
            this.lblName.Location = new System.Drawing.Point(0, 33);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(66, 14);
            this.lblName.TabIndex = 0;
            this.lblName.Tag = "hide";
            this.lblName.Text = "Tên";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblName.Visible = false;
            this.lblName.TextChanged += new System.EventHandler(this.lblName_TextChanged);
            // 
            // btnSua
            // 
            this.btnSua.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSua.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSua.Image = global::V6ControlManager.Properties.Resources.Pencil16;
            this.btnSua.Location = new System.Drawing.Point(35, 0);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(41, 25);
            this.btnSua.TabIndex = 4;
            this.btnSua.TabStop = false;
            this.btnSua.Tag = "cancel";
            this.btnSua.Text = "&2";
            this.btnSua.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.btnSua, "Sửa");
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoa.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXoa.Image = global::V6ControlManager.Properties.Resources.Delete16;
            this.btnXoa.Location = new System.Drawing.Point(0, 25);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(35, 25);
            this.btnXoa.TabIndex = 5;
            this.btnXoa.TabStop = false;
            this.btnXoa.Tag = "cancel";
            this.btnXoa.Text = "&3";
            this.btnXoa.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.btnXoa, "Xóa");
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnMoi
            // 
            this.btnMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMoi.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnMoi.Image = global::V6ControlManager.Properties.Resources.Add16;
            this.btnMoi.Location = new System.Drawing.Point(0, 0);
            this.btnMoi.Name = "btnMoi";
            this.btnMoi.Size = new System.Drawing.Size(35, 25);
            this.btnMoi.TabIndex = 0;
            this.btnMoi.Tag = "cancel";
            this.btnMoi.Text = "&1";
            this.btnMoi.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.btnMoi, "Thêm");
            this.btnMoi.UseVisualStyleBackColor = true;
            this.btnMoi.Click += new System.EventHandler(this.btnMoi_Click);
            // 
            // btnNhan
            // 
            this.btnNhan.Enabled = false;
            this.btnNhan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNhan.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNhan.Image = global::V6ControlManager.Properties.Resources.Apply16;
            this.btnNhan.Location = new System.Drawing.Point(35, 25);
            this.btnNhan.Name = "btnNhan";
            this.btnNhan.Size = new System.Drawing.Size(41, 25);
            this.btnNhan.TabIndex = 3;
            this.btnNhan.Tag = "cancel";
            this.btnNhan.Text = "&4";
            this.btnNhan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.btnNhan, "Nhận");
            this.btnNhan.UseVisualStyleBackColor = true;
            this.btnNhan.Click += new System.EventHandler(this.btnThem_Click);
            this.btnNhan.KeyDown += new System.Windows.Forms.KeyEventHandler(this.btnThem_KeyDown);
            // 
            // hScrollBar1
            // 
            this.hScrollBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.hScrollBar1.Location = new System.Drawing.Point(0, 33);
            this.hScrollBar1.Name = "hScrollBar1";
            this.hScrollBar1.Size = new System.Drawing.Size(732, 17);
            this.hScrollBar1.TabIndex = 0;
            this.hScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.hScrollBar1_Scroll);
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.Controls.Add(this.hScrollBar1);
            this.panel2.Controls.Add(this.panelControls);
            this.panel2.Location = new System.Drawing.Point(146, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(732, 50);
            this.panel2.TabIndex = 3;
            this.panel2.SizeChanged += new System.EventHandler(this.panel1and2_SizeChanged);
            // 
            // HD_Detail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.panel0);
            this.Controls.Add(this.btnMoi);
            this.Controls.Add(this.btnNhan);
            this.Name = "HD_Detail";
            this.Size = new System.Drawing.Size(881, 50);
            this.panel0.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        /// <summary>
        /// Panel chứa control bên phải.
        /// </summary>
        public System.Windows.Forms.Panel panelControls;
        /// <summary>
        /// Panel chứa control cố định bên trái
        /// </summary>
        public System.Windows.Forms.Panel panel0;
        public V6FormButton btnNhan;
        public V6FormButton btnSua;
        public V6FormButton btnXoa;
        public V6FormButton btnMoi;
        private System.Windows.Forms.ToolTip toolTip1;
        public V6Label lblName;
        private System.Windows.Forms.HScrollBar hScrollBar1;
        /// <summary>
        /// Panel chứa panelControls (có thanh kéo ngang).
        /// </summary>
        public System.Windows.Forms.Panel panel2;
        private DropDownButton btnChucNang;
    }
}
