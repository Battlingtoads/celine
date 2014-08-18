using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gnow.util.behringer
{
    public class X32AuxIn : X32ChannelBase
    {
		public List<X32Level> busSends;

        public X32AuxIn()
        {
            Level = new X32Level(Constants.NO_LEVEL, 1024);
            busSends = new List<X32Level>(16);
            for(int i = 0; i<16; i++)
            {
                X32Level temp = new X32Level(Constants.NO_LEVEL, 161);
                busSends.Add(temp);
            }
            eq = new X32Eq();
            color = Constants.COLOR.WHITE;
            isMuted = true;
        }
    }
}
