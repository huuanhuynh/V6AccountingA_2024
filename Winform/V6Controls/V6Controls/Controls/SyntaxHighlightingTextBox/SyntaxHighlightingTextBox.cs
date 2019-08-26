using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using UrielGuy.SyntaxHighlightingTextBox;
//using System;
//using System.Windows.Forms;
//using System.Drawing;
//using System.Runtime.InteropServices;

namespace V6Controls.Controls.SyntaxHighlightingTextBox
{
	/// <summary>
	/// A textbox the does syntax highlighting.
	/// </summary>
	public class SyntaxHighlightingTextBox :	System.Windows.Forms.RichTextBox 
	{
	    #region Members

		//Members exposed via properties
		private SeperaratorCollection mSeperators = new SeperaratorCollection();  
		private HighLightDescriptorCollection mHighlightDescriptors = new HighLightDescriptorCollection();
		private bool mCaseSesitive = false;
		private bool mFilterAutoComplete = true;

		//Internal use members
		private bool mAutoCompleteShown = false;
		private bool mParsing = false;
		private bool mIgnoreLostFocus = false;

		private AutoCompleteForm mAutoCompleteForm = new AutoCompleteForm();

		//Undo/Redo members
		private ArrayList mUndoList = new ArrayList();
		private Stack mRedoStack = new Stack();
		private bool mIsUndo = false;
		private UndoRedoInfo mLastInfo = new UndoRedoInfo("", new Win32.POINT(), 0);
		private int mMaxUndoRedoSteps = 50;

		#endregion

		#region Properties
		/// <summary>
		/// Determines if token recognition is case sensitive.
		/// </summary>
		[Category("Behavior")]
		public bool CaseSensitive 
		{ 
			get 
			{ 
				return mCaseSesitive; 
			}
			set 
			{ 
				mCaseSesitive = value;
			}
		}


		/// <summary>
		/// Sets whether or not to remove items from the Autocomplete window as the user types...
		/// </summary>
		[Category("Behavior")]
		public bool FilterAutoComplete 
		{
			get 
			{
				return mFilterAutoComplete;
			}
			set 
			{
				mFilterAutoComplete = value;
			}
		}

		/// <summary>
		/// Set the maximum amount of Undo/Redo steps.
		/// </summary>
		[Category("Behavior")]
		public int MaxUndoRedoSteps 
		{
			get 
			{
				return mMaxUndoRedoSteps;
			}
			set
			{
				mMaxUndoRedoSteps = value;
			}
		}

			/// <summary>
			/// A collection of charecters. a token is every string between two seperators.
			/// </summary>
			/// 
			public SeperaratorCollection Seperators 
		{
			get 
			{
				return mSeperators;
			}
		}
		
		/// <summary>
		/// The collection of highlight descriptors.
		/// </summary>
		/// 
		public HighLightDescriptorCollection HighlightDescriptors 
		{
			get 
			{
				return mHighlightDescriptors;
			}
		}

		#endregion

		#region Overriden methods
		
		public void CallOnTextChange()
	    {
	        OnTextChanged(new EventArgs());
	    }
		/// <summary>
		/// The on text changed overrided. Here we parse the text into RTF for the highlighting.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnTextChanged(EventArgs e)
		{
			if (mParsing) return;
			mParsing = true;
			Win32.LockWindowUpdate(Handle);
			base.OnTextChanged(e);

			if (!mIsUndo)
			{
				mRedoStack.Clear();
				mUndoList.Insert(0,mLastInfo);
				this.LimitUndo();
				mLastInfo = new UndoRedoInfo(Text, GetScrollPos(), SelectionStart);
			}
			
			//Save scroll bar an cursor position, changeing the RTF moves the cursor and scrollbars to top positin
			Win32.POINT scrollPos = GetScrollPos();
			int cursorLoc = SelectionStart;

			//Created with an estimate of how big the stringbuilder has to be...
			StringBuilder sb = new
				StringBuilder((int)(Text.Length * 1.5 + 150));
	
			//Adding RTF header
			sb.Append(@"{\rtf1\fbidis\ansi\ansicpg1255\deff0\deflang1037{\fonttbl{");
            //sb.Append(@"{\rtf1\fbidis\ansi\ansicpg1252\deff0\deflang1033{\fonttbl{");
			
			//Font table creation
			int fontCounter = 0;
			Hashtable fonts = new Hashtable();
			AddFontToTable(sb, Font, ref fontCounter, fonts);
			foreach (HighlightDescriptor hd in mHighlightDescriptors)
			{
				if ((hd.Font !=  null) && !fonts.ContainsKey(hd.Font.Name))
				{
					AddFontToTable(sb, hd.Font,ref fontCounter, fonts);
				}
			}
			sb.Append("}\n");

			//ColorTable
			
			sb.Append(@"{\colortbl ;");
			Hashtable colors = new Hashtable();
			int colorCounter = 1;
			AddColorToTable(sb, ForeColor, ref colorCounter, colors);
			AddColorToTable(sb, BackColor, ref colorCounter, colors);
			
			foreach (HighlightDescriptor hd in mHighlightDescriptors)
			{
				if (!colors.ContainsKey(hd.Color))
				{
					AddColorToTable(sb, hd.Color, ref colorCounter, colors);
				}
			}		

			//Parsing text
	
			sb.Append("}\n").Append(@"\viewkind4\uc1\pard\ltrpar");
			SetDefaultSettings(sb, colors, fonts);

			char[] sperators = mSeperators.GetAsCharArray();
            
            //Fix unicode
            string text2 = UnicodeEscapeCharacters(Text);
            string[] lines = text2.Split('\n');
			for (int lineCounter = 0 ; lineCounter < lines.Length; lineCounter++)
			{
				if (lineCounter != 0)
				{
					AddNewLine(sb);
				}
				string line = lines[lineCounter];
				string[] tokens = mCaseSesitive ? line.Split(sperators) : line.ToUpper().Split(sperators);
				if (tokens.Length == 0)
				{
					sb.Append(line);
					AddNewLine(sb);
					continue;
				}

				int tokenCounter = 0;
				for (int i = 0; i < line.Length ;)
				{
					char curChar = line[i];
					if (mSeperators.Contains(curChar))
					{
						sb.Append(curChar);
						i++;
					}
					else
					{
						string curToken = tokens[tokenCounter++];
						bool bAddToken = true;
						foreach	(HighlightDescriptor hd in mHighlightDescriptors)
						{
							string	compareStr = mCaseSesitive ? hd.Token : hd.Token.ToUpper();
							bool match = false;

							//Check if the highlight descriptor matches the current toker according to the DescriptoRecognision property.
							switch	(hd.DescriptorRecognition)
							{
								case DescriptorRecognition.WholeWord:
									if (curToken == compareStr)
									{
											match = true;
									}
									break;
								case DescriptorRecognition.StartsWith:
									if (curToken.StartsWith(compareStr))
									{
										match = true;
									}
									break;
								case DescriptorRecognition.Contains:
									if (curToken.IndexOf(compareStr) != -1)
									{
										match = true;
									}
									break;
							}
							if (!match)
							{
								//If this token doesn't match chech the next one.
								continue;
							}

							//printing this token will be handled by the inner code, don't apply default settings...
							bAddToken = false;
	
							//Set colors to current descriptor settings.
							SetDescriptorSettings(sb, hd, colors, fonts);

							//Print text affected by this descriptor.
							switch (hd.DescriptorType)
							{
								case DescriptorType.Word:
									sb.Append(line.Substring(i, curToken.Length));
									SetDefaultSettings(sb, colors, fonts);
									i += curToken.Length;
									break;
								case DescriptorType.ToEOL:
									sb.Append(line.Remove(0, i));
									i = line.Length;
									SetDefaultSettings(sb, colors, fonts);
									break;
								case DescriptorType.ToCloseToken:
                                    while ((line.IndexOf(hd.CloseToken, i) == -1) && (lineCounter < lines.Length))									
									{
										sb.Append(line.Remove(0, i));
										lineCounter++;
										if (lineCounter < lines.Length)
										{
											AddNewLine(sb);
											line = lines[lineCounter];
											i = 0;
										}
										else
										{
											i = line.Length;
										}
									}
									if (line.IndexOf(hd.CloseToken, i) != -1)
									{
										sb.Append(line.Substring(i, line.IndexOf(hd.CloseToken, i) + hd.CloseToken.Length - i) );
										line = line.Remove(0, line.IndexOf(hd.CloseToken, i) + hd.CloseToken.Length);
										tokenCounter = 0;
										tokens = mCaseSesitive ? line.Split(sperators) : line.ToUpper().Split(sperators);
										SetDefaultSettings(sb, colors, fonts);
										i = 0;
									}
									break;
							}
							break;
						}
						if (bAddToken)
						{
							//Print text with default settings...
							sb.Append(line.Substring(i, curToken.Length));
							i+=	curToken.Length;
						}
					}
				}
			}
	
//			System.Diagnostics.Debug.WriteLine(sb.ToString());
			Rtf = sb.ToString();
            
			//Restore cursor and scrollbars location.
			SelectionStart = cursorLoc;

			mParsing = false;
		
			SetScrollPos(scrollPos);
			Win32.LockWindowUpdate((IntPtr)0);
			Invalidate();
			
			if (mAutoCompleteShown)
			{
				if (mFilterAutoComplete)
				{
					SetAutoCompleteItems();
					SetAutoCompleteSize();
					SetAutoCompleteLocation(false);
				}
				SetBestSelectedAutoCompleteItem();
			}

		}

        /// <summary>
        /// <para>https://en.wikipedia.org/wiki/Rich_Text_Format#Character_encoding</para>
        /// <para>    For a Unicode escape the control word \u is used, followed by a 16-bit signed decimal integer giving the Unicode UTF-16 code unit number.</para>
        /// <para>For the benefit of programs without Unicode support, this must be followed by the nearest representation of this character in the specified code page.</para>
        /// <para>For example, \u1576? would give the Arabic letter bāʼ ب, specifying that older programs which do not have Unicode support should render it as a question mark instead.</para>
        /// </summary>
        /// <param name="uString"></param>
        /// <returns></returns>
	    private string UnicodeEscapeCharacters(string uString)
	    {
	        string result = "";
	        if (uString != null)
	        {
                // Replacing "\" to "\\" for RTF...
	            uString = uString.Replace("\\", "\\\\").Replace("{", "\\{").Replace("}", "\\}");
                // Change unicode char to \u1234__
	            foreach (char c in uString)
	            {
	                if (c > 255) result += "\\u" + (int) c + "  ";
	                else result += c;
	            }
	        }
	        return result;
	    }


		protected override void OnVScroll(EventArgs e)
		{
			if (mParsing) return;
			base.OnVScroll (e);
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			HideAutoCompleteForm();
			base.OnMouseDown (e);
		}

		/// <summary>
		/// Taking care of Keyboard events
		/// </summary>
		/// <param name="m"></param>
		/// <remarks>
		/// Since even when overriding the OnKeyDown methoed and not calling the base function 
		/// you don't have full control of the input, I've decided to catch windows messages to handle them.
		/// </remarks>
		protected override void WndProc(ref Message m)
		{
			switch (m.Msg)
			{
				case Win32.WM_PAINT:
				{
					//Don't draw the control while parsing to avoid flicker.
					if (mParsing)
					{
						return;
					}
					break;
				}
				case Win32.WM_KEYDOWN:
				{
					if (mAutoCompleteShown)
					{
						switch ((Keys)(int)m.WParam)
						{
							case Keys.Down:
							{
								if (mAutoCompleteForm.Items.Count != 0)
								{
									mAutoCompleteForm.SelectedIndex = (mAutoCompleteForm.SelectedIndex + 1) % mAutoCompleteForm.Items.Count;
								}
								return;
							}
							case Keys.Up:
							{
								if (mAutoCompleteForm.Items.Count != 0)
								{
									if (mAutoCompleteForm.SelectedIndex < 1)
									{
										mAutoCompleteForm.SelectedIndex = mAutoCompleteForm.Items.Count - 1;
									}
									else
									{
										mAutoCompleteForm.SelectedIndex--;
									}
								}
								return;
							}
							case Keys.Enter:
							case Keys.Space:
							{
								AcceptAutoCompleteItem();
								return;
							}
							case Keys.Escape:
							{
								HideAutoCompleteForm();
								return;
							}
								
						}
					}
					else
					{
						if (((Keys)(int)m.WParam == Keys.Space) && 
							((Win32.GetKeyState(Win32.VK_CONTROL) & Win32.KS_KEYDOWN) != 0))
						{
							CompleteWord();
						} 
						else if (((Keys)(int)m.WParam == Keys.Z) && 
							((Win32.GetKeyState(Win32.VK_CONTROL) & Win32.KS_KEYDOWN) != 0))
						{
							Undo();
							return;
						}
						else if (((Keys)(int)m.WParam == Keys.Y) && 
							((Win32.GetKeyState(Win32.VK_CONTROL) & Win32.KS_KEYDOWN) != 0))
						{
							Redo();
							return;
						}
					}
					break;
				}
				case Win32.WM_CHAR:
				{
					switch ((Keys)(int)m.WParam)
					{
						case Keys.Space:
							if ((Win32.GetKeyState(Win32.VK_CONTROL) & Win32.KS_KEYDOWN )!= 0)
							{
								return;
							}
							break;
						case Keys.Enter:
							if (mAutoCompleteShown) return;
							break;
					}
				}
				break;

			}
			base.WndProc (ref m);
		}


		/// <summary>
		/// Hides the AutoComplete form when losing focus on textbox.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnLostFocus(EventArgs e)
		{
			if (!mIgnoreLostFocus)
			{
				HideAutoCompleteForm();
			}
			base.OnLostFocus (e);
		}


		#endregion

		#region Undo/Redo Code
		public new bool CanUndo 
		{
			get 
			{
				return mUndoList.Count > 0;
			}
		}
		public new bool CanRedo
		{
			get 
			{
				return mRedoStack.Count > 0;
			}
		}

		private void LimitUndo()
		{
			while (mUndoList.Count > mMaxUndoRedoSteps)
			{
				mUndoList.RemoveAt(mMaxUndoRedoSteps);
			}
		}

		public new void Undo()
		{
			if (!CanUndo)
				return;
			mIsUndo = true;
			mRedoStack.Push(new UndoRedoInfo(Text, GetScrollPos(), SelectionStart));
			UndoRedoInfo info = (UndoRedoInfo)mUndoList[0];
			mUndoList.RemoveAt(0);
			Text = info.Text;
			SelectionStart = info.CursorLocation;
			SetScrollPos(info.ScrollPos);
			mLastInfo = info;
			mIsUndo = false;
		}
		public new void Redo()
		{
			if (!CanRedo)
				return;
			mIsUndo = true;
			mUndoList.Insert(0,new UndoRedoInfo(Text, GetScrollPos(), SelectionStart));
			LimitUndo();
			UndoRedoInfo info = (UndoRedoInfo)mRedoStack.Pop();
			Text = info.Text;
			SelectionStart = info.CursorLocation;
			SetScrollPos(info.ScrollPos);
			mIsUndo = false;
		}

		private class UndoRedoInfo
		{
			public UndoRedoInfo(string text, Win32.POINT scrollPos, int cursorLoc)
			{
				Text = text;
				ScrollPos = scrollPos;
				CursorLocation = cursorLoc;
			}
			public readonly Win32.POINT ScrollPos;
			public readonly int CursorLocation;
			public readonly string Text;
		}
		#endregion

		#region AutoComplete functions

		/// <summary>
		/// Entry point to autocomplete mechanism.
		/// Tries to complete the current word. if it fails it shows the AutoComplete form.
		/// </summary>
		private void CompleteWord()
		{
            int curTokenStartIndex = SelectionStart - 1;
            while (curTokenStartIndex > 0)
            {
                if (mSeperators.Contains(Text[curTokenStartIndex - 1])) break;
                curTokenStartIndex--;
            }
            if (curTokenStartIndex < 0) curTokenStartIndex = 0;
            int curTokenEndIndex = SelectionStart;
            string curTokenString = Text.Substring(curTokenStartIndex, curTokenEndIndex - curTokenStartIndex).ToUpper();
			
			string token = null;
			foreach (HighlightDescriptor hd in mHighlightDescriptors)
			{
				if (hd.UseForAutoComplete && hd.Token.ToUpper().StartsWith(curTokenString))
				{
					if (token == null)
					{
						token = hd.Token;
					}
					else
					{
						token = null;
						break;
					}
				}
			}
			if (token == null)
			{
				ShowAutoComplete();
			}
			else
			{
				SelectionStart = curTokenStartIndex;
				SelectionLength = curTokenEndIndex - curTokenStartIndex;
				SelectedText = token;
				//SelectionStart = SelectionStart + SelectionLength;
				//SelectionLength = 0;
			}
		}

		/// <summary>
		/// replace the current word of the cursor with the one from the AutoComplete form and closes it.
		/// </summary>
		/// <returns>If the operation was succesful</returns>
		private bool AcceptAutoCompleteItem()
		{
			if (mAutoCompleteForm.SelectedItem == null)
			{
				return false;
			}

            int curTokenStartIndex = SelectionStart - 1;
            while (curTokenStartIndex > 0)
            {
                if (mSeperators.Contains(Text[curTokenStartIndex - 1])) break;
                curTokenStartIndex--;
            }
            if (curTokenStartIndex < 0) curTokenStartIndex = 0;
            int curTokenEndIndex = SelectionStart;
            
			if (curTokenEndIndex == -1) 
			{
				curTokenEndIndex = Text.Length;
			}
			SelectionStart = curTokenStartIndex;
			SelectionLength = curTokenEndIndex - curTokenStartIndex;
			SelectedText = mAutoCompleteForm.SelectedItem;
			//SelectionStart = SelectionStart + SelectionLength;
			//SelectionLength = 0;
			
			HideAutoCompleteForm();
			return true;
		}



		/// <summary>
		/// Finds the and sets the best matching token as the selected item in the AutoCompleteForm.
		/// </summary>
		private void SetBestSelectedAutoCompleteItem()
		{
			int curTokenStartIndex = SelectionStart - 1;
            while (curTokenStartIndex > 0)
		    {
		        if (mSeperators.Contains(Text[curTokenStartIndex - 1])) break;
		        curTokenStartIndex--;
		    }
		    if (curTokenStartIndex < 0) curTokenStartIndex = 0;
            int curTokenEndIndex= SelectionStart;
			string curTokenString = Text.Substring(curTokenStartIndex, curTokenEndIndex - curTokenStartIndex).ToUpper();
			
			if ((mAutoCompleteForm.SelectedItem != null) && 
				mAutoCompleteForm.SelectedItem.ToUpper().StartsWith(curTokenString))
			{
				return;
			}

			int matchingChars = -1;
			string bestMatchingToken = null;

			foreach (string item in mAutoCompleteForm.Items)
			{
				bool isWholeItemMatching = true;
				for (int i = 0 ; i < Math.Min(item.Length, curTokenString.Length); i++)
				{
					if (char.ToUpper(item[i]) != char.ToUpper(curTokenString[i]))
					{
						isWholeItemMatching = false;
						if (i-1 > matchingChars)
						{
							matchingChars = i;
							bestMatchingToken = item;
							break;
						}
					}
				}
				if (isWholeItemMatching &&
					(Math.Min(item.Length, curTokenString.Length) > matchingChars))
				{
					matchingChars = Math.Min(item.Length, curTokenString.Length);
					bestMatchingToken = item;
				}
			}
			
			if (bestMatchingToken != null)
			{
				mAutoCompleteForm.SelectedIndex = mAutoCompleteForm.Items.IndexOf(bestMatchingToken);
			}


		}

		/// <summary>
		/// Sets the items for the AutoComplete form.
		/// </summary>
		private void SetAutoCompleteItems()
		{
			mAutoCompleteForm.Items.Clear();
			string filterString = "";
			if (mFilterAutoComplete)
			{
                int filterTokenStartIndex = SelectionStart - 1;
                while (filterTokenStartIndex > 0)
                {
                    if (mSeperators.Contains(Text[filterTokenStartIndex - 1])) break;
                    filterTokenStartIndex--;
                }
                if (filterTokenStartIndex < 0) filterTokenStartIndex = 0;
                int filterTokenEndIndex = SelectionStart;
				
                try
                {
                    filterString = Text.Substring(filterTokenStartIndex, filterTokenEndIndex - filterTokenStartIndex).ToUpper();
                }
                catch { }
			}
		
			foreach (HighlightDescriptor hd in mHighlightDescriptors)
			{
				if (hd.Token.ToUpper().StartsWith(filterString) && hd.UseForAutoComplete)
				{
					mAutoCompleteForm.Items.Add(hd.Token);
				}
			}
			mAutoCompleteForm.UpdateView();
		}
		
		/// <summary>
		/// Sets the size. the size is limited by the MaxSize property in the form itself.
		/// </summary>
		private void SetAutoCompleteSize()
		{
			mAutoCompleteForm.Height = Math.Min(
				Math.Max(mAutoCompleteForm.Items.Count, 1) * mAutoCompleteForm.ItemHeight + 4, 
				mAutoCompleteForm.MaximumSize.Height);
		}

		/// <summary>
		/// closes the AutoCompleteForm.
		/// </summary>
		private void HideAutoCompleteForm()
		{
			mAutoCompleteForm.Visible = false;
			mAutoCompleteShown = false;
		}
		

		/// <summary>
		/// Sets the location of the AutoComplete form, maiking sure it's on the screen where the cursor is.
		/// </summary>
		/// <param name="moveHorizontly">determines wheather or not to move the form horizontly.</param>
		private void SetAutoCompleteLocation(bool moveHorizontly)
		{
			Point cursorLocation = GetPositionFromCharIndex(SelectionStart);
			Screen screen = Screen.FromPoint(cursorLocation);
			Point optimalLocation = new Point(PointToScreen(cursorLocation).X-15, (int)(PointToScreen(cursorLocation).Y + Font.Size*2 + 2));
			Rectangle desiredPlace = new Rectangle(optimalLocation , mAutoCompleteForm.Size);
			desiredPlace.Width = 190;
			if (desiredPlace.Left < screen.Bounds.Left) 
			{
				desiredPlace.X = screen.Bounds.Left;
			}
			if (desiredPlace.Right > screen.Bounds.Right)
			{
				desiredPlace.X -= (desiredPlace.Right - screen.Bounds.Right);
			}
			if (desiredPlace.Bottom > screen.Bounds.Bottom)
			{
				desiredPlace.Y = cursorLocation.Y - 2 - desiredPlace.Height;
			}
			if (!moveHorizontly)
			{
				desiredPlace.X = mAutoCompleteForm.Left;
			}

			mAutoCompleteForm.Bounds = desiredPlace;
		}

		/// <summary>
		/// Shows the Autocomplete form.
		/// </summary>
		public void ShowAutoComplete()
		{
			SetAutoCompleteItems();
			SetAutoCompleteSize();
			SetAutoCompleteLocation(true);
			mIgnoreLostFocus = true;
			mAutoCompleteForm.Visible = true;
			SetBestSelectedAutoCompleteItem();
			mAutoCompleteShown = true;
			Focus();
			mIgnoreLostFocus = false;
		}

		#endregion 

		#region Rtf building helper functions

		/// <summary>
		/// Set color and font to default control settings.
		/// </summary>
		/// <param name="sb">the string builder building the RTF</param>
		/// <param name="colors">colors hashtable</param>
		/// <param name="fonts">fonts hashtable</param>
		private void SetDefaultSettings(StringBuilder sb, Hashtable colors, Hashtable fonts)
		{
			SetColor(sb, ForeColor, colors);
			SetFont(sb, Font, fonts);
			SetFontSize(sb, (int)Font.Size);
			EndTags(sb);
		}

		/// <summary>
		/// Set Color and font to a highlight descriptor settings.
		/// </summary>
		/// <param name="sb">the string builder building the RTF</param>
		/// <param name="hd">the HighlightDescriptor with the font and color settings to apply.</param>
		/// <param name="colors">colors hashtable</param>
		/// <param name="fonts">fonts hashtable</param>
		private void SetDescriptorSettings(StringBuilder sb, HighlightDescriptor hd, Hashtable colors, Hashtable fonts)
		{
			SetColor(sb, hd.Color, colors);
			if (hd.Font != null)
			{
				SetFont(sb, hd.Font, fonts);
				SetFontSize(sb, (int)hd.Font.Size);
			}
			EndTags(sb);

		}
		/// <summary>
		/// Sets the color to the specified color
		/// </summary>
		private void SetColor(StringBuilder sb, Color color, Hashtable colors)
		{
			sb.Append(@"\cf").Append(colors[color]);
		}
		/// <summary>
		/// Sets the backgroung color to the specified color.
		/// </summary>
		private void SetBackColor(StringBuilder sb, Color color, Hashtable colors)
		{
			sb.Append(@"\cb").Append(colors[color]);
		}
		/// <summary>
		/// Sets the font to the specified font.
		/// </summary>
		private void SetFont(StringBuilder sb, Font font, Hashtable fonts)
		{
            //font = null;//huuan add
			if (font == null) return;
			sb.Append(@"\f").Append(fonts[font.Name]);
		}
		/// <summary>
		/// Sets the font size to the specified font size.
		/// </summary>
		private void SetFontSize(StringBuilder sb, int size)
		{
			sb.Append(@"\fs").Append(size*2);
		}
		/// <summary>
		/// Adds a newLine mark to the RTF.
		/// </summary>
		private void AddNewLine(StringBuilder sb)
		{
			sb.Append("\\par\n");
		}

		/// <summary>
		/// Ends a RTF tags section.
		/// </summary>
		private void EndTags(StringBuilder sb)
		{
			sb.Append(' ');
		}

		/// <summary>
		/// Adds a font to the RTF's font table and to the fonts hashtable.
		/// </summary>
		/// <param name="sb">The RTF's string builder</param>
		/// <param name="font">the Font to add</param>
		/// <param name="counter">a counter, containing the amount of fonts in the table</param>
		/// <param name="fonts">an hashtable. the key is the font's name. the value is it's index in the table</param>
		private void AddFontToTable(StringBuilder sb, Font font, ref int counter, Hashtable fonts)
		{
	
			sb.Append(@"\f").Append(counter).Append(@"\fnil\fcharset0").Append(font.Name).Append(";}");
			fonts.Add(font.Name, counter++);
		}

		/// <summary>
		/// Adds a color to the RTF's color table and to the colors hashtable.
		/// </summary>
		/// <param name="sb">The RTF's string builder</param>
		/// <param name="color">the color to add</param>
		/// <param name="counter">a counter, containing the amount of colors in the table</param>
		/// <param name="colors">an hashtable. the key is the color. the value is it's index in the table</param>
		private void AddColorToTable(StringBuilder sb, Color color, ref int counter, Hashtable colors)
		{
	
			sb.Append(@"\red").Append(color.R).Append(@"\green").Append(color.G).Append(@"\blue")
				.Append(color.B).Append(";");
			colors.Add(color, counter++);
		}

		#endregion

		#region Scrollbar positions functions
		/// <summary>
		/// Sends a win32 message to get the scrollbars' position.
		/// </summary>
		/// <returns>a POINT structore containing horizontal and vertical scrollbar position.</returns>
        private Win32.POINT GetScrollPos()//unsafe
		{
			Win32.POINT res = new Win32.POINT();
            //IntPtr ptr = new IntPtr(res);
            //Win32.SendMessage(Handle, Win32.EM_GETSCROLLPOS, 0, ptr);
			return res;

		}

		/// <summary>
		/// Sends a win32 message to set scrollbars position.
		/// </summary>
		/// <param name="point">a POINT conatining H/Vscrollbar scrollpos.</param>
        private void SetScrollPos(Win32.POINT point)//unsafe
		{
            //IntPtr ptr = new IntPtr(&point);
            //Win32.SendMessage(Handle, Win32.EM_SETSCROLLPOS, 0, ptr);
		}
		#endregion

        private void InitializeComponent()
        {
            this.SuspendLayout();
            this.ResumeLayout(false);

        }
        #region =============== HuuAn add===================
        #region ===Biến===
        public bool coThayDoi = true;
        public string tenFile = "ADNT";
        //public static string filter = "";
        #endregion
        #region ===Struct===
        [StructLayout(LayoutKind.Sequential)]
        private struct STRUCT_RECT
        {
            public Int32 left;
            public Int32 top;
            public Int32 right;
            public Int32 bottom;
        }
        [StructLayout(LayoutKind.Sequential)]
        private struct STRUCT_CHARRANGE
        {
            public Int32 cpMin;
            public Int32 cpMax;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct STRUCT_FORMATRANGE
        {
            public IntPtr hdc;
            public IntPtr hdcTarget;
            public STRUCT_RECT rc;
            public STRUCT_RECT rcPage;
            public STRUCT_CHARRANGE chrg;
        }



        [ StructLayout( LayoutKind.Sequential )]
        private struct STRUCT_CHARFORMAT
        {
            public int    cbSize; 
            public UInt32 dwMask; 
            public UInt32 dwEffects; 
            public Int32  yHeight; 
            public Int32  yOffset; 
            public Int32   crTextColor; 
            public byte   bCharSet; 
            public byte   bPitchAndFamily; 
            [MarshalAs(UnmanagedType.ByValArray, SizeConst=32)]
            public char[] szFaceName; 
        }
        #endregion
        #region ===DLL import===
        [DllImport("user32.dll")]        
        private static extern Int32 
            SendMessage(IntPtr hWnd, Int32 msg, Int32 wParam, IntPtr lParam);
        
        private const Int32 WM_USER        = 0x400;
        private const Int32 EM_FORMATRANGE = WM_USER+57;

        [DllImport("kernel32.dll", EntryPoint = "LoadLibraryW",
        CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern IntPtr LoadLibraryW(string s_File);
        public static IntPtr LoadLibrary(string s_File)
        {
            var module = LoadLibraryW(s_File);
            if (module != IntPtr.Zero)
                return module;
            var error = Marshal.GetLastWin32Error();
            throw new Win32Exception(error);
        }
        protected override CreateParams CreateParams
        {
            get
            {	// Update RichTextBox
                var cp = base.CreateParams;
                try
                {
                    LoadLibrary("MsftEdit.dll"); // Available since XP SP1
                    cp.ClassName = "RichEdit50W";
                }
                catch { /* Windows XP without any Service Pack.*/ }
                return cp;
            }
        }
        #endregion
        #region ===Hàm===
        /// <summary>
        /// Calculate or render the contents of our RichTextBox for printing
        /// </summary>
        /// <param name="measureOnly">If true, only the calculation is performed,
        /// otherwise the text is rendered as well</param>
        /// <param name="e">The PrintPageEventArgs object from the
        /// PrintPage event</param>
        /// <param name="charFrom">Index of first character to be printed</param>
        /// <param name="charTo">Index of last character to be printed</param>
        /// <returns>(Index of last character that fitted on the
        /// page) + 1</returns>
        public int FormatRange(bool measureOnly, PrintPageEventArgs e,
                               int charFrom, int charTo)
        {
            // Specify which characters to print
            STRUCT_CHARRANGE cr;
            cr.cpMin = charFrom;
            cr.cpMax = charTo;

            // Specify the area inside page margins
            STRUCT_RECT rc;
            rc.top = HundredthInchToTwips(e.MarginBounds.Top);
            rc.bottom = HundredthInchToTwips(e.MarginBounds.Bottom);
            rc.left = HundredthInchToTwips(e.MarginBounds.Left);
            rc.right = HundredthInchToTwips(e.MarginBounds.Right);

            // Specify the page area
            STRUCT_RECT rcPage;
            rcPage.top = HundredthInchToTwips(e.PageBounds.Top);
            rcPage.bottom = HundredthInchToTwips(e.PageBounds.Bottom);
            rcPage.left = HundredthInchToTwips(e.PageBounds.Left);
            rcPage.right = HundredthInchToTwips(e.PageBounds.Right);

            // Get device context of output device
            IntPtr hdc = e.Graphics.GetHdc();

            // Fill in the FORMATRANGE struct
            STRUCT_FORMATRANGE fr;
            fr.chrg = cr;
            fr.hdc = hdc;
            fr.hdcTarget = hdc;
            fr.rc = rc;
            fr.rcPage = rcPage;

            // Non-Zero wParam means render, Zero means measure
            Int32 wParam = (measureOnly ? 0 : 1);

            // Allocate memory for the FORMATRANGE struct and
            // copy the contents of our struct to this memory
            IntPtr lParam = Marshal.AllocCoTaskMem(Marshal.SizeOf(fr));
            Marshal.StructureToPtr(fr, lParam, false);

            // Send the actual Win32 message
            int res = SendMessage(Handle, EM_FORMATRANGE, wParam, lParam);

            // Free allocated memory
            Marshal.FreeCoTaskMem(lParam);

            // and release the device context
            e.Graphics.ReleaseHdc(hdc);

            return res;
        }
        /// <summary>
        /// Convert between 1/100 inch (unit used by the .NET framework)
        /// and twips (1/1440 inch, used by Win32 API calls)
        /// </summary>
        /// <param name="n">Value in 1/100 inch</param>
        /// <returns>Value in twips</returns>
        private Int32 HundredthInchToTwips(int n)
        {
            return (Int32)(n * 14.4);
        }
        /// <summary>
        /// Free cached data from rich edit control after printing
        /// </summary>
        public void FormatRangeDone()
        {
            IntPtr lParam = new IntPtr(0);
            SendMessage(Handle, EM_FORMATRANGE, 0, lParam);
        }
        //==========================Print==================        
        private string printerName = "";

        public string PrinterName
        {
            //get { return printerName; }
            set { printerName = value; }
        }        
        
        private void printDocSetting(PrintDocument printDoc)
        {
            if(printerName != null && printerName != "")
                printDoc.PrinterSettings.PrinterName = printerName;
        }
        public void PrintRichTextContents()
        {
            PrintDocument printDoc = new PrintDocument();
            printDocSetting(printDoc);
            printDoc.BeginPrint += new PrintEventHandler(printDoc_BeginPrint);
            printDoc.PrintPage += new PrintPageEventHandler(printDoc_PrintPage);
            printDoc.EndPrint += new PrintEventHandler(printDoc_EndPrint);
            // Start printing process
            printDoc.Print();
        }
        // variable to trace text to print for pagination
        private int m_nFirstCharOnPage;

        private void printDoc_BeginPrint(object sender,
            System.Drawing.Printing.PrintEventArgs e)
        {
            // Start at the beginning of the text
            m_nFirstCharOnPage = 0;
        }

        private void printDoc_PrintPage(object sender,
            System.Drawing.Printing.PrintPageEventArgs e)
        {
            // To print the boundaries of the current page margins
            // uncomment the next line:
            // e.Graphics.DrawRectangle(System.Drawing.Pens.Blue, e.MarginBounds);

            // make the RichTextBoxEx calculate and render as much text as will
            // fit on the page and remember the last character printed for the
            // beginning of the next page
            m_nFirstCharOnPage = this.FormatRange(false,
                                                    e,
                                                    m_nFirstCharOnPage,
                                                    this.TextLength);

            // check if there are more pages to print
            if (m_nFirstCharOnPage < this.TextLength)
                e.HasMorePages = true;
            else
                e.HasMorePages = false;
        }

        private void printDoc_EndPrint(object sender,
            System.Drawing.Printing.PrintEventArgs e)
        {
            // Clean up cached information
            this.FormatRangeDone();
        }

        

        //===============
        public int demSoTuKhoaMoTruDong(string tuKhoaBatDau, string tuKhoaKetThuc)
        {
            int dem = 0;
            string s = this.Text.Substring(0, this.SelectionStart);
            while (s.Contains(tuKhoaBatDau))
            {
                dem++;
                s = s.Substring(s.IndexOf(tuKhoaBatDau) + tuKhoaBatDau.Length);
            }

            s = this.Text.Substring(0, this.SelectionStart);
            while (s.Contains(tuKhoaKetThuc))
            {
                dem--;
                s = s.Substring(s.IndexOf(tuKhoaKetThuc) + tuKhoaKetThuc.Length);
            }
            //MessageBox.Show(s);
            return dem;
        }
        #endregion Hàm       

        
        #endregion=============== HuuAn add===================

        
    }
        
}