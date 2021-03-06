using CustomerAPI.Controllers;
using CustomerAPI.InMemoryDB;
using CustomerAPI.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CustomerAPI.UnitTests
{
    public class CustomerControllerTest
    {
        [Fact]
        public async Task Getcustomers_all_success()
        {
            IEnumerable<Customer> fakeCustomers = CreateFakeCustomers();
            var mockService = new Mock<ICustomerService>();
            mockService.Setup(s => s.GetCustomers()).Returns(Task.FromResult(fakeCustomers));

            CustomerController controller = new CustomerController(mockService.Object);
            var okResult = await controller.Get();

            var okObjectResult = okResult.Result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            var model = okObjectResult.Value as IEnumerable<Customer>;
            Assert.NotNull(model);
            int count = model.Count();
            Assert.Equal(3, count);
        }

        [Fact]
        public async Task Getcustomer_existingcustomer_success()
        {
            IEnumerable<Customer> fakeCustomers = new List<Customer>() {
            new Customer
                {
                    Name = "Johans Bulk AB",
                    Address = "Balkvägen 12, 222 22 Stockholm"
                }
            };
            var mockService = new Mock<ICustomerService>();
            mockService.Setup(s => s.GetCutomer(2)).Returns(Task.FromResult(fakeCustomers));

            CustomerController controller = new CustomerController(mockService.Object);
            var okResult = await controller.GetCustomer(2);

            var okObjectResult = okResult.Result as OkObjectResult;
            Assert.NotNull(okObjectResult);
            var model = okObjectResult.Value as IEnumerable<Customer>;
            Assert.NotNull(model);
            int count = model.Count();
            Assert.Equal(1, count);
            Assert.Equal("Johans Bulk AB", model.FirstOrDefault().Name);
        }

        private static IEnumerable<Customer> CreateFakeCustomers()
        {
            var customers = new List<Customer>() {
            new Customer
                {
                    Name = "Kalles Grustransporter AB",
                    Address = "Cementvägen 8, 111 11 Södertälje"
                },new Customer
                {
                    Name = "Johans Bulk AB",
                    Address = "Balkvägen 12, 222 22 Stockholm"
                },
                new Customer
                {
                    Name = "Haralds Värdetransporter AB",
                    Address = "Budgetvägen 1, 333 33 Uppsala"
                }
            };
            return customers;
        }
    }
}
