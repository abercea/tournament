using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;

namespace SportLife.Dal.Contracts
{
    public interface IObjectContext : IDisposable
    {
        ObjectSet<T> CreateObjectSet<T>() where T : class;
        ObjectResult<T> ExecuteFunction<T>(string functionName, params ObjectParameter[] parameters);
        ObjectQuery<T> CreateQuery<T>(string queryString, params ObjectParameter[] parameters);
        ObjectResult<TElement> ExecuteStoreQuery<TElement>(string commandText, params object[] parameters);
        int SaveChanges();
    }

    public interface IDbContext : IDisposable
    {
      //  DbSet<T> Set<T>() where T : class;
        DbSet<T> Get<T>() where T : class;
        int SaveChanges();
    }
}
