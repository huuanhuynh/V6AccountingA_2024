using System.Collections.Generic;
using V6Accounting_EntityFramework;
using V6Accounting_EntityFramework.Entities;
using V6Accouting_Temp1.Farmers;
using V6Soft.Models.Accounting;
using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;

namespace V6Accouting_Temp1.Dealers
{
    public class Temp1DataDealer : ITemp1DataDealer
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ITemp1DataFarmer warehouseDataFarmer;

        //public Temp1DataDealer(IUnitOfWork unitOfWork, ITemp1DataFarmer warehouseDataFarmer)
        //{
        //    this.unitOfWork = unitOfWork;
        //    this.warehouseDataFarmer = warehouseDataFarmer;
        //}

        public Temp1DataDealer()
        {
            var context = new V6AccountingContext();
            unitOfWork = new UnitOfWork(context);
            warehouseDataFarmer = new Temp1DataFarmer(context);
        }

        public PagedSearchResult<Temp1> GetTemp1s(SearchCriteria criteria)
        {
            return warehouseDataFarmer.SearchTemp1s(criteria);
        }
    }
}
