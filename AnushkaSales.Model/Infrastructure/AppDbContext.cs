using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data.Entity;
using AnushkaSales.Model.Models;

namespace AnushkaSales.Model
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("name=AppConnectionString")
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<AppDbContext>());
        }

        public DbSet<Branch> Branches { get; set; }

        public DbSet<Customer> Customers { get; set; }
    }
}
