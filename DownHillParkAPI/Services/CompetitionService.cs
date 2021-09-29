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
        Competition Create(CompetitionRequest item);
        IEnumerable<Competition> GetAll();
        Competition FindById(int id);
        void Update(Competition competition);
        void Delete(int id);
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

        public Competition Create(CompetitionRequest item)
        {
            var competition = _competitionManager.Add(
                _mapper.Map<Competition>(item));
            return competition;
        }

        public IEnumerable<Competition> GetAll()
        {
            return _competitionManager.GetAll();
        }

        public Competition FindById(int id)
        {
            return _competitionManager.FindById(id);
        }

        public void Delete(int id)
        {
            _competitionManager.Remove(id);
        }

        public void Update(Competition competition)
        {
            _competitionManager.Update(competition);
        }
    }
}
