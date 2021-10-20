using AutoMapper;
using DownHillParkAPI.Controllers;
using DownHillParkAPI.Data;
using DownHillParkAPI.Mapper;
using DownHillParkAPI.Models;
using DownHillParkAPI.Repositories;
using DownHillParkAPI.RequestModels;
using DownHillParkAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace Tests
{
    class BikeTest
    {
        public BikeTest()
        {
            InitContext();
        }
        private DownHillParkAPIContext _context;
        private IBikeService _bikeService;
        private BikeController _bikeController;
        private IUnitOfWork _unitOfWork;
        private ILoggerFactory _loggerFactory;
        private IBikeRepository _bikeRepository;

        private void InitContext()
        {
            var builder = new DbContextOptionsBuilder<DownHillParkAPIContext>().UseInMemoryDatabase(databaseName:"Test");
            var context = new DownHillParkAPIContext(builder.Options);
            var bikes = Enumerable.Range(1, 10)
            .Select(i => new Bike { Id = i, Manufacturer = $"Manufacturer{i}", Model = $"Model{i}", UserId = $"User{i}" });
            foreach(Bike bike in bikes)
            {
                context.Bikes.Add(bike);
            }
            context.SaveChanges();
            _context = context;
            _bikeRepository = new BikeRepository(_context);
            _unitOfWork = new UnitOfWork(_context);
            _loggerFactory = new LoggerFactory();

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            
            _bikeService = new BikeService(_unitOfWork,mapper);
            _bikeController = new BikeController(_bikeService, _loggerFactory);
        }
        [Test]
        public async Task TestGetBikeByIdAsync()
        {
            var actionResult = await _bikeController.GetById(2);
            OkObjectResult okResult = actionResult as OkObjectResult;
            Bike bike = okResult.Value as Bike;
            Assert.AreEqual(2, bike.Id);
        }
        [Test]
        public void TestCreateBike()
        {
            var request = new BikeRequest() { Manufacturer = "GetTest", Model = "GetTest", Country = "GetTest" };
            var actionResult = _bikeController.Create(request);
            CreatedAtRouteResult routeResult = actionResult.Result as CreatedAtRouteResult;
            Bike bike = routeResult.Value as Bike;
            Assert.AreEqual("GetTest", bike.Manufacturer);
        }
        
    }
}
