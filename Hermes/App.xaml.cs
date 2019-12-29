using Hermes.Model;
using Hermes.Model.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Hermes.View;
namespace Hermes
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {

            List<User> list = new Repository().GetUsers();

            foreach(User user in list)
            {
                Console.WriteLine(user.Name);
            }
        }
    }
}
