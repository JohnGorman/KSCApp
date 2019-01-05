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
using KSCApp.Services;

namespace KSCApp.Pages.Members.Players
{
    public class IndexModel : BasePageModel
    {
        private readonly ITeamPlayerService _teamPlayerService;

        public IList<TeamPlayer> TeamPlayers { get; set; }
        public IQueryable<TeamPlayer> TeamPlayersIQ { get; set; }

        public string NameSort { get; set; }
        public string PointSort { get; set; }
        public string TeamSort { get; set; }
        public string LevelSort { get; set; }

        public IndexModel(ApplicationDbContext context, ITeamPlayerService teamPlayerService) : base(context)
        {
            _teamPlayerService = teamPlayerService;
        }


        public async Task OnGetAsync(string sortOrder)
        {


        SetCurrentLeague();

            NameSort = String.IsNullOrEmpty(sortOrder) ? "Name_desc" : "";

            PointSort = sortOrder == "Points_desc" ? "Points" : "Points_desc";

            TeamSort = sortOrder == "Team" ? "Team_desc" : "Team";

            LevelSort = sortOrder == "Level" ? "Level_desc" : "Level";


            TeamPlayers = await _teamPlayerService.GetTeamPlayersForLeagueAsync(LeagueSelectVM.SelectedLeague.LeagueId);

            TeamPlayersIQ = TeamPlayers.AsQueryable();

            switch (sortOrder)
            {
                case "Name_desc":                   
                    TeamPlayersIQ = TeamPlayersIQ.OrderByDescending(tp => tp.Player.PlayerName);
                    break;
                case "Points":
                    TeamPlayersIQ = TeamPlayersIQ.OrderBy(tp => tp.GamesWon);
                    break;
                case "Points_desc":
                    TeamPlayersIQ = TeamPlayersIQ.OrderByDescending(tp => tp.GamesWon);
                    break;
                case "Team":
                    TeamPlayersIQ = TeamPlayersIQ.OrderBy(tp => tp.Team.TeamNo);
                    break;
                case "Team_desc":
                    TeamPlayersIQ = TeamPlayersIQ.OrderByDescending(tp => tp.Team.TeamNo);
                    break;
                case "Level":
                    TeamPlayersIQ = TeamPlayersIQ.OrderBy(tp => tp.Level).ThenByDescending(tp=>tp.GamesWon);
                    break;
                case "Level_desc":
                    TeamPlayersIQ = TeamPlayersIQ.OrderByDescending(tp => tp.Level);
                    break;
                default:
                    TeamPlayersIQ = TeamPlayersIQ.OrderBy(tp => tp.Player.PlayerName);
                    break;
            }

            TeamPlayers = TeamPlayersIQ.AsNoTracking().ToList();

        }
}
}
