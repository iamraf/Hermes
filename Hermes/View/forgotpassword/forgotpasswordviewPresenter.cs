using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using Hermes.Model;
using Hermes.Model.Models;
using Hermes.Util;


namespace Hermes.View.forgotpassword
{
    class ForgotPasswordviewPresenter
    {
        private readonly IForgotPasswordView _view;
        private readonly MyProfileRepository _repository;
        private readonly LoginRepository _loginRepository;

        public ForgotPasswordviewPresenter(IForgotPasswordView view)
        {
            _view = view;
            _repository = new MyProfileRepository();
        }

        public User GetCurrentUser()
        {
            return (User)MemoryCache.Default["User"];
        }

        public User GetUserByEmail(String email)
        {
            return _loginRepository.GetUser(email);
        }

        public void UpdateUsersPassword(String newPass)
        {
            User user = new User(GetCurrentUser().Id, GetCurrentUser().Username, HashingHelper.HashPassword(newPass), GetCurrentUser().Name, GetCurrentUser().Surname, GetCurrentUser().Address, GetCurrentUser().Email, GetCurrentUser().Telephone);
            bool result = _repository.EditUser(user);
        }
    }
}