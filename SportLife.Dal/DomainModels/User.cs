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
        public int UserId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Description { get; set; }

        public DateTime AccountCreationDate { get; set; }


        public int? SportId { get; set; }

        [ForeignKey("SportId")]
        public virtual Sport Sport { get; set; }
    }
}
