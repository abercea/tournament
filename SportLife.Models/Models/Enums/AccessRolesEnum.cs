using SportLife.Models.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportLife.Models.Models
{
    public enum AccessRolesEnum
    {
        [StringValue("none")]
        NoAcces = 0,
        [StringValue("player")]
        Player = 1,
        [StringValue("admin")]
        Admin = 2,
        [StringValue("notActive")]
        AccountNotActivated = 9
    }
}
