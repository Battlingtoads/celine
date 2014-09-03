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
		public bool isLowcutOn;
		public float LowcutFrequency;
		public List<X32EqBand> eqBands;

        public bool SetValuesFromOSC(string[] parameters, object value)
        {
            //TODO: Set values
            return true;
        }
	}
}
