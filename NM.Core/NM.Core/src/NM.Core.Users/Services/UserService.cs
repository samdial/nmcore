using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NM.Core.src.NM.Core.Database.Models;
using NM.Core.src.NM.Core.Users.Services.Interfaces;

namespace NM.Core.src.NM.Core.Users.Services
{
    public partial class UserService : IUserService
    {
        private readonly ApplicationContext _context;

        public UserService(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Users.ToListAsync(cancellationToken);
        }

        public async Task<User> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<User> CreateAsync(User user, CancellationToken cancellationToken)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync(cancellationToken);
            return user;
        }

        public async Task<User> UpdateAsync(int id, User user, CancellationToken cancellationToken)
        {
            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser != null)
            {
                existingUser.Name = user.Name;
               
                await _context.SaveChangesAsync(cancellationToken);
            }
            return existingUser;
        }

        public async Task<User> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync(cancellationToken);
            }
            return user;
        }
    }
}