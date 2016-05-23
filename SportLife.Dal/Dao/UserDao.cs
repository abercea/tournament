using SportLife.Dal.Contracts;
using SportLife.Dal.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportLife.Dal.Dao
{
    public class UserDao : AbstractDao<Student>, IUserDao
    {
        public UserDao(IDbContext objectContext)
            : base(objectContext)
        {
        }

        public void addFirst()
        {

            this.Add(new Student() { StudentName = "New Student AbstractDao", DateOfBirth = DateTime.Now });
            SaveContext();
            //using (var ctx = new CodeFirstDbContext())
            //{
            //    Student stud = new Student() { StudentName = "New Student", DateOfBirth = DateTime.Now };

            //    ctx.Students.Add(stud);
            //    ctx.SaveChanges();
            //}
        }

    }
}
