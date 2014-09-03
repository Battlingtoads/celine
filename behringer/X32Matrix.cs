using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gnow.util.behringer
{
    public class X32Matrix : X32ChannelBase
    {
        public X32Dynamic m_Dynamic = new X32Dynamic();
        public X32Eq m_Eq = new X32Eq(6);
    }
}
