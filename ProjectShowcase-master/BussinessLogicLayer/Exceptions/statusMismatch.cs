using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessLogicLayer.Exceptions
{
    public class statusMismatch : Exception
    {
        public statusMismatch(string message) : base(message) { }
    }
}
