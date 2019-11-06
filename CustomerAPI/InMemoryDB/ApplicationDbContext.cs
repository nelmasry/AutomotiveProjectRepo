using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerAPI.InMemoryDB
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> context) : base(context)
        {

        }
        public void CreateCustomers()
        {
            if (!Customers.Any())
            {
                Customers.AddRange(new Customer
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
                });
                SaveChanges();
            }
        }
    }

    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

    }
}
