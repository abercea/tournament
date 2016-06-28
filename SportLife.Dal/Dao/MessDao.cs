using SportLife.Dal.Contracts;
using SportLife.Dal.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportLife.Dal.Dao
{
    public class MessDao : AbstractDao<ChatMessage>, IMessDao
    {
        public MessDao(IDbContext objectContext)
            : base(objectContext)
        {
        }

    }
}
