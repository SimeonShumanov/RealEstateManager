using AutoMapper;
using RealEstateManager.BL.Interfaces;
using RealEstateManager.BL.Services;
using RealEstateManager.Controllers;
using RealEstateManager.DL.Interfaces;
using RealEstateManager.Host.Extensions;
using RealEstateManager.Models.DTO;
using RealEstateManager.Models.Requests;
using RealEstateManager.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Serilog;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Xunit;

namespace RealEstateManager.Test
{
    public class HouseTests
    {
        private readonly IMapper _mapper;
        private Mock<IHouseRepository> _houseRepository;
        private IHouseService _houseService;
        private HouseController _controller;
        private Mock<ILogger> _logger;

        private IList<House> Houses = new List<House>()
        {
            { new House() { EstateID = 100, Location = "Center"} },
            { new House() { EstateID = 101, Location = "Outskirt"} },
        };

        public HouseTests()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapping());
            });

            _mapper = mockMapper.CreateMapper();

            _houseRepository = new Mock<IHouseRepository>();

            _logger = new Mock<ILogger>();

            _houseService = new HouseService(_houseRepository.Object, _logger.Object);

            //inject
            _controller = new HouseController(_houseService, _mapper);
        }

        [Fact]
        public void House_GetAll_Count_Check()
        {
            //setup
            var expectedCount = 2;

            var mockedService = new Mock<IHouseService>();

            mockedService.Setup(x => x.GetAll())
                .Returns(Houses);
            //inject
            var controller = new HouseController(mockedService.Object, _mapper);

            //Act
            var result = controller.GetAll();

            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var positions = okObjectResult.Value as IEnumerable<House>;
            Assert.NotNull(positions);
            Assert.Equal(expectedCount, positions.Count());
        }

        [Fact]
        public void House_GetById_NameCheck()
        {
            //setup
            var houseId = 101;
            var expectedLocation = "Outskirt";

            _houseRepository.Setup(x => x.GetByID(houseId))
                .Returns(Houses.FirstOrDefault(x => x.EstateID == houseId));

            //Act
            var result = _controller.Get(houseId);

            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var response = okObjectResult.Value as House;
            var house = _mapper.Map<House>(response);

            Assert.NotNull(house);
            Assert.Equal(expectedLocation, house.Location);
        }

        [Fact]
        public void House_GetByID_NotFound()
        {
            //setup
            var userPositionId = 3;

            _houseRepository.Setup(x => x.GetByID(userPositionId))
                .Returns(Houses.FirstOrDefault(x => x.EstateID == userPositionId));

            //Act
            var result = _controller.Get(userPositionId);

            //Assert
            var notFoundObjectResult = result as NotFoundObjectResult;
            Assert.NotNull(notFoundObjectResult);
        }

        [Fact]
        public void House_Update_EmployeeName()
        {
            //setup
            var houseId = 100;
            var expectedHouseLocation = "New House Location";

            var house = Houses.FirstOrDefault(x => x.EstateID == houseId);
            house.Location = expectedHouseLocation;

            _houseRepository.Setup(x => x.GetByID(house.EstateID)).Returns(Houses.FirstOrDefault(x => x.EstateID == houseId));
            _houseRepository.Setup(x => x.Update(house)).Returns(Houses.FirstOrDefault(x => x.EstateID == houseId));


            //Act
            var result = _controller.Update(house);

            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var pos = okObjectResult.Value as House;
            Assert.NotNull(pos);
            Assert.Equal(expectedHouseLocation, pos.Location);
        }

        [Fact]
        public void House_Delete_Existing_PositionName()
        {
            //setup
            var houseId = 100;

            var house = Houses.FirstOrDefault(x => x.EstateID == houseId);


            _houseRepository.Setup(x => x.Delete(houseId)).Callback(() => Houses.Remove(house)).Returns(house);

            //Act
            var result = _controller.Delete(houseId);

            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.Equal(okObjectResult.StatusCode, (int)HttpStatusCode.OK);

            Assert.Null(Houses.FirstOrDefault(x => x.EstateID == houseId));
        }

        [Fact]
        public void House_Delete_NotExisting_PositionName()
        {
            //setup
            var userPositionId = 3;

            var position = Houses.FirstOrDefault(x => x.EstateID == userPositionId);


            _houseRepository.Setup(x => x.Delete(userPositionId)).Callback(() => Houses.Remove(position));

            //Act
            var result = _controller.Delete(userPositionId);

            //Assert
            Assert.Null(Houses.FirstOrDefault(x => x.EstateID == userPositionId));
        }

        [Fact]
        public void House_Create_PositionName()
        {
            //setup
            var house = new House()
            {
                EstateID = 103,
                Location = "Close center",
            };

            _houseRepository.Setup(x => x.GetAll())
                .Returns(Houses);

            _houseRepository.Setup(x => x.Create(It.IsAny<House>())).Callback(() =>
            {
                Houses.Add(house);
            }).Returns(new House()
            {
                EstateID = 103,
                Location = "Close center",
            });

            //Act
            var result = _controller.Create(_mapper.Map<HouseRequest>(house));

            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.Equal(okObjectResult.StatusCode, (int)HttpStatusCode.OK);

            Assert.NotNull(Houses.FirstOrDefault(x => x.EstateID == house.EstateID));
        }

    }
}
