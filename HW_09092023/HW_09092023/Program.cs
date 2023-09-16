using HW_09092023.Types.FootballTournament;
using HW_09092023.Types.FootballTournament.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace HW_09092023
{
    internal class Program
    {
        private static string connectionString => ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();

        private static string GetSelectString(string tbName) => $"select * from {tbName}";

        static void Main(string[] args)
        {

            using(var context = new FootballTournamentContext())
            {

                //var entity = new TeamInfo() 
                //{   
                //    Name = "Wolfs", 
                //    City = "Kyiv", 
                //    Defeats = 2, 
                //    GamesDrawn = 5, 
                //    Victories = 12 };

                //context.Teams.Add(entity);
                //context.SaveChanges();


                var teams = context.Teams.ToList();

                foreach (var team in teams)
                {
                    Console.WriteLine($"TeamId: {team.Id}\n"+
                    $"Name: { team.Name},\n" +
                    $"City: {team.City},\n" +
                    $"Victories: {team.Victories},\n" +
                    $"Defeats: {team.Defeats},\n" +
                    $"Games drawn: {team.GamesDrawn}\n");
                }
            }
        }
    }
}