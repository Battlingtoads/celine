using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gnow.util.osc;

namespace gnow.util.behringer
{
	public class X32Channel : X32ChannelBase
	{
        public float pan;
        public Constants.ON_OFF StereoOn;
        public Constants.ON_OFF MonoOn;
        public X32Level MonoLevel;
        public X32Gate m_Gate;

        public X32Channel() : base()
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

        public void SetGateValues(string[] parameters, object value)
        {
            switch (parameters[2])
            {
                case "on":
                    m_Gate.m_isOn = (Constants.ON_OFF)(int)value;
                    break;
                case "mode":
                    m_Gate.m_Mode = (Constants.GATE_MODE)(int)value;
                    break;
                case "thr":
                    m_Gate.m_Threshold.Value = (float)value;
                    break;
                case "range":
                    m_Gate.m_Range = (float)value;
                    break;
                case "attack":
                    m_Gate.m_Attack = (float)value;
                    break;
                case "hold":
                    m_Gate.m_Hold = (float)value;
                    break;
                case "release":
                    m_Gate.m_Release = (float)value;
                    break;
                case "keysrc":
                    m_Gate.m_KeySource = (int)value;
                    break;
                default:
                    switch(parameters[3])
                    {
                        case "on":
                            m_Gate.m_FilterOn = (Constants.ON_OFF)(int)value;
                            break;
                        case "type":
                            m_Gate.m_FilterType = (Constants.FILTER_TYPE)value;
                            break;
                        case "f":
                            m_Gate.m_FilterFrequency = (float)value;
                            break;
                    }
            }
        }
    }
}
