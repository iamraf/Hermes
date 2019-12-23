using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.View
{
    public interface IDemoView
    {
        string LabelText { set; get; }

        string ErrorDialog { set; }
    }
}
