using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gnow.util.behringer
{
    class X32Matrix : X32ChannelBase
    {
        public X32Matrix()
        {
            Level = new X32Level(Constants.NO_LEVEL, 1024);
            eq = new X32Eq();
            color = Constants.COLOR.WHITE;
            isMuted = true;
        }
    }
}
