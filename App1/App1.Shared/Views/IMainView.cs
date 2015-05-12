using System;
using System.Collections.Generic;
using System.Text;
using gnow.util.behringer;
using gnow.UI;

namespace gnow.util
{
    interface IMainView : IView
    {
        IEnumerable<float> FaderValues { get; set; }
        IEnumerable<bool> Mutes { get; set; }
        IEnumerable<X32ScribbleStrip> Labels { get; }
        FaderBank Bank { get; set; }

        event FaderValueChangedEventHandler FaderValueChanged;
        event MuteValueChangedEventHandler MuteValueChanged;
    }
}
