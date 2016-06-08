using SportLife.Models.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportLife.Models.Models
{
    public class EventViewModel
    {
        [Required(ErrorMessage = "* mandatory")]
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        [Required(ErrorMessage = "* mandatory")]

        public bool IsVisible { get; set; }
        [Required(ErrorMessage = "* mandatory")]

        public bool IsOpened { get; set; }
        public DateTime? DateCreation { get; set; }
        public DateTime EventDate { get; set; }
        public int CreatorId { get; set; }
        public string EventPlace { get; set; }
        [Required(ErrorMessage = "* mandatory")]

        public int MaxNumberOfParticipants { get; set; }
        [Required(ErrorMessage = "* mandatory")]

        public DateTime? ClosingRegistrationDate { get; set; }
        public int EventFee { get; set; }
        public string EventPoster { get; set; }

        public EventTypeEnum Type { get; set; }
    }
}
