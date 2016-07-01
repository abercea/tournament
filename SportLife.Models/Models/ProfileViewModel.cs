using SportLife.Dal.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportLife.Models.Models
{
    public class ProfileViewModel
    {
        public List<Document> Documents { get; set; }
        public List<EventViewModel> Events { get; set; }
        public UserViewModel Subject { get; set; }
        public List<ChatMessage> Messages { get; set; }
        public List<SkilViewModel> Skils { get; set; }

        public ProfileViewModel()
        {
            Documents = new List<Document>();
            Events = new List<EventViewModel>();
            Subject = new UserViewModel();
            Messages = new List<ChatMessage>();
            Skils = new List<SkilViewModel>();
        }
    }
}
