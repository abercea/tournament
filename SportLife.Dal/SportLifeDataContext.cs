using SportLife.Dal.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportLife.Dal
{
    public class SportLifeDataContext : ObjectContext, IObjectContext, IDisposable
    {
        public SportLifeDataContext()
            : base("name= LocalDBEntities", "LocalDBEntities")
            {
                this.ContextOptions.LazyLoadingEnabled = true;
            }
    }
}
