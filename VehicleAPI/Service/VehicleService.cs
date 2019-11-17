using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleAPI.InMemoryDB;

namespace VehicleAPI.Service
{
    public interface IVehicleService
    {
        Task<IEnumerable<Vehicle>> GetVehicles();
        Task<IEnumerable<Vehicle>> GetCutomerVehicles(int id);

        Task<Vehicle> GetVehicle(int id);

        Task UpdateVehicle(Vehicle vehicle);

        Task<IEnumerable<Vehicle>> GetOnlineVehicles();

        Task<IEnumerable<Vehicle>> GetOfflineVehicles();
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
        public async Task<IEnumerable<Vehicle>> GetVehicles()
        {
            return await _context.Vehicles.ToListAsync();
        }
        /// <summary>
        /// Get customer vehicles
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Vehicle>> GetCutomerVehicles(int id)
        {
            return await _context.Vehicles.Where(v => v.CustomerId == id).ToListAsync();
        }
        /// <summary>
        /// Get vehicle by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Vehicle> GetVehicle(int id)
        {
            return await Task.FromResult(_context.Vehicles.FirstOrDefault(i => i.Id == id));
        }
        /// <summary>
        /// Update vehicle
        /// </summary>
        /// <param name="vehicle"></param>
        public async Task UpdateVehicle(Vehicle vehicle)
        {
            _context.Vehicles.Update(vehicle);
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Get online vehicles
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Vehicle>> GetOnlineVehicles()
        {
            return await _context.Vehicles.Where(v => v.LastPingDate > DateTime.Now.AddMinutes(-1)).ToListAsync();
        }
        /// <summary>
        /// Get offline vehicles
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Vehicle>> GetOfflineVehicles()
        {
            return await _context.Vehicles.Where(v => v.LastPingDate < DateTime.Now.AddMinutes(-1)).ToListAsync();
        }
    }
}
