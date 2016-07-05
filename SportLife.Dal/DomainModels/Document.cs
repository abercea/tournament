using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportLife.Dal.DomainModels
{
    public class Document
    {
        public int DocumentId { get; set; }

        public string Name { get; set; }
        public string Path { get; set; }
        public DateTime DateUploaded { get; set; }
      //  public bool IsMainImage { get; set; }

        // [ForeignKey("UserId")]
        public int UserId { get; set; }
    }
}
