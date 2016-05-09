using SportLife.Bll.Contracts;
using SportLife.Dal.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportLife.Bll.Bus
{
    public class UserBus :IUserBus
    {
        private IUserDao _iUserDao;

        public UserBus(IUserDao iUserDao)
        {
            _iUserDao = iUserDao;
        }
    }
}
