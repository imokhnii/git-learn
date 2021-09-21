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
        void Create(Competition competition);
        IEnumerable<Competition> GetAll();
        Competition FindById(int id);
        Competition FindByName(string name);
        void Update(Competition competition);
        void AddParticipant(int id, string UserId);
        void RemoveParticipant(int id, string UserId);
        void Remove(int id);

    }
    public class CompetitionRepository : ICompetitionRepository
    {
        private readonly DownHillParkAPIContext db;
        public CompetitionRepository(DownHillParkAPIContext db)
        {
            this.db = db;
        }
        public void Create(Competition competition)
        {
            db.Competitions.Add(competition);
            db.SaveChanges();
        }
        public IEnumerable<Competition> GetAll()
        {
            return db.Competitions;
        }
        public Competition FindById(int id)
        {
            return db.Competitions.Where(a => a.Id == id).Single();
        }
        public Competition FindByName(string name)
        {
            return db.Competitions.Where(a => a.Name == name).Single();
        }
        public void Update(Competition competition)
        {
            var Item = db.Competitions.Find(competition.Id);
            Item.Name = competition.Name;
            Item.DateOfStart = competition.DateOfStart;
            Item.DateOfEnd = competition.DateOfEnd;
            Item.FirstPlace = competition.FirstPlace;
            Item.SecondPlace = competition.SecondPlace;
            Item.ThirdPlace = competition.ThirdPlace;
            db.SaveChanges();
        }
        public void AddParticipant(int id, string UserId)
        {
            var participant = (User)db.Users.Find(UserId);
            participant.CurrentCompetition = db.Competitions.Find(id);
            db.SaveChanges();
        }
        public void RemoveParticipant(int id, string UserId)//should be fixed later
        {
            var competition = FindById(id);
            competition.Participants.Remove((User)db.Users.Find(UserId));
        }
        public void Remove(int id)
        {
            db.Competitions.Remove(db.Competitions.Find(id));
            db.SaveChanges();
        }
    }
}
