using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gnow.util.behringer
{
    public abstract class X32ChannelBase
    {
        public X32Level Level;
        public string name;
        public Constants.COLOR color;
        public bool isMuted;
        public X32Eq eq;
    }
}
