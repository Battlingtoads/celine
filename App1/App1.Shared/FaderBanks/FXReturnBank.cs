using System;
using System.Collections.Generic;
using System.Text;
using gnow.util.behringer;

namespace gnow.util
{
    public class FXReturnBank : FaderBank
    {
        public void setMute(int index, bool value)
        {
            int channel = 1 + index;
            string subAddress = "mix/on";
            string address = "/fxrtn/" + channel.ToString().PadLeft(2, '0') + subAddress;
            X32Console.Instance.Channels[channel].StereoOn = value;
        }

        public void setLevel(int index, float value)
        {
            int channel = 1 + index;
            string subAddress = "mix/fader";
            string address = "/fxrtn/" + channel.ToString().PadLeft(2, '0') + subAddress;
            X32Console.Instance.Channels[channel].Level.DbFSLevel = value;
        }

        public Constants.FADER_GROUP getEnum()
        {
            return Constants.FADER_GROUP.FX_RETURNS;
        }
    }
}
