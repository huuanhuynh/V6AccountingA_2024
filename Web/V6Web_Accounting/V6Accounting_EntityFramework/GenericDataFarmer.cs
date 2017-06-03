using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using V6Accounting_EntityFramework.Entities;
using V6Soft.Common.Utils;

namespace V6Accounting_EntityFramework
{
    public abstract class GenericDataFarmer<T> : IGenericRepository<T> where T : class
    {
        protected DbContext DbContext;
        protected readonly IDbSet<T> Dbset;
        private int commandTimeOut;

        protected static SequentialGuid s_UID = new SequentialGuid();

        protected Guid NextUID
        {
            get
            {
                s_UID++;
                return s_UID.Value;
            }
        }

        protected GenericDataFarmer(V6AccountingContext context)
        {
            DbContext = context;
            Dbset = context.Set<T>();
        }

        public int CommandTimeOut
        {
            get
            {
                if (commandTimeOut > 0)
                    return commandTimeOut;
                int.TryParse(ConfigurationManager.AppSettings["CommandTimeOut"], out commandTimeOut);
                commandTimeOut = commandTimeOut > 0 ? commandTimeOut : 300;
                return commandTimeOut;
            }
            set { commandTimeOut = value; }
        }

        public virtual IEnumerable<T> GetAll()
        {
            return Dbset.AsEnumerable();
        }

        public IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate)
        {
            IEnumerable<T> query = Dbset.Where(predicate).AsEnumerable();
            return query;
        }

        public virtual T Add(T entity)
        {
            return Dbset.Add(entity);
        }

        public virtual T Delete(T entity)
        {
            return Dbset.Remove(entity);
        }

        public virtual void Edit(T entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Save()
        {
            DbContext.SaveChanges();
        }

        public int ExecuteStoredProcedure(string commandText, params object[] parameters)
        {
            return DbContext.Database.ExecuteSqlCommand(commandText, CommandTimeOut, parameters);
        }

        public IEnumerable<TS> Execute<TS>(string commandText, params object[] parameters)
        {
            return DbContext.Database.SqlQuery<TS>(commandText, CommandTimeOut, parameters);
        }

        public IEnumerable Execute(Type type, string commandText, params object[] parameters)
        {
            return DbContext.Database.SqlQuery(type, commandText, CommandTimeOut, parameters);
        }

        public int Count()
        {
            return Dbset.Count();
        }
    }
}
