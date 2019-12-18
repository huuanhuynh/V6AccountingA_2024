using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using V6Tools.V6Convert;

namespace V6Controls
{
	/// <summary>
	/// Summary description for V6ColorComboBox.
	/// </summary>
	public class V6ColorComboBox : V6ComboBox
	{
		private SolidBrush blackBrush; 
		private SolidBrush whiteBrush;

        //public Color SelectedColor
        //{
        //    get
        //    {
        //        return this.BackColor;
        //    }
        //    set
        //    {
        //        this.BackColor = value;
        //    }
        //}

        /// <summary>
        /// Cột chỉ thị màu trong data source.
        /// </summary>
        [Category("V6")]
        [DefaultValue("RGB")]
        [Description("Cột chỉ thị màu trong data source.")]
        public string ColorField { get; set; }

		public V6ColorComboBox()
		{
			//
			// TODO: Add constructor logic here
			//

			blackBrush = new SolidBrush( Color.Black );
			whiteBrush = new SolidBrush( Color.White );

            //this.Items.Add("Empty");
            //this.Items.Add( GetStringFromColor( Color.AliceBlue ) );
            //this.Items.Add( GetStringFromColor( Color.AntiqueWhite ) );
            //this.Items.Add( GetStringFromColor( Color.Aqua ) );
            //this.Items.Add( GetStringFromColor( Color.Aquamarine ) );
            //this.Items.Add( GetStringFromColor( Color.Azure ) );
            //this.Items.Add( GetStringFromColor( Color.Black ) );
            //this.Items.Add( GetStringFromColor( Color.BlanchedAlmond ) );
            //this.Items.Add( GetStringFromColor( Color.Blue ) );
            //this.Items.Add( GetStringFromColor( Color.BlueViolet ) );
            //this.Items.Add( GetStringFromColor( Color.Brown ) );
            //this.Items.Add( GetStringFromColor( Color.BurlyWood ) );
            //this.Items.Add( GetStringFromColor( Color.CadetBlue ) );
            //this.Items.Add( GetStringFromColor( Color.Chartreuse ) );
            //this.Items.Add( GetStringFromColor( Color.Chocolate ) );
            //this.Items.Add( GetStringFromColor( Color.Coral ) );
            //this.Items.Add( GetStringFromColor( Color.CornflowerBlue ) );
            //this.Items.Add( GetStringFromColor( Color.Cornsilk ) );
            //this.Items.Add( GetStringFromColor( Color.Crimson ) );
            //this.Items.Add( GetStringFromColor( Color.Cyan ) );
            //this.Items.Add( GetStringFromColor( Color.DarkBlue ) );
            //this.Items.Add( GetStringFromColor( Color.DarkCyan ) );
            //this.Items.Add( GetStringFromColor( Color.DarkGoldenrod ) );
            //this.Items.Add( GetStringFromColor( Color.DarkGray ) );
            //this.Items.Add( GetStringFromColor( Color.DarkGreen ) );
            //this.Items.Add( GetStringFromColor( Color.DarkKhaki ) );
            //this.Items.Add( GetStringFromColor( Color.DarkMagenta ) );
            //this.Items.Add( GetStringFromColor( Color.DarkOliveGreen ) );
            //this.Items.Add( GetStringFromColor( Color.DarkOrange ) );
            //this.Items.Add( GetStringFromColor( Color.DarkOrchid ) );
            //this.Items.Add( GetStringFromColor( Color.DarkRed ) ); 
            //this.Items.Add( GetStringFromColor( Color.DarkSalmon ) );
            //this.Items.Add( GetStringFromColor( Color.DarkSeaGreen ) );
            //this.Items.Add( GetStringFromColor( Color.DarkSlateBlue ) );
            //this.Items.Add( GetStringFromColor( Color.DarkSlateGray ) );
            //this.Items.Add( GetStringFromColor( Color.DarkTurquoise ) );
            //this.Items.Add( GetStringFromColor( Color.DarkViolet ) );
            //this.Items.Add( GetStringFromColor( Color.DeepSkyBlue ) );
            //this.Items.Add( GetStringFromColor( Color.DimGray ) );
            //this.Items.Add( GetStringFromColor( Color.DodgerBlue ) ); 
            //this.Items.Add( GetStringFromColor( Color.Firebrick ) );
            //this.Items.Add( GetStringFromColor( Color.FloralWhite ) );
            //this.Items.Add( GetStringFromColor( Color.ForestGreen ) );
            //this.Items.Add( GetStringFromColor( Color.Fuchsia ) );
            //this.Items.Add( GetStringFromColor( Color.Gainsboro ) );
            //this.Items.Add( GetStringFromColor( Color.GhostWhite ) );
            //this.Items.Add( GetStringFromColor( Color.Gold ) );
            //this.Items.Add( GetStringFromColor( Color.Goldenrod ) );
            //this.Items.Add( GetStringFromColor( Color.Gray ) );
            //this.Items.Add( GetStringFromColor( Color.Green ) );
            //this.Items.Add( GetStringFromColor( Color.GreenYellow ) );
            //this.Items.Add( GetStringFromColor( Color.Honeydew ) );
            //this.Items.Add( GetStringFromColor( Color.HotPink ) );
            //this.Items.Add( GetStringFromColor( Color.IndianRed ) );
            //this.Items.Add( GetStringFromColor( Color.Indigo ) );
            //this.Items.Add( GetStringFromColor( Color.Ivory ) );
            //this.Items.Add( GetStringFromColor( Color.Khaki ) );
            //this.Items.Add( GetStringFromColor( Color.Lavender ) );
            //this.Items.Add( GetStringFromColor( Color.LavenderBlush ) );
            //this.Items.Add( GetStringFromColor( Color.LawnGreen ) );
            //this.Items.Add( GetStringFromColor( Color.LemonChiffon ) );
            //this.Items.Add( GetStringFromColor( Color.LightBlue ) );
            //this.Items.Add( GetStringFromColor( Color.LightCoral ) );
            //this.Items.Add( GetStringFromColor( Color.LightCyan ) );
            //this.Items.Add( GetStringFromColor( Color.LightGoldenrodYellow ) );
            //this.Items.Add( GetStringFromColor( Color.LightGray ) );
            //this.Items.Add( GetStringFromColor( Color.LightGreen ) );
            //this.Items.Add( GetStringFromColor( Color.LightPink ) );
            //this.Items.Add( GetStringFromColor( Color.LightSalmon ) );
            //this.Items.Add( GetStringFromColor( Color.LightSeaGreen ) );
            //this.Items.Add( GetStringFromColor( Color.LightSkyBlue ) );
            //this.Items.Add( GetStringFromColor( Color.LightSlateGray ) );
            //this.Items.Add( GetStringFromColor( Color.LightSteelBlue ) );
            //this.Items.Add( GetStringFromColor( Color.LightYellow ) );
            //this.Items.Add( GetStringFromColor( Color.Lime ) );
            //this.Items.Add( GetStringFromColor( Color.LimeGreen ) );
            //this.Items.Add( GetStringFromColor( Color.Linen ) );
            //this.Items.Add( GetStringFromColor( Color.Magenta ) );
            //this.Items.Add( GetStringFromColor( Color.Maroon ) );
            //this.Items.Add( GetStringFromColor( Color.MediumAquamarine ) );
            //this.Items.Add( GetStringFromColor( Color.MediumBlue ) );
            //this.Items.Add( GetStringFromColor( Color.MediumOrchid ) );
            //this.Items.Add( GetStringFromColor( Color.MediumPurple ) );
            //this.Items.Add( GetStringFromColor( Color.MediumSeaGreen ) );
            //this.Items.Add( GetStringFromColor( Color.MediumSlateBlue ) );
            //this.Items.Add( GetStringFromColor( Color.MediumSpringGreen ) );
            //this.Items.Add( GetStringFromColor( Color.MediumTurquoise ) );
            //this.Items.Add( GetStringFromColor( Color.MediumVioletRed ) );
            //this.Items.Add( GetStringFromColor( Color.MidnightBlue ) );
            //this.Items.Add( GetStringFromColor( Color.MintCream ) );
            //this.Items.Add( GetStringFromColor( Color.MistyRose ) );
            //this.Items.Add( GetStringFromColor( Color.Moccasin ) );
            //this.Items.Add( GetStringFromColor( Color.NavajoWhite ) );
            //this.Items.Add( GetStringFromColor( Color.Navy ) );
            //this.Items.Add( GetStringFromColor( Color.OldLace ) );
            //this.Items.Add( GetStringFromColor( Color.Olive ) );
            //this.Items.Add( GetStringFromColor( Color.OliveDrab ) );
            //this.Items.Add( GetStringFromColor( Color.Orange ) );
            //this.Items.Add( GetStringFromColor( Color.OrangeRed ) );
            //this.Items.Add( GetStringFromColor( Color.Orchid ) );
            //this.Items.Add( GetStringFromColor( Color.PaleGoldenrod ) );
            //this.Items.Add( GetStringFromColor( Color.PaleGreen ) );
            //this.Items.Add( GetStringFromColor( Color.PaleTurquoise ) );
            //this.Items.Add( GetStringFromColor( Color.PaleVioletRed ) );
            //this.Items.Add( GetStringFromColor( Color.PapayaWhip ) );
            //this.Items.Add( GetStringFromColor( Color.PeachPuff ) );
            //this.Items.Add( GetStringFromColor( Color.Peru ) );
            //this.Items.Add( GetStringFromColor( Color.Pink ) );
            //this.Items.Add( GetStringFromColor( Color.Plum ) );
            //this.Items.Add( GetStringFromColor( Color.PowderBlue ) );
            //this.Items.Add( GetStringFromColor( Color.Purple ) );
            //this.Items.Add( GetStringFromColor( Color.Red ) );
            //this.Items.Add( GetStringFromColor( Color.RosyBrown ) );
            //this.Items.Add( GetStringFromColor( Color.RoyalBlue ) );
            //this.Items.Add( GetStringFromColor( Color.SaddleBrown ) );
            //this.Items.Add( GetStringFromColor( Color.Salmon ) );
            //this.Items.Add( GetStringFromColor( Color.SandyBrown ) );
            //this.Items.Add( GetStringFromColor( Color.SeaGreen ) );
            //this.Items.Add( GetStringFromColor( Color.SeaShell ) );
            //this.Items.Add( GetStringFromColor( Color.Sienna ) );
            //this.Items.Add( GetStringFromColor( Color.Silver ) );
            //this.Items.Add( GetStringFromColor( Color.SkyBlue ) );
            //this.Items.Add( GetStringFromColor( Color.SlateBlue ) );
            //this.Items.Add( GetStringFromColor( Color.SlateGray ) );
            //this.Items.Add( GetStringFromColor( Color.Snow ) );
            //this.Items.Add( GetStringFromColor( Color.SpringGreen ) );
            //this.Items.Add( GetStringFromColor( Color.SteelBlue ) );
            //this.Items.Add( GetStringFromColor( Color.Tan ) );
            //this.Items.Add( GetStringFromColor( Color.Teal ) );
            //this.Items.Add( GetStringFromColor( Color.Thistle ) );
            //this.Items.Add( GetStringFromColor( Color.Tomato ) );
            //this.Items.Add( GetStringFromColor( Color.Transparent ) );
            //this.Items.Add( GetStringFromColor( Color.Turquoise ) );
            //this.Items.Add( GetStringFromColor( Color.Violet ) );
            //this.Items.Add( GetStringFromColor( Color.Wheat ) );
            //this.Items.Add( GetStringFromColor( Color.White ) );
            //this.Items.Add( GetStringFromColor( Color.WhiteSmoke ) );
            //this.Items.Add( GetStringFromColor( Color.Yellow ) );
            //this.Items.Add( GetStringFromColor( Color.YellowGreen ) );

			this.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;

			this.DrawItem += new DrawItemEventHandler( OnDrawItem );
			this.SelectedIndexChanged += new System.EventHandler( OnSelectedIndexChanged );
			this.DropDown += new System.EventHandler( OnDropDown );
			
		}

		private void OnDrawItem(object sender, DrawItemEventArgs e)
		{
		    Graphics graphics = e.Graphics;
		    if (e.Index >= 0 && this.Items.Count > e.Index)
		    {
		        object currentItem = Items[e.Index];
		        Color itemColor = GetItemTextColor(currentItem);
		        SolidBrush brush = new SolidBrush(itemColor);
		        // Tô nền
		        graphics.FillRectangle(whiteBrush, e.Bounds);
                if ((e.State & DrawItemState.Selected) == DrawItemState.Selected)
		        {
                    //V6Controls.Forms.V6ControlFormHelper.ShowMainMessage(e.State.ToString());
                    if ((e.State & DrawItemState.ComboBoxEdit) == 0)
		            graphics.DrawRectangle(new Pen(Color.Blue), e.Bounds);
		        }
                else if ((e.State & DrawItemState.Focus)==0)
                {
                    graphics.DrawRectangle(new Pen(Color.White), e.Bounds);
                }
                //e.DrawFocusRectangle();
		        // Vẽ chữ
		        string drawText = GetItemText(currentItem);
                graphics.DrawString(drawText, e.Font, brush, e.Bounds);

                //if (itemColor.GetBrightness() < 0.5
                //    || itemColor == Color.Blue || itemColor == Color.MidnightBlue
                //    || itemColor == Color.DarkBlue || itemColor == Color.Indigo
                //    || itemColor == Color.MediumBlue || itemColor == Color.Maroon
                //    || itemColor == Color.Navy || itemColor == Color.Purple)
                //{
                //    graphics.DrawString(drawText, e.Font, whiteBrush, e.Bounds);
                //}
                //else
                //{
                //    graphics.DrawString(drawText, e.Font, blackBrush, e.Bounds);
                //}

		        this.SelectionStart = 0;
		        this.SelectionLength = 0;
		    }
		}

	    private void OnSelectedIndexChanged(object sender, System.EventArgs e)
	    {
	        //this.BackColor = GetItemTextColor(this.SelectedItem);
	    }

		/// <summary>
		/// prevents the hightlighted text being shown when drop down
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// 
		private void OnDropDown(object sender, System.EventArgs e)
		{
		    //this.BackColor = GetItemTextColor(this.SelectedItem);
		}

	    private Color GetItemTextColor(object itemObject)
	    {
            Color result = Color.Black;
	        DataRowView rowView = itemObject as DataRowView;
	        if (rowView != null)
	        {
	            var row = rowView.Row;
	            if (row.Table.Columns.Contains(ColorField))
	            {
	                result = ObjectAndString.StringToColor(row[ColorField].ToString());
	            }
	        }
            if (result.A < 255) result = Color.Black;
	        return result;
	    }

	}
}
