using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gnow.util.osc;

namespace gnow.util.behringer
{
	/// <summary>Base class for X32 input and output components</summary>
    public abstract class X32ChannelBase : SettableFromOSC
    {
		/// <summary>Main level</summary>
        public X32Level Level;

		/// <summary>Color of the channel</summary>
        public Constants.COLOR color;

		/// <summary>Mute state of the channel</summary>
        public Constants.ON_OFF Mute;

		/// <summary>Equalizer</summary>
        public X32Eq eq;

		/// <summary>List of the auxiliary//matrix sends this channel sends to.</summary>
        public List<X32Send> Sends;

		/// <summary>Name of the channel.</summary>
        public string Name;


		/// <summary>Constructor</summary>
        public X32ChannelBase()
        {
            Name = "Unnamed";
            color = Constants.COLOR.WHITE;
            Mute = Constants.ON_OFF.ON;
            Level = new X32Level(Constants.NO_LEVEL, 1024);
        }

        public virtual bool SetValuesFromOSC(string[] parameters, object value)
        {
            if(parameters[1] == "config")
            {
                bool setAValue = false;
                switch (parameters[2])
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
            else if (parameters[1] == "mix")
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
            return false;
        }


    }
}
