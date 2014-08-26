using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gnow.util.osc;

namespace gnow.util.behringer
{
    public class X32Dynamic
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
        public float m_Hold = 0.02f;
        public float m_Release = 5.0f;
        public Constants.SIMPLE_POS m_TapPoint = Constants.SIMPLE_POS.PRE;
        public int m_KeySource = 0;
        public Constants.ON_OFF m_FilterOn = Constants.ON_OFF.OFF;
        public Constants.FILTER_TYPE m_FilterType = Constants.FILTER_TYPE.LC6;
        public float m_FilterFrequency = 20.0f;

    }
}
