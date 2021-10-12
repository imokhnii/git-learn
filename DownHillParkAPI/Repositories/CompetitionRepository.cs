using DownHillParkAPI.Data;
using DownHillParkAPI.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DownHillParkAPI.Repositories
{
    public interface ICompetitionRepository
    {
        Task <Competition> AddAsync(Competition competition);
        IEnumerable<Competition> GetAll();
        Task<Competition> FindByIdAsync(int id);
        void Update(Competition competition);
        Task RemoveAsync(int id);

    }
    public class CompetitionRepository : ICompetitionRepository
    {
        private readonly DownHillParkAPIContext db;
        public CompetitionRepository(DownHillParkAPIContext db)
        {
            this.db = db;
        }
        public async Task<Competition> AddAsync(Competition competition)
        {
            await db.Competitions.AddAsync(competition);
            return competition;
        }
        public IEnumerable<Competition> GetAll()
        {
            return db.Competitions;
        }
        public async Task<Competition> FindByIdAsync(int id)
        {
            return await db.Competitions.FindAsync(id);
        }
        public void Update(Competition competition)
        {
            db.Entry(competition).CurrentValues.SetValues(competition);
        }
        public async Task RemoveAsync(int id)
        {
            db.Competitions.Remove(await db.Competitions.FindAsync(id));
        }
    }
}
