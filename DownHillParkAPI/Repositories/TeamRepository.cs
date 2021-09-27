using DownHillParkAPI.Data;
using DownHillParkAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DownHillParkAPI.Repositories
{
    public interface ITeamRepository
    {
        void Add(Team team);
        void Update(Team team);
        IEnumerable<Team> GetAll();
        Team FindById(int id);
        void Remove(int id);

    }
    public class TeamRepository : ITeamRepository
    {
        private readonly DownHillParkAPIContext db;
        public TeamRepository(DownHillParkAPIContext db)
        {
            this.db = db;
        }

        public void Add(Team team)
        {
            db.Teams.Add(team);
            db.SaveChanges();
        }
        public void Update(Team team)
        {
            db.Entry(team).CurrentValues.SetValues(team);
            db.SaveChanges();
        }
        public IEnumerable<Team> GetAll()
        {
            return db.Teams;
        }
        public Team FindById(int id)
        {
            return db.Teams.Find(id);
        }
        public void Remove(int id)
        {
            db.Teams.Remove(db.Teams.Find(id));
            db.SaveChanges();
        }
    }
}
