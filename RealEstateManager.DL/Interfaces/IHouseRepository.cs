using RealEstateManager.Models.DTO;
using System.Collections.Generic;

namespace RealEstateManager.DL.Interfaces
{
    public interface IHouseRepository
    {
        House Create(House house);

        House Update(House house);

        House Delete(int id);

        House GetByID(int id);

        IEnumerable<House> GetAll();

    }
}
