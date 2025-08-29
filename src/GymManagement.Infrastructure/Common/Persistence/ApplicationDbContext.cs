using GymManagement.Application;
using GymManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Infrastructure.Common;

public class ApplicationDbContext : DbContext, IUnitOfWork
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
    
    public DbSet<Subscription> Subscriptions { get; set; } = null!;

    public async Task CommitChangesAsync()
    {
        await base.SaveChangesAsync();
    }
}