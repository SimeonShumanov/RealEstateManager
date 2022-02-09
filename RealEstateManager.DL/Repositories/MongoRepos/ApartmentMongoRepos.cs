
using RealEstateManager.Models.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using Apartment = RealEstateManager.Models.DTO.Apartment;
using RealEstateManager.DL.Interfaces;

namespace RealEstateManager.DL.Repositories.MongoRepos
{
    public class ApartmentMongoRepository : IApartmentRepository
    {
        private readonly IMongoCollection<Apartment> _apartmentCollection;


        public ApartmentMongoRepository(IOptions<MongoDbConfig> config)
        {
            var client = new MongoClient(config.Value.ConnectionString);
            var database = client.GetDatabase(config.Value.DatabaseName);

            _apartmentCollection = database.GetCollection<Apartment>("Apartments");
        }

        public Apartment Create(Apartment userPosition)
        {
            _apartmentCollection.InsertOne(userPosition);
            return userPosition;
        }

        public Apartment Delete(int id)
        {
            var apartment = GetByID(id);
            _apartmentCollection.DeleteOne(apartment => apartment.EstateID == id);

            return apartment;
        }

        public IEnumerable<Apartment> GetAll()
        {
            return _apartmentCollection.Find(apartment => true).ToList();
        }

        public Apartment GetByID(int id) =>
            _apartmentCollection.Find(userPosition => userPosition.EstateID == id).FirstOrDefault();

        public Apartment Update(Apartment apartment)
        {
            _apartmentCollection.ReplaceOne(apartmentToReplace => apartmentToReplace.EstateID == apartment.EstateID, apartment);
            return apartment;
        }
    }
}