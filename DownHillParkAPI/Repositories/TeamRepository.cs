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
        IEnumerable<Team> GetAll();
        Team FindById(int id);
        Team FindByName(string name);
        void Remove(int id);

    }
    public class TeamRepository
    {
        private readonly ApplicationContext db;
        public TeamRepository(ApplicationContext db)
        {
            this.db = db;
        }

        public void Add(Team team)
        {
            db.Teams.Add(team);
            db.SaveChanges();
        }
        public IEnumerable<Team> GetAll()
        {
            return db.Teams;
        }
        public Team FindById(int id)
        {
            return db.Teams.Where(a => a.Id == id).Single();
        }
        public Team FindByName(string name)
        {
            return db.Teams.Where(a => a.Name == name).Single();
        }
        public void Remove(int id)
        {
            db.Teams.Remove(db.Teams.Find(id));
            db.SaveChanges();
        }
    }
}
