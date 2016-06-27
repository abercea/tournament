using System;
using System.Collections.Generic;
using System.Linq;

namespace SportLife.Dal.Contracts
{
    public interface IDao<T>
    {
        void Delete(T entity);

        void Add(T entity);

        void Attach(T entity);

        IEnumerable<T> GetAll();

        void SaveContext();

        IQueryable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate);
    }
}
