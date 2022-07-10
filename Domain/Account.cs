using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class Account
    {
        public Guid Id { get; set; }

        public string name { get; set; }

        public ICollection<Contact> Contacts { get; set; }

        public Incident Incident { get; set; }
        public string IncidentName { get; set; }
    }
}