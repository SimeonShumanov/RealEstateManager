using RealEstateManager.Models.DTO;
using System.Collections.Generic;
using System.Linq;
using RealEstateManager.DL.InMemoryDB;
using RealEstateManager.DL.Interfaces;

namespace RealEstateManager.DL.Repositories.InMemoryRepos
{
    public class ApartmentInMemoryRepos : IApartmentRepository
    {

        public void  ApartmentInMemoryRepository()
        {

        }

        public Apartment Create(Apartment apartment)
        {
            ApartmentInMemoryCollection.ApartmentDb.Add(apartment);

            return apartment;
        }

        public Apartment Delete(int id)
        {
            var apartment = ApartmentInMemoryCollection.ApartmentDb.FirstOrDefault(x => x.EstateID == id);

            if (apartment != null) ApartmentInMemoryCollection.ApartmentDb.Remove(apartment);

            return apartment;
        }

        public IEnumerable<Apartment> GetAll()
        {
            return ApartmentInMemoryCollection.ApartmentDb;
        }

        public Apartment GetByID(int id)
        {
            return ApartmentInMemoryCollection.ApartmentDb.FirstOrDefault(x => x.EstateID == id);
        }

        public Apartment Update(Apartment apartment)
        {
            var item = ApartmentInMemoryCollection.ApartmentDb.FirstOrDefault(x => x.EstateID == apartment.EstateID);

            item.Location = apartment.Location;
            item.Price = apartment.Price;

            return apartment;
        }
    }
}