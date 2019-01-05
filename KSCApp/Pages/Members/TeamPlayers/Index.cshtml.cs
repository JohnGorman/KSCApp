using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using KSCApp.Data;
using KSCApp.Models;
using KSCApp.Services;

namespace KSCApp.Pages.Members.TeamPlayers
{
    public class IndexModel : PageModel
    {
        private readonly ITeamPlayerService _teamPlayerService;

        public IndexModel(ITeamPlayerService teamPlayerService)
        {

            _teamPlayerService = teamPlayerService;
        }

        public IList<TeamPlayer> TeamPlayer { get;set; }

        public async Task OnGetAsync()
        {
            TeamPlayer = await _teamPlayerService.GetTeamPlayersForLeagueAsync(21);
        }
    }
}
