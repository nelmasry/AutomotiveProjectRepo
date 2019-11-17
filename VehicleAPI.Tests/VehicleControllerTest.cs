using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using VehicleAPI.Controllers;
using VehicleAPI.InMemoryDB;
using VehicleAPI.Service;
using Xunit;

namespace CustomerAPI.Tests
{
    public class VehicleControllerTest
    {
        [Fact]
        public void Getvehicles_success()
        {
            IEnumerable<Vehicle> fakeVehicles = CreateFakeVehicles();
            var mockService = new Mock<IVehicleService>();
            mockService.Setup(s => s.GetVehicles()).Returns(fakeVehicles);

            VehicleController controller = new VehicleController(mockService.Object);
            var okResult = controller.Get();

            var okObjectResult = okResult.Result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            var model = okObjectResult.Value as IEnumerable<Vehicle>;
            Assert.NotNull(model);
            int count = model.Count();
            Assert.Equal(7, count);
        }
        [Fact]
        public void Getcutomervehicles_success()
        {
            int fakeCustomerId = 3;
            IEnumerable<Vehicle> fakeVehicles = CreateFakeVehicles()
                .Where(v => v.CustomerId == fakeCustomerId).ToList();
            var mockService = new Mock<IVehicleService>();
            mockService.Setup(s => s.GetCutomerVehicles(fakeCustomerId)).Returns(fakeVehicles);

            VehicleController controller = new VehicleController(mockService.Object);
            var okResult = controller.GetCutomerVehicles(fakeCustomerId);

            var okObjectResult = okResult.Result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            var model = okObjectResult.Value as IEnumerable<Vehicle>;
            Assert.NotNull(model);
            int count = model.Count();
            Assert.Equal(2, count);
            Assert.Equal(fakeCustomerId, model.FirstOrDefault().CustomerId);
        }
        [Fact]
        public void Getcutomervehicles_nonexisitngcustomer_emptyresult()
        {
            int fakeCustomerId = 40;
            IEnumerable<Vehicle> fakeVehicles = new List<Vehicle>();
            var mockService = new Mock<IVehicleService>();
            mockService.Setup(s => s.GetCutomerVehicles(fakeCustomerId)).Returns(fakeVehicles);

            VehicleController controller = new VehicleController(mockService.Object);
            var okResult = controller.GetCutomerVehicles(fakeCustomerId);

            var okObjectResult = okResult.Result as OkObjectResult;
            Assert.Null(okObjectResult);
            Assert.IsAssignableFrom<EmptyResult>(okResult.Result);
        }
        [Fact]
        public void GetOnlinceVehicles_initialstart_emptyresult()
        {
            IEnumerable<Vehicle> onlineVehicles = CreateFakeVehicles()
                .Where(v=>v.LastPingDate > DateTime.Now.AddMinutes(-1)).ToList();
            var mockService = new Mock<IVehicleService>();
            mockService.Setup(s => s.GetOnlineVehicles()).Returns(onlineVehicles);

            VehicleController controller = new VehicleController(mockService.Object);
            var okResult = controller.GetVehiclesByStatus(1);

            var okObjectResult = okResult.Result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            var model = okObjectResult.Value as IEnumerable<Vehicle>;
            Assert.NotNull(model);
            int count = model.Count();
            Assert.Equal(0, count);
        }
        [Fact]
        public void GetOfflineVehicles_initialstart_getall()
        {
            IEnumerable<Vehicle> offlineVehicles = CreateFakeVehicles();
            var mockService = new Mock<IVehicleService>();
            mockService.Setup(s => s.GetOfflineVehicles()).Returns(offlineVehicles);

            VehicleController controller = new VehicleController(mockService.Object);
            var okResult = controller.GetVehiclesByStatus(0);

            var okObjectResult = okResult.Result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            var model = okObjectResult.Value as IEnumerable<Vehicle>;
            Assert.NotNull(model);
            int count = model.Count();
            Assert.Equal(7, count);
            Assert.True(model.FirstOrDefault().LastPingDate < DateTime.Now.AddMinutes(-1));
        }
        [Fact]
        public void Pingvehicle_exisitingvehicle_nocontentresult()
        {
            IEnumerable<Vehicle> offlineVehicles = new List<Vehicle>();
            var mockService = new Mock<IVehicleService>();
            mockService.Setup(s => s.GetVehicle(3)).Returns(new Vehicle() { Id = 3 });

            VehicleController controller = new VehicleController(mockService.Object);
            var result = controller.PingVehicle(3);

            var objectResult = result as OkObjectResult;
            Assert.Null(objectResult);
            Assert.IsAssignableFrom<NoContentResult>(result);
        }
        [Fact]
        public void Pingvehicle_nonexisitngvehicle_emptyresult()
        {
            IEnumerable<Vehicle> offlineVehicles = new List<Vehicle>();
            var mockService = new Mock<IVehicleService>();
            //mockService.Setup(s => s.UpdateVehicle()).Returns(offlineVehicles);

            VehicleController controller = new VehicleController(mockService.Object);
            var result = controller.PingVehicle(40);

            var objectResult = result as OkObjectResult;
            Assert.Null(objectResult);
            Assert.IsAssignableFrom<EmptyResult>(result);
        }



        private static IEnumerable<Vehicle> CreateFakeVehicles()
        {
            return new List<Vehicle>() {
            new Vehicle
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
                },
                new Vehicle
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
            },
                new Vehicle
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
            }
            };
        }
    }
}
