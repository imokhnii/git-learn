using DownHillParkAPI.Models;
using DownHillParkAPI.Repositories;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    public class TeamTest
    {
        private Mock<ITeamRepository> _teamRepository;
        [SetUp]
        public void Setup()
        {
            var InMemoryDatabase = new List<Team>
            {
                new Team() {Id = 1, Name = "Team1", Country = "UKR"},
                new Team() {Id = 2, Name = "Team2", Country = "USA"},
                new Team() {Id = 3, Name = "Team3", Country = "USA"}
            };
            _teamRepository = new Mock<ITeamRepository>();
            _teamRepository
                .Setup(x => x.FindByIdAsync(It.IsAny<int>()).Result)
                .Returns((int i) => InMemoryDatabase.Single(bo => bo.Id == i));
            _teamRepository
                .Setup(x => x.GetAll())
                .Returns(new List<Team>(InMemoryDatabase));

        }

        [Test]
        public void GetByIdTeam()
        {
            var team = _teamRepository.Object.FindByIdAsync(3).Result;
            Assert.AreEqual(3, team.Id);
            Assert.AreEqual("Team3", team.Name);
            Assert.AreEqual("USA", team.Country);
        }
        [Test]
        public void GetAllTeams()
        {
            var InMemoryDatabase = new List<Team>
            {
                new Team() {Id = 1, Name = "Team1", Country = "UKR"},
                new Team() {Id = 2, Name = "Team2", Country = "USA"},
                new Team() {Id = 3, Name = "Team3", Country = "USA"}
            };
            var teams = _teamRepository.Object.GetAll().ToList();
            Assert.AreEqual(InMemoryDatabase.Count, teams.Count);
            for (int i = 0; i < InMemoryDatabase.Count; i++)
            {
                Assert.AreEqual(InMemoryDatabase[i].Id, teams[i].Id);
                Assert.AreEqual(InMemoryDatabase[i].Name, teams[i].Name);
                Assert.AreEqual(InMemoryDatabase[i].Country, teams[i].Country);
            }
        }
        
    }
}