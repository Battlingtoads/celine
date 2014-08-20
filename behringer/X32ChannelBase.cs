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
        public string Name;


        public X32ChannelBase()
        {
            Name = "Unnamed";
            color = Constants.COLOR.WHITE;
            Mute = Constants.ON_OFF.ON;
            Level = new X32Level(Constants.NO_LEVEL, 1024);
        }

        public bool SetConfigValues(string parameter, object value)
        {
            bool setAValue = false;
            switch (parameter)
            {
                case "name":
                    Name = (string)value;
                    setAValue = true;
                    break;
                case "color":
                    color = (Constants.COLOR)(int)value;
                    setAValue = true;
                    break;
                default:
                    break;
            }

            return setAValue;
        }

        public virtual bool SetMixValues(string[] parameters, object value)
        {
            bool setAValue = false;
            switch(parameters[2])
            {
                case "on":
                    Mute = (Constants.ON_OFF)(int)value;
                    setAValue = true;
                    break;
                case "fader":
                    Level.RawLevel = (float)value;
                    setAValue = true;
                    break;

                //these cases are handled must be handled in a method in the subclass
                case "pan":
                case "st":
                case "mono":
                case "mlevel":
                    break;

                //sends
                default:
                    int send = Convert.ToInt32(parameters[2]) - 1;
                    switch (parameters[3])
                    {
                        case "on":
                            Sends[send].Mute = (Constants.ON_OFF)(int)value;
                            setAValue = true;
                            break;
                        case "level":
                            Sends[send].Level.RawLevel = (float)value;
                            setAValue = true;
                            break;
                        case "pan":
                            break;
                        case "type":
                            Sends[send].Type = (Constants.MIX_TAP)(int)value;
                            Sends[send + 1].Type = (Constants.MIX_TAP)(int)value;
                            setAValue = true;
                            break;

                    }
                    break;
            }
            return setAValue;
        }


    }
}
