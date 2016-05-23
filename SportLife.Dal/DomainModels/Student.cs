using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportLife.Dal.DomainModels
{
    public class Student
    {
        public Student()
        {
        }

        public int StudentID { get; set; }
        public string StudentName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public byte[] Photo { get; set; }
        public decimal Height { get; set; }
        public float Weight { get; set; }

        public int? StdId { get; set; }

        [ForeignKey("StdId")]
        public virtual Standard Standard { get; set; }
    }
}
