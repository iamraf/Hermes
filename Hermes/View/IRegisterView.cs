using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.View
{
    interface IRegisterView
    {
        string TextBoxUsername { get; }
        string TextBoxPassword1 { get; }
        string TextBoxPassword2 { get; }
        string TextBoxEmail { get; }
        string TextBoxName { get; }
        string TextBoxPhoneNumber { get; }
        string TextBoxAddress { get; }
        string ErrorDialog { set; }
    }
}
