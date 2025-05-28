using Microsoft.EntityFrameworkCore;
using Midgar.Domain.Entities;
using Midgar.Persistence.Interfaces;
using Midgar.Persistence.Context;

namespace Midgar.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MidgarContext _context;

        public UserRepository(MidgarContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync() => await _context.Users.AsNoTracking().ToListAsync();

        public async Task<User> GetByIdAsync(int id) => await _context.Users.FindAsync(id);

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);

            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if (user != null)
            {
                _context.Users.Remove(user);

                await _context.SaveChangesAsync();
            }
        }
    }
}