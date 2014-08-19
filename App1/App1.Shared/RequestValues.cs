using System;
using System.Collections.Generic;
using System.Text;
using App1;
using gnow.util.osc;
using System.Threading.Tasks;
using gnow.util.behringer;
using System.IO;

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
                await OSCOutPort.Instance.SendAsync(msg);
            }
        }

        public static void FromLocal(Constants.FADER_GROUP group)
        {
            //Generate fake received messages
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
                        string baseAddress = "/ch" + '/' + (1 + i + 8 * (int)group).ToString().PadLeft(2, '0');
                                               
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
                    for (int i = 1; i < 9; i++ )
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
                    for (int i = 1; i < 9; i++ )
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
                        int multiplier = group == Constants.FADER_GROUP.BUS_1_8 ? 0 : 1;
                        string baseAddress = "/bus" + '/' + (1 + i + 8 * multiplier).ToString().PadLeft(2, '0');
                                               
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
                    for (int i = 1; i < 7; i++ )
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
                if (s.EndsWith("on"))
                {
                    int rand = (new Random(DateTime.Now.Millisecond).Next(0,13))%2;
                    OSCMessage msg = new OSCMessage(s, (oscInt)rand);
                    OSCInPort.Instance.RaiseEventFake(msg);
                }

                else
                {
                    float rand = (new Random(DateTime.Now.Millisecond).Next(0,100));
                    OSCMessage msg = new OSCMessage(s, (oscFloat)(rand/100f));
                    OSCInPort.Instance.RaiseEventFake(msg);
                }
                new System.Threading.ManualResetEvent(false).WaitOne(2);
            }

        }
        public static void FromLocal(Constants.METER_TYPE type)
        {
            List<float> temp = new List<float>();
            Random randGen = new Random(DateTime.Now.Millisecond);
            for(int i = 0; i < 70; i++)
            {
                temp.Add((float)randGen.NextDouble());
            }

            MemoryStream toSend = new MemoryStream();
            for(int i = 0; i < 70; i++)
            {
                toSend.Write(BitConverter.GetBytes(temp[i]), 0, 4);
            }
            OSCMessage msg = new OSCMessage("/meters/0", (oscStream)toSend);
            OSCInPort.Instance.RaiseEventFake(msg);
        }
        public static void FromOSC(Constants.METER_TYPE type)
        {
            //TODO Implement this ya dummy
        }
    }
}
