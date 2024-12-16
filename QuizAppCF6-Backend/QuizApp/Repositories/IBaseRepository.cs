using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizApp.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id); 
        Task<IEnumerable<T>> GetAllAsync(); 
        Task AddAsync(T entity);
        Task<bool> UpdateAsync(T entity); 
        Task<bool> DeleteAsync(int id); 
        Task<bool> SaveChangesAsync(); 
    }
}
