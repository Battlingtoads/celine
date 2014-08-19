using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gnow.util.behringer
{
	public class X32Channel : X32ChannelBase
	{
        public float pan;
        public Constants.ON_OFF StereoOn;
        public Constants.ON_OFF MonoOn;
        public X32Level MonoLevel;

        public X32Channel() : base()
        {
            Sends = new List<X32Send>(16);
            for(int i = 0; i<16; i++)
            {
                
                Sends.Add(new X32Send());
            }
            eq = new X32Eq();
            StereoOn = Constants.ON_OFF.ON;
            MonoOn = Constants.ON_OFF.OFF;
            MonoLevel = new X32Level(0.0f, 161);
        }
	}
}
