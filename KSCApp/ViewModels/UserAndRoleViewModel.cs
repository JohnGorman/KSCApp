using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KSCApp.ViewModels
{
    public class UserAndRoleViewModel
    {
        public string UserId { get; set; }

        public string UserName { get; set; }

        public IList<string> Roles { get; set; }

        //public UserAndRoleViewModel()
        //{
        //    UserId = "none";
        //    UserName = "none";

        //}
    }

}
