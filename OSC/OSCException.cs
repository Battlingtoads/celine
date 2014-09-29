using System;
using System.Collections.Generic;
using System.Text;

namespace gnow.utils.osc
{
    public class OSCException : Exception
    {
        public OSCException()
        {

        }

        public OSCException(string message)
            : base(message)
        {

        }

        public OSCException(string message, Exception inner)
            : base(message, inner)
        {

        }
    }
}
