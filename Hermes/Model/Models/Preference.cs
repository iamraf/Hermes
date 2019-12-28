using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hermes.Model.Models
{
    class Preference
    {
        public int Id { get; }
        public bool ReceiveEmail { get; }

        public Preference(int id, bool receiveEmail)
        {
            Id = id;
            ReceiveEmail = receiveEmail;
        }
    }
}
