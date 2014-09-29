using System;
using gnow.util.behringer;
namespace gnow.util
{
    public class FaderValueChangedArgs : EventArgs
    {
        public int offset;
        public float value;
    }

    public class MuteValueChangedArgs : EventArgs
    {
        public int offset;
        public bool value;
    }
}
