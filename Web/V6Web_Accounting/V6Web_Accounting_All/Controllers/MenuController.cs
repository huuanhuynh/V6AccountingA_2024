using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

using Newtonsoft.Json;

using V6Soft.Accounting.Misc.Dealers;
using V6Soft.Models.Core.ViewModels;


namespace V6Soft.Web.Accounting.Controllers
{
    public class MenuController : Controller
    {
        private readonly IMenuDataDealer m_MenuDealer;

        public MenuController(IMenuDataDealer menuDealer)
        {
            m_MenuDealer = menuDealer;
        }
        
        /// <summary>
        ///     /Menu/ViewSubItems
        /// </summary>
        /// <param name="id">Id of the menu to get sub-items</param>
        public async Task<ActionResult> ViewSubItems(int id)
        {
            // TODO: If `menuId` < 0, redirects to error page.
            if (id < 0) { return new EmptyResult(); }

            IList<MenuItem> menuItems = await m_MenuDealer.GetChildren(id);
            ParseJsonMetadata(menuItems);

            return View(menuItems ?? new List<MenuItem>());
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