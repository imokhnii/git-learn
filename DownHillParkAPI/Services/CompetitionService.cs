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
    public interface ICompetitionService
    {
        Task<Competition> CreateAsync(CompetitionRequest item);
        IEnumerable<Competition> GetAll();
        Task<Competition> FindByIdAsync(int id);
        void Update(Competition competition);
        Task DeleteAsync(int id);
    }
    public class CompetitionService : ICompetitionService
    {
        public CompetitionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public async Task<Competition> CreateAsync(CompetitionRequest item)
        {
            var competition = await _unitOfWork.Competitions.AddAsync(
                _mapper.Map<Competition>(item));
            _unitOfWork.Complete();
            return competition;
        }

        public IEnumerable<Competition> GetAll()
        {
            return _unitOfWork.Competitions.GetAll();
        }

        public async Task<Competition> FindByIdAsync(int id)
        {
            return await _unitOfWork.Competitions.FindByIdAsync(id);
        }

        public async Task DeleteAsync(int id)
        {
            await _unitOfWork.Competitions.RemoveAsync(id);
            _unitOfWork.Complete();
        }

        public void Update(Competition competition)
        {
            _unitOfWork.Competitions.Update(competition);
            _unitOfWork.Complete();
        }
    }
}
