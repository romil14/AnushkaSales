using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AnushkaSales.Model.Infrastructure
{
    public interface IRepository<T> : IDisposable
    {
        T FindById(int id);
        
        void Add(T newEntity);

        void Delete(T entity);

        void Update(T entity);

        int Save();

        IQueryable<T> Where(Expression<Func<T, bool>> criteria);        

        Task<int> SaveAsync();
    }
}
