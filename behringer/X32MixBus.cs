using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gnow.util.behringer
{
    public class X32MixBus: X32ChannelBase
    {
		public List<X32Level> matrixSends;

        public X32MixBus()
        {
            Level = new X32Level(Constants.NO_LEVEL, 1024);
            matrixSends = new List<X32Level>(6);
            for(int i = 0; i<6; i++)
            {
                X32Level temp = new X32Level(Constants.NO_LEVEL, 161);
                matrixSends.Add(temp);
            }
            eq = new X32Eq();
            color = Constants.COLOR.YELLOW;
            isMuted = true;
        }
    }
}
