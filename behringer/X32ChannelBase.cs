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
        public List<X32Send> Sends;


        public X32ChannelBase()
        {
            color = Constants.COLOR.WHITE;
            Mute = Constants.ON_OFF.ON;
            Level = new X32Level(Constants.NO_LEVEL, 1024);
        }

        public bool SetMixValues(string[] parameters, object value)
        {
            bool setAValue = false;
            switch(parameters[2])
            {
                case "on":
                    Mute = (Constants.ON_OFF)(int)(oscInt)value;
                    setAValue = true;
                    break;
                case "fader":
                    Level.RawLevel = (oscFloat)value;
                    setAValue = true;
                    break;
                case "pan":
                case "st":
                case "mono":
                case "mlevel":
                    break;
                default:
                    int send = Convert.ToInt32(parameters[2]) - 1;
                    switch (parameters[3])
                    {
                        case "on":
                            Sends[send].Mute = (Constants.ON_OFF)(int)(oscInt)value;
                            setAValue = true;
                            break;
                        case "level":
                            Sends[send].Level.RawLevel = (oscFloat)value;
                            setAValue = true;
                            break;
                        case "pan":
                            break;
                        case "type":
                            Sends[send].Type = (Constants.MIX_TAP)(int)(oscInt)value;
                            Sends[send + 1].Type = (Constants.MIX_TAP)(int)(oscInt)value;
                            setAValue = true;
                            break;

                    }
                    break;
                
            }
            return setAValue;
        }

    }
}
