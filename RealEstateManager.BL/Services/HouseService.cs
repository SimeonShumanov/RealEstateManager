using System;
using RealEstateManager.BL.Interfaces;
using RealEstateManager.DL.Interfaces;
using RealEstateManager.Models.DTO;
using System.Collections.Generic;
using System.Linq;
using Serilog;
namespace RealEstateManager.BL.Services
{
    public class HouseService : IHouseService
    {
        private readonly IHouseRepository _houseRepository;
        private readonly ILogger _logger;

        public HouseService(IHouseRepository houseRepository, ILogger logger)
        {
            _houseRepository = houseRepository;
            _logger = logger;
        }

        public House Create(House house)
        {
            var index = _houseRepository.GetAll()?.OrderByDescending(x => x.EstateID).FirstOrDefault()?.EstateID;

            house.EstateID = (int)(index != null ? index + 1 : 1);

            return _houseRepository.Create(house);
        }

        public House Update(House house)
        {
            return _houseRepository.Update(house);
        }

        public House Delete(int id)
        {
            return _houseRepository.Delete(id);
        }

        public House GetByID(int id)
        {
            return _houseRepository.GetByID(id);
        }

        public IEnumerable<House> GetAll()
        {
            _logger.Information("House GetAll");

            return _houseRepository.GetAll();
        }
    }
}