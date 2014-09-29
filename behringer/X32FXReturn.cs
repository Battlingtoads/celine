using gnow.util.osc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gnow.util.behringer
{
    public class X32FXReturn : X32ChannelBase
    {
        public bool StereoOn;
        public bool MonoOn;
        public X32Level MonoLevel;
        public Byte m_MuteGroup = 0;

        public X32FXReturn() : base()
        {
            Sends = new List<X32Send>(16);
            for(int i = 0; i<16; i++)
            {
                Sends.Add(new X32Send());
            }
            StereoOn = true;
            MonoOn = false;
            MonoLevel = new X32Level(0.0f, 161);
        }
        public override bool SetValuesFromOSC(string[] parameters, object value)
        {
            if(!base.SetValuesFromOSC(parameters, value))
            {
                if(parameters[1] == "grp")
                {
                    switch(parameters[2])
                    {
                        case "dca":
                            break;
                        case "mute":
                            m_MuteGroup = (byte)value;
                            break;
                    }
                }
                else
                {
                   switch (parameters[2])
                   {
                       case "pan":
                           break;
                       case "st":
                           StereoOn = (bool)value;
                           break;
                       case "mono":
                           MonoOn = (bool)value;
                           break;
                       case "mlevel":
                           MonoLevel.RawLevel = (float)value;
                           break;
                   }
                }
            }
            return true;
        }
    }
}
