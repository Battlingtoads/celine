using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gnow.util.osc;

namespace gnow.util.behringer
{
    public abstract class X32ChannelBase
    {
        public X32Level Level;
        public string name;
        public Constants.COLOR color;
        public Constants.ON_OFF Mute;
        public X32Eq eq;
        public void SetMixValues(string parameter, object value)
        {
            switch(parameter)
            {
                case "on":
                    Mute = (Constants.ON_OFF)(int)(oscInt)value;
                    break;
                case "fader":
                    Level.RawLevel = (oscFloat)value;
                    break;
            }
        }
    }
}
