using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using V6Soft.Accounting.Farmers.EnFw.AutoMapper;
using V6Soft.Accounting.Farmers.EnFw.Entities;
using V6Soft.Accounting.Receipt.Farmers;

using DTO = V6Soft.Models.Accounting.DTO;


namespace V6Soft.Accounting.Farmers.EnFw.MssqlDataFarmer
{
    public class MssqlReceiptDataFarmer : EnFwDataFarmerBase<AM81, DTO.Receipt>, IReceiptDataFarmer
    {
        private IModelMapper m_ModelMapper;

        public MssqlReceiptDataFarmer(IV6AccountingContext dbContext, IModelMapper modelMapper)
            : base(dbContext)
        {
            m_ModelMapper = modelMapper;
        }

        public new IList<DTO.Receipt> GetAll()
        {
            throw new NotImplementedException();
        }

        public new Models.Core.ViewModels.PagedSearchResult<DTO.Receipt> FindByCriteria(Models.Core.SearchCriteria criteria)
        {
            throw new NotImplementedException();
        }

        public DTO.Receipt Add(DTO.Receipt entity)
        {
            throw new NotImplementedException();
        }

        public bool Edit(DTO.Receipt entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<DTO.Receipt> AsQueryable()
        {
            return m_Dbset.ProjectTo<DTO.Receipt>();
        }

        protected override DTO.Receipt ToAppModel(AM81 model)
        {
            throw new NotImplementedException();
        }

        protected override AM81 ToEntityModel(DTO.Receipt model)
        {
            throw new NotImplementedException();
        }
    }
}
