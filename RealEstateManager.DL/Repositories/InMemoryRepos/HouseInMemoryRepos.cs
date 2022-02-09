using RealEstateManager.Models.DTO;
using System.Collections.Generic;
using System.Linq;
using RealEstateManager.DL.InMemoryDB;
using RealEstateManager.DL.Interfaces;

namespace RealEstateManager.DL.Repositories.InMemoryRepos
{
    public class HouseInMemoryRepos : IHouseRepository
    {

        public void HouseInMemoryRepository()
        {
           
        }

        public House Create(House house)
        {
            HouseInMemoryCollection.HouseDb.Add(house);

            return house;
        }

        public House Delete(int id)
        {
            var house = HouseInMemoryCollection.HouseDb.FirstOrDefault(x => x.EstateID == id);

            if (house != null) HouseInMemoryCollection.HouseDb.Remove(house);

            return house;
        }

        public IEnumerable<House> GetAll()
        {
            return HouseInMemoryCollection.HouseDb;
        }

        public House GetByID(int id)
        {
            return HouseInMemoryCollection.HouseDb.FirstOrDefault(x => x.EstateID == id);
        }

        public House Update(House house)
        {
            var item = HouseInMemoryCollection.HouseDb.FirstOrDefault(x => x.EstateID == house.EstateID);

            item.Location = house.Location;
            item.Price = house.Price;

            return house;
        }
    }
}