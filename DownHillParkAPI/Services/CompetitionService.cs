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
        Task UpdateAsync(Competition competition);
        Task DeleteAsync(int id);
    }
    public class CompetitionService : ICompetitionService
    {
        public CompetitionService(ICompetitionRepository competitionManager, IMapper mapper)
        {
            _competitionManager = competitionManager;
            _mapper = mapper;
        }
        private readonly ICompetitionRepository _competitionManager;
        private readonly IMapper _mapper;

        public async Task<Competition> CreateAsync(CompetitionRequest item)
        {
            var competition = await _competitionManager.AddAsync(
                _mapper.Map<Competition>(item));
            return competition;
        }

        public IEnumerable<Competition> GetAll()
        {
            return _competitionManager.GetAll();
        }

        public async Task<Competition> FindByIdAsync(int id)
        {
            return await _competitionManager.FindByIdAsync(id);
        }

        public async Task DeleteAsync(int id)
        {
            await _competitionManager.RemoveAsync(id);
        }

        public async Task UpdateAsync(Competition competition)
        {
            await _competitionManager.UpdateAsync(competition);
        }
    }
}
