using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;

using V6Soft.Models.Core;
using V6Soft.Models.Core.ViewModels;


namespace V6Soft.Accounting.Common.Farmers
{
    /// <summary>
    /// Provides API for data farmer layer.
    /// </summary>
    /// <typeparam name="TV6Model">Type of application model.</typeparam>
    public interface IDataFarmerBase<TV6Model> 
        where TV6Model : V6Model
    {
        /// <summary>
        ///     Fetches add records in corresponding table.
        /// </summary>
        IList<TV6Model> GetAll();

        /// <summary>
        ///     Counts all existing records in corresponding table.
        /// </summary>
        long CountAll();

        /// <summary>
        ///     Fetches records matching given criteria.
        /// </summary>
        PagedSearchResult<TV6Model> FindByCriteria(SearchCriteria criteria);

        ObjectQuery GetObjectQuery();

        /// <summary>
        /// 
        /// </summary>
        //TV6Model FindByGuid(string id);

        /// <summary>
        /// 
        /// </summary>
        //IQueryable<TV6Model> FindBy(Func<TV6Model, bool> predicate);

        /// <summary>
        /// 
        /// </summary>
        //IQueryable<TV6Model> FindByExp(Expression<Func<TV6Model, bool>> predicate);

        ///// <summary>
        ///// 
        ///// </summary>
        //IQueryable<TV6Model> FindBy(Func<TV6Model, bool> predicate, string lazyIncludeString);

        /// <summary>
        ///     Persists this model's values to database.
        ///     <para/>Returns this model with some field filled with database-generated values (eg. GUID).
        /// </summary>
        TV6Model Add(TV6Model entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        bool Delete(string key);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        bool Edit(TV6Model entity);
        
        /// <summary>
        /// 
        /// </summary>
        int ExecuteStoredProcedure(string commandText, params object[] parameters);
        /// <summary>
        /// 
        /// </summary>
        IEnumerable<TS> Execute<TS>(string commandText, params object[] parameters);
        /// <summary>
        /// 
        /// </summary>
        IEnumerable Execute(Type type, string commandText, params object[] parameters);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        DataSet ExecuteSqlStoredProcedure(string sqlCommand, string connectionString, List<SqlParameter> parameters);

        // TODO: All data farmers should inherit this method
        /// <summary>
        /// 
        /// </summary>
        //IQueryable<TV6Model> AsQueryable();
    }
}
