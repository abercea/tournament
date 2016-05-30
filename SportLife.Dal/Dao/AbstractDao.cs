using SportLife.Dal.Contracts;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Linq;

namespace SportLife.Dal.Dao
{
    public class AbstractDao<T> : IDao<T> where T :class
    {
        protected IDbContext _objectContext;
        protected DbSet<T> _objectSet;

        public AbstractDao(IDbContext objectContext)
        {
            _objectContext = objectContext;
            _objectSet = objectContext.Get<T>();
        }

        /// <summary>
        /// Delete enetity from context
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(T entity)
        {
            _objectSet.Remove(entity);
        }

        /// <summary>
        /// Add newly created entity to current context 
        /// </summary>
        /// <param name="entity"></param>
        public void Add(T entity)
        {
            _objectSet.Add(entity);
        }

        /// <summary>
        /// Attach an entity to the context
        /// </summary>
        /// <param name="entity"></param>
        public void Attach(T entity)
        {
            _objectSet.Attach(entity);
        }

        public virtual IEnumerable<T> GetAll()
        {
            return _objectSet;
        }

        public void SaveContext()
        {
            _objectContext.SaveChanges();
        }
    }
}
