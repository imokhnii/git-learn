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
        Competition Add(Competition competition);
        IEnumerable<Competition> GetAll();
        Competition FindById(int id);
        void Update(Competition competition);
        void Remove(int id);

    }
    public class CompetitionRepository : ICompetitionRepository
    {
        private readonly DownHillParkAPIContext db;
        public CompetitionRepository(DownHillParkAPIContext db)
        {
            this.db = db;
        }
        public Competition Add(Competition competition)
        {
            db.Competitions.Add(competition);
            db.SaveChanges();
            return competition;
        }
        public IEnumerable<Competition> GetAll()
        {
            return db.Competitions;
        }
        public Competition FindById(int id)
        {
            return db.Competitions.Where(a => a.Id == id).Single();
        }
        public void Update(Competition competition)
        {
            db.Entry(competition).CurrentValues.SetValues(competition);
            db.SaveChanges();
        }
        public void Remove(int id)
        {
            db.Competitions.Remove(db.Competitions.Find(id));
            db.SaveChanges();
        }
    }
}
