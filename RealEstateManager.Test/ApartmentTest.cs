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
    public class ApartmentTests
    {
        private readonly IMapper _mapper;
        private Mock<IApartmentRepository> _apartmentRepository;
        private IApartmentService _apartmentService;
        private ApartmentController _controller;
        private Mock<ILogger> _logger;

        private IList<Apartment> Apartments = new List<Apartment>()
        {
            { new Apartment() { EstateID = 200, Location = "Center"} },
            { new Apartment() { EstateID = 201, Location = "Outskirt"} },
        };

        public ApartmentTests()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapping());
            });

            _mapper = mockMapper.CreateMapper();

            _apartmentRepository = new Mock<IApartmentRepository>();

            _logger = new Mock<ILogger>();

            _apartmentService = new ApartmentService(_apartmentRepository.Object, _logger.Object);

            //inject
            _controller = new ApartmentController(_apartmentService, _mapper);
        }

        [Fact]
        public void Apartment_GetAll_Count_Check()
        {
            //setup
            var expectedCount = 2;

            var mockedService = new Mock<IApartmentService>();

            mockedService.Setup(x => x.GetAll())
                .Returns(Apartments);
            //inject
            var controller = new ApartmentController(mockedService.Object, _mapper);

            //Act
            var result = controller.GetAll();

            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var positions = okObjectResult.Value as IEnumerable<Apartment>;
            Assert.NotNull(positions);
            Assert.Equal(expectedCount, positions.Count());
        }

        [Fact]
        public void Apartment_GetById_NameCheck()
        {
            //setup
            var apartmentId = 201;
            var expectedLocation = "Outskirt";

            _apartmentRepository.Setup(x => x.GetByID(apartmentId))
                .Returns(Apartments.FirstOrDefault(x => x.EstateID == apartmentId));

            //Act
            var result = _controller.Get(apartmentId);

            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var response = okObjectResult.Value as Apartment;
            var apartment = _mapper.Map<Apartment>(response);

            Assert.NotNull(apartment);
            Assert.Equal(expectedLocation, apartment.Location);
        }

        [Fact]
        public void Apartment_GetByID_NotFound()
        {
            //setup
            var userPositionId = 3;

            _apartmentRepository.Setup(x => x.GetByID(userPositionId))
                .Returns(Apartments.FirstOrDefault(x => x.EstateID == userPositionId));

            //Act
            var result = _controller.Get(userPositionId);

            //Assert
            var notFoundObjectResult = result as NotFoundObjectResult;
            Assert.NotNull(notFoundObjectResult);
        }

        [Fact]
        public void Apartment_Update_EmployeeName()
        {
            //setup
            var apartmentId = 200;
            var expectedApartmentLocation = "New Apartment Location";

            var apartment = Apartments.FirstOrDefault(x => x.EstateID == apartmentId);
            apartment.Location = expectedApartmentLocation;

            _apartmentRepository.Setup(x => x.GetByID(apartment.EstateID)).Returns(Apartments.FirstOrDefault(x => x.EstateID == apartmentId));
            _apartmentRepository.Setup(x => x.Update(apartment)).Returns(Apartments.FirstOrDefault(x => x.EstateID == apartmentId));


            //Act
            var result = _controller.Update(apartment);

            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.NotNull(okObjectResult);

            var pos = okObjectResult.Value as Apartment;
            Assert.NotNull(pos);
            Assert.Equal(expectedApartmentLocation, pos.Location);
        }

        [Fact]
        public void Apartment_Delete_Existing_PositionName()
        {
            //setup
            var apartmentId = 200;

            var apartment = Apartments.FirstOrDefault(x => x.EstateID == apartmentId);


            _apartmentRepository.Setup(x => x.Delete(apartmentId)).Callback(() => Apartments.Remove(apartment)).Returns(apartment);

            //Act
            var result = _controller.Delete(apartmentId);

            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.Equal(okObjectResult.StatusCode, (int)HttpStatusCode.OK);

            Assert.Null(Apartments.FirstOrDefault(x => x.EstateID == apartmentId));
        }

        [Fact]
        public void Apartment_Delete_NotExisting_PositionName()
        {
            //setup
            var userPositionId = 3;

            var position = Apartments.FirstOrDefault(x => x.EstateID == userPositionId);


            _apartmentRepository.Setup(x => x.Delete(userPositionId)).Callback(() => Apartments.Remove(position));

            //Act
            var result = _controller.Delete(userPositionId);

            //Assert
            Assert.Null(Apartments.FirstOrDefault(x => x.EstateID == userPositionId));
        }

        [Fact]
        public void Apartment_Create_PositionName()
        {
            //setup
            var apartment = new Apartment()
            {
                EstateID = 203,
                Location = "Close center",
            };

            _apartmentRepository.Setup(x => x.GetAll())
                .Returns(Apartments);

            _apartmentRepository.Setup(x => x.Create(It.IsAny<Apartment>())).Callback(() =>
            {
                Apartments.Add(apartment);
            }).Returns(new Apartment()
            {
                EstateID = 203,
                Location = "Close center",
            });

            //Act
            var result = _controller.Create(_mapper.Map<ApartmentRequest>(apartment));

            //Assert
            var okObjectResult = result as OkObjectResult;
            Assert.Equal(okObjectResult.StatusCode, (int)HttpStatusCode.OK);

            Assert.NotNull(Apartments.FirstOrDefault(x => x.EstateID == apartment.EstateID));
        }

    }
}
