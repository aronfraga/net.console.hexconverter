using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HexConverter
{
    public class MissingArgumentsException : ApplicationException
    {
        public override string Message => "Arguments are missing or something is wrong.";
    }
}
