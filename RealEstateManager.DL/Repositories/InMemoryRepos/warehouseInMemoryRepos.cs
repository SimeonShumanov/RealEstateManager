using System;
using RealEstateManager.Models.DTO;
using System.Collections.Generic;
using System.Linq;
using RealEstateManager.DL.InMemoryDB;
using RealEstateManager.DL.Interfaces;

namespace RealEstateManager.DL.Repositories.InMemoryRepos
{
    public class warehouseInMemoryRepos : IwarehouseRepository
    {

        public  void warehouseInMemoryRepository()
        {

        }

        public warehouse Create(warehouse Warehouse)
        {
            warehouseInMemoryCollection.warehouseDb.Add(Warehouse);

            return Warehouse;
        }

        public warehouse Delete(int id)
        {
            var Warehouse = warehouseInMemoryCollection.warehouseDb.FirstOrDefault(x => x.EstateID == id);

            if (Warehouse != null) warehouseInMemoryCollection.warehouseDb.Remove(Warehouse);

            return Warehouse;
        }

        public IEnumerable<warehouse> GetAll()
        {
            return warehouseInMemoryCollection.warehouseDb;
        }

        public warehouse GetByID(int id)
        {
            return warehouseInMemoryCollection.warehouseDb.FirstOrDefault(x => x.EstateID == id);
        }

        public warehouse Update(warehouse Warehouse)
        {
            var item = warehouseInMemoryCollection.warehouseDb.FirstOrDefault(x => x.EstateID == Warehouse.EstateID);

            item.Location = Warehouse.Location;
            item.Price = Warehouse.Price;

            return Warehouse;
        }
    }
}