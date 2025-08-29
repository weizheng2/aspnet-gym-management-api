using GymManagement.Domain;

namespace GymManagement.Application;

public interface ISubscriptionsRepository
{
    Task AddSubscriptionAsync(Subscription subscription);
    Task<Subscription?> GetByIdAsync(Guid subscriptionId);
}