using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gnow.util.osc;

namespace gnow.util.behringer
{
   public class X32Gate : SettableFromOSC
   {
      public Constants.ON_OFF m_isOn;
      public Constants.GATE_MODE m_Mode;
      public LinearFloat m_Threshold = new LinearFloat(-80.0f, 0.0f, .5f);
      public LinearFloat m_Range = new LinearFloat(3.00f, 60.0f, 1.00f);
      public LinearFloat m_Attack = new LinearFloat(0.00f, 120.00f, 1.00f);
      public LogFloat m_Hold = new LogFloat(0.020f, 2000, 101);
      public LogFloat m_Release = new LogFloat(5.000f, 4000.000f, 101);
      public int m_KeySource;
      public Constants.ON_OFF m_FilterOn;
      public Constants.FILTER_TYPE m_FilterType;
      public LogFloat m_FilterFrequency = new LogFloat(20.000f, 20000f, 201);

      public X32Gate()
      {
         m_isOn = Constants.ON_OFF.OFF;
         m_Mode = Constants.GATE_MODE.GATE;
         m_KeySource = 0;
         m_FilterOn = Constants.ON_OFF.OFF;
         m_FilterType = Constants.FILTER_TYPE.LC6;
      }

      public bool SetValuesFromOSC(string[] parameters, object value)
      {
          switch (parameters[2])
          {
              case "on":
                  m_isOn = (Constants.ON_OFF)(int)value;
                  break;
              case "mode":
                  m_Mode = (Constants.GATE_MODE)(int)value;
                  break;
              case "thr":
                  m_Threshold.Value = (float)value;
                  break;
              case "range":
                  m_Range.Value = (float)value;
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
