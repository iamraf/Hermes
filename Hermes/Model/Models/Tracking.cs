using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.Model.Models
{
    class Tracking
    {
        public int Id { get; }
        public long LastLogin { get; }
        public long LastPasswordChange { get; }

        public Tracking(int id, long lastLogin, long lastPasswordChange)
        {
            Id = id;
            LastLogin = lastLogin;
            LastPasswordChange = lastPasswordChange;
        }
    }
}
