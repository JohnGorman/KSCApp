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

        public IList<PlayerVM> lplayerVM { get; set; }

        public string NameSort { get; set; }
        public string RankSort { get; set; }

        public async Task OnGetAsync(string sortOrder)
        {
            NameSort = String.IsNullOrEmpty(sortOrder) ? "Name_desc" : "";
            RankSort = sortOrder == "Rank" ? "Rank_desc" : "Rank";

            IQueryable<PlayerVM> lplayerVMIQ = (from player in _context.Player join user in _context.Users on player.UserId equals user.Id into pvm from con in pvm.DefaultIfEmpty() select new PlayerVM
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
            });


            switch (sortOrder)
            {
                case "Name_desc":
                    lplayerVMIQ = lplayerVMIQ.OrderByDescending(p => p.PlayerName);
                    break;
                case "Rank":
                    lplayerVMIQ = lplayerVMIQ.OrderBy(p => p.Rank);
                    break;
                case "Rank_desc":
                    lplayerVMIQ = lplayerVMIQ.OrderByDescending(p => p.Rank);
                    break;
                default:
                    lplayerVMIQ = lplayerVMIQ.OrderBy(p => p.PlayerName);
                    break;
            }

            lplayerVM = await lplayerVMIQ.AsNoTracking().ToListAsync();

        }
    }
}
