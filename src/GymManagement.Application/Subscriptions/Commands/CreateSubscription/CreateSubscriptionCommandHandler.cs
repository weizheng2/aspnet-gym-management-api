using ErrorOr;
using GymManagement.Domain;
using MediatR;

namespace GymManagement.Application.Subscriptions;

public class CreateSubscriptionCommandHandler : IRequestHandler<CreateSubscriptionCommand, ErrorOr<Subscription>>
{
    private readonly ISubscriptionsRepository _subscriptionsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateSubscriptionCommandHandler(ISubscriptionsRepository subscriptionsRepository, IUnitOfWork unitOfWork)
    {
        _subscriptionsRepository = subscriptionsRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<Subscription>> Handle(CreateSubscriptionCommand request, CancellationToken cancellationToken)
    {
        // Create sub
        var subscription = new Subscription
        {
            Id = Guid.NewGuid(),
            SubscriptionType = request.SubscriptionType
        };

        // Add to db
        await _subscriptionsRepository.AddSubscriptionAsync(subscription);
        await _unitOfWork.CommitChangesAsync();   

        // return id
        return subscription;
    }
}