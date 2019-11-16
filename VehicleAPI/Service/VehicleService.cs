using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using VehicleAPI.InMemoryDB;

namespace VehicleAPI.Service
{
    public interface IVehicleService
    {
        IEnumerable<Vehicle> GetVehicles();
        IEnumerable<Vehicle> GetCutomerVehicles(int id);

        Vehicle GetVehicle(int id);

        void UpdateVehicle(Vehicle vehicle);

        IEnumerable<Vehicle> GetOnlineVehicles();

        IEnumerable<Vehicle> GetOfflineVehicles();
        void CreateVehicles();
    }
    public class VehicleService : IVehicleService
    {
        private ApplicationDbContext _context;

        public VehicleService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Create all vehicles in memoryDB at first launch
        /// </summary>
        public void CreateVehicles()
        {
            _context.CreateVehicles();
        }

        /// <summary>
        /// Get all vehicles
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Vehicle> GetVehicles()
        {
            return _context.Vehicles;
        }
        /// <summary>
        /// Get customer vehicles
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<Vehicle> GetCutomerVehicles(int id)
        {
            return _context.Vehicles.Where(v => v.CustomerId == id).ToList();
        }
        /// <summary>
        /// Get vehicle by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Vehicle GetVehicle(int id)
        {
            return _context.Vehicles.FirstOrDefault(i => i.Id == id);
        }
        /// <summary>
        /// Update vehicle
        /// </summary>
        /// <param name="vehicle"></param>
        public void UpdateVehicle(Vehicle vehicle)
        {
            _context.Vehicles.Update(vehicle);
            _context.SaveChanges();
        }
        /// <summary>
        /// Get online vehicles
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Vehicle> GetOnlineVehicles()
        {
            return _context.Vehicles.Where(v => v.LastPingDate < DateTime.Now.AddMinutes(-1)).ToList();
        }
        /// <summary>
        /// Get offline vehicles
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Vehicle> GetOfflineVehicles()
        {
            return _context.Vehicles.Where(v => v.LastPingDate > DateTime.Now.AddMinutes(-1)).ToList();
        }
    }
}
