using DownHillParkAPI.Models;
using DownHillParkAPI.Repositories;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    public class Tests
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
        public void Test1()
        {
            var team = _teamRepository.Object.FindByIdAsync(3).Result;
            Assert.IsNotNull(team);
            Assert.AreEqual(team.Id, 3);
            Assert.AreEqual(team.Name, "Team3");
            Assert.AreEqual(team.Country, "USA");
        }
    }
}