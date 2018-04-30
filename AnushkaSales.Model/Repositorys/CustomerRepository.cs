using AnushkaSales.Model.Infrastructure;
using AnushkaSales.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnushkaSales.Model.Repositorys
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        IQueryable<Customer> All();
    }
    public class CustomerRepository : RepositoryBase<Customer>, ICustomerRepository
    {
        private readonly AppDbContext context;
        public CustomerRepository(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public IQueryable<Customer> All()
        {
            return this.context.Customers.AsQueryable();
        }

        public override Customer FindById(int id)
        {
            return this.Queryable.FirstOrDefault(c => c.Id == id);
        }
    }
}
