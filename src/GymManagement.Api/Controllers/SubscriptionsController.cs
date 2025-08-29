using GymManagement.Application.Subscriptions;
using GymManagement.Contracts.Subscriptions;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
   
        var response = new SubscriptionResponse(result.Value.Id, Enum.Parse<SubscriptionType>(result.Value.SubscriptionType));
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSubscription(CreateSubscriptionRequest request)
    {
        var command = new CreateSubscriptionCommand(request.SubscriptionType.ToString(), request.AdminId);
        var result = await _mediator.Send(command);

        if (result.IsError)
            return Problem();
   
        var response = new SubscriptionResponse(result.Value.Id, request.SubscriptionType);
        return Ok(response);
    }
}