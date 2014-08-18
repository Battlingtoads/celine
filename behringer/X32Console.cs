using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gnow.util.behringer
{
    class X32Console
    {
        List<X32Channel> Channels;
        List<X32AuxIn> AuxInputs;
        List<X32FXReturn> FXReturns;
        List<X32MixBus> MixBusses;
        List<X32Matrix> Matrices;
        X32Main StereoMain;
        X32Main MonoMain;

        public X32Console()
        {
            Channels = new List<X32Channel>(32);
            for(int i = 0; i < 32; i++)
            {
                Channels.Add(new X32Channel());
            }
            MixBusses = new List<X32MixBus>(16);
            for(int i = 0; i < 16; i++)
            {
                MixBusses.Add(new X32MixBus());
            }
            AuxInputs = new List<X32AuxIn>(8);
            FXReturns = new List<X32FXReturn>(8);
            for(int i = 0; i < 8; i++)
            {
                AuxInputs.Add(new X32AuxIn());
                FXReturns.Add(new X32FXReturn());
            }
            Matrices = new List<X32Matrix>(6);
            for(int i = 0; i < 6; i++)
            {
                Matrices.Add(new X32Matrix());
            }
            StereoMain = new X32Main();
            MonoMain = new X32Main();
        }
    }
}
