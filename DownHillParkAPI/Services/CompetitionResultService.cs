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
    public interface ICompetitionResultService
    {
        Task<CompetitionResult> Create(ResultRequest item);
        IEnumerable<CompetitionResult> GetByCompId(int CompetitionId);
        CompetitionResult CalculateWinner(IEnumerable<CompetitionResult> results);
    }
    public class CompetitionResultService : ICompetitionResultService
    {
        public CompetitionResultService()
        {

        }
        public CompetitionResultService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public async Task<CompetitionResult> Create(ResultRequest item)
        {
            var result = _mapper.Map<ResultRequest,CompetitionResult>(item);
            var competition = await _unitOfWork.Competitions.FindByIdAsync(result.CompetitionId);
            result.Type = competition.Type;
            
            foreach (Lap lap in result.Laps)
            {
                result.TotalTime += lap.LapTime;
                lap.UserId = result.UserId;
            }
            
            await _unitOfWork.Results.AddAsync(result);
            _unitOfWork.Complete();
            return result;
        }

        public IEnumerable<CompetitionResult> GetByCompId(int CompetitionId)
        {
            return _unitOfWork.Results.GetByCompetitionId(CompetitionId);
           
        }

        public CompetitionResult CalculateWinner(IEnumerable<CompetitionResult> results)
        {
            var minTime = results.Min(a => a.TotalTime);
            return results.Where(a => (TimeSpan.Compare(a.TotalTime, minTime) == 0)).FirstOrDefault();
        }
    }
}
