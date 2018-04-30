using AnushkaSales.Model.Infrastructure;
using AnushkaSales.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnushkaSales.Model.Repositorys
{
    public interface IBranchRepository : IRepository<Branch>
    {
        Branch FindByPinCode(int pinCode);

        IQueryable<Branch> All();
    }

    public class BranchRepository : RepositoryBase<Branch>, IBranchRepository
    {
        private readonly AppDbContext context;
        public BranchRepository(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public IQueryable<Branch> All()
        {            
            return this.context.Branches.AsQueryable();
        }

        public override Branch FindById(int id)
        {
            return this.Queryable.FirstOrDefault(c => c.Id == id);
        }

        public Branch FindByPinCode(int pinCode)
        {
            return this.Queryable.FirstOrDefault(c => c.BranchPinCode == pinCode);
        }
    }
}
