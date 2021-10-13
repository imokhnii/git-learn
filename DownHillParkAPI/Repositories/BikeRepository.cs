using DownHillParkAPI.Data;
using DownHillParkAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DownHillParkAPI.Repositories
{
    public interface IBikeRepository
    {
        Task<Bike> AddAsync(Bike bike);
        IEnumerable<Bike> GetAll();
        Task<Bike> FindByIdAsync(int id);
        void Update(Bike bike);
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
        public void Update(Bike bike)
        {
            db.Entry(bike).CurrentValues.SetValues(bike);
        }
        public async Task RemoveAsync(int id)
        {
            db.Bikes.Remove(await db.Bikes.FindAsync(id));
        }
    }
}
