using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VehicleAPI.InMemoryDB;

namespace VehicleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private ApplicationDbContext _context;
        public VehicleController(ApplicationDbContext context)
        {
            _context = context;
            _context.CreateVehicles();
        }
        [HttpGet("getvehicles")]
        public ActionResult<IEnumerable<Vehicle>> Get()
        {
            return _context.Vehicles;
        }

        [HttpGet("getcustomervehicles/{id}")]
        public ActionResult<IEnumerable<Vehicle>> GetCutomerVehicles(int id)
        {
            return _context.Vehicles.Where(v => v.CustomerId == id).ToList();
        }

        //[HttpPost("ping")]
        [HttpPut("ping/{id}")]
        public IActionResult PingVehicle(int id)
        {
            if (id == 0)
                return BadRequest();

            var vehicle = _context.Vehicles.FirstOrDefault(i => i.Id == id);
            if (vehicle == null)
                return NotFound();

            vehicle.LastPingDate = DateTime.Now;

            _context.Vehicles.Update(vehicle);
            _context.SaveChanges();
            return new NoContentResult();
        }

        [HttpGet("getonlinevehicles")]
        public ActionResult<IEnumerable<Vehicle>> GetOnlinceVehicles()
        {
            var onlineVehicles = _context.Vehicles.Where(v => v.LastPingDate > DateTime.Now.AddMinutes(-1)).ToList();
            return onlineVehicles;
        }
        [HttpGet("getofflinevehicles")]
        public ActionResult<IEnumerable<Vehicle>> GetOfflineVehicles()
        {
            var offlineVehicles = _context.Vehicles.Where(v => v.LastPingDate < DateTime.Now.AddMinutes(-1)).ToList();
            return offlineVehicles;
        }
    }
}