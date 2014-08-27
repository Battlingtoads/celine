using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gnow.util.behringer
{
    public class X32Main : X32ChannelBase
    {
        public float pan;
        public X32Dynamic m_Dynamic = new X32Dynamic();

        public X32Main()
        {
            Level = new X32Level(Constants.NO_LEVEL, 1024);
            Sends = new List<X32Send>(6);
            for(int i = 0; i<6; i++)
            {
                Sends.Add(new X32Send());
            }
            eq = new X32Eq();
            color = Constants.COLOR.WHITE;
            Mute = Constants.ON_OFF.ON;
            pan = 0;
        }

        public void SetDynamicValues(string[] parameters, object value)
        {
            switch (parameters[2])
            {
                case "on":
                    m_Dynamic.m_isOn = (Constants.ON_OFF)(int)value;
                    break;
                case "mode":
                    m_Dynamic.m_Mode = (Constants.DYN_MODE)(int)value;
                    break;
                case "det":
                    m_Dynamic.m_Determiner = (Constants.DYN_DET)(int)value;
                    break;
                case "env":
                    m_Dynamic.m_Envelope = (Constants.DYN_ENV)(int)value;
                    break;
                case "thr":
                    m_Dynamic.m_Threshold.Value = (float)value;
                    break;
                case "ratio":
                    m_Dynamic.m_Ratio = (Constants.DYN_RATIO)(int)value;
                    break;
                case "knee":
                    m_Dynamic.m_Knee.Value = (float)value;
                    break;
                case "attack":
                    m_Dynamic.m_Attack.Value = (float)value;
                    break;
                case "hold":
                    m_Dynamic.m_Hold = (float)value;
                    break;
                case "release":
                    m_Dynamic.m_Release = (float)value;
                    break;
                case "pos":
                    m_Dynamic.m_TapPoint = (Constants.SIMPLE_POS)(int)value;
                    break;
                case "keysrc":
                    m_Dynamic.m_KeySource = (int)value;
                    break;
                default:
                    switch (parameters[3])
                    {
                        case "on":
                            m_Dynamic.m_FilterOn = (Constants.ON_OFF)(int)value;
                            break;
                        case "type":
                            m_Dynamic.m_FilterType = (Constants.FILTER_TYPE)value;
                            break;
                        case "f":
                            m_Dynamic.m_FilterFrequency = (float)value;
                            break;
                    }
                    break;
            }
        }
    }
}
