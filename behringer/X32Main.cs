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
        public X32Eq m_Eq = new X32Eq(6);

        public X32Main()
        {
            Sends = new List<X32Send>(6);
            for(int i = 0; i<6; i++)
            {
                Sends.Add(new X32Send());
            }
            Mute = Constants.ON_OFF.ON;
            pan = 0;
        }

    }
}
