using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleAPI.InMemoryDB
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> context) : base(context)
        {

        }
        public void CreateVehicles()
        {
            if (!Vehicles.Any())
            {
                // Customer 1
                Vehicles.AddRange(new Vehicle
                {
                    CustomerId = 1,
                    VehicleId = "YS2R4X20005399401",
                    RegistrationNumber = "ABC123",
                    LastPingDate = DateTime.Now.AddMinutes(-5)
                },new Vehicle
                {
                    CustomerId = 1,
                    VehicleId = "VLUR4X20009093588",
                    RegistrationNumber = "DEF456",
                    LastPingDate = DateTime.Now.AddMinutes(-5)
                },
                new Vehicle
                {
                    CustomerId = 1,
                    VehicleId = "VLUR4X20009048066",
                    RegistrationNumber = "GHI789",
                    LastPingDate = DateTime.Now.AddMinutes(-5)
                });
                // Customer 2
                Vehicles.AddRange(new Vehicle
                {
                    CustomerId = 2,
                    VehicleId = "YS2R4X20005388011",
                    RegistrationNumber = "JKL012",
                    LastPingDate = DateTime.Now.AddMinutes(-5)
                }, new Vehicle
                {
                    CustomerId = 2,
                    VehicleId = "YS2R4X20005387949",
                    RegistrationNumber = "MNO345",
                    LastPingDate = DateTime.Now.AddMinutes(-5)
                });
                // Customer 3
                Vehicles.AddRange(new Vehicle
                {
                    CustomerId = 3,
                    VehicleId = "VLUR4X20009048066",
                    RegistrationNumber = "PQR678",
                    LastPingDate = DateTime.Now.AddMinutes(-5)
                }, new Vehicle
                {
                    CustomerId = 3,
                    VehicleId = "YS2R4X20005387055",
                    RegistrationNumber = "STU901",
                    LastPingDate = DateTime.Now.AddMinutes(-5)
                });
                SaveChanges();
            }
        }
    }

    public class Vehicle
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string VehicleId { get; set; }

        public string RegistrationNumber { get; set; }

        public DateTime LastPingDate { get; set; }

    }
}
