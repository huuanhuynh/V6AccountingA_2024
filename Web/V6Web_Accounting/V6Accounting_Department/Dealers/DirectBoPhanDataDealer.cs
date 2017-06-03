using System;
using System.Collections.Generic;
using System.Linq;
using V6Soft.Accounting.Common.Dealers;
using V6Soft.Accounting.BoPhan.Farmers;
using V6Soft.Accounting.Department.Farmers;
using V6Soft.Common.Logging;
using V6Soft.Common.Utils;
using DTO = V6Soft.Models.Accounting.DTO;
using System.Web.Http.OData.Query;

namespace V6Soft.Accounting.BoPhan.Dealers
{
    /// <summary>
    ///     Provides BoPhanItem-related operations (BoPhan CRUD, BoPhan group CRUD) by
    ///     contacting directly with data layer.
    /// </summary>
    public class DirectBoPhanDataDealer : DataDealerBase, IBoPhanDataDealer
    {
        private readonly ILogger m_Logger;
        private readonly IBoPhanDataFarmer m_BoPhanFarmer;

        public DirectBoPhanDataDealer(ILogger logger, IBoPhanDataFarmer BoPhanFarmer)
            : base(BoPhanFarmer.AsQueryable())
        {
            Guard.ArgumentNotNull(logger, "logger");
            Guard.ArgumentNotNull(BoPhanFarmer, "BoPhanFarmer");

            m_Logger = logger;
            m_BoPhanFarmer = BoPhanFarmer;

        }

        public IQueryable<DTO.Department> AsQueryable()
        {
            return m_BoPhanFarmer.AsQueryable();
        }

        public void Save(IList<DynamicObject> models)
        {
            throw new NotImplementedException();
        }

        public IQueryable<DTO.Department> AsQueryable(ODataQueryOptions<DTO.Department> queryOptions)
        {
            return (IQueryable<DTO.Department>) queryOptions.ApplyTo(m_BoPhanFarmer.AsQueryable());
        }

        public DTO.Department GetBoPhan(Guid guid)
        {
            return m_BoPhanFarmer.AsQueryable().SingleOrDefault(re => re.UID.Equals(guid));
        }
    }
}