using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gnow.util.behringer
{
	public class X32Eq : SettableFromOSC
	{
		public int numEqBands;
		public bool isOn;
		public List<X32EqBand> eqBands;

		public X32Eq(int bands)
        {
            eqBands = new List<X32EqBand>(bands);
        }

        public bool SetValuesFromOSC(string[] parameters, object value)
        {
            switch(parameters[2])
            {
                case "on":
                    isOn = (bool)value;
                    break;
                default:
                    int band = Convert.ToInt32(parameters[2]) - 1;
                    switch(parameters[3])
                    {
                        case "type":
                            eqBands[band].type = (Constants.EQ_TYPE)(int)value;
                            break;
                        case "f":
                            eqBands[band].frequency.Value = (float)value;
                            break;
                        case "g":
                            eqBands[band].gain.Value = (float)value;
                            break;
                        case "q":
                            eqBands[band].q.Value = (float)value;
                            break;
                    }
                    break;
            }
            return true;
        }
	}
}
