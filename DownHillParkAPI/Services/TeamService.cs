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
    public interface ITeamService
    {
        Team Create(TeamRequest item);
        IEnumerable<Team> GetAll();
        Team FindById(int id);
        void Update(Team team);
        void Delete(int id);
    }
    public class TeamService : ITeamService
    {
        public TeamService(ITeamRepository teamManager, IMapper mapper)
        {
            _teamManager = teamManager;
            _mapper = mapper;
        }
        private readonly ITeamRepository _teamManager;
        private readonly IMapper _mapper;

        public Team Create(TeamRequest item)
        {
            var team = _teamManager.Add(
                _mapper.Map<Team>(item));
            return team;

        }

        public IEnumerable<Team> GetAll()
        {
            return _teamManager.GetAll();
        }

        public Team FindById(int id)
        {
            return _teamManager.FindById(id);
        }

        public void Delete(int id)
        {
            _teamManager.Remove(id);
        }

        public void Update(Team team)
        {
            _teamManager.Update(team);
        }
    }
}
