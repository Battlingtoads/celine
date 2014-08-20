using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gnow.util.behringer.events
{
    public class MetersReceivedEventArgs : EventArgs
    {
        public byte[] data;
        public Constants.METER_TYPE type;
        
    }

    public delegate void MetersReceivedEventHandler(object sender, MetersReceivedEventArgs e);

    public class ConfigReceivedEventArgs : EventArgs
    {
        public string subAddress;
        public object value;
        public bool fromLocal = false;
    }

    public delegate void ConfigReceivedEventHandler(object sender, ConfigReceivedEventArgs e);

    public class ChannelReceivedEventArgs : EventArgs
    {
        public int channel;
        public string subAddress;
        public object value;
        public bool fromLocal = false;
    }

    public delegate void ChannelReceivedEventHandler(object sender, ChannelReceivedEventArgs e);

    public class AuxinReceivedEventArgs : EventArgs
    {
        public int channel;
        public string subAddress;
        public object value;
        public bool fromLocal = false;
    }

    public delegate void AuxinReceivedEventHandler(object sender, AuxinReceivedEventArgs e);

    public class FXReturnReceivedEventArgs : EventArgs
    {
        public int channel;
        public string subAddress;
        public object value;
        public bool fromLocal = false;
    }

    public delegate void FXReturnReceivedEventHandler(object sender, FXReturnReceivedEventArgs e);

    public class BusReceivedEventArgs : EventArgs
    {
        public int bus;
        public string subAddress;
        public object value;
        public bool fromLocal = false;
    }

    public delegate void BusReceivedEventHandler(object sender, BusReceivedEventArgs e);

    public class MatrixReceivedEventArgs : EventArgs
    {
        public int matrix;
        public string subAddress;
        public object value;
        public bool fromLocal = false;
    }

    public delegate void MatrixReceivedEventHandler(object sender, MatrixReceivedEventArgs e);

    public class MainReceivedEventArgs : EventArgs
    {
        public string subAddress;
        public object value;
        public bool fromLocal = false;
    }

    public delegate void MainReceivedEventHandler(object sender, MainReceivedEventArgs e);

    public class MonoReceivedEventArgs : EventArgs
    {
        public string subAddress;
        public object value;
        public bool fromLocal = false;
    }

    public delegate void MonoReceivedEventHandler(object sender, MonoReceivedEventArgs e);

    public class DCAReceivedEventArgs : EventArgs
    {
        public int dca;
        public string subAddress;
        public object value;
        public bool fromLocal = false;
    }

    public delegate void DCAReceivedEventHandler(object sender, DCAReceivedEventArgs e);

    public class EffectReceivedEventArgs : EventArgs
    {
        public int effect;
        public string subAddress;
        public object value;
        public bool fromLocal = false;
    }

    public delegate void EffectReceivedEventHandler(object sender, EffectReceivedEventArgs e);

    public class MainOutReceivedEventArgs : EventArgs
    {
        public int mainout;
        public string subAddress;
        public object value;
        public bool fromLocal = false;
    }

    public delegate void MainOutReceivedEventHandler(object sender, MainOutReceivedEventArgs e);

    public class AuxOutReceivedEventArgs : EventArgs
    {
        public int auxout;
        public string subAddress;
        public object value;
        public bool fromLocal = false;
    }

    public delegate void AuxOutReceivedEventHandler(object sender, AuxOutReceivedEventArgs e);

    public class P16OutReceivedEventArgs : EventArgs
    {
        public int p16out;
        public string subAddress;
        public object value;
        public bool fromLocal = false;
    }

    public delegate void P16OutReceivedEventHandler(object sender, P16OutReceivedEventArgs e);

    public class AESOutReceivedEventArgs : EventArgs
    {
        public int aesout;
        public string subAddress;
        public object value;
        public bool fromLocal = false;
    }

    public delegate void AESOutReceivedEventHandler(object sender, AESOutReceivedEventArgs e);

    public class RecordOutReceivedEventArgs : EventArgs
    {
        public int recordout;
        public string subAddress;
        public object value;
        public bool fromLocal = false;
    }

    public delegate void RecordOutReceivedEventHandler(object sender, RecordOutReceivedEventArgs e);

    public class HeadampReceivedEventArgs : EventArgs
    {
        public int headamp;
        public string subAddress;
        public object value;
        public bool fromLocal = false;
    }

    public delegate void HeadampReceivedEventHandler(object sender, HeadampReceivedEventArgs e);
}
