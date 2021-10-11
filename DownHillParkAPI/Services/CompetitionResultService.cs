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
            if (result != null)
            {
                var competition = await _unitOfWork.Competitions.FindByIdAsync(result.CompetitionId);
                result.Type = competition.Type;
                if (result.Type == "WithoutLaps")
                {
                    result.Laps = null;
                }
                await _unitOfWork.Results.AddAsync(result);
                await _unitOfWork.Competitions.UpdateAsync(competition);
                return result;
            }
            return null;
        }
    }
}
