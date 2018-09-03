using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using KSCApp.Data;
using KSCApp.Models;
using KSCApp.ViewModels;

namespace KSCApp.Pages.Admin.KSCAppUsers
{
    public class CreateModel : PageModel
    {
        private readonly KSCApp.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public CreateModel(KSCApp.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public CreateUserRoleVM createUserRoleVM { get; set; }

        public IActionResult OnGet()
        {
            var userlist = _context.Users.Select(u => new
            {
                Id = u.Id,
                Email = u.Email
            }).ToList();

            ViewData["SelectUserId"] = new SelectList(userlist, "Id", "Email");

            List<RolesVM> roleList = new List<RolesVM>();

            RolesVM rolevm1 = new RolesVM
            {
                Id = 1,
                Name = "Admin"
            };

            RolesVM rolevm2 = new RolesVM
            {
                Id = 2,
                Name = "Member"
            };

            roleList.Add(rolevm1);
            roleList.Add(rolevm2);

            ViewData["SelectRole"] = new SelectList(roleList, "Id", "Name");


            return Page();
        }



        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var user = _context.Users.FirstOrDefault(u => u.Id == createUserRoleVM.UserId);

            string strRole;

            if (createUserRoleVM.RoleId == 1)
                strRole = "Admin";
            else
                strRole = "Member";

            await _userManager.AddToRoleAsync(user, strRole);


            return RedirectToPage("./Index");
        }
    }
}