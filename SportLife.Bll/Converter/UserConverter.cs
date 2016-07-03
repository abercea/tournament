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
                        UserId = model.UserId,
                        Username = model.Username
                    };
                user.Password = string.IsNullOrEmpty(model.Password) ? string.Empty : EncryptionHelper.Encrypt(model.Password);
            }

            return user;
        }

        internal static UserViewModel FromUserDbModelToViewModel(User dbUser)
        {
            if (dbUser != null)
            {
                var model = new UserViewModel
                {
                    Lastname = dbUser.Lastname,
                    Firstname = dbUser.Firstname,
                    Email = dbUser.Email,
                    Role = (AccessRolesEnum)dbUser.Role,
                    UserId = dbUser.UserId,
                    SportId = dbUser.SportId,
                    Username = dbUser.Username,
                    Uploads = dbUser.Uploads.ToList(),
                    MainFile = dbUser.ProfilePhto ?? 0,
                    Avatar = dbUser.Avatar != null ? dbUser.Avatar.Path : string.Empty

                };

                model.Role = dbUser.AccountActive ? model.Role : AccessRolesEnum.AccountNotActivated;

                return model;
            }

            return null;
        }
    }
}
