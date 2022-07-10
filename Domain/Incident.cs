using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class Incident
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<Account> Accounts { get; set; }
    }
}