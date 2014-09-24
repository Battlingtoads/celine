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

    public class MuteValueChangedArgs : EventArgs
    {
        public int offset;
        public Constants.FADER_GROUP bank;
        public bool value;
    }
}
