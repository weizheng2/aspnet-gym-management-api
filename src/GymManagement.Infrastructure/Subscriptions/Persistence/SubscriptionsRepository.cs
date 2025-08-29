using GymManagement.Application;
using GymManagement.Domain.Subscriptions;
using GymManagement.Infrastructure.Common;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Infrastructure.Subscriptions;

public class SubscriptionsRepository : ISubscriptionsRepository
{
    private readonly ApplicationDbContext _dbContext;
    public SubscriptionsRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task AddSubscriptionAsync(Subscription subscription)
    {
        await _dbContext.Subscriptions.AddAsync(subscription);
    }

    public async Task<Subscription?> GetByIdAsync(Guid subscriptionId)
    {
        return await _dbContext.Subscriptions.FirstOrDefaultAsync(s => s.Id == subscriptionId);
    }
}