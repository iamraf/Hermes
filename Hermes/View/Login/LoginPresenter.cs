using System;
using Hermes.Model;
using Hermes.Model.Models;
using System.Runtime.Caching;
/* LoginPresenter connect view with model classes
 *  it gets data from the repositories 
 *  and pass them to view
 */

namespace Hermes.View.login
{
    class LoginPresenter
    {
        private readonly ILoginView _view;
        private readonly LoginRepository _repository;

        public LoginPresenter(ILoginView view)
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
                Cache.Add("User", user, DateTime.Now.AddDays(30));
                _view.LoggedUser = user;
            }
        }
    }
 }

    

