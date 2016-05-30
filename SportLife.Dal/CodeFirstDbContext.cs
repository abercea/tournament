using SportLife.Dal.Contracts;
using SportLife.Dal.DomainModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportLife.Dal
{
    public class CodeFirstDbContext : DbContext, IDbContext, IDisposable
    {
        public CodeFirstDbContext()
            : base("name=SchoolDBConnectionString")
        {
             Database.SetInitializer<CodeFirstDbContext>(new CreateDatabaseIfNotExists<CodeFirstDbContext>());

           // Database.SetInitializer<CodeFirstDbContext>(new DropCreateDatabaseIfModelChanges<CodeFirstDbContext>());
            //Database.SetInitializer<CodeFirstDbContext>(new DropCreateDatabaseAlways<CodeFirstDbContext>());
            //Database.SetInitializer<CodeFirstDbContext>(new SchoolDBInitializer());

            //Disable initializer for production
            //   Database.SetInitializer<CodeFirstDbContext>(null);
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Standard> Standards { get; set; }

        public DbSet<T> Get<T>() where T : class
        {
            return this.Set<T>();
        }
    }
}
