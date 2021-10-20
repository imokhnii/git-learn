using DownHillParkAPI.Data;
using System;

namespace DownHillParkAPI.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IBikeRepository Bikes { get; }
        ICompetitionRepository Competitions { get; }
        ICompetitionPrizeRepository Prizes { get; }
        ITeamRepository Teams { get; }
        ICompetitionResultRepository Results { get; }
        int Complete();
    }
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DownHillParkAPIContext db;
        public IBikeRepository Bikes { get; }
        public ICompetitionRepository Competitions { get; }
        public ICompetitionPrizeRepository Prizes { get; }
        public ITeamRepository Teams { get; }
        public ICompetitionResultRepository Results { get; }
        public UnitOfWork(DownHillParkAPIContext db)
        {
            this.db = db;
            Bikes = new BikeRepository(db);
            Competitions = new CompetitionRepository(db);
            Prizes = new CompetitionPrizeRepository(db);
            Teams = new TeamRepository(db);
            Results = new CompetitionResultRepository(db);
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
