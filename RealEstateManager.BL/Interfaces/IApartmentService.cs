using System;
using RealEstateManager.Models.DTO;
using System.Collections.Generic;
using System.Text;

namespace RealEstateManager.BL.Interfaces
{
    public interface IApartmentService
    {
        Apartment Create(Apartment apartment);

        Apartment Update(Apartment apartment);

        Apartment Delete(int id);

        Apartment GetByID(int id);

        IEnumerable<Apartment> GetAll();
    }
}
