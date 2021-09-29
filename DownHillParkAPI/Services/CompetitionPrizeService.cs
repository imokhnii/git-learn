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
        CompetitionPrize Create(CompetitionPrizeRequest item);
        CompetitionPrize AddPrizesToCompetition(int CompetitionId, int PrizeId);
        CompetitionPrize FindById(int id);
        void Delete(int id);
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

        public CompetitionPrize Create(CompetitionPrizeRequest item)
        {
            var prize = _prizeManager.Add(
                _mapper.Map<CompetitionPrize>(item));
            return prize;
        }

        public CompetitionPrize AddPrizesToCompetition(int CompetitionId, int PrizeId)
        {
            var competition = _competitionService.FindById(CompetitionId);
            var prize = _prizeManager.FindById(PrizeId);
            if (competition != null && prize != null)
            {
                prize.CompetitionId = CompetitionId;
                competition.CompetitionPrizeId = PrizeId;
                _prizeManager.Update(prize);
                return prize;
            }
            return null;
        }

        public CompetitionPrize FindById(int id)
        {
            return _prizeManager.FindById(id);
        }

        public void Delete(int id)
        {
            _prizeManager.Remove(id);
        }
    }
}
