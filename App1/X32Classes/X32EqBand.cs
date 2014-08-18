using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gnow.util.behringer
{
	class X32EqBand
	{
		public Constants.EQ_TYPE type;
		public frequency;
		public q;
		private float m_gain;
		public float gain{get{return m_gain;} 
			set
			{
				if(type == Constants.EQ_TYPE.PEQ ||
			   	   type == Constants.EQ_TYPE.VEQ)
				{
					m_gain = value;
				}
			};
		}

	}
}
