using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gnow.util.behringer
{
    public class X32Main : X32ChannelBase
    {
        public float pan;
        public X32Dynamic m_Dynamic = new X32Dynamic();

        public X32Main()
        {
            Level = new X32Level(Constants.NO_LEVEL, 1024);
            Sends = new List<X32Send>(6);
            for(int i = 0; i<6; i++)
            {
                Sends.Add(new X32Send());
            }
            eq = new X32Eq();
            color = Constants.COLOR.WHITE;
            Mute = Constants.ON_OFF.ON;
            pan = 0;
        }

    }
}
