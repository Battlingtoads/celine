using System;
using gnow.util.behringer;
namespace gnow.util
{
    public class FaderValueChangedArgs : EventArgs
    {
        public int offset;
        public Constants.FADER_GROUP bank;
        public float value;
    }
}