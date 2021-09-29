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
        Bike Create(BikeRequest item);
        IEnumerable<Bike> GetAll();
        Bike FindById(int id);
        void Update(Bike bike);
        void Delete(int id);
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

        public Bike Create(BikeRequest item)
        {
            var bike = _bikeManager.Add(
                _mapper.Map<Bike>(item));
            return bike;
        }

        public IEnumerable<Bike> GetAll()
        {
            return _bikeManager.GetAll();
        }

        public Bike FindById(int id)
        {
            return _bikeManager.FindById(id);
        }

        public void Delete(int id)
        {
            _bikeManager.Remove(id);
        }

        public void Update(Bike bike)
        {
            _bikeManager.Update(bike);
        }
    }
}
