using System.Collections.Generic;

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
