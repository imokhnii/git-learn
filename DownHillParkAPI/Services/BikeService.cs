using AutoMapper;
using DownHillParkAPI.Models;
using DownHillParkAPI.Repositories;
using DownHillParkAPI.RequestModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DownHillParkAPI.Services
{
    public interface IBikeService
    {
        Task<Bike> CreateAsync(BikeRequest item);
        IEnumerable<Bike> GetAll();
        Task<Bike> FindByIdAsync(int id);
        Task UpdateAsync(Bike bike);
        Task DeleteAsync(int id);
    }
    public class BikeService : IBikeService
    {
        public BikeService(IBikeRepository bikeManager, IMapper mapper)
        {
            _bikeManager = bikeManager;
            _mapper = mapper;
        }
        private readonly IBikeRepository _bikeManager;
        private readonly IMapper _mapper;

        public async Task<Bike> CreateAsync(BikeRequest item)
        {
            var bike = await _bikeManager.AddAsync(
                _mapper.Map<Bike>(item));
            return bike;
        }

        public IEnumerable<Bike> GetAll()
        {
            return _bikeManager.GetAll();
        }

        public async Task<Bike> FindByIdAsync(int id)
        {
            return await _bikeManager.FindByIdAsync(id);
        }

        public async Task DeleteAsync(int id)
        {
           await _bikeManager.RemoveAsync(id);
        }

        public async Task UpdateAsync(Bike bike)
        {
            await _bikeManager.UpdateAsync(bike);
        }
    }
}
