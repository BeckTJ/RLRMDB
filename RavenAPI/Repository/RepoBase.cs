using Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using RavenDB.Data;

namespace Repository
{
    public abstract class RepoBase<T> : IRepoBase<T> where T : class
    {
        protected RavenContext RavenContext { get; set; }
        public RepoBase(RavenContext dbContext)
        {
            RavenContext = dbContext;
        }
        public void Create(T entity) => RavenContext.Set<T>().Add(entity);

        public void Delete(T entity) => RavenContext.Set<T>().Remove(entity);

        public IQueryable<T> FindAll() => RavenContext.Set<T>().AsNoTracking();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) => 
            RavenContext.Set<T>().Where(expression).AsNoTracking();

        public void Update(T entity) => RavenContext.Set<T>().Update(entity);
    }
}