using System;
using System.Windows.Forms;
using System.Collections;

namespace V6Controls
{
    public partial class SortListView : ListView
    {
        private ListViewColumnSorter lvwColumnSorter;

        public SortListView()
        {
            InitializeComponent();
            this.lvwColumnSorter = new ListViewColumnSorter();
            this.ListViewItemSorter = this.lvwColumnSorter;
        }

        private void this_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            if (e.Column == 0)
            {
                lvwColumnSorter.CompareMode = 1;
            }
            else
            {
                lvwColumnSorter.CompareMode = 0;
            }
            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == lvwColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (lvwColumnSorter.Order == SortOrder.Ascending)
                {
                    lvwColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    lvwColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                lvwColumnSorter.SortColumn = e.Column;
                lvwColumnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            this.Sort();
        }

        ListViewHitTestInfo listViewHitInfo = null;
        private void this_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
                listViewHitInfo = this.HitTest(e.Location);
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listViewHitInfo != null)
                Clipboard.SetText(listViewHitInfo.SubItem.Text);
            else
                Clipboard.SetText(" ");
        }

        private void SortListView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            if (e.ColumnIndex == ((ListViewColumnSorter)this.ListViewItemSorter).SortColumn)
            {

            }
        }
    }


    /// <summary>
    /// This class is an implementation of the 'IComparer' interface.
    /// </summary>
    public class ListViewColumnSorter : IComparer
    {
        /// <summary>
        /// Specifies the column to be sorted
        /// </summary>
        private int columnToSort;
        /// <summary>
        /// Specifies the order in which to sort (i.e. 'Ascending').
        /// </summary>
        private SortOrder orderOfSort;
        /// <summary>
        /// Case insensitive comparer object
        /// </summary>
        private CaseInsensitiveComparer objectCompare;

        /// <summary>
        /// Class constructor.  Initializes various elements
        /// </summary>
        public ListViewColumnSorter()
        {
            // Initialize the column to '0'
            columnToSort = 0;

            // Initialize the sort order to 'none'
            orderOfSort = SortOrder.None;

            // Initialize the CaseInsensitiveComparer object
            objectCompare = new CaseInsensitiveComparer();
        }

        /// <summary>
        /// 1 is number, another is text
        /// </summary>
        public int CompareMode { get; set; }

        /// <summary>
        /// This method is inherited from the IComparer interface.  It compares the two objects passed using a case insensitive comparison.
        /// </summary>
        /// <param name="x">First object to be compared</param>
        /// <param name="y">Second object to be compared</param>
        /// <returns>The result of the comparison. "0" if equal, negative if 'x' is less than 'y' and positive if 'x' is greater than 'y'</returns>
        public int Compare(object x, object y)
        {
            int compareResult = 0;
            ListViewItem listviewX, listviewY;
            string valueX = "", valueY = "";
            // Cast the objects to be compared to ListViewItem objects
            listviewX = (ListViewItem)x;
            listviewY = (ListViewItem)y;

            if (listviewX.SubItems.Count > columnToSort)
            {
                valueX = listviewX.SubItems[columnToSort].Text;
            }
            if (listviewY.SubItems.Count > columnToSort)
            {
                valueY = listviewY.SubItems[columnToSort].Text;
            }

            if (CompareMode == 1)
            {
                try
                {
                    int a = 0, b = 0;
                    a = int.Parse(valueX);
                    b = int.Parse(valueY);
                    compareResult = a - b;
                }
                catch
                {
                    compareResult = objectCompare.Compare(valueX, valueY);
                }
            }
            else
            {
                // Compare the two items
                compareResult = objectCompare.Compare(valueX, valueY);
            }

            // Calculate correct return value based on object comparison
            if (orderOfSort == SortOrder.Ascending)
            {
                // Ascending sort is selected, return normal result of compare operation
                return compareResult;
            }
            else if (orderOfSort == SortOrder.Descending)
            {
                // Descending sort is selected, return negative result of compare operation
                return (-compareResult);
            }
            else
            {
                // Return '0' to indicate they are equal
                return 0;
            }
        }

        /// <summary>
        /// Gets or sets the number of the column to which to apply the sorting operation (Defaults to '0').
        /// </summary>
        public int SortColumn
        {
            set
            {
                columnToSort = value;
            }
            get
            {
                return columnToSort;
            }
        }

        /// <summary>
        /// Gets or sets the order of sorting to apply (for example, 'Ascending' or 'Descending').
        /// </summary>
        public SortOrder Order
        {
            set
            {
                orderOfSort = value;
            }
            get
            {
                return orderOfSort;
            }
        }

    }
}
