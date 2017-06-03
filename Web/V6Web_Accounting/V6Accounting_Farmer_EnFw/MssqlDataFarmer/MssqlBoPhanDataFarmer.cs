using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using AutoMapper.QueryableExtensions;
using V6Soft.Accounting.BoPhan.Farmers;
using V6Soft.Accounting.Department.Farmers;
using V6Soft.Accounting.Farmers.EnFw.Entities;
using V6Soft.Common.Utils.Linq;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;
using V6Soft.Accounting.Farmers.EnFw.AutoMapper;

using DTO = V6Soft.Models.Accounting.DTO;


namespace V6Soft.Accounting.Farmers.EnFw.MssqlDataFarmer
{
    /// <summary>
    ///     Implements <see cref="V6Soft.Services.Accounting.Interfaces.IBoPhanDataFarmer"/>
    /// </summary>
    public class MssqlBoPhanDataFarmer : EnFwDataFarmerBase<Albp, DTO.Department>, IBoPhanDataFarmer
    {
        private readonly IModelMapper m_ModelMapper;

        public MssqlBoPhanDataFarmer(IV6AccountingContext dbContext, IModelMapper modelMapper)
            : base(dbContext)
        {
            m_ModelMapper = modelMapper;
        }

        public PagedSearchResult<DTO.Department> GetBoPhans(SearchCriteria criteria)
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

        public DTO.Department GetBoPhanById(Guid guid)
        {
            var khachHangDb = m_Dbset.SingleOrDefault(x => x.UID == guid);
            return ToAppModel(khachHangDb);
        }

        public IQueryable<DTO.Department> AsQueryable()
        {
            return m_Dbset.ProjectTo<DTO.Department>();
        }

        public override ObjectQuery GetObjectQuery()
        {
            ObjectContext objectContext = ((IObjectContextAdapter)m_DbContext).ObjectContext;
            ObjectSet<Albp> objectSet = objectContext.CreateObjectSet<Albp>("AlbpSet");
            return (ObjectQuery<Albp>)objectSet;
        }

        protected override DTO.Department ToAppModel(Albp dbModel)
        {
            return m_ModelMapper.Map<Albp, DTO.Department>(dbModel);
        }

        protected override Albp ToEntityModel(DTO.Department appModel)
        {
            return m_ModelMapper.Map<DTO.Department, Albp>(appModel);
        }
    }
}
