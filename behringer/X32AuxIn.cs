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
        public Constants.ON_OFF StereoOn;
        public Constants.ON_OFF MonoOn;
        public X32Level MonoLevel;
        public LinearFloat m_Trim = new LinearFloat(-12.000f, 12.000f, 0.250f);
        public Constants.ON_OFF m_PreampInvert = Constants.ON_OFF.OFF;

        public X32AuxIn() : base()
        {
            Sends = new List<X32Send>(16);
            for(int i = 0; i<16; i++)
            {
                Sends.Add(new X32Send());
            }
            eq = new X32Eq();
            StereoOn = Constants.ON_OFF.ON;
            MonoOn = Constants.ON_OFF.OFF;
            MonoLevel = new X32Level(0.0f, 161);
        }
        public override bool SetMixValues(string[] parameters, object value)
        {
            if(!base.SetMixValues(parameters, value))
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

        public void SetPreampValues(string parameter, object value)
        {
            switch (parameter)
            {
                case "trim":
                    m_Trim.Value = (float)value;
                    break;
                case "invert":
                    m_PreampInvert = (Constants.ON_OFF)(int)value;
                    break;
            }
        }
    }
}
