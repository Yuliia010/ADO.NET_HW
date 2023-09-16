using HW_09092023.Types.FootballTournament.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_09092023.Types.FootballTournament
{
    internal class FootballTournamentContext : DbContext
    {
        private static string connectionString => ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();

        public DbSet<TeamInfo> Teams { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TeamInfo>().HasData(
                new TeamInfo()
                {
                    Id = 1,
                    Name = "Wolfs",
                    City = "Kyiv",
                    Defeats = 2,
                    GamesDrawn = 5,
                    Victories = 12
                },
                new TeamInfo()
                {
                    Id = 2,
                    Name = "Cats",
                    City = "Lviv",
                    Defeats = 5,
                    GamesDrawn = 2,
                    Victories = 7
                },
                new TeamInfo()
                {
                    Id = 3,
                    Name = "Devs",
                    City = "Rivne",
                    Defeats = 1,
                    GamesDrawn = 5,
                    Victories = 9
                }
                );
            base.OnModelCreating(modelBuilder);
        }
    }
}
