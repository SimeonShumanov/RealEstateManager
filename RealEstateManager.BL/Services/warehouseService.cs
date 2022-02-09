using System;
using RealEstateManager.BL.Interfaces;
using RealEstateManager.DL.Interfaces;
using RealEstateManager.Models.DTO;
using System.Collections.Generic;
using System.Linq;
using Serilog;
namespace RealEstateManager.BL.Services
{
    public class warehouseService : IwarehouseService
    {
        private readonly IwarehouseRepository _WarehouseRepository;
        private readonly ILogger _logger;

        public warehouseService(IwarehouseRepository WarehouseRepository, ILogger logger)
        {
            _WarehouseRepository = WarehouseRepository;
            _logger = logger;
        }

        public warehouse Create(warehouse Warehouse)
        {
            var index = _WarehouseRepository.GetAll()?.OrderByDescending(x => x.EstateID).FirstOrDefault()?.EstateID;

            Warehouse.EstateID = (int)(index != null ? index + 1 : 1);

            return _WarehouseRepository.Create(Warehouse);
        }

        public warehouse Update(warehouse Warehouse)
        {
            return _WarehouseRepository.Update(Warehouse);
        }

        public warehouse Delete(int id)
        {
            return _WarehouseRepository.Delete(id);
        }

        public warehouse GetByID(int id)
        {
            return _WarehouseRepository.GetByID(id);
        }

        public IEnumerable<warehouse> GetAll()
        {
            _logger.Information("warehouse GetAll");

            return _WarehouseRepository.GetAll();
        }
    }
}