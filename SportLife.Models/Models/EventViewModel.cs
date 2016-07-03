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
        public EventViewModel()
        {
            EventFee = 45;
            MaxNumberOfParticipants = 32;
        }

        [Required(ErrorMessage = "* mandatory")]
        public string EventName { get; set; }
        public string EventDescription { get; set; }
        [Required(ErrorMessage = "* mandatory")]
        public bool IsVisible { get; set; }
        [Required(ErrorMessage = "* mandatory")]
        public bool IsOpened { get; set; }
        public bool IsClosed { get; set; }
        public DateTime? DateCreation { get; set; }

        [BiggerThan("ClosingRegistrationDate", ErrorMessage = "Event date must be after closing registration date")]
        public DateTime? EventDate { get; set; }
        public int CreatorId { get; set; }
        public string EventPlace { get; set; }
        [Required(ErrorMessage = "* mandatory")]

        public int MaxNumberOfParticipants { get; set; }
        [Required(ErrorMessage = "* mandatory")]

        public DateTime? ClosingRegistrationDate { get; set; }
        public int EventFee { get; set; }
        public string EventPoster { get; set; }

        public int EventId { get; set; }
        public EventTypeEnum Type { get; set; }

        public int RegisterdPlayers { get; set; }
    }

    public class BiggerThanAttribute : ValidationAttribute
    {
        private readonly string _other;
        public BiggerThanAttribute(string other)
        {
            _other = other;

        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return null;
            }
            var property = validationContext.ObjectType.GetProperty(_other);
            if (property == null)
            {
                return new ValidationResult(
                    string.Format("Unknown property: {0}", _other)
                );
            }
            var otherValue = property.GetValue(validationContext.ObjectInstance, null);

            // at this stage you have "value" and "otherValue" pointing
            // to the value of the property on which this attribute
            // is applied and the value of the other property respectively
            // => you could do some checks
            if (otherValue != null && (DateTime)value < (DateTime)otherValue)
            {
                // here we are verifying whether the 2 values are equal
                // but you could do any custom validation you like
                return new ValidationResult(this.FormatErrorMessage(ErrorMessage));
            }
            return null;
        }
    }
}
