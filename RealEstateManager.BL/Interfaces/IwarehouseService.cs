using System;
using RealEstateManager.Models.DTO;
using System.Collections.Generic;
using System.Text;

namespace RealEstateManager.BL.Interfaces
{
    public interface IwarehouseService
    {
        warehouse Create(warehouse Warehouse);

        warehouse Update(warehouse Warehouse);

        warehouse Delete(int id);

        warehouse GetByID(int id);

        IEnumerable<warehouse> GetAll();
    }
}
