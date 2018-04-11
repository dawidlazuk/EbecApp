using System;
using System.Linq;

using EbecShop.Model;
using EbecShop.DataAccess.Repositiories;
using EbecShop.DataAccess.Repositiories.Interfaces;
using EbecShop.DataAccess;

namespace ConsoleApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                ITeamRepository teamRepository = unitOfWork.Teams;

                teamRepository.Add(new Team
                {
                    Name = "DodawanyProcedurą"
                });

                var teams = teamRepository.GetAll();

                for (int i = 1; i <= teams.Count(); ++i)
                {
                    var team = teamRepository.GetTeam(i);
                    Console.WriteLine($"Id: {team.Id}, Name: {team.Name}");
                    Console.WriteLine($"Single select: {teamRepository.Find(team.Id).Name}");
                }
            }
        }
    }
}
