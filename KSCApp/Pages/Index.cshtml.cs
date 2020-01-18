using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using KSCApp.Models;
using KSCApp.Services;
using KSCApp.ViewModels;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using KSCApp.Data;
using Microsoft.Extensions.Configuration;


namespace KSCApp.Pages
{
    public class IndexModel : BasePageModel
    {
        private readonly ITeamPlayerService _teamPlayerService;
        private readonly IConfiguration _configuration;

        public IList<TeamVM> LeagueTableA { get; set; }
        public IList<TeamVM> LeagueTableB { get; set; }
        //public string Message;

        public IndexModel(KSCApp.Data.ApplicationDbContext context, ITeamPlayerService teamPlayerService, IConfiguration configuration) : base(context)
        {
            _teamPlayerService = teamPlayerService;
            _configuration = configuration;
        }

        public async Task OnGetAsync()
        {
            //Message = "My key val = " + _configuration["SecretDataConnection"];

            SetCurrentLeague();

            var teamPlayers = await _teamPlayerService.GetTeamPlayersForLeagueAsync(LeagueSelectVM.SelectedLeague.LeagueId);

            var tempLeagueTable = teamPlayers.GroupBy(t => t.Team)
                            .Select(ct => new TeamVM
                            {
                                TeamId = ct.First().Team.TeamId,
                                TeamName = ct.First().Team.TeamName,
                                TeamNo = ct.First().Team.TeamNo,
                                Section = ct.First().Team.Section,
                                MatchesPlayed = ct.Sum(c => c.MatchesPlayed),
                                MatchesWon = ct.Sum(c => c.MatchesWon),
                                GamesWon = ct.Sum(c => c.GamesWon),
                                GamesLost = ct.Sum(c => c.GamesLost)
                            }).OrderByDescending(c => c.GamesWon).ToList();

            LeagueTableA = tempLeagueTable.Where(t => t.Section == "A").ToList();
            LeagueTableB = tempLeagueTable.Where(t => t.Section == "B").ToList();

        }
    }
}

