using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VehicleAPI.InMemoryDB;
using VehicleAPI.Service;

namespace VehicleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private IVehicleService _vehicleService;
        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
            _vehicleService.CreateVehicles();
        }
        /// <summary>
        /// Get all vehicles
        /// </summary>
        /// <returns></returns>
        [HttpGet("getvehicles")]
        public ActionResult<IEnumerable<Vehicle>> Get()
        {
            try
            {
                return Ok(_vehicleService.GetVehicles());
            }
            catch
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// Get vehicles by customer
        /// </summary>
        /// <param name="id">Customer Id</param>
        /// <returns></returns>
        [HttpGet("getcustomervehicles/{id}")]
        public ActionResult<IEnumerable<Vehicle>> GetCutomerVehicles(int id)
        {
            try
            {
                if (id > 0)
                    return Ok(_vehicleService.GetCutomerVehicles(id));
                return new EmptyResult();
            }
            catch
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// Get vehicles by status (Online/Offline)
        /// </summary>
        /// <param name="status">0 to get offline and 1 to get online vehicles</param>
        /// <returns></returns>
        [HttpGet("getvehiclesbystatus/{status}")]
        public ActionResult<IEnumerable<Vehicle>> GetVehiclesByStatus(int status)
        {
            try
            {
                if (status >= 0)
                {
                    if (status == 0)
                        return Ok(_vehicleService.GetOfflineVehicles());
                    if (status == 1)
                        return Ok(_vehicleService.GetOnlineVehicles());
                }
                return new EmptyResult();
            }
            catch
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// Simulate vehicle send status request
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("ping/{id}")]
        public IActionResult PingVehicle(int id)
        {
            try
            {
                if (id == 0)
                    return BadRequest();

                var vehicle = _vehicleService.GetVehicle(id);
                if (vehicle == null)
                    return new EmptyResult();

                vehicle.LastPingDate = DateTime.Now;
                _vehicleService.UpdateVehicle(vehicle);

                return new NoContentResult();
            }
            catch
            {
                return BadRequest("Ping failed due to technical error.");
            }
        }
    }
}