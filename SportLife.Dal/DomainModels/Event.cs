using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportLife.Dal.DomainModels
{
    public class Event
    {
        public Event()
        {
            RegisteredPlayers = new HashSet<User>();
        }

        public int EventId { get; set; }
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        public bool IsVisible { get; set; }
        public bool IsOpened { get; set; }
        public DateTime? DateCreation { get; set; }
        public DateTime EventDate { get; set; }

        public int CreatorId { get; set; }

        public string EventPlace { get; set; }
        public int MaxNumberOfParticipants { get; set; }
        public DateTime? ClosingRegistrationDate { get; set; }
        public int EventFee { get; set; }
        public string EventPoster { get; set; }

        public int Type { get; set; }

        [ForeignKey("CreatorId")]
        public virtual User Creator { get; set; }

        public ICollection<User> RegisteredPlayers { get; set; }
    }
}
