using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gnow.util.osc;

namespace gnow.util.behringer
{
    public class X32Channel : X32ChannelBase , SettableFromOSC
    {
        public float pan;
        public Constants.ON_OFF StereoOn;
        public Constants.ON_OFF MonoOn;
        public X32Level MonoLevel;
        public X32Gate m_Gate = new X32Gate();
        public X32Dynamic m_Dynamic = new X32Dynamic();
        public X32PreAmp m_PreAmp = new X32PreAmp();
        public X32Eq m_Eq = new X32Eq(4);


        public X32Channel()
            : base()
        {
            Sends = new List<X32Send>(16);
            for (int i = 0; i < 16; i++)
            {
                
                Sends.Add(new X32Send());
            }
            eq = new X32Eq();
            StereoOn = Constants.ON_OFF.ON;
            MonoOn = Constants.ON_OFF.OFF;
            MonoLevel = new X32Level(0.0f, 161);
        }

        public override bool SetValuesFromOSC(string[] parameters, object value)
        {
            if (!base.SetValuesFromOSC(parameters, value))
            {
                switch (parameters[2])
                {
                    case "pan":
                        break;
                    case "st":
                        StereoOn = (Constants.ON_OFF)(int)value;
                        break;
                    case "mono":
                        MonoOn = (Constants.ON_OFF)(int)value;
                        break;
                    case "mlevel":
                        MonoLevel.RawLevel = (float)value;
                        break;
                }
            }
            return true;
        }

    }
}
