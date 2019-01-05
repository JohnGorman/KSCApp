using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KSCApp.Models;

namespace KSCApp.Services
{
    public interface ITeamPlayerService
    {

        Task<IList<TeamPlayer>> GetTeamPlayersForLeagueAsync(int leagueId);

        Task<IList<TeamPlayer>> GetAllTeamPlayersAsync();

        Task<IList<TeamPlayer>> GetTeamPlayersForTeamAsync(int TeamId);

    }
}
