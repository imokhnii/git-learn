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
        Task<Team> CreateAsync(TeamRequest item);
        IEnumerable<Team> GetAll();
        Task<Team> FindByIdAsync(int id);
        Task DeleteAsync(int id);
        Task UpdateAsync(Team team);
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

        public async Task <Team> CreateAsync(TeamRequest item)
        {
            var team = await _teamManager.AddAsync(
                _mapper.Map<Team>(item));
            return team;

        }

        public IEnumerable<Team> GetAll()
        {
            return  _teamManager.GetAll();
        }

        public async Task<Team> FindByIdAsync(int id)
        {
            return await _teamManager.FindByIdAsync(id);
        }

        public async Task DeleteAsync(int id)
        {
            await _teamManager.RemoveAsync(id);
        }

        public async Task UpdateAsync(Team team)
        {
            await _teamManager.UpdateAsync(team);
        }
    }
}
