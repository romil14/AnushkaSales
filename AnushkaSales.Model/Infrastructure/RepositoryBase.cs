using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AnushkaSales.Model.Infrastructure
{
    public abstract class RepositoryBase<T> : IRepository<T>
        where T : class
    {
        protected readonly IQueryable<T> Queryable;
        private readonly IDbSet<T> set;
        private readonly AppDbContext context;
        private bool disposed;

        protected RepositoryBase(AppDbContext context)
        {
            this.context = context;
            this.set = context.Set<T>();
            this.Queryable = this.set;
        }

        public IDbSet<T> Set
        {
            get
            {
                return this.set;
            }
        }

        public abstract T FindById(int id);

        public void Add(T newEntity)
        {
            this.set.Add(newEntity);
            this.context.SaveChanges();
        }

        public void Delete(T entity)
        {
            this.set.Remove(entity);
            this.context.SaveChanges();
        }

        public void Update(T entity)
        {
            this.context.Entry(entity).State = EntityState.Modified;
            this.context.SaveChanges();
        }

        public int Save()
        {
            return this.context.SaveChanges();
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> criteria)
        {
            return this.Queryable.Where(criteria);
        }       

        public async Task<int> SaveAsync()
        {
            try
            {
                return await this.context.SaveChangesAsync();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this.disposed || !disposing || this.context == null)
            {
                return;
            }

            this.context.Dispose();
            this.disposed = true;
        }
    }
}
