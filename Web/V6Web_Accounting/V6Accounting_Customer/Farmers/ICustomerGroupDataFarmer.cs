using System;
using System.Collections.Generic;
using V6Soft.Accounting.Common.Farmers;
using V6Soft.Models.Accounting.DTO;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Soft.Accounting.Customer.Farmers
{
    public interface ICustomerGroupDataFarmer
    {
        PagedSearchResult<Models.Accounting.DTO.NhomKhachHang> GetNhomKhachHangs(SearchCriteria criteria);

        /// <summary>
        ///     See <see cref="IDataFarmerBase{TV6Model}.GetAll()"/>
        /// </summary>
        IList<NhomKhachHang> GetAll();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        NhomKhachHang FindByGuid(Guid guid);

        /// <summary>
        ///     See <see cref="IDataFarmerBase{TV6Model}.FindByCriteria"/>
        /// </summary>
        PagedSearchResult<NhomKhachHang> FindByCriteria(SearchCriteria criteria);

        /// <summary>
        ///     See <see cref="IDataFarmerBase{TV6Model}.Add"/>
        /// </summary>
        NhomKhachHang Add(NhomKhachHang newModel);

        /// <summary>
        ///     See <see cref="IDataFarmerBase{TV6Model}.Delete"/>
        /// </summary>
        bool Delete(NhomKhachHang deletedModel);

        /// <summary>
        ///     See <see cref="IDataFarmerBase{TV6Model}.Edit"/>
        /// </summary>
        bool Edit(NhomKhachHang editedModel);

        /// <summary>
        ///     See <see cref="IDataFarmerBase{TV6Model}.Save()"/>
        /// </summary>
        bool Save();

        /// <summary>
        ///     See <see cref="IDataFarmerBase{TV6Model}.CountAll()"/>
        /// </summary>
        long CountAll();
    }
}