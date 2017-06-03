using System.Collections.Generic;


namespace V6Soft.Models.Core.ViewModels
{
    /// <summary>
    ///     Represents a menu item, which can have chilren.
    /// </summary>
    public class MenuItem
    {
        /// <summary>
        ///     Gets or sets OID.
        /// </summary>
        public int OID { get; set; }

        /// <summary>
        ///     Gets or sets the label from which
        ///     a localized string will be lookup up.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        ///     Gets or sets the class name to show glyphicon.
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        ///     Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     Gets or sets the route to execute when
        ///     this menu item is activated.
        /// </summary>
        public string Route { get; set; }

        /// <summary>
        ///     Gets or sets the position of this menu item
        ///     compared to its siblings.
        /// </summary>
        public byte Position { get; set; }

        /// <summary>
        ///     Gets or sets OID of the upper menu item.
        /// </summary>
        public int? ParentOID { get; set; }

        /// <summary>
        ///     Gets or sets the upper menu item which contains this
        ///     menu item. If null, this is root menu item.
        /// </summary>
        public MenuItem Parent { get; set; }

        /// <summary>
        ///     Gets or sets list of menu items below this item.
        /// </summary>
        public IList<MenuItem> Descendants { get; set; }

        /// <summary>
        ///     Gets or sets the value indidating if this item has children.
        /// </summary>
        public bool HasDescendants { get; set; }

        /// <summary>
        ///     Gets or sets level of current item.
        /// </summary>
        public byte Level { get; set; }

        /// <summary>
        ///     Gets or sets additional data in JSON format.
        /// </summary>
        public object Metadata { get; set; }

        public override string ToString()
        {
            return string.Format("{0}-{1}", OID, Label);
        }
    }
}
