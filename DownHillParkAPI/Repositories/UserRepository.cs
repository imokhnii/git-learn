using DownHillParkAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DownHillParkAPI.Repositories
{
    public interface IUserRepository
    {
        void Add(User user);
        IEnumerable<User> GetAll();
        User FindById(int id);
        User FindByFullName(string firstname, string lastname);
        User FindByBike(Bike bike);
        void Remove(int Id);
        void Update(User user);

    }
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext db;
        public UserRepository(ApplicationContext db)
        {
            this.db = db;
        }
        public void Add(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
        }
        public IEnumerable<User> GetAll()
        {
            return db.Users;
        }
        public User FindById(int id)
        {
            return db.Users.Where(a => a.Id == id).Single();
        }
        public User FindByFullName(string firstname, string lastname)
        {
            return db.Users.Where(a => a.firstName == firstname && a.lastName == lastname).Single();
        }
        public User FindByBike(Bike bike)
        {
            return db.Users.Where(a => a.Bike == bike).Single();
        }
        public void Remove(int id)
        {
            db.Users.Remove(db.Users.Find(id));
            db.SaveChanges();
        }
        public void Update(User user)
        {
            var Item = db.Users.Find(user.Id);
            Item.firstName = user.firstName;
            Item.lastName = user.lastName;
            Item.Team = user.Team;
            db.SaveChanges();
        }

    }
}
