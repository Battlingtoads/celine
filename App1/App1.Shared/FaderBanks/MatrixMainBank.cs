using System;
using System.Collections.Generic;
using System.Text;
using gnow.util.behringer;

namespace gnow.util
{
    class MatrixMainBank: FaderBank
    {
        public void setMute(int index, bool value)
        {
            int channel;
            string subAddress;
            string address;
            if(index < 6)
            {
                channel = 1 + index;
                subAddress = "mix/on";
                address = "/mtx/" + channel.ToString().PadLeft(2, '0') + subAddress;
                X32Console.Instance.Matrices[channel].Mute = (Constants.ON_OFF)value;
            }
            else if(index == 7)
            {
                channel = 0;
                address = "/main/st/mix/on";
                X32Console.Instance.StereoMain.Mute = (Constants.ON_OFF)value;
            }
            else
            {
                channel = 0;
                address = "/main/m/mix/on";
                //X32Console.Instance.Mono.Mute = (Constants.ON_OFF)value;
            }
        }

        public void setLevel(int index, float value)
        {
            int channel;
            string subAddress;
            string address;
            if(index < 6)
            {
                channel = 1 + index;
                subAddress = "mix/fader";
                address = "/mtx/" + channel.ToString().PadLeft(2, '0') + subAddress;
                X32Console.Instance.Matrices[channel].Level.DbFSLevel = value;
            }
            else if(index == 7)
            {
                channel = 0;
                address = "/main/st/mix/fader";
                X32Console.Instance.StereoMain.Level.DbFSLevel = value;
            }
            else
            {
                channel = 0;
                address = "/main/m/mix/fader";
                //X32Console.Instance.Mono.Mute = (Constants.ON_OFF)value;
            }
        }

        public Constants.FADER_GROUP getEnum()
        {
            return Constants.FADER_GROUP.MATRIX_MAIN;
        }
    }
}
