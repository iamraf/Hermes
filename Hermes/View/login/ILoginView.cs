using Hermes.Model.Models;

namespace Hermes.View.login
{
    public interface ILoginView
    {
        string LabelUsername { set; get; }
        string LabelPassword { set; get; }
        User LoggedUser { set; }
        string ErrorDialog { set; }
    }
}
