using SportLife.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportLife.Bll.Contracts
{
    public interface IEventBus
    {
        bool AdEdit(EventViewModel ev, UserViewModel user);
        List<EventViewModel> GetAllEvents();
        EventViewModel GetById(int id);
        string Delete(int id);
    }
}
