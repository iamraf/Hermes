using Hermes.Model.Models;
/* ILoginView interface connect view presenter and view classes
*  it gets data from the repositories 
*  and pass them to view which is implementing this interface
*/
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
