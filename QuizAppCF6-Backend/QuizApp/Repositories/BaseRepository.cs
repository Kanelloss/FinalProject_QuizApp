using Microsoft.EntityFrameworkCore;
using QuizApp.Data;

namespace QuizApp.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly QuizAppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(QuizAppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>(); 
        }

        
        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

       
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        
        public virtual async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        
        public virtual async Task<bool> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            return await SaveChangesAsync();
        }

        
        public virtual async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null)
            {
                return false; 
            }

            _dbSet.Remove(entity); 
            return await SaveChangesAsync();
        }

        // Save changes to the database
        public virtual async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
