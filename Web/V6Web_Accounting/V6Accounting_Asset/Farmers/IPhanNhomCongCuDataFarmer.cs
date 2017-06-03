﻿using System;
using V6Soft.Accounting.Common.Farmers;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

using AccModels = V6Soft.Models.Accounting.DTO;


namespace V6Soft.Accounting.Asset.Farmers
{
    /// <summary>
    ///     Provides API for customer data farmer.
    /// </summary>
    public interface IPhanNhomCongCuDataFarmer : IDataFarmerBase<AccModels.PhanNhomCongCu>
    {
        /// <summary>
        ///     Gets list of customers satisfying given conditions.
        ///     <para />Returns null if there is no results.
        /// </summary>
        PagedSearchResult<AccModels.PhanNhomCongCu> GetPhanNhomCongCus(SearchCriteria criteria);
        AccModels.PhanNhomCongCu GetPhanNhomCongCuById(Guid guid);
    }
}
