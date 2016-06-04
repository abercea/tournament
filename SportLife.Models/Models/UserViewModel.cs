using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportLife.Models.Models
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirm { get; set; }
        public string Description { get; set; }
        public bool AcceptTerms { get; set; }
        public DateTime AccountCreationDate { get; set; }


        public int? SportId { get; set; }
    }
}
