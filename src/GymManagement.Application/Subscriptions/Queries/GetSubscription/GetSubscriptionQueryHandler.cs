using ErrorOr;
using GymManagement.Domain;
using MediatR;

namespace GymManagement.Application.Subscriptions;

public class GetSubscriptionQueryHandler : IRequestHandler<GetSubscriptionQuery, ErrorOr<Subscription>>
{
    private readonly ISubscriptionsRepository _subscriptionsRepository;

    public GetSubscriptionQueryHandler(ISubscriptionsRepository subscriptionsRepository)
    {
        _subscriptionsRepository = subscriptionsRepository;
    }

    public async Task<ErrorOr<Subscription>> Handle(GetSubscriptionQuery request, CancellationToken cancellationToken)
    {
        var subscription = await _subscriptionsRepository.GetByIdAsync(request.SubscriptionId);
        if (subscription is null)
            return Error.NotFound(description: "Subscription not found");

        return subscription;
    }
}
