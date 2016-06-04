using SportLife.Bll.Contracts;
using SportLife.Bll.Converter;
using SportLife.Dal.Contracts;
using SportLife.Dal.DomainModels;
using SportLife.Models.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportLife.Bll.Bus
{
    public class UserBus : IUserBus
    {
        private IUserDao _iUserDao;

        public UserBus(IUserDao iUserDao)
        {
            _iUserDao = iUserDao;
        }

        public void addFirst()
        {
            var all = _iUserDao.GetAll().ToList();
            _iUserDao.addFirst();
        }


        public CustomResponseModel<UserViewModel> ValidateUser(UserViewModel user)
        {
            var response = new CustomResponseModel<UserViewModel>
            {
                Model = user,
                WithError = true,
                ViewMessage = "Account successfully created. Please check your mail and activate your account.",
                SCode = System.Net.HttpStatusCode.BadRequest
            };

            var emailvalidater = new EmailAddressAttribute();
            if (!emailvalidater.IsValid(user.Email))
            {
                response.ViewMessage = "Invalid email address";

                return response;
            }
            else
            {
                if (_iUserDao.FindBy(u => u.Email == user.Email).Any())
                {
                    response.ViewMessage = "Email in use";

                    return response;
                }
            }

            if (user.Password != user.PasswordConfirm)
            {
                response.ViewMessage = "Password don't match";

                return response;
            }

            response.WithError = false;
            response.SCode = System.Net.HttpStatusCode.OK;

            return response;
        }


        public CustomResponseModel<UserViewModel> RegisterUser(UserViewModel user)
        {
            var response = new CustomResponseModel<UserViewModel>
            {
                Model = user,
                WithError = true,
                ViewMessage = "Something went wrong. If it persists please contact the administrator"
            };

            if (user != null)
            {
                User userDb = UserConverter.FromUserViewModelToDbModel(user);
                userDb.AccountCreationDate = DateTime.Now;
                _iUserDao.Add(userDb);
                _iUserDao.SaveContext();
                response.Model.UserId = userDb.UserId;
                response.WithError = userDb.UserId <= 0;
                response.ViewMessage = "Account successfully created. Please check your mail and activate your account.";
                response.DebugMessage = "Login";
            }

            return response;
        }
    }
}
