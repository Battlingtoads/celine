using gnow.util.osc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gnow.util.behringer
{
    public class X32AuxIn : X32ChannelBase
    {
        public bool StereoOn;
        public bool MonoOn;
        public X32Level MonoLevel;
        public X32PreAmp m_PreAmp = new X32PreAmp();
        public X32Eq m_Eq = new X32Eq(4);
        public byte m_MuteGroup = 0;

        public X32AuxIn() : base()
        {
            Sends = new List<X32Send>(16);
            for(int i = 0; i<16; i++)
            {
                Sends.Add(new X32Send());
            }
            StereoOn = true;
            MonoOn = true;
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
                           StereoOn = Convert.ToBoolean(value);
                           break;
                       case "mono":
                           MonoOn = Convert.ToBoolean(value);
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
