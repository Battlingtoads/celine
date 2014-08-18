using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gnow.util.behringer
{
    class X32Main : X32ChannelBase
    {
		public List<X32Level> matrixSends;
        public float pan;

        public X32Main()
        {
            Level = new X32Level(Constants.NO_LEVEL, 1024);
            matrixSends = new List<X32Level>(6);
            for(int i = 0; i<6; i++)
            {
                X32Level temp = new X32Level(Constants.NO_LEVEL, 161);
                matrixSends.Add(temp);
            }
            eq = new X32Eq();
            color = Constants.COLOR.WHITE;
            isMuted = true;
            pan = 0;
        }
    }
}
