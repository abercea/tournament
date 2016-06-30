using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportLife.Dal.DomainModels
{
    public class User
    {
        public User()
        {
            RegisteredEvents = new HashSet<Event>();
        }

        public int UserId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Description { get; set; }
        public bool AccountActive { get; set; }
        public int Role { get; set; }
        public string Username { get; set; }
        public int ProfilePhto { get; set; } 

        public DateTime AccountCreationDate { get; set; }


        public int? SportId { get; set; }

        [ForeignKey("SportId")]
        public virtual Sport Sport { get; set; }

        public virtual ICollection<Event> RegisteredEvents { get; set; }
        public virtual ICollection<Document> Uploads { get; set; }
    }
}
