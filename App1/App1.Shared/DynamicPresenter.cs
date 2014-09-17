using System;
using System.Collections.Generic;
using System.Text;

namespace gnow.util
{
    class DynamicPresenter : Presenter
    {
        public DynamicPresenter(IGateView view)
        {
            this.view = (IGateView)view;
        }
    }
}
