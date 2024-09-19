using System.Linq.Expressions;

namespace WorkoutApi.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(Expression<Func<T, bool>> filter);
        Task Add(T entity);
        void Remove(T entity);
    }
}
