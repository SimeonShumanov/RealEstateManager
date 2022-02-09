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
    public class HouseController : ControllerBase
    {
        private readonly IHouseService _houseService;
        private readonly IMapper _mapper;

        public HouseController(IHouseService houseService, IMapper mapper)
        {
            _houseService = houseService;
            _mapper = mapper;
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _houseService.GetAll();

            if (result != null) return Ok(result);

            return NoContent();
        }

        [HttpGet("getById")]
        public IActionResult Get(int id)
        {
            var result = _houseService.GetByID(id);

            if (result != null) return Ok(result);

            return NotFound(result);
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] HouseRequest houseRequest)
        {
            if (houseRequest == null) return BadRequest();

            var house = _mapper.Map<House>(houseRequest);

            var result = _houseService.Create(house);

            return Ok(result);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if (id < 0) return BadRequest(id);

            var result = _houseService.Delete(id);

            if (result != null) return Ok(result);

            return NotFound(result);
        }

        [HttpPost("Update")]
        public IActionResult Update([FromBody] House house)
        {
            if (house == null) return BadRequest();

            var searchTag = _houseService.GetByID(house.EstateID);

            if (searchTag == null) return NotFound(house);

            var result = _houseService.Update(house);

            if (result != null) return Ok(result);

            return NotFound(result);
        }



    }
}