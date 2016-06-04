using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SportLife.Models.Models
{
    public class CustomResponseModel<T>
    {
        public string ViewMessage { get; set; }
        public string DebugMessage { get; set; }
        public T Model { get; set; }
        public bool WithError { get; set; }
        public HttpStatusCode SCode { get; set; }
    }
}
