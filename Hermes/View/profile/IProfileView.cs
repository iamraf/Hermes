using System.Collections.Generic;
/* IProfileView interface connect view presenter and view classes
 *  it gets data from the repositories 
 *  and pass them to view which is implementing this interface
 */
namespace Hermes.View.profile
{
    interface IProfileView
    {
        string Username { set; get; }
        string PasswordEditUser { set; get; }
        string PasswordEditPassword { set; get; }
        string PasswordEditPasswordNew1 { set; get; }
        string PasswordEditPasswordNew2 { set; get; }
        string Email { set; get; }
        string Name { set; get; }
        string Surname { set; get; }
        string Telephone { set; get; }
        List<string> Locations { set;  }
        string SetSelectedLocation { set; }
        int SetSelectedLocationIndex { set; }
        List<string> LocationsTK { set; }
        string SetSelectedLocationTK { set; }
        int SetSelectedLocationTKIndex { set; }
        string SelectedLocationTK { get; }
        string ErrorDialog { set; }
        string WarningDialog { set; }
        bool Navigate { set; }
    }
}
