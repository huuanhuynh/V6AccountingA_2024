using System;
using System.Collections.Generic;
using System.Linq;
using V6Soft.Accounting.Common.Dealers;
using V6Soft.Accounting.System.Extensions;
using V6Soft.Accounting.System.Farmers;
using V6Soft.Common.Logging;
using V6Soft.Common.Utils;
using V6Soft.Models.Accounting.DTO;
using V6Soft.Models.Accounting.ViewModels.V6Option;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Soft.Accounting.System.Dealers
{
    /// <summary>
    ///     Provides V6OptionItem-related operations (v6option CRUD, v6option group CRUD) by
    ///     contacting directly with data layer.
    /// </summary>
    public class DirectV6OptionDataDealer : DataDealerBase, IV6OptionDataDealer
    {
        private readonly ILogger m_Logger;
        private readonly IV6OptionDataFarmer m_V6OptionFarmer;

        public DirectV6OptionDataDealer(ILogger logger, IV6OptionDataFarmer v6optionFarmer)
            : base(v6optionFarmer.AsQueryable())
        {
            Guard.ArgumentNotNull(logger, "logger");
            Guard.ArgumentNotNull(v6optionFarmer, "v6optionFarmer");

            m_Logger = logger;
            m_V6OptionFarmer = v6optionFarmer;

        }

        /// <summary>
        ///     See <see cref="IV6OptionDataDealer.GetV6Options()"/>
        /// </summary>
        public PagedSearchResult<V6OptionListItem> GetV6Options(SearchCriteria criteria)
        {
            PagedSearchResult<V6OptionListItem> allV6Options = m_V6OptionFarmer.GetV6Options(criteria).ToV6OptionViewModel();

            allV6Options.Data = allV6Options.Data
                .Select(item =>
                {
                    item.Name = VnCodec.TCVNtoUNICODE(item.Name);
                    return item;
                })
                .ToList();
            return allV6Options;
        }
        /// <summary>
        ///     See <see cref="IV6OptionDataDealer.AddV6Option()"/>
        /// </summary>
        public bool AddV6Option(Models.Accounting.DTO.V6Option v6Option)
        {
            v6Option.CreatedDate = DateTime.Now;
            v6Option.ModifiedDate = DateTime.Now;
            v6Option.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            v6Option.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            var result = m_V6OptionFarmer.Add(v6Option);
            return result != null;
        }
        /// <summary>
        /// 
        /// </summary>
        public bool DeleteV6Option(string key)
        {
            return m_V6OptionFarmer.Delete(key);
        }
        /// <summary>
        /// 
        /// </summary>
        public bool UpdateV6Option(V6Option v6Option)
        {
            v6Option.CreatedDate = DateTime.Now;
            v6Option.ModifiedDate = DateTime.Now;
            v6Option.CreatedTime = DateTime.Now.ToString("hh:mm:ss");
            v6Option.ModifiedTime = DateTime.Now.ToString("hh:mm:ss");
            return m_V6OptionFarmer.Edit(v6Option);
        }

        /// <summary>
        ///     See <see cref="IODataFriendly.AsQueryable"/>
        /// </summary>
        public IQueryable<V6Option> AsQueryable()
        {
            return m_QueryProvider.CreateQuery<V6Option>();
        }

        public void Save(IList<DynamicObject> models)
        {

        }

    }
}
