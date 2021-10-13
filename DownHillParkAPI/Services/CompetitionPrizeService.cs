using AutoMapper;
using DownHillParkAPI.Models;
using DownHillParkAPI.Repositories;
using DownHillParkAPI.RequestModels;
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
        public CompetitionPrizeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public async Task<CompetitionPrize> CreateAsync(CompetitionPrizeRequest item)
        {
            var prize = await _unitOfWork.Prizes.AddAsync(
                _mapper.Map<CompetitionPrize>(item));
            _unitOfWork.Complete();
            return prize;
        }

        public async Task<CompetitionPrize> AddPrizesToCompetitionAsync(int CompetitionId, int PrizeId)
        {
            var competition = await _unitOfWork.Competitions.FindByIdAsync(CompetitionId);
            var prize = await _unitOfWork.Prizes.FindByIdAsync(PrizeId);
            if (competition != null && prize != null)
            {
                prize.CompetitionId = CompetitionId;
                competition.CompetitionPrizeId = PrizeId;
                _unitOfWork.Prizes.Update(prize);
                _unitOfWork.Complete();
                return prize;
            }
            return null;
        }

        public async Task<CompetitionPrize> FindByIdAsync(int id)
        {
            return await _unitOfWork.Prizes.FindByIdAsync(id);
        }

        public async Task DeleteAsync(int id)
        {
            await _unitOfWork.Prizes.RemoveAsync(id);
            _unitOfWork.Complete();
        }
    }
}
