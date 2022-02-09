using RealEstateManager.Models.DTO;
using System.Collections.Generic;

namespace RealEstateManager.DL.Interfaces
{
    public interface IApartmentRepository
    {
        Apartment Create(Apartment apartment);

        Apartment Update(Apartment apartment);

        Apartment Delete(int id);

        Apartment GetByID(int id);

        IEnumerable<Apartment> GetAll();

    }
}
