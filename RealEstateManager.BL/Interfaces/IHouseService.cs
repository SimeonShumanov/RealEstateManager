using System;
using RealEstateManager.Models.DTO;
using System.Collections.Generic;
using System.Text;

namespace RealEstateManager.BL.Interfaces
{
    public interface IHouseService
    {
        House Create(House house);

        House Update(House house);

        House Delete(int id);

        House GetByID(int id);

        IEnumerable<House> GetAll();
    }
}
