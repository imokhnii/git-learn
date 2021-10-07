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
    public interface ICompetitionPrizeService
    {
        Task<CompetitionPrize> CreateAsync(CompetitionPrizeRequest item);
        Task<CompetitionPrize> AddPrizesToCompetitionAsync(int CompetitionId, int PrizeId);
        Task<CompetitionPrize> FindByIdAsync(int id);
        Task DeleteAsync(int id);
    }
    public class CompetitionPrizeService : ICompetitionPrizeService
    {
        public CompetitionPrizeService(ICompetitionPrizeRepository prizeManager,ICompetitionService competitionService, IMapper mapper)
        {
            _prizeManager = prizeManager;
            _competitionService = competitionService;
            _mapper = mapper;
        }
        private readonly ICompetitionPrizeRepository _prizeManager;
        private readonly ICompetitionService _competitionService;
        private readonly IMapper _mapper;

        public async Task<CompetitionPrize> CreateAsync(CompetitionPrizeRequest item)
        {
            var prize = await _prizeManager.AddAsync(
                _mapper.Map<CompetitionPrize>(item));
            return prize;
        }

        public async Task<CompetitionPrize> AddPrizesToCompetitionAsync(int CompetitionId, int PrizeId)
        {
            var competition = await _competitionService.FindByIdAsync(CompetitionId);
            var prize = await _prizeManager.FindByIdAsync(PrizeId);
            if (competition != null && prize != null)
            {
                prize.CompetitionId = CompetitionId;
                competition.CompetitionPrizeId = PrizeId;
                await _prizeManager.UpdateAsync(prize);
                return prize;
            }
            return null;
        }

        public async Task<CompetitionPrize> FindByIdAsync(int id)
        {
            return await _prizeManager.FindByIdAsync(id);
        }

        public async Task DeleteAsync(int id)
        {
            await _prizeManager.RemoveAsync(id);
        }
    }
}
