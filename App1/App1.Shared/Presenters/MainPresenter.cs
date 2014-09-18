using System;
using System.Collections.Generic;
using System.Text;

namespace gnow.util
{
    class MainPresenter : Presenter
    {
        public MainPresenter(IMainView view)
        {
            this.view = view;
        }
    }
}
