using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gnow.util.behringer
{
	public class X32EqBand
	{
		public Constants.EQ_TYPE type = Constants.EQ_TYPE.PEQ;
		public LogFloat frequency = new LogFloat(20.000f, 20000f, 201);
		public LogFloat q = new LogFloat(10.000f, 0.300f, 72);
		public LinearFloat gain = new LinearFloat(-15.000f, 15.000f, .250f);

	}
}
