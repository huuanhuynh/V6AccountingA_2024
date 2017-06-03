using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using AutoMapper.QueryableExtensions;
using V6Soft.Accounting.Customer.Farmers;
using V6Soft.Accounting.Farmers.EnFw.Entities;
using V6Soft.Common.Utils.Linq;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using V6Soft.Accounting.Farmers.EnFw.AutoMapper;

using DTO = V6Soft.Models.Accounting.DTO;


namespace V6Soft.Accounting.Farmers.EnFw.MssqlDataFarmer
{
    /// <summary>
    ///     Implements <see cref="V6Soft.Services.Accounting.Interfaces.ICustomerDataFarmer"/>
    /// </summary>
    public class MssqlCustomerDataFarmer : EnFwDataFarmerBase<Alkh, DTO.Customer>, ICustomerDataFarmer
    {
        private readonly IModelMapper m_ModelMapper;

        public MssqlCustomerDataFarmer(IV6AccountingContext dbContext, IModelMapper modelMapper)
            : base(dbContext)
        {
            m_ModelMapper = modelMapper;
        }

        public PagedSearchResult<DTO.Customer> GetCustomers(SearchCriteria criteria)
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

        public DTO.Customer GetCustomerById(Guid guid)
        {
            var khachHangDb = m_Dbset.SingleOrDefault(x => x.UID == guid);
            return ToAppModel(khachHangDb);
        }
        
        public IQueryable<DTO.Customer> AsQueryable()
        {
            return m_Dbset.ProjectTo<DTO.Customer>();
        }

        public override ObjectQuery GetObjectQuery()
        {
            ObjectContext objectContext = ((IObjectContextAdapter)m_DbContext).ObjectContext;
            ObjectSet<Alkh> objectSet = objectContext.CreateObjectSet<Alkh>("AlkhSet");
            return (ObjectQuery<Alkh>)objectSet;
        }

        protected override DTO.Customer ToAppModel(Alkh dbModel)
        {
            return m_ModelMapper.Map<Alkh, DTO.Customer>(dbModel);
        }

        protected override Alkh ToEntityModel(DTO.Customer appModel)
        {
            return m_ModelMapper.Map<DTO.Customer, Alkh>(appModel);
        }
    }
}
