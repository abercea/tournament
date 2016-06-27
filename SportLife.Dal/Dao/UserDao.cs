using SportLife.Dal.Contracts;
using SportLife.Dal.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportLife.Dal.Dao
{
    public class UserDao : AbstractDao<User>, IUserDao
    {
        public UserDao(IDbContext objectContext)
            : base(objectContext)
        {
        }

        public void addFirst()
        {

            this.Add(new User() { Lastname = "New Student AbstractDao",  AccountCreationDate = DateTime.Now });
            SaveContext();
        }

    }
}
