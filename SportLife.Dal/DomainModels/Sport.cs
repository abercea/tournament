using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportLife.Dal.DomainModels
{
   public class Sport
    {
       public int SportId { get; set; }
       public int SportType { get; set; }
       public string Description { get; set; }
       public string Regulations { get; set; }
    }
}
