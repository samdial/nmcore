using NM.Core.src.NM.Core.Database.Models;

namespace NM.Core.src.NM.Core.Users.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken);
        Task<User> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<User> CreateAsync(User user, CancellationToken cancellationToken);
        Task<User> UpdateAsync(int id, User user, CancellationToken cancellationToken);
        Task<User> DeleteAsync(int id, CancellationToken cancellationToken);
    }
}

