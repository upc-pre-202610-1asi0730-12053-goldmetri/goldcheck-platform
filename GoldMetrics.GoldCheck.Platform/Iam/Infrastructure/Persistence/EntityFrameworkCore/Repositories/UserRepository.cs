using GoldMetrics.GoldCheck.Platform.Iam.Domain.Model.Aggregates;
using GoldMetrics.GoldCheck.Platform.Iam.Domain.Repositories;
using GoldMetrics.GoldCheck.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration;
using Microsoft.EntityFrameworkCore;

namespace GoldMetrics.GoldCheck.Platform.Iam.Infrastructure.Persistence.EntityFrameworkCore.Repositories;

public class UserRepository(AppDbContext context) : IUserRepository
{
    public async Task<User?> FindByIdAsync(int id) =>
        await context.Set<User>().FindAsync(id);

    public async Task<User?> FindByUsernameAsync(string username) =>
        await context.Set<User>().FirstOrDefaultAsync(u => u.Username.Value == username);

    public async Task<User?> FindByEmailAsync(string email) =>
        await context.Set<User>().FirstOrDefaultAsync(u => u.Email.Address == email);

    public async Task<IEnumerable<User>> FindAllAsync() =>
        await context.Set<User>().ToListAsync();

    public async Task AddAsync(User user) =>
        await context.Set<User>().AddAsync(user);

    public void Update(User user) =>
        context.Set<User>().Update(user);
}