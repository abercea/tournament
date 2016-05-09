using SportLife.Dal.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportLife.Dal.Dao
{
    public class UserDao : AbstractDao<User>, IUserDao
    {
        public UserDao(IObjectContext objectContext)
            : base(objectContext)
        {
        }
    }
}
