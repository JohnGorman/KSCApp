using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using KSCApp.Models;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using KSCApp.ViewModels;

namespace KSCApp.Pages
{
    public class ResultsModel : BasePageModel
    {
        public IList<MatchResultVM> ResultList { get; set; }

        public ResultsModel(KSCApp.Data.ApplicationDbContext context) : base(context)
        {
        }

        public async Task OnGetAsync()
        {
            SetCurrentLeague();

            ResultList = await _context.Match.Where(m => m.PlayedDate != null && m.Fixture.LeagueId == LeagueSelectVM.SelectedLeague.LeagueId)
                .Include(m => m.Fixture)
                .Include(m => m.Fixture.TeamA)
                .Include(m => m.Fixture.TeamB)
                .Include(m => m.PlayerA)
                .Include(m => m.PlayerB)
                .Include(m => m.GameResults)
                .OrderByDescending(m=> m.PlayedDate)
                .Select(u => new MatchResultVM
                {
                    FixtureDetails = u.Fixture.TeamA.TeamName + " v " + u.Fixture.TeamB.TeamName,
                    MatchDetails = String.Format("{0} ({1}) v {2} ({3})", u.PlayerA.PlayerName, u.PlayerAgames, u.PlayerB.PlayerName, u.PlayerBgames),
                    GameResults = "",
                    MatchId = u.MatchId,
                    DatePlayed = (u.PlayedDate ?? DateTime.Now).ToString("dd/MM/yyy")
                })
                .AsNoTracking()
                .ToListAsync();


            foreach (var res in ResultList)
            {
                string gameResults = "(";
                var games = _context.GameResult.Where(g => g.MatchId == res.MatchId);
                foreach (var game in games)
                {
                    gameResults += string.Format(" {0}-{1},", game.PlayerApoints, game.PlayerBpoints);
                }
                gameResults = gameResults.Remove(gameResults.Length - 1);
                gameResults += " )";
                res.GameResults = gameResults;
            }
        }

    }
}