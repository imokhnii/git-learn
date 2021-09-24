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
        void Update(Bike bike);
        void Remove(int id);
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
    }
}
