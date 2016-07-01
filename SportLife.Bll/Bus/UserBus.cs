using SportLife.Bll.Contracts;
using SportLife.Bll.Converter;
using SportLife.Dal.Contracts;
using SportLife.Dal.DomainModels;
using SportLife.Models.Models;
using SportLife.Utils;
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


        public CustomResponseModel<UserViewModel> RegisterUser(UserViewModel user, string validationUrl)
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
                userDb.Role = (int)AccessRolesEnum.Admin; //admin
                userDb.AccountActive = false;
                _iUserDao.Add(userDb);
                _iUserDao.SaveContext();
                response.Model.UserId = userDb.UserId;
                response.WithError = userDb.UserId <= 0;
                response.ViewMessage = "Account successfully created. Please check your mail and activate your account.";
                response.DebugMessage = "Login";
            }

            try
            {
                MailService mailService = new MailService();
                mailService.SendMail(user.Email, validationUrl + "?accessToken=" + EncryptionHelper.Encrypt(user.Email));
            }
            catch (Exception ex)
            {

            }

            return response;
        }


        public UserViewModel CheckCredentials(UserViewModel user)
        {
            if (user != null && !string.IsNullOrEmpty(user.Password))
            {
                string passwordEncrypt = EncryptionHelper.Encrypt(user.Password);
                var dbUser = _iUserDao.FindBy(u => u.Email == user.Email && u.Password == passwordEncrypt).FirstOrDefault();
                if (dbUser != null)
                {
                    var userModel = UserConverter.FromUserDbModelToViewModel(dbUser);

                    return userModel;
                }
            }

            return new UserViewModel { Role = AccessRolesEnum.NoAcces };
        }


        public bool ActivataAccount(string accessToken)
        {
            string email = EncryptionHelper.Decrypt(accessToken);
            var userDb = _iUserDao.FindBy(u => u.Email == email && !u.AccountActive).FirstOrDefault();

            if (userDb == null)
            {
                return false;
            }

            userDb.AccountActive = true;
            _iUserDao.SaveContext();

            return true;
        }


        public UserViewModel GetByEmail(string userEmail)
        {
            var userdB = _iUserDao.FindBy(u => u.Email == userEmail).FirstOrDefault();

            return UserConverter.FromUserDbModelToViewModel(userdB);
        }

        public bool SetProfPicture(int userId, int docId)
        {
            var user = _iUserDao.FindBy(u => u.UserId == userId).FirstOrDefault();
            if (user != null)
            {
                try
                {
                    user.ProfilePhto = docId;
                    _iUserDao.SaveContext();
                    return true;
                }
                catch (Exception e) { }
            }
            return false;
        }
    }
}
