using System.Collections.Generic;

namespace Hermes.View.profile
{
    interface IProfileView
    {
        string Username { set; get; }
        string Password1 { set; get; }
        string Password2 { set; get; }
        string Email { set; get; }
        string Name { set; get; }
        string Surname { set; get; }
        string Telephone { set; get; }
        List<string> Locations { set;  }
        string SetSelectedLocation { set; }
        List<string> LocationsTK { set; }
        string SetSelectedLocationTK { set; }
        string SelectedLocationTK { get; }
        string ErrorDialog { set; }
        string WarningDialog { set; }
        bool Navigate { set; }
    }
}
