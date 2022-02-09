using System;
using AutoMapper;
using RealEstateManager.BL.Interfaces;
using RealEstateManager.Models.DTO;
using RealEstateManager.Models.Requests;
using RealEstateManager.Models.Responses;
using Microsoft.AspNetCore.Mvc;
namespace RealEstateManager.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ApartmentController : ControllerBase
    {
        private readonly IApartmentService _apartmentService;
        private readonly IMapper _mapper;

        public ApartmentController(IApartmentService apartmentService, IMapper mapper)
        {
            _apartmentService = apartmentService;
            _mapper = mapper;
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _apartmentService.GetAll();

            if (result != null) return Ok(result);

            return NoContent();
        }

        [HttpGet("getById")]
        public IActionResult Get(int id)
        {
            var result = _apartmentService.GetByID(id);

            if (result != null) return Ok(result);

            return NotFound(result);
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] ApartmentRequest apartmentRequest)
        {
            if (apartmentRequest == null) return BadRequest();

            var apartment = _mapper.Map<Apartment>(apartmentRequest);

            var result = _apartmentService.Create(apartment);

            return Ok(result);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if (id < 0) return BadRequest(id);

            var result = _apartmentService.Delete(id);

            if (result != null) return Ok(result);

            return NotFound(result);
        }

        [HttpPost("Update")]
        public IActionResult Update([FromBody] Apartment apartment)
        {
            if (apartment == null) return BadRequest();

            var searchTag = _apartmentService.GetByID(apartment.EstateID);

            if (searchTag == null) return NotFound(apartment);

            var result = _apartmentService.Update(apartment);

            if (result != null) return Ok(result);

            return NotFound(result);
        }



    }
}