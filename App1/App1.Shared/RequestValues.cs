using System;
using System.Collections.Generic;
using System.Text;
using App1;
using gnow.util.osc;
using System.Threading.Tasks;

namespace gnow.util
{
    public static class RequestValues 
    {
        public static async Task FromOSC(Constants.FADER_GROUP group)
        {
            List<string> requests = new List<string>();
            //Generate requests
            switch (group)
            {
                case Constants.FADER_GROUP.CHANNEL_1_8:
                case Constants.FADER_GROUP.CHANNEL_9_16:
                case Constants.FADER_GROUP.CHANNEL_17_24:
                case Constants.FADER_GROUP.CHANNEL_25_32:
                    for (int i = 0; i < 8; i++ )
                    {
                        StringBuilder address = new StringBuilder();
                        string baseAddress = "/ch" + '/' + (i * (int)group).ToString().PadLeft(2, '0');
                                               
                        //Get main level
                        address.Append(baseAddress);
                        address.Append("/mix/fader");
                        requests.Add(address.ToString());
                        address.Clear();

                        //Get mute
                        address.Append(baseAddress);
                        address.Append("/mix/on");
                        requests.Add(address.ToString());
                    }
                    break;
                case Constants.FADER_GROUP.AUX_1_8:
                    for (int i = 0; i < 8; i++ )
                    {
                        StringBuilder address = new StringBuilder();
                        string baseAddress = "/auxin" + '/' + i.ToString().PadLeft(2, '0');
                                               
                        //Get main level
                        address.Append(baseAddress);
                        address.Append("/mix/fader");
                        requests.Add(address.ToString());
                        address.Clear();

                        //Get mute
                        address.Append(baseAddress);
                        address.Append("/mix/on");
                        requests.Add(address.ToString());
                    }
                    break;
                case Constants.FADER_GROUP.FX_RETURNS:
                    for (int i = 0; i < 8; i++ )
                    {
                        StringBuilder address = new StringBuilder();
                        string baseAddress = "/fxrtn" + '/' + i.ToString().PadLeft(2, '0');
                                               
                        //Get main level
                        address.Append(baseAddress);
                        address.Append("/mix/fader");
                        requests.Add(address.ToString());
                        address.Clear();

                        //Get mute
                        address.Append(baseAddress);
                        address.Append("/mix/on");
                        requests.Add(address.ToString());
                    }
                    break;
                case Constants.FADER_GROUP.BUS_1_8:
                case Constants.FADER_GROUP.BUS_9_16:
                    for (int i = 0; i < 8; i++ )
                    {
                        StringBuilder address = new StringBuilder();
                        string baseAddress = "/bus" + '/' + (i * (int)group).ToString().PadLeft(2, '0');
                                               
                        //Get main level
                        address.Append(baseAddress);
                        address.Append("/mix/fader");
                        requests.Add(address.ToString());
                        address.Clear();

                        //Get mute
                        address.Append(baseAddress);
                        address.Append("/mix/on");
                        requests.Add(address.ToString());
                    }
                    break;
                case Constants.FADER_GROUP.MATRIX_MAIN:
                    for (int i = 0; i < 6; i++ )
                    {
                        
                        StringBuilder address = new StringBuilder();
                        string baseAddress = "/mtx" + '/' + i.ToString().PadLeft(2, '0');
                        //Get main level
                        address.Append(baseAddress);
                        address.Append("/mix/fader");
                        requests.Add(address.ToString());
                        address.Clear();

                        //Get mute
                        address.Append(baseAddress);
                        address.Append("/mix/on");
                        requests.Add(address.ToString());
                        address.Clear();
                    }

                    //Get Main stereo level
                    string mainFader = "/main/st/mix/fader";
                    requests.Add(mainFader);
                    string mainMute = "/main/st/mix/on";
                    requests.Add(mainMute);

                    break;
                case Constants.FADER_GROUP.DCA:
                    for (int i = 0; i < 8; i++ )
                    {
                        StringBuilder address = new StringBuilder();
                        string baseAddress = "/dca" + '/' + i.ToString().PadLeft(2, '0');
                                               
                        //Get main level
                        address.Append(baseAddress);
                        address.Append("/fader");
                        requests.Add(address.ToString());
                        address.Clear();

                        //Get mute
                        address.Append(baseAddress);
                        address.Append("/on");
                        requests.Add(address.ToString());
                    }
                    break;
                    
            }
            //send requests
            foreach(string s in requests)
            {
                OSCMessage msg = new OSCMessage(s);
                await OSCOutPort.SendAsync(msg);
            }
        }

        public void FromLocal()
        {
            //Generate fake received messages

            //raise events
        }
    }
}
