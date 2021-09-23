using DownHillParkAPI.Data;
using DownHillParkAPI.Models;
using DownHillParkAPI.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DownHillParkAPI.Repositories
{
    public interface IBikeRepository
    {
        Bike Add(Bike bike);
        IEnumerable<Bike> GetAll();
        Bike FindById(int id);
        Bike FindByUser(User user);
        void Update(Bike bike);
        void Remove(int id);
        void SaveChanges();
    }
    public class BikeRepository : IBikeRepository
    {
        private readonly DownHillParkAPIContext db;
        public BikeRepository(DownHillParkAPIContext db)
        {
            this.db = db;
        }
        public Bike Add(Bike bike)
        {
            db.Bikes.Add(bike);
            db.SaveChanges();
            return bike;
        }
        public IEnumerable<Bike> GetAll()
        {
            return db.Bikes;
        }
        public Bike FindById(int id)
        {
            return (Bike)db.Bikes.Where(a => a.Id == id).Single();
        }
        public Bike FindByUser(User user)
        {
            return db.Bikes.Where(a => a.User == user).Single();
        }
        public void Update(Bike bike)
        {
            db.Entry(bike).CurrentValues.SetValues(bike);
            db.SaveChanges();
        }
        public void Remove(int id)
        {
            db.Bikes.Remove(db.Bikes.Find(id));
            db.SaveChanges();
        }
        public void SaveChanges()
        {
            db.SaveChanges();
        }
    }
}
