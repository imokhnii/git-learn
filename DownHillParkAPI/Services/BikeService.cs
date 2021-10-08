﻿using AutoMapper;
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
        public BikeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public async Task<Bike> CreateAsync(BikeRequest item)
        {
            var bike = await _unitOfWork.Bikes.AddAsync(
                _mapper.Map<Bike>(item));
            return bike;
        }

        public IEnumerable<Bike> GetAll()
        {
            return _unitOfWork.Bikes.GetAll();
        }

        public async Task<Bike> FindByIdAsync(int id)
        {
            return await _unitOfWork.Bikes.FindByIdAsync(id);
        }

        public async Task DeleteAsync(int id)
        {
           await _unitOfWork.Bikes.RemoveAsync(id);
        }

        public async Task UpdateAsync(Bike bike)
        {
            await _unitOfWork.Bikes.UpdateAsync(bike);
        }
    }
}
