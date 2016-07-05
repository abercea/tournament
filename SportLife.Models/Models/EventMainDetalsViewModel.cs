using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportLife.Models.Models
{
    public class EventMainDetalsViewModel
    {
        public EventMainDetalsViewModel()
        {
            RegisterdPlayers = new List<UserViewModel>();
            Event = new EventViewModel();
        }
        public List<UserViewModel> RegisterdPlayers { get; set; }
        public EventViewModel Event { get; set; }
    }
}
