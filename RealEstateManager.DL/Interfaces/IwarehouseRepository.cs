using RealEstateManager.Models.DTO;
using System.Collections.Generic;

namespace RealEstateManager.DL.Interfaces
{
    public interface IwarehouseRepository
    {
        warehouse Create(warehouse Warehouse);

        warehouse Update(warehouse Warehouse);

        warehouse Delete(int id);

        warehouse GetByID(int id);

        IEnumerable<warehouse> GetAll();

    }
}
