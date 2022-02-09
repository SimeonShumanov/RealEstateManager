
using RealEstateManager.Models.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using House = RealEstateManager.Models.DTO.House;
using RealEstateManager.DL.Interfaces;

namespace RealEstateManager.DL.Repositories.MongoRepos
{
    public class HouseMongoRepository : IHouseRepository
    {
        private readonly IMongoCollection<House> _houseCollection;


        public HouseMongoRepository(IOptions<MongoDbConfig> config)
        {
            var client = new MongoClient(config.Value.ConnectionString);
            var database = client.GetDatabase(config.Value.DatabaseName);

            _houseCollection = database.GetCollection<House>("Houses");
        }

        public House Create(House userPosition)
        {
            _houseCollection.InsertOne(userPosition);
            return userPosition;
        }

        public House Delete(int id)
        {
            var house = GetByID(id);
            _houseCollection.DeleteOne(house => house.EstateID == id);

            return house;
        }

        public IEnumerable<House> GetAll()
        {
            return _houseCollection.Find(house => true).ToList();
        }

        public House GetByID(int id) =>
            _houseCollection.Find(userPosition => userPosition.EstateID == id).FirstOrDefault();

        public House Update(House house)
        {
            _houseCollection.ReplaceOne(houseToReplace => houseToReplace.EstateID == house.EstateID, house);
            return house;
        }
    }
}