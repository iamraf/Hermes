using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.Model.Models
{
    public class User
    {
        public int Id { get; }
        public string Username { get; }
        public string Password { get; }
        public string Name { get; }
        public string Surname { get; }
        public string Address { get; }
        public string Email { get; }
        public string Telephone { get; }

        public User(int id, string username, string password, string name, string surname, string address, string email, string telephone)
        {
            Id = id;
            Username = username;
            Password = password;
            Name = name;
            Surname = surname;
            Address = address;
            Email = email;
            Telephone = telephone;
        }
    }
}
