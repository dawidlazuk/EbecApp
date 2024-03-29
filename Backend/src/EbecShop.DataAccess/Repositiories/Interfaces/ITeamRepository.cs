﻿using System.Collections.Generic;
using System.Data;
using EbecShop.Model;

namespace EbecShop.DataAccess.Repositiories.Interfaces
{
    public interface ITeamRepository
    {
        Team Get(int id);
        IEnumerable<Team> GetAll();
        Team Add(Team team);
        Team Update(Team team);
        void Remove(int id);

        Team GetTeam(int id);
        void Save(Team team);

        IDictionary<Product, decimal> GetTeamLimits(int teamId);
        IDictionary<Product, decimal> GetTeamLimits(Team team);

        decimal GetProductLimitForTeam(Team team, ProductType product);
        decimal GetProductLimitForTeam(int teamId, int productId);

    }
}
