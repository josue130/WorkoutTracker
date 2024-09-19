using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WorkoutApi.Data;
using WorkoutApi.Repository.IRepository;

namespace WorkoutApi.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _db;
        internal DbSet<T> dbSet;
        public Repository(AppDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }
        public async Task Add(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public async Task<T> Get(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbSet;
            query = query.Where(filter);
            return await query.FirstAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            IQueryable<T> query = dbSet;
            return await query.ToListAsync();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);

     
        }
    }
}
