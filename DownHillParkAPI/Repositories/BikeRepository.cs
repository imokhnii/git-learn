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
        Task<Bike> AddAsync(Bike bike);
        IEnumerable<Bike> GetAll();
        Task<Bike> FindByIdAsync(int id);
        Task UpdateAsync(Bike bike);
        Task RemoveAsync(int id);
    }
    public class BikeRepository : IBikeRepository
    {
        private readonly DownHillParkAPIContext db;
        public BikeRepository(DownHillParkAPIContext db)
        {
            this.db = db;
        }
        public async Task<Bike> AddAsync(Bike bike)
        {
            await db.Bikes.AddAsync(bike);
            await db.SaveChangesAsync();
            return bike;
        }
        public IEnumerable<Bike> GetAll()
        {
            return db.Bikes;
        }
        public async Task<Bike> FindByIdAsync(int id)
        {
            return await db.Bikes.FindAsync(id);
        }
        public async Task UpdateAsync(Bike bike)
        {
            db.Entry(bike).CurrentValues.SetValues(bike);
            await db.SaveChangesAsync();
        }
        public async Task RemoveAsync(int id)
        {
            db.Bikes.Remove(await db.Bikes.FindAsync(id));
            await db.SaveChangesAsync();
        }
    }
}
