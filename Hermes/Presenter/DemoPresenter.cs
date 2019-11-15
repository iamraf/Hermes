using Hermes.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.Presenter
{
    class DemoPresenter
    {
        private IDemoView _view;

        public DemoPresenter(IDemoView view)
        {
            _view = view;
        }

        public void SetLabelText(string text)
        {
            _view.LabelText = text;
        }
    }
}
