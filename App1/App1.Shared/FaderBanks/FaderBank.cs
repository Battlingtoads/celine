using System;
using System.Collections.Generic;
using System.Text;
using gnow.util.behringer;

namespace gnow.util 
{
    public interface FaderBank
    {
        void setMute(int index, bool value);
        void setLevel(int index, float value);
        Constants.FADER_GROUP getEnum();
    }
}
