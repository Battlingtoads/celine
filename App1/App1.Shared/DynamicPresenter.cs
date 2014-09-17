using System;
using System.Collections.Generic;
using System.Text;

namespace gnow.util
{
    class DynamicPresenter : Presenter
    {
        public DynamicPresenter(IDynamicView view)
        {
            this.view = (IDynamicView)view;
        }
    }
}
