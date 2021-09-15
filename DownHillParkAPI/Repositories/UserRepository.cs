using DownHillParkAPI.Data;
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
        private readonly DownHillParkAPIContext db;
        public UserRepository(DownHillParkAPIContext db)
        {
            this.db = db;
        }

        public void Add(User user)
        {
            
        }

        public User FindByBike(Bike bike)
        {
            throw new NotImplementedException();
        }

        public User FindByFullName(string firstname, string lastname)
        {
            throw new NotImplementedException();
        }

        public User FindById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Remove(int Id)
        {
            throw new NotImplementedException();
        }

        public void Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}
