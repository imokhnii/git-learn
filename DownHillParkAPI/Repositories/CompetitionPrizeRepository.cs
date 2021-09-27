using DownHillParkAPI.Data;
using DownHillParkAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DownHillParkAPI.Repositories
{
    public interface ICompetitionPrizeRepository
    {
        CompetitionPrize Add(CompetitionPrize prize);
        public IEnumerable<CompetitionPrize> GetAll();
        public CompetitionPrize FindById(int id);
        public void Update(CompetitionPrize prize);
        void Remove(int id);
    }
    public class CompetitionPrizeRepository : ICompetitionPrizeRepository
    {
        private readonly DownHillParkAPIContext db;
        public CompetitionPrizeRepository(DownHillParkAPIContext db)
        {
            this.db = db;
        }

        public CompetitionPrize Add(CompetitionPrize prize)
        {
            db.CompetitionPrizes.Add(prize);
            db.SaveChanges();
            return prize;
        }

        public IEnumerable<CompetitionPrize> GetAll()
        {
            return db.CompetitionPrizes;
        }
        public CompetitionPrize FindById(int id)
        {
            return db.CompetitionPrizes.Find(id);
        }
        public void Update(CompetitionPrize prize)
        {
            db.Entry(prize).CurrentValues.SetValues(prize);
            db.SaveChanges();
        }
        public void Remove(int id)
        {
            db.CompetitionPrizes.Remove(db.CompetitionPrizes.Find(id));
            db.SaveChanges();
        }
    }
}
