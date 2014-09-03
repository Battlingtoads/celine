using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gnow.util.osc;

namespace gnow.util.behringer
{
    public class X32Dynamic : SettableFromOSC
    {
        public Constants.ON_OFF m_isOn = Constants.ON_OFF.OFF;
        public Constants.DYN_MODE m_Mode = Constants.DYN_MODE.COMP;
        public Constants.DYN_DET m_Determiner = Constants.DYN_DET.PEAK;
        public Constants.DYN_ENV m_Envelope = Constants.DYN_ENV.LIN;
        public LinearFloat m_Threshold = new LinearFloat(-80.000f, 0.00f, 0.500f);
        public Constants.DYN_RATIO m_Ratio = Constants.DYN_RATIO._3;
        public LinearFloat m_Knee = new LinearFloat(0.000f, 5.000f, 1.000f);
        public LinearFloat m_Gain = new LinearFloat(0.000f, 24.000f, .500f);
        public LinearFloat m_Attack = new LinearFloat(0.000f, 120.000f, 1.000f);
        public LogFloat m_Hold = new LogFloat(0.020f, 2000f, 101);
        public LogFloat m_Release = new LogFloat(5.000f, 4000.000f, 101);
        public Constants.SIMPLE_POS m_TapPoint = Constants.SIMPLE_POS.PRE;
        public int m_KeySource = 0;
        public Constants.ON_OFF m_FilterOn = Constants.ON_OFF.OFF;
        public Constants.FILTER_TYPE m_FilterType = Constants.FILTER_TYPE.LC6;
        public LogFloat m_FilterFrequency = new LogFloat(20.000f, 200000f, 201);

        public bool SetValuesFromOSC(string[] parameters, object value)
        {
            switch (parameters[2])
            {
                case "on":
                    m_isOn = (Constants.ON_OFF)(int)value;
                    break;
                case "mode":
                    m_Mode = (Constants.DYN_MODE)(int)value;
                    break;
                case "det":
                    m_Determiner = (Constants.DYN_DET)(int)value;
                    break;
                case "env":
                    m_Envelope = (Constants.DYN_ENV)(int)value;
                    break;
                case "thr":
                    m_Threshold.Value = (float)value;
                    break;
                case "ratio":
                    m_Ratio = (Constants.DYN_RATIO)(int)value;
                    break;
                case "knee":
                    m_Knee.Value = (float)value;
                    break;
                case "attack":
                    m_Attack.Value = (float)value;
                    break;
                case "hold":
                    m_Hold.Value = (float)value;
                    break;
                case "release":
                    m_Release.Value = (float)value;
                    break;
                case "pos":
                    m_TapPoint = (Constants.SIMPLE_POS)(int)value;
                    break;
                case "keysrc":
                    m_KeySource = (int)value;
                    break;
                case "filter":
                    switch (parameters[3])
                    {
                        case "on":
                            m_FilterOn = (Constants.ON_OFF)(int)value;
                            break;
                        case "type":
                            m_FilterType = (Constants.FILTER_TYPE)value;
                            break;
                        case "f":
                            m_FilterFrequency.Value = (float)value;
                            break;
                    }
                    break;
                default:
                    return false;
            }
            return true;
        }

    }
}
