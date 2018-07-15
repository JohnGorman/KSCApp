using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KSCApp.Data;
using KSCApp.Models;

namespace KSCApp.Pages.Admin.Players
{
    public class IndexModel : PageModel
    {
        private readonly KSCApp.Data.ApplicationDbContext _context;

        public IndexModel(KSCApp.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Player> Player { get;set; }

        public async Task OnGetAsync()
        {
            Player = await _context.Player.ToListAsync();


            //var userlist = _context.Users.Select(u => new
            //{
            //    Id = u.Id,
            //    Email = u.Email
            //}).ToList();

            //var PlayerView = _context.Player
            //                            .Join(userlist, 
            //                                    p=>p.UserId,
            //                                    u=>u.Id,
            //                                    (p, u)=> new
            //                                    {
            //                                        PlayerID = p.PlayerId,
            //                                        PlayerName = p.PlayerName,
            //                                        PlayerStatus = p.PlayerStatus,
            //                                        PlayingLeague = p.PlayingLeague,
            //                                        PlayerType = p.PlayerType,
            //                                        ContactNo = p.ContactNo,
            //                                        PlayerEmail = u.Email

            //                                    });
        }
    }
}
