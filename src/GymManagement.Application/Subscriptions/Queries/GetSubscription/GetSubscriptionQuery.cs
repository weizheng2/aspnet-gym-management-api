using ErrorOr;
using GymManagement.Domain;
using MediatR;

namespace GymManagement.Application.Subscriptions;

public record GetSubscriptionQuery(Guid SubscriptionId) : IRequest<ErrorOr<Subscription>>;