using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KSCApp.Data;
using KSCApp.Models;
using KSCApp.ViewModels;

namespace KSCApp.Pages.Admin.KSCAppUsers
{
    public class DeleteModel : PageModel
    {
        private readonly KSCApp.Data.ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public DeleteModel(KSCApp.Data.ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public UserAndRoleViewModel userAndRoleViewModel { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usr = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (usr == null)
            {
                return NotFound();
            }

            //userAndRoleViewModel = new UserAndRoleViewModel();

            //userAndRoleViewModel.UserId = usr.Id;
            //userAndRoleViewModel.UserName = usr.UserName;
            //userAndRoleViewModel.Roles = await _userManager.GetRolesAsync(usr);

            userAndRoleViewModel = new UserAndRoleViewModel
            {
                UserId = usr.Id,
                UserName = usr.UserName,
                Roles = await _userManager.GetRolesAsync(usr)
            };

            //userAndRoleViewModel = Model;

            return Page();
        }


        public async Task<IActionResult> OnPostAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usr = await _context.Users.FindAsync(id);



            if (usr != null)
            {
                userAndRoleViewModel = new UserAndRoleViewModel
                {
                    UserId = usr.Id,
                    UserName = usr.UserName,
                    Roles = await _userManager.GetRolesAsync(usr)
                };

                foreach (var role in userAndRoleViewModel.Roles)
                {
                    await _userManager.RemoveFromRoleAsync(usr, role);
                }
                

                //_context.Player.Remove(Player);
                //await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
