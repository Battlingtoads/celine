using System;
using System.Collections.Generic;
using System.Text;
using gnow.util.behringer;

namespace gnow.util
{
    interface IDynamicView : IView
    {
        float Threshold { get; set; }
        float Ratio { get; set; }
        float Gain { get; set; }
        float Attack { get; set; }
        float Hold { get; set; }
        float Release { get; set; }
        int SelectedChannelIndex { get; set; }
        Constants.COMPONENT_TYPE SelectedChannelType { get; set; }
    }
}
