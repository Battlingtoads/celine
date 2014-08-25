using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gnow.util.osc;

namespace gnow.util.behringer
{
   public class X32Gate
   {
      public Constants.ON_OFF m_isOn;
      public Constants.GATE_MODE m_Mode;
      public float m_Threshold;
      public float m_Range;
      public float m_Attack;
      public float m_Hold;
      public float m_Release;
      public int m_KeySource;
      public Constants.ON_OFF m_FilterOn;
      public Constants.FILTER_TYPE m_FilterType;
      public float m_FilterFrequency;

      public X32Gate()
      {
         m_isOn = Constants.ON_OFF.OFF;
         m_Mode = Constants.GATE_MODE.GATE;
         m_Threshold = -80.0f;
         m_Range = 3.0f;
      }
   }
}
