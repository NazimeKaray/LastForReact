using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyCar_Creation.Context;
using MyCar_Creation.Dtos;
using MyCar_Creation.Entities;

namespace MyCar_Creation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        public VehicleController(ApplicationDbContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
        }
        [HttpPost]
        public ActionResult CreateVehicle(VehicleDTO model)
        {
            var obj = _mapper.Map<Vehicle>(model);
            _context.Vehicles.Add(obj);
            if (_context.SaveChanges() > 0)
            {
                return Ok();
            }
            return BadRequest();
        }
        [HttpDelete("{vehicleId}")]
        public ActionResult DeleteVehicle([FromRoute] Guid vehicleId)
        {
            var vehicle = _context.Vehicles.Find(vehicleId);
            if (vehicle != null)
            {
                _context.Vehicles.Remove(vehicle);
                if (_context.SaveChanges() > 0)
                {
                    return Ok();
                }
            }
            return BadRequest();
        }
        [HttpGet]
        public ActionResult GetAllVehicle()
        {
            List<Vehicle> vehicles = _context.Vehicles.ToList();
            if (vehicles is not null)
            {
                return Ok(vehicles);
            }
            return BadRequest();
        }

        [HttpPut("{vehicleId}")]
        public async Task<ActionResult> UpdateVehicle([FromRoute] Guid vehicleId, VehicleDTO model)
        {
            var vehicle = _context.Vehicles.Find(vehicleId);
            if (vehicle != null)
            {
                Vehicle obj = _mapper.Map<VehicleDTO, Vehicle>(model, vehicle);
                if (await _context.SaveChangesAsync() > 0)
                {
                    return Ok(obj);
                }
                return BadRequest();
            }
            return NotFound();


        }

        

        [HttpGet("Vehicle/{vehicleId}")]

        public ActionResult GetVehiclesById([FromRoute] Guid vehicleId) 
        {
            Vehicle vehicle = _context.Vehicles.Find(vehicleId);
            if (vehicle is not null)
            {
                return Ok(vehicle);
            }
            return NotFound();
        }
    }
}
