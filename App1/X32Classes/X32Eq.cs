using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gnow.util.behringer
{
	class X32Eq
	{
		public int numEqBands;
		public bool isOn;
		public bool isLowcutOn;
		public float LowcutFrequency;
		public List<X32EqBand> eqBands;
	}
}
