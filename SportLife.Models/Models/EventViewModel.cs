using SportLife.Models.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportLife.Models.Models
{
    public class EventViewModel
    {
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

        public EventTypeEnum Type { get; set; }
    }
}
