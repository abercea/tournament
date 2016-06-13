using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportLife.Models.Models.Enums
{
    public enum EventTypeEnum
    {
        [StringValue("None")]
        NotSelected = 0,
        [StringValue("Draw")]
        Draw = 1,
        [StringValue("Group")]
        Group = 2
    }

    public enum ActionType
    {
        None,
        AddOk,
        AddFail,
        EdditOk,
        EdditFail,
        DeleteOk,
        DeleteFail
    }
}
