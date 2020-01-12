using System.Collections.Generic;
/* IRegisterView interface connect view presenter and view classes
 *  it gets data from the repositories 
 *  and pass them to view which is implementing this interface
 */
namespace Hermes.View.register
{
    interface IRegisterView
    {
        string ErrorDialog { set; }
        List<string> Locations { set; }
        List<string> LocationsTK { set; }
        bool Navigate { set; }
    }
}
