using DownHillParkAPI.Models;
using DownHillParkAPI.Repositories;
using DownHillParkAPI.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Tests
{
    class ResultTest
    {
        private ICompetitionResultService _resultService;
        [SetUp]
        public void Setup()
        {
            _resultService = new CompetitionResultService();
        }
        [Test]
        public void GetWinnerOfCompetition()
        {
            var InMemoryDatabase = new List<CompetitionResult>
            {
                new CompetitionResult() {Id = 1, Type = "TestType1", CompetitionId = 1, UserId = "TestUserId1", TotalTime = new TimeSpan(0,0,22,15,0)},
                new CompetitionResult() {Id = 2, Type = "TestType2", CompetitionId = 1, UserId = "TestUserId2", TotalTime = new TimeSpan(0,0,18,33,0)},
                new CompetitionResult() {Id = 3, Type = "TestType3", CompetitionId = 1, UserId = "TestUserId3", TotalTime = new TimeSpan(0,0,18,2,0)}
            };
            var someWinner = _resultService.CalculateWinner(InMemoryDatabase);
            Assert.AreEqual("TestUserId3", someWinner.UserId);
        }
    }
}
