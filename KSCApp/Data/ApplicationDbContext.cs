using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using KSCApp.Models;

namespace KSCApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<KSCApp.Models.League> League { get; set; }
        public DbSet<KSCApp.Models.Team> Team { get; set; }
        public DbSet<KSCApp.Models.Player> Player { get; set; }
        public DbSet<KSCApp.Models.Fixture> Fixture { get; set; }
        public DbSet<KSCApp.Models.TeamPlayer> TeamPlayer { get; set; }
        public DbSet<KSCApp.Models.Match> Match { get; set; }
        public DbSet<KSCApp.Models.GameResult> GameResult { get; set; }
        public DbSet<KSCApp.Models.RankHistory> RankHistory { get; set; }
        public DbSet<KSCApp.Models.LevelTime> LevelTime { get; set; }
        public DbSet<KSCApp.Models.KSCAppUser> KSCAppUser { get; set; }
        public DbSet<KSCApp.Models.MatchSlot> MatchSlot { get; set; }
        public DbSet<KSCApp.Models.FixtureDate> FixtureDate { get; set; }
    }
}
