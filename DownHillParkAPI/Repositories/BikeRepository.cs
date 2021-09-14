using DownHillParkAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DownHillParkAPI.Repositories
{
    public interface IBikeRepository
    {
        void Add(Bike bike);
        IEnumerable<Bike> GetAll();
        Bike FindById(int id);
        Bike FindByUser(User user);
        void Update(Bike bike);
        void Remove(int id);
    }
    public class BikeRepository
    {
        private readonly ApplicationContext db;
        public BikeRepository(ApplicationContext db)
        {
            this.db = db;
        }
        public void Add(Bike bike)
        {
            db.Bikes.Add(bike);
            db.SaveChanges();
        }
        public IEnumerable<Bike> GetAll()
        {
            return db.Bikes;
        }
        public Bike FindById(int id)
        {
            return db.Bikes.Where(a => a.Id == id).Single();
        }
        public Bike FindByUser(User user)
        {
            return db.Bikes.Where(a => a.User == user).Single();
        }
        public void Update(Bike bike)
        {
            var Item = db.Bikes.Find(bike.Id);
            Item.Manufacturer = bike.Manufacturer;
            Item.Model = bike.Model;
            Item.Country = bike.Country;
            db.SaveChanges();
        }
        public void Remove(int id)
        {
            db.Bikes.Remove(db.Bikes.Find(id));
            db.SaveChanges();
        }
    }
}
