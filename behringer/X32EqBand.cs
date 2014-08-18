using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gnow.util.behringer
{
	public class X32EqBand
	{
		public Constants.EQ_TYPE type;
		public float frequency;
		public float q;
		private float m_gain;
		public float gain{get{return m_gain;} 
			set
			{
				if(type == Constants.EQ_TYPE.PEQ ||
			   	   type == Constants.EQ_TYPE.VEQ)
				{
					m_gain = value;
				}
			}
		}

	}
}
