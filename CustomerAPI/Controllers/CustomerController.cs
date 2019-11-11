using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerAPI.InMemoryDB;
using CustomerAPI.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CustomerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomerService _service;
        public CustomerController(ICustomerService service)
        {
            _service = service;
            _service.CreateCustomers();
        }

        [HttpGet("getcustomers")]
        public ActionResult<IEnumerable<Customer>> Get()
        {
            try
            {
                return Ok(_service.GetCustomers());
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Customer>> GetCustomer(int id)
        {
            try
            {
                return Ok(_service.GetCutomer(id));
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}