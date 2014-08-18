using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gnow.util.behringer
{
	public class X32Channel
	{
		public float dbValue;
		private float m_fLevel;
		public string name;
		public bool isMuted;
		public List<float> busSends;
		public X32Eq eq;
	}
}
