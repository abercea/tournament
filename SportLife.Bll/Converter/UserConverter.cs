using SportLife.Dal.DomainModels;
using SportLife.Models.Models;
using SportLife.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportLife.Bll.Converter
{
    public static class UserConverter
    {
        public static User FromUserViewModelToDbModel(UserViewModel model)
        {
            User user = null;

            if (model != null)
            {
                user = new User
                    {
                        Lastname = model.Lastname,
                        Firstname = model.Firstname,
                        Email = model.Email,
                        Description = model.Description,
                        AccountCreationDate = model.AccountCreationDate,
                        SportId = model.SportId,
                        UserId = model.UserId
                    };
                user.Password = string.IsNullOrEmpty(model.Password) ? string.Empty : EncryptionHelper.Encrypt(model.Password);
            }

            return user;
        }
    }
}
