using Hermes.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.View.MyProfile
{
    interface IProfilePage
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
    }
}
