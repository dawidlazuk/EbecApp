using System;
using System.Linq;

using EbecShop.Model;
using EbecShop.DataAccess.Repositiories;
using EbecShop.DataAccess.Repositiories.Interfaces;

namespace ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ITeamRepository teamRepository = new TeamRepository();

            teamRepository.Add(new Team
            {
                Name = "DodawanyProcedurą"
            });

            var teams = teamRepository.GetAll();                      
           
            for(int i = 1; i <= teams.Count(); ++i)
            {
                var team = teamRepository.GetFullTeam(i);
                Console.WriteLine($"Id: {team.Id}, Name: {team.Name}");
                Console.WriteLine($"Single select: {teamRepository.Find(team.Id).Name}");
            }
        }
    }
}
