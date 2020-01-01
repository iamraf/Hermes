using Hermes.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.View
{
    public interface ILoginpage
    {
        string LabelUsername { set; get; }
        string LabelPassword { set; get; }
        User LoggedUser { set; }
        string ErrorDialog { set; }
    }
}
