using AutoMapper;
using DownHillParkAPI.Models;
using DownHillParkAPI.Repositories;
using DownHillParkAPI.RequestModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DownHillParkAPI.Services
{
    public interface ITeamService
    {
        Task<Team> CreateAsync(TeamRequest item);
        IEnumerable<Team> GetAll();
        Task<Team> FindByIdAsync(int id);
        Task DeleteAsync(int id);
        void Update(Team team);
    }
    public class TeamService : ITeamService
    {
        public TeamService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public async Task <Team> CreateAsync(TeamRequest item)
        {
            var team = await _unitOfWork.Teams.AddAsync(
                _mapper.Map<Team>(item));
            _unitOfWork.Complete();
            return team;
        }

        public IEnumerable<Team> GetAll()
        {
            return  _unitOfWork.Teams.GetAll();
        }

        public async Task<Team> FindByIdAsync(int id)
        {
            return await _unitOfWork.Teams.FindByIdAsync(id);
        }

        public async Task DeleteAsync(int id)
        {
            await _unitOfWork.Teams.RemoveAsync(id);
            _unitOfWork.Complete();
        }

        public void Update(Team team)
        {
            _unitOfWork.Teams.Update(team);
            _unitOfWork.Complete();
        }
    }
}
