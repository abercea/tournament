using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportLife.Models.Models
{
    public class DashboardViewModel
    {
        public DashboardViewModel()
        {
            Users = new List<UserViewModel>();
            Events = new List<EventViewModel>();
        }
        public List<UserViewModel> Users { get; set; }
        public List<EventViewModel> Events { get; set; }
    }
}
