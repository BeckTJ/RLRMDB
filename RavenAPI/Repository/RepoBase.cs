using Contracts;
using RavenDAL;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using RavenDAL.Data;

namespace Repository
{
    public abstract class RepoBase<T> : IRepoBase<T> where T : class
    {
        protected RavenDBContext RavenDBContext { get; set; }
        public RepoBase(RavenDBContext dbContext)
        {
            RavenDBContext = dbContext;
        }
        public void Create(T entity) => RavenDBContext.Set<T>().Add(entity);

        public void Delete(T entity) => RavenDBContext.Set<T>().Remove(entity);

        public IQueryable<T> FindAll() => RavenDBContext.Set<T>().AsNoTracking();

        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) => 
            RavenDBContext.Set<T>().Where(expression).AsNoTracking();

        public void Update(T entity) => RavenDBContext.Set<T>().Update(entity);
    }
}