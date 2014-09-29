using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gnow.util.behringer
{
    public class X32Send
    {
        public X32Level Level;
        public bool On;
        public Constants.MIX_TAP Type;
        public LinearFloat m_Pan = new LinearFloat(-100.00f, 100.00f, 2.00f);

        public X32Send()
        {
            Level = new X32Level(0.0f, 161);
            On = true;
            Type = Constants.MIX_TAP.PRE;
        }
    }
}
