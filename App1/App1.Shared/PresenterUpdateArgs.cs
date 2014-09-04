using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Controls;
using gnow.util.behringer;

namespace gnow.util
{
    public class PresenterUpdateArgs
    {
        public enum MAJOR_TYPE
        {
            CONFIG, DELAY, PREAMP, GATE, DYNAMIC, INSERT, EQ, MIX, GROUP
        }

    }
}
