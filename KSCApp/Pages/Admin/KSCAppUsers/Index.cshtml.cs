using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using KSCApp.Models;
using KSCApp.ViewModels;

namespace KSCApp.Pages.Admin.KSCAppUsers
{
    public class IndexModel : PageModel
    {
        private readonly KSCApp.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public IndexModel(KSCApp.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public IList<UserAndRoleViewModel> AllUsersAndRoles { get; set; }

        public async Task OnGetAsync()
        {
            var AllUsers = _context.Users.OrderBy(u=>u.UserName).ToList();

            AllUsersAndRoles = new List<UserAndRoleViewModel>();


            foreach (var usr in AllUsers)
            {
                var model = new UserAndRoleViewModel
                {
                    UserId = usr.Id,

                    UserName = usr.UserName,

                    Roles = await _userManager.GetRolesAsync(usr)
                };

                AllUsersAndRoles.Add(model);
            }
        }
    }
}