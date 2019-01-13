using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace KSCApp.Models
{
    public class KSCAppUser : IdentityUser
    {
        public KSCAppUser()
        {
            //Players = new HashSet<Player>();
        }

        public string Name { get; set; }
        public string PhoneNo { get; set; }


        public ICollection<Player> Players { get; set; }
    }
}
