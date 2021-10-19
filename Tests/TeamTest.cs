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
            _teamRepository.Setup(x => x.FindByIdAsync(It.IsAny<int>()).Result)
          .Returns((int i) => InMemoryDatabase.Single(bo => bo.Id == i));
        }

        [Test]
        public void TestCreatedRepositoryAndGetById()
        {
            var team = _teamRepository.Object.FindByIdAsync(3).Result;
            Assert.IsNotNull(team);
            Assert.AreEqual(3, team.Id);
            Assert.AreEqual("Team3", team.Name);
            Assert.AreEqual("USA", team.Country);
        }
        
    }
}