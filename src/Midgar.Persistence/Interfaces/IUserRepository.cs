using Midgar.Domain.Entities;

namespace Midgar.Persistence.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();

        Task<User> GetByIdAsync(int id);

        Task AddAsync(User user);

        Task UpdateAsync(User user);
        
        Task DeleteAsync(int id);
    }
}