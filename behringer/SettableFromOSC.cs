using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gnow.util.behringer
{
    interface SettableFromOSC
    {
        bool SetValuesFromOSC(string[] parameters, object value);
    }
}
