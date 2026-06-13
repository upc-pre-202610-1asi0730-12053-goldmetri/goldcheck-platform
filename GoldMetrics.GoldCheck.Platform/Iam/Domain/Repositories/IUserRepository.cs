using GoldMetrics.GoldCheck.Platform.Iam.Domain.Model.Aggregates;

namespace GoldMetrics.GoldCheck.Platform.Iam.Domain.Repositories;

public interface IUserRepository
{
    Task<User?> FindByIdAsync(int id);
    Task<User?> FindByUsernameAsync(string username);
    Task<User?> FindByEmailAsync(string email);
    Task<IEnumerable<User>> FindAllAsync();
    Task AddAsync(User user);
    void Update(User user);
}

