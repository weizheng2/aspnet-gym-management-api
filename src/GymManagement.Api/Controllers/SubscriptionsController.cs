using GymManagement.Application.Subscriptions;
using GymManagement.Contracts.Subscriptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using DomainSubscriptionType = GymManagement.Domain.Subscriptions.SubscriptionType;
namespace GymManagement.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class SubscriptionsController : ControllerBase
{
    private readonly ISender _mediator;
    public SubscriptionsController( ISender mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{subscriptionId}")]
    public async Task<IActionResult> GetSubscription(Guid subscriptionId)
    {
        var command = new GetSubscriptionQuery(subscriptionId);
        var result = await _mediator.Send(command); 
        if (result.IsError)
            return Problem(title: result.FirstError.Description);
   
        var response = new SubscriptionResponse(result.Value.Id, Enum.Parse<SubscriptionType>(result.Value.SubscriptionType.Name));
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSubscription(CreateSubscriptionRequest request)
    {
        if (!DomainSubscriptionType.TryFromName(request.SubscriptionType.ToString(), out var subscriptionType))
            return Problem(statusCode: StatusCodes.Status400BadRequest, detail: "Invalid subscription type");
        
        var command = new CreateSubscriptionCommand(subscriptionType, request.AdminId);
        var result = await _mediator.Send(command);

        if (result.IsError)
            return Problem();
   
        var response = new SubscriptionResponse(result.Value.Id, request.SubscriptionType);
        return Ok(response);
    }
}