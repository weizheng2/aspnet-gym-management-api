using ErrorOr;
using GymManagement.Domain;
using MediatR;

namespace GymManagement.Application.Subscriptions;

public record CreateSubscriptionCommand(string SubscriptionType, Guid AdminId) : IRequest<ErrorOr<Subscription>>;