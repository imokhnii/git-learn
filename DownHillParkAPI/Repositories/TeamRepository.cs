using DownHillParkAPI.Data;
using DownHillParkAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DownHillParkAPI.Repositories
{
    public interface ITeamRepository
    {
        Task<Team> AddAsync(Team team);
        void Update(Team team);
        IEnumerable<Team> GetAll();
        Task<Team> FindByIdAsync(int id);
        Task RemoveAsync(int id);

    }
    public class TeamRepository : ITeamRepository
    {
        private readonly DownHillParkAPIContext db;
        public TeamRepository(DownHillParkAPIContext db)
        {
            this.db = db;
        }

        public async Task<Team> AddAsync(Team team)
        {
            await db.Teams.AddAsync(team);
            return team;
        }
        public void Update(Team team)
        {
            db.Entry(team).CurrentValues.SetValues(team);
        }
        public IEnumerable<Team> GetAll()
        {
            return db.Teams;
        }

        public async Task<Team> FindByIdAsync(int id)
        {
            return await db.Teams.FindAsync(id);
        }
        public async Task RemoveAsync(int id)
        {
            db.Teams.Remove(await db.Teams.FindAsync(id));
        }
    }
}
