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
    }
    public class CompetitionResultService : ICompetitionResultService
    {
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
    }
}
