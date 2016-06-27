using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportLife.Models.Models.Enums
{
    public class IntValueAttribute : Attribute
    {
        public int IntValue { get; private set; }

        public IntValueAttribute(int value)
        {
            this.IntValue = value;
        }
    }
}
