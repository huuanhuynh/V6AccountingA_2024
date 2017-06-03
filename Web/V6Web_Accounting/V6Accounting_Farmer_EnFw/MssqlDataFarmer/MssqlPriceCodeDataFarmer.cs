using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using AutoMapper.QueryableExtensions;
using V6Soft.Accounting.PriceCode.Farmers;
using V6Soft.Accounting.Farmers.EnFw.Entities;
using V6Soft.Common.Utils.Linq;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using V6Soft.Accounting.Farmers.EnFw.AutoMapper;

using DTO = V6Soft.Models.Accounting.DTO;


namespace V6Soft.Accounting.Farmers.EnFw.MssqlDataFarmer
{
    /// <summary>
    ///     Implements <see cref="V6Soft.Services.Accounting.Interfaces.IPriceCodeDataFarmer"/>
    /// </summary>
    public class MssqlPriceCodeDataFarmer : EnFwDataFarmerBase<Almagia, DTO.PriceCode>, IPriceCodeDataFarmer
    {
        private readonly IModelMapper m_ModelMapper;

        public MssqlPriceCodeDataFarmer(IV6AccountingContext dbContext, IModelMapper modelMapper)
            : base(dbContext)
        {
            m_ModelMapper = modelMapper;
        }

        public PagedSearchResult<DTO.PriceCode> GetPriceCodes(SearchCriteria criteria)
        {
            // TODO: Should test with empty table to ensure this method returns NULL.
            if (null == criteria.Sort)
            {
                criteria.Sort = new List<SortDescriptor>()
                {
                    new SortDescriptor("MaKhachHang")
                };
            }
            return FindByCriteria(criteria);
        }

        public DTO.PriceCode GetPriceCodeById(Guid guid)
        {
            var khachHangDb = m_Dbset.SingleOrDefault(x => x.UID == guid);
            return ToAppModel(khachHangDb);
        }

        public IQueryable<DTO.PriceCode> AsQueryable()
        {
            return m_Dbset.ProjectTo<DTO.PriceCode>();
        }

        public override ObjectQuery GetObjectQuery()
        {
            ObjectContext objectContext = ((IObjectContextAdapter)m_DbContext).ObjectContext;
            ObjectSet<Almagia> objectSet = objectContext.CreateObjectSet<Almagia>("DMAlmagia");
            return (ObjectQuery<Almagia>)objectSet;
        }

        protected override DTO.PriceCode ToAppModel(Almagia dbModel)
        {
            return m_ModelMapper.Map<Almagia, DTO.PriceCode>(dbModel);
        }

        protected override Almagia ToEntityModel(DTO.PriceCode appModel)
        {
            return m_ModelMapper.Map<DTO.PriceCode, Almagia>(appModel);
        }
    }
}
