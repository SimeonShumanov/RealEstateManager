
using RealEstateManager.Models.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using warehouse = RealEstateManager.Models.DTO.warehouse;
using RealEstateManager.DL.Interfaces;

namespace RealEstateManager.DL.Repositories.MongoRepos
{
    public class warehouseMongoRepository : IwarehouseRepository
    {
        private readonly IMongoCollection<warehouse> _WarehouseCollection;


        public warehouseMongoRepository(IOptions<MongoDbConfig> config)
        {
            var client = new MongoClient(config.Value.ConnectionString);
            var database = client.GetDatabase(config.Value.DatabaseName);

            _WarehouseCollection = database.GetCollection<warehouse>("warehouses");
        }

        public warehouse Create(warehouse userPosition)
        {
            _WarehouseCollection.InsertOne(userPosition);
            return userPosition;
        }

        public warehouse Delete(int id)
        {
            var Warehouse = GetByID(id);
            _WarehouseCollection.DeleteOne(Warehouse => Warehouse.EstateID == id);

            return Warehouse;
        }

        public IEnumerable<warehouse> GetAll()
        {
            return _WarehouseCollection.Find(Warehouse => true).ToList();
        }

        public warehouse GetByID(int id) =>
            _WarehouseCollection.Find(userPosition => userPosition.EstateID == id).FirstOrDefault();

        public warehouse Update(warehouse Warehouse)
        {
            _WarehouseCollection.ReplaceOne(WarehouseToReplace => WarehouseToReplace.EstateID == Warehouse.EstateID, Warehouse);
            return Warehouse;
        }
    }
}