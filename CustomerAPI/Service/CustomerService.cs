using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CustomerAPI.InMemoryDB;

namespace CustomerAPI.Service
{
    public interface ICustomerService
    {
        IEnumerable<Customer> GetCustomers();
        IEnumerable<Customer> GetCutomer(int id);
        void CreateCustomers();
    }
    public class CustomerService : ICustomerService
    {
        private ApplicationDbContext _context;

        public CustomerService(ApplicationDbContext context)
        {
            _context = context;
        }

        public void CreateCustomers()
        {
            _context.CreateCustomers();
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return _context.Customers;
        }

        public IEnumerable<Customer> GetCutomer(int id)
        {
            return _context.Customers.Where(c => c.Id == id).ToList();
        }
    }
}
