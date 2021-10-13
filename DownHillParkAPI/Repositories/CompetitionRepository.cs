using DownHillParkAPI.Data;
using DownHillParkAPI.Models;
using DownHillParkAPI.RequestModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DownHillParkAPI.Repositories
{
    public interface ICompetitionRepository
    {
        Task <Competition> AddAsync(Competition competition);
        IEnumerable<Competition> GetAll(PageRequest pageRequest);
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
        public IEnumerable<Competition> GetAll(PageRequest pageRequest)
        {
            var comps = new List<Competition>();
            switch(pageRequest.SortOption)
            {
                case SortOptions.TypeAsc: comps = db.Competitions.OrderBy(c => c.Type).ToList();break;
                case SortOptions.TypeDesc: comps = db.Competitions.OrderByDescending(c => c.Type).ToList(); break;
                case SortOptions.DateOfStartAsc: comps = db.Competitions.OrderBy(c => c.DateOfStart).ToList(); break;
                case SortOptions.DateOfStartDesc:  comps = db.Competitions.OrderByDescending(c => c.DateOfStart).ToList(); break;
                case SortOptions.DateOfEndAsc: comps = db.Competitions.OrderBy(c => c.DateOfEnd).ToList(); break;
                case SortOptions.DateOfEndDesc: comps = db.Competitions.OrderByDescending(c => c.DateOfEnd).ToList(); break;
                default:comps = db.Competitions.ToList(); break;
            }
            return comps
            .Skip((pageRequest.PageNumber - 1) * pageRequest.PageSize)
            .Take(pageRequest.PageSize)
            .ToList();
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
