namespace H_Controls.Controls.Lookup
{
    partial class LookupForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pagingGridView1 = new H_Controls.Controls.PagingGridView();
            this.SuspendLayout();
            // 
            // pagingGridView1
            // 
            this.pagingGridView1.AllowAddRows = false;
            this.pagingGridView1.AllowDeletedRows = false;
            this.pagingGridView1.AllowOrderColumns = false;
            this.pagingGridView1.DisplayFields = null;
            this.pagingGridView1.DisplayHeaders = null;
            this.pagingGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pagingGridView1.Location = new System.Drawing.Point(0, 0);
            this.pagingGridView1.Name = "pagingGridView1";
            this.pagingGridView1.Readonly = true;
            this.pagingGridView1.Size = new System.Drawing.Size(584, 361);
            this.pagingGridView1.SortField = null;
            this.pagingGridView1.TabIndex = 0;
            this.pagingGridView1.TableName = null;
            this.pagingGridView1.CellDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.colorGridViewPaging1_CellDoubleClick);
            // 
            // LookupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 361);
            this.Controls.Add(this.pagingGridView1);
            this.Name = "LookupForm";
            this.ShowIcon = false;
            this.Text = "LookupForm";
            this.ResumeLayout(false);

        }

        #endregion

        private PagingGridView pagingGridView1;
    }
}