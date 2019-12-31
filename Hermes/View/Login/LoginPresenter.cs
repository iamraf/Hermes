using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hermes.Model;
using Hermes.Model.Models;
using Hermes.View;

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

            if(user == null)
            {
                _view.ErrorDialog = "Login failed";
            }
            else
            {
                _view.LoggedUser = user;
            }
        }
    }
 }

    

