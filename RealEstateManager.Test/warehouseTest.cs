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
    public class warehouseTests
    {
        private readonly IMapper _mapper;
        private Mock<IwarehouseRepository> _WarehouseRepository;
        private IwarehouseService _WarehouseService;
        private warehouseController _controller;
        private Mock<ILogger> _logger;

        private IList<warehouse> warehouses = new List<warehouse>()
        {
            { new warehouse() { EstateID = 300, Location = "Industrial Zone"} },
            { new warehouse() { EstateID = 301, Location = "Outskirt"} },
        };

        public warehouseTests()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapping());
            });

            _mapper = mockMapper.CreateMapper();

            _WarehouseRepository = new Mock<IwarehouseRepository>();

            _logger = new Mock<ILogger>();

            _WarehouseService = new warehouseService(_WarehouseRepository.Object, _logger.Object);

            //inject
            _controller = new warehouseController(_WarehouseService, _mapper);
        }

        [Fact]
        public void warehouse_GetAll_Count_Check()
        {
            //setup
            var expectedCount = 2;

            var mockedService = new Mock<IwarehouseService>();

            mockedService.Setup(x => x.GetAll())
                .Returns(warehouses);
            //inject
            var controller = new warehouseController(mockedService.Object, _mapper);

            //Act
            var result = controller.GetAll();

            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var positions = okObjectResult.Value as IEnumerable<warehouse>;
            Assert.NotNull(positions);
            Assert.Equal(expectedCount, positions.Count());
        }

        [Fact]
        public void warehouse_GetById_NameCheck()
        {
            //setup
            var WarehouseId = 301;
            var expectedLocation = "Outskirt";

            _WarehouseRepository.Setup(x => x.GetByID(WarehouseId))
                .Returns(warehouses.FirstOrDefault(x => x.EstateID == WarehouseId));

            //Act
            var result = _controller.Get(WarehouseId);

            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var response = okObjectResult.Value as warehouse;
            var Warehouse = _mapper.Map<warehouse>(response);

            Assert.NotNull(Warehouse);
            Assert.Equal(expectedLocation, Warehouse.Location);
        }

        [Fact]
        public void warehouse_GetByID_NotFound()
        {
            //setup
            var userPositionId = 3;

            _WarehouseRepository.Setup(x => x.GetByID(userPositionId))
                .Returns(warehouses.FirstOrDefault(x => x.EstateID == userPositionId));

            //Act
            var result = _controller.Get(userPositionId);

            //Assert
            var notFoundObjectResult = result as NotFoundObjectResult;
            Assert.NotNull(notFoundObjectResult);
        }

        [Fact]
        public void warehouse_Update_EmployeeName()
        {
            //setup
            var WarehouseId = 300;
            var expectedWarehouseLocation = "New Warehouse Location";

            var Warehouse = warehouses.FirstOrDefault(x => x.EstateID == WarehouseId);
            Warehouse.Location = expectedWarehouseLocation;

            _WarehouseRepository.Setup(x => x.GetByID(Warehouse.EstateID)).Returns(warehouses.FirstOrDefault(x => x.EstateID == WarehouseId));
            _WarehouseRepository.Setup(x => x.Update(Warehouse)).Returns(warehouses.FirstOrDefault(x => x.EstateID == WarehouseId));


            //Act
            var result = _controller.Update(Warehouse);

            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var pos = okObjectResult.Value as warehouse;
            Assert.NotNull(pos);
            Assert.Equal(expectedWarehouseLocation, pos.Location);
        }

        [Fact]
        public void House_Delete_Existing_PositionName()
        {
            //setup
            var WarehouseId = 300;

            var Warehouse = warehouses.FirstOrDefault(x => x.EstateID == WarehouseId);


            _WarehouseRepository.Setup(x => x.Delete(WarehouseId)).Callback(() => warehouses.Remove(Warehouse)).Returns(Warehouse);

            //Act
            var result = _controller.Delete(WarehouseId);

            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.Equal(okObjectResult.StatusCode, (int)HttpStatusCode.OK);

            Assert.Null(warehouses.FirstOrDefault(x => x.EstateID == WarehouseId));
        }

        [Fact]
        public void House_Delete_NotExisting_PositionName()
        {
            //setup
            var userPositionId = 3;

            var position = warehouses.FirstOrDefault(x => x.EstateID == userPositionId);


            _WarehouseRepository.Setup(x => x.Delete(userPositionId)).Callback(() => warehouses.Remove(position));

            //Act
            var result = _controller.Delete(userPositionId);

            //Assert
            Assert.Null(warehouses.FirstOrDefault(x => x.EstateID == userPositionId));
        }

        [Fact]
        public void House_Create_PositionName()
        {
            //setup
            var Warehouse = new warehouse()
            {
                EstateID = 303,
                Location = "Close center",
            };

            _WarehouseRepository.Setup(x => x.GetAll())
                .Returns(warehouses);

            _WarehouseRepository.Setup(x => x.Create(It.IsAny<warehouse>())).Callback(() =>
            {
                warehouses.Add(Warehouse);
            }).Returns(new warehouse()
            {
                EstateID = 303,
                Location = "Close center",
            });

            //Act
            var result = _controller.Create(_mapper.Map<warehouseRequest>(Warehouse));

            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.Equal(okObjectResult.StatusCode, (int)HttpStatusCode.OK);

            Assert.NotNull(warehouses.FirstOrDefault(x => x.EstateID == Warehouse.EstateID));
        }

    }
}
