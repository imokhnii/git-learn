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
        public UnitOfWork(DownHillParkAPIContext db, IBikeRepository bikeRepository, ICompetitionPrizeRepository prizeRepository, ICompetitionRepository competitionRepository, ITeamRepository teamRepository, ICompetitionResultRepository competitionResultRepository)
        {
            this.db = db;
            Bikes = bikeRepository;
            Competitions = competitionRepository;
            Prizes = prizeRepository;
            Teams = teamRepository;
            Results = competitionResultRepository;
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
