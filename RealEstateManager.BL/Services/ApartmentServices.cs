using System;
using RealEstateManager.BL.Interfaces;
using RealEstateManager.DL.Interfaces;
using RealEstateManager.Models.DTO;
using System.Collections.Generic;
using System.Linq;
using Serilog;
namespace RealEstateManager.BL.Services
{
    public class ApartmentService : IApartmentService
    {
        private readonly IApartmentRepository _apartmentRepository;
        private readonly ILogger _logger;

        public ApartmentService(IApartmentRepository apartmentRepository, ILogger logger)
        {
            _apartmentRepository = apartmentRepository;
            _logger = logger;
        }

        public Apartment Create(Apartment apartment)
        {
            var index = _apartmentRepository.GetAll()?.OrderByDescending(x => x.EstateID).FirstOrDefault()?.EstateID;

            apartment.EstateID = (int)(index != null ? index + 1 : 1);

            return _apartmentRepository.Create(apartment);
        }

        public Apartment Update(Apartment apartment)
        {
            return _apartmentRepository.Update(apartment);
        }

        public Apartment Delete(int id)
        {
            return _apartmentRepository.Delete(id);
        }

        public Apartment GetByID(int id)
        {
            return _apartmentRepository.GetByID(id);
        }

        public IEnumerable<Apartment> GetAll()
        {
            _logger.Information("Apartment GetAll");

            return _apartmentRepository.GetAll();
        }
    }
}