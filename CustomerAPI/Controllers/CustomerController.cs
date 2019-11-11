using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerAPI.InMemoryDB;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CustomerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ApplicationDbContext _context;
        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
            _context.CreateCustomers();
        }

        [HttpGet("getcustomers")]
        public ActionResult<IEnumerable<Customer>> Get()
        {
            return _context.Customers;
        }
        [HttpGet("{id}")]
        public ActionResult<IEnumerable<Customer>> GetCustomer(int id)
        {
            return _context.Customers.Where(c => c.Id == id).ToList();
        }
        //[HttpGet("getcustomers/{id}")]
        //public ActionResult<IEnumerable<Customer>> GetCustomers(string ids)
        //{
        //    var separated = ids.Split(new char[] { ',' });
        //    return _context.Customers.Where(c => separated.Contains(c.Id.ToString())).ToList();
        //}
    }
}