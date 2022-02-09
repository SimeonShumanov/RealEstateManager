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
    public class warehouseController : ControllerBase
    {
        private readonly IwarehouseService _WarehouseService;
        private readonly IMapper _mapper;

        public warehouseController(IwarehouseService WarehouseService, IMapper mapper)
        {
            _WarehouseService = WarehouseService;
            _mapper = mapper;
        }

        [HttpGet("getAll")]
        public IActionResult GetAll()
        {
            var result = _WarehouseService.GetAll();

            if (result != null) return Ok(result);

            return NoContent();
        }

        [HttpGet("getById")]
        public IActionResult Get(int id)
        {
            var result = _WarehouseService.GetByID(id);

            if (result != null) return Ok(result);

            return NotFound(result);
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] warehouseRequest WarehouseRequest)
        {
            if (WarehouseRequest == null) return BadRequest();

            var Warehouse = _mapper.Map<warehouse>(WarehouseRequest);

            var result = _WarehouseService.Create(Warehouse);

            return Ok(result);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if (id < 0) return BadRequest(id);

            var result = _WarehouseService.Delete(id);

            if (result != null) return Ok(result);

            return NotFound(result);
        }

        [HttpPost("Update")]
        public IActionResult Update([FromBody] warehouse Warehouse)
        {
            if (Warehouse == null) return BadRequest();

            var searchTag = _WarehouseService.GetByID(Warehouse.EstateID);

            if (searchTag == null) return NotFound(Warehouse);

            var result = _WarehouseService.Update(Warehouse);

            if (result != null) return Ok(result);

            return NotFound(result);
        }



    }
}