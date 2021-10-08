using DownHillParkAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DownHillParkAPI.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IBikeRepository Bikes { get; }
        ICompetitionRepository Competitions { get; }
        ICompetitionPrizeRepository Prizes { get; }
        ITeamRepository Teams { get; }
        int Complete();
    }
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DownHillParkAPIContext db;
        public IBikeRepository Bikes { get; }
        public ICompetitionRepository Competitions { get; }
        public ICompetitionPrizeRepository Prizes { get; }
        public ITeamRepository Teams { get; }
        public UnitOfWork(DownHillParkAPIContext db, IBikeRepository bikeRepository, ICompetitionPrizeRepository prizeRepository, ICompetitionRepository competitionRepository, ITeamRepository teamRepository)
        {
            this.db = db;
            Bikes = bikeRepository;
            Competitions = competitionRepository;
            Prizes = prizeRepository;
            Teams = teamRepository;
        }

        public int Complete()
        {
            return db.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);

        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
        }
    }
}
