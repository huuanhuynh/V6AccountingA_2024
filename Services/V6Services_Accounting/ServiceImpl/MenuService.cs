using System.Collections.Generic;
using System.ServiceModel;

using V6Soft.Models.Core;
using V6Soft.Services.Accounting.Interfaces;
using V6Soft.Services.Wcf.Common.Attributes;


namespace V6Soft.Services.Accounting.ServiceImpl
{
    /// <summary>
    ///     Implements <see cref="IMenuService"/>
    /// </summary>
    [ServiceBehavior(AddressFilterMode = AddressFilterMode.Any)]
    [ServiceLogging]
    public class MenuService : IMenuService
    {
        private IMenuDataBaker m_MenuBaker;

        public MenuService(IMenuDataBaker menuBaker)
        {
            m_MenuBaker = menuBaker;
        }

        /// <summary>
        ///     See <see cref="IMenuService.GetMenuItems"/>
        /// </summary>
        public IList<MenuItem> GetMenuTree(byte level)
        {
            return m_MenuBaker.GetMenuTree(level);
        }
        
        /// <summary>
        ///     See <see cref="IMenuService.GetChildren"/>
        /// </summary>
        public IList<MenuItem> GetChildren(int oid)
        {
            return m_MenuBaker.GetChildren(oid);
        }
    }
}
