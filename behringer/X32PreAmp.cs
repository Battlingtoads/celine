using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gnow.util.behringer
{
    public class X32PreAmp : SettableFromOSC
    {
        public LinearFloat m_Trim = new LinearFloat(-12.000f, 12.000f, 0.250f);
        public bool m_PreampInvert = false;
        public bool m_HighPassOn = false;
        public Constants.HP_SLOPE m_HighPassSlope = Constants.HP_SLOPE._12;

        //TODO Change to logfloat
        public LogFloat m_HighPassFrequency = new LogFloat(20.000f, 400.000f, 101);

        public bool SetValuesFromOSC(string[] parameters, object value)
        {
            switch (parameters[2])
            {
                case "trim":
                    m_Trim.Value = (float)value;
                    break;
                case "invert":
                    m_PreampInvert = Convert.ToBoolean(value);
                    break;
                case "hpon":
                    m_HighPassOn = Convert.ToBoolean(value);
                    break;
                case "hpslope":
                    m_HighPassSlope = (Constants.HP_SLOPE)(int)value;
                    break;
                case "hpf":
                    m_HighPassFrequency.Value = (float)value;
                    break;
                default:
                    return false;
            }
            return true;
        }
    }
}
