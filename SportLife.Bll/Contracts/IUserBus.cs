using SportLife.Dal.DomainModels;
using SportLife.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportLife.Bll.Contracts
{
    public interface IUserBus
    {
        void addFirst();

        CustomResponseModel<UserViewModel> ValidateUser(UserViewModel user);
        CustomResponseModel<UserViewModel> RegisterUser(UserViewModel user);
    }
}
