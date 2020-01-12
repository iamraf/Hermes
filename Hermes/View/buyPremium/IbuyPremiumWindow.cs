using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
/* IbuyPremiumWindow interface connect view presenter and view classes
 *  it gets data from the repositories 
 *  and pass them to view which is implementing this interface
 */
namespace Hermes.View.buyPremium
{
    interface IbuyPremiumWindow
    {
        Boolean ReturnOk { get; set; }
    }
}
