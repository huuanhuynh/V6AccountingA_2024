using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing;
namespace V6TabControlLib
{
	/// <summary>
	/// Summary description for TabPage.
	/// </summary>
	public class V6TabPage : System.Windows.Forms.TabPage
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
        public V6TabPage(System.ComponentModel.IContainer container)
		{
			///
			/// Required for Windows.Forms Class Composition Designer support
			///
			container.Add(this);
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		public V6TabPage()
		{
			///
			/// Required for Windows.Forms Class Composition Designer support
			///
			InitializeComponent();
         
			//
			// TODO: Add any constructor code after InitializeComponent call
			//
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

   
      
		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}

        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value + "               ";
            }
        }
        private ContextMenu ctxtMenu = null;
        public ContextMenu Menu
        {
            get
            {
                return this.ctxtMenu;
            }
            set
            {

                this.ctxtMenu = value;
            }
        }
        
          
      

		#endregion
	}
}
