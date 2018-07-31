using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KSCApp.Data;
using KSCApp.Models;
using KSCApp.ViewModels;

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
        public IList<PlayerVM> lplayerVM { get; set; }

        public void OnGet()
        {
            //Player = await _context.Player
            //                        .Include(u=>u.KSCAppUser).ToListAsync();

            //List<PlayerVM> lplayerVM = new List<PlayerVM>();

            lplayerVM = (from player in _context.Player join user in _context.Users on player.UserId equals user.Id into pvm from con in pvm.DefaultIfEmpty() select new PlayerVM
            {
                Id = player.PlayerId,
                PlayerName = player.PlayerName,
                PlayerStatus = player.PlayerStatus,
                PlayingLeague = player.PlayingLeague,
                ProfilePicture = player.ProfilePicture,
                ContactNo = player.ContactNo,
                PlayerType = player.PlayerType,
                Rank = player.Rank,
                Email = con.Email
            }).ToList();

        }
    }
}
