using DownHillParkAPI.Data;
using DownHillParkAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DownHillParkAPI.Repositories
{
    public interface ICompetitionPrizeRepository
    {
        Task<CompetitionPrize> AddAsync(CompetitionPrize prize);
        IEnumerable<CompetitionPrize> GetAll();
        Task<CompetitionPrize> FindByIdAsync(int id);
        void Update(CompetitionPrize prize);
        Task RemoveAsync(int id);
    }
    public class CompetitionPrizeRepository : ICompetitionPrizeRepository
    {
        private readonly DownHillParkAPIContext db;
        public CompetitionPrizeRepository(DownHillParkAPIContext db)
        {
            this.db = db;
        }

        public async Task<CompetitionPrize> AddAsync(CompetitionPrize prize)
        {
            await db.CompetitionPrizes.AddAsync(prize);
            return prize;
        }

        public IEnumerable<CompetitionPrize> GetAll()
        {
            return db.CompetitionPrizes;
        }
        public async Task<CompetitionPrize> FindByIdAsync(int id)
        {
            return await db.CompetitionPrizes.FindAsync(id);
        }
        public void Update(CompetitionPrize prize)
        {
            db.Entry(prize).CurrentValues.SetValues(prize);
        }
        public async Task RemoveAsync(int id)
        {
            db.CompetitionPrizes.Remove(await db.CompetitionPrizes.FindAsync(id));
        }
    }
}
