using gnow.util.behringer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace gnow.util
{
    public interface IDetailView : IView
    {
        float Gain { get; set; }
        float GateThreshold { get; set; }
        float DynamicThreshold { get; set; }
        bool PhantomPower { get; set; }
        bool Phase { get; set; }
        bool Link { get; set; }
        int SelectedChannelIndex { get; set; }
        Constants.COMPONENT_TYPE SelectedChannelType { get; set; }
    }
}
