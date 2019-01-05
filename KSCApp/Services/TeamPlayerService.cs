using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KSCApp.Data;
using KSCApp.Models;
using Microsoft.EntityFrameworkCore;

namespace KSCApp.Services
{
    public class TeamPlayerService : ITeamPlayerService
    {
        private readonly ApplicationDbContext _context;

        public TeamPlayerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IList<TeamPlayer>> GetAllTeamPlayersAsync()
        {
            return await _context.TeamPlayer.ToListAsync();
        }

        public async Task<IList<TeamPlayer>> GetTeamPlayersForLeagueAsync(int leagueId)
        {
            return await _context.TeamPlayer
                .Where(tp=>tp.Team.LeagueId==leagueId)
                .Include(p=>p.Player)
                .Include(t=>t.Team)
                .ToArrayAsync();
        }

        public async Task<IList<TeamPlayer>> GetTeamPlayersForTeamAsync(int teamId)
        {
            return await _context.TeamPlayer
                .Where(tp => tp.TeamId == teamId)
                .Include(p => p.Player)
                .Include(t => t.Team)
                .ToArrayAsync();
        }
    }
}
