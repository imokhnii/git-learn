using DownHillParkAPI.Data;
using DownHillParkAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DownHillParkAPI.Repositories
{
    public interface ICompetitionResultRepository
    {
        Task<CompetitionResult> AddAsync(CompetitionResult result);
        void Update(Competition result);
        Task<CompetitionResult> FindByIdAsync(int id);
        Task RemoveAsync(int id);
        IEnumerable<CompetitionResult> GetByCompetitionId(int competitionId);
    }
    public class CompetitionResultRepository : ICompetitionResultRepository
    {
        private readonly DownHillParkAPIContext db;
        public CompetitionResultRepository(DownHillParkAPIContext db)
        {
            this.db = db;
        }
        public async Task<CompetitionResult> AddAsync(CompetitionResult result)
        {
            await db.CompetitionResults.AddAsync(result);
            return result;
        }

        public async Task<CompetitionResult> FindByIdAsync(int id)
        {
            return await db.CompetitionResults.FindAsync(id);
        }

        public IEnumerable<CompetitionResult> GetByCompetitionId(int competitionId)
        {
            return db.CompetitionResults.Where(a => a.CompetitionId == competitionId);
        }

        public async Task RemoveAsync(int id)
        {
            db.CompetitionResults.Remove(await db.CompetitionResults.FindAsync(id));
        }

        public void Update(Competition result)
        {
            db.Entry(result).CurrentValues.SetValues(result);
        }
    }
}
