﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using KSCApp.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KSCApp.Data;
using KSCApp.Models;
using KSCApp.Services;


namespace KSCApp.Pages
{
    public class TeamsModel : PageModel
    {
        //private readonly ITeamPlayerService _teamPlayerService;

        private readonly ApplicationDbContext _context;

        public TeamsModel(ApplicationDbContext context)
        {
            _context = context;
            //_teamPlayerService = teamPlayerService;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var SelectedTeam = await _context.Team.FirstOrDefaultAsync(t => t.TeamId == id);

            TeamDetails = SelectedTeam.TeamName;

            int LeagueId = SelectedTeam.LeagueId;

            var SelectedLeague = await _context.League.FirstOrDefaultAsync(l => l.LeagueId == LeagueId);

            LeagueDetails = SelectedLeague.LeagueName;

            //Team = await _teamPlayerService.GetTeamPlayersForTeamAsync(id);

            Team = await _context.TeamPlayer.Where(tp => tp.TeamId == id)
                                            .Include(p => p.Player)
                                            .OrderBy(t => t.Level)
                                            .ToListAsync();


            if (Team == null)
            {
                return NotFound();
            }

            return Page();


        }

        [BindProperty]
        public IList<TeamPlayer> Team { get; set; }
        public string TeamDetails { get; set; }
        public string LeagueDetails { get; set; }




    }
}