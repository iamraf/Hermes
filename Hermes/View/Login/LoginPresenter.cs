using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hermes.Model;
using Hermes.Model.Models;
using Hermes.View;
using System.Runtime;
using System.Security.AccessControl;
using System.Runtime.Caching;

namespace Hermes.View
{
    class LoginPresenter
    {
        private readonly ILoginpage _view;
        private readonly LoginRepository _repository;

        public LoginPresenter(ILoginpage view)
        {
            _view = view;
            _repository = new LoginRepository();
        }

        public void UserLogin()
        {
            string username = _view.LabelUsername;
            string password = _view.LabelPassword;

            User user = _repository.LoginUser(username, password);

            ObjectCache Cache = MemoryCache.Default;
            

            if (user == null)
            {
                _view.ErrorDialog = "Login failed";
            }
            else
            {
                //cache obj
                Cache.Add("User", user, DateTime.Now.AddDays(30));

                //Testing
                //User user1 = (User)Cache["User"];
                //Console.WriteLine(user1.Name);
                _view.LoggedUser = user;
            }
        }
    }
 }

    

