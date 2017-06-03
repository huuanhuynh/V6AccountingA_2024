using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

using Newtonsoft.Json;

using V6Soft.Interfaces.Accounting.Customer.DataDealers;
using V6Soft.Models.Core;

using ActionNames = V6Soft.Web.Accounting.Constants.Names.Actions;


namespace V6Soft.Web.Accounting.Controllers
{
    public class MenuApiController : ApiController
    {
        private readonly IMenuDataDealer m_MenuDealer;

        /// <summary>
        ///     Initializes a new instance of MenuApiController with
        ///     specified menu data dealer.
        /// </summary>
        public MenuApiController(IMenuDataDealer menuDealer)
        {
            m_MenuDealer = menuDealer;
        }
        
        /// <summary>
        ///     GET /api/Menu/GetMenuTree?level=...
        ///     
        ///     <para/>Gets menu tree including menu items and their chilren with
        ///     limited number of levels. Passing 0 to get all levels.
        ///     <para/>Returns a well-fed or null collection. Never returns null.
        /// </summary>
        [ActionName(ActionNames.GetMenuTree)]
        [AcceptVerbs("Get")]
        public async Task<IEnumerable<MenuItem>> GetMenuTree(byte level)
        {
            IList<MenuItem> menuItems = await m_MenuDealer.GetMenuTree(level);
            ParseJsonMetadata(menuItems);
            return menuItems ?? Enumerable.Empty<MenuItem>();
        }

        /// <summary>
        ///     GET /api/Menu/GetChildren?menuId
        ///     
        ///     <para/>Gets subitems of the menu item with specified id.
        ///     <para/>Returns a well-fed or null collection. Never returns null.
        /// </summary>
        [ActionName(ActionNames.GetChildren)]
        [AcceptVerbs("Get")]
        public async Task<IEnumerable<MenuItem>> GetChildren(ushort menuId)
        {
            IList<MenuItem> menuItems = await m_MenuDealer.GetChildren(menuId);
            ParseJsonMetadata(menuItems);
            return menuItems ?? Enumerable.Empty<MenuItem>();
        }

        private void ParseJsonMetadata(IEnumerable<MenuItem> items)
        {
            if (items == null) { return; }

            foreach (var item in items)
            {
                if (item.Metadata != null)
                {
                    item.Metadata = JsonConvert.DeserializeObject((string)item.Metadata);
                }

                if (item.Descendants != null)
                {
                    ParseJsonMetadata(item.Descendants);
                }
            }
        }
    }
}