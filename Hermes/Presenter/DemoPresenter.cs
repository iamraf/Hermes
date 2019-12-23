using Hermes.Model;
using Hermes.View;

namespace Hermes.Presenter
{
    class DemoPresenter
    {
        private IDemoView _view;
        private readonly Repository _repository;

        public DemoPresenter(IDemoView view)
        {
            _view = view;
            _repository = new Repository();
        }

        public void LoadName()
        {
            string result = _repository.LoadName();

            if(result != null)
            {
                _view.LabelText = result;
            }
            else
            {
                _view.ErrorDialog = "Could not load data";
            }
        }
    }
}
