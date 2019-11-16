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
        /// <summary>
        /// Create all customers in memoryDB customers at first launch
        /// </summary>
        public void CreateCustomers()
        {
            _context.CreateCustomers();
        }
        /// <summary>
        /// Get all customers
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Customer> GetCustomers()
        {
            return _context.Customers;
        }
        /// <summary>
        /// Get customer by id
        /// </summary>
        /// <param name="id">Customer Id</param>
        /// <returns></returns>
        public IEnumerable<Customer> GetCutomer(int id)
        {
            return _context.Customers.Where(c => c.Id == id).ToList();
        }
    }
}
