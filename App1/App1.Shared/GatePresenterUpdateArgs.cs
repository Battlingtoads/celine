
using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Controls;
using gnow.util.behringer;

namespace gnow.util
{
    public class GatePresenterUpdateArgs
    {
        GatePresenterUpdateArgs()
        {
        }
        public enum SUB_TYPE
        {
            ON, MODE, THRESHOLD, RANGE, ATTACK, HOLD, RELEASE, KEYSOURCE, FILTER_ON,
            FILTER_TYPE, FILTER_FREQ
        }
        public object value;
    }
}
