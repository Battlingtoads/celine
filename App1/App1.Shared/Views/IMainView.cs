using System;
using System.Collections.Generic;
using System.Text;
using gnow.util.behringer;

namespace gnow.util
{
    interface IMainView : IView
    {
        IEnumerable<float> FaderValues { get; }
        IEnumerable<bool> Mutes { get; }
        IEnumerable<X32ScribbleStrip> Labels { get; }
        FaderBank Bank { get; set; }

        event FaderValueChangedEventHandler FaderValueChanged;
        event MuteValueChangedEventHandler MuteValueChanged;
    }
}
