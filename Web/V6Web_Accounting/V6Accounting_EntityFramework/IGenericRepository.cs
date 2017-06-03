using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace V6Accounting_EntityFramework
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);
        T Add(T entity);
        T Delete(T entity);
        int Count();
        void Edit(T entity);
        void Save();
        int ExecuteStoredProcedure(string commandText, params object[] parameters);
        IEnumerable<TS> Execute<TS>(string commandText, params object[] parameters);
        IEnumerable Execute(Type type, string commandText, params object[] parameters);
    }
}
