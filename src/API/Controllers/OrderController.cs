using API.Requets.Order;
using APPLICATION.Order.GetByIdAsync;
using APPLICATION.Order.GetOrdersGroupByStatus;
using APPLICATION.Order.NextStepOrder;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/v1/[Controller]")]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<OrderController> _logger;

    public OrderController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync([FromQuery] GetOrderRequest request)
    {
        var result = await _mediator.Send(request.ToQuery());
        return Ok(result);
    }

    [HttpGet("{orderId}")]
    public async Task<IActionResult> GetOrderById(Guid orderId)
    {
        var result = await _mediator.Send(new GetOrderByIdQuery(orderId));
        if (result is null)
        {
            return NotFound();
        }

        return Ok(result);
    }


    [HttpPost]
    public async Task<IActionResult> CreateOrderAsync([FromBody] CreateOrderRequest order)
    {
        var result = await _mediator.Send(order.ToCommand());

        if (result == null) return BadRequest("Error to create Order");

        return Ok(result);
    }

    [HttpGet("GetOrdersGroupByStatus")]
    public async Task<IActionResult> GetOrdersGroupByStatusAsync()
    {
        var result = await _mediator.Send(new GetOrdersGroupByStatusQuery());
        return Ok(result);
    }

    [HttpPost]
    [Route(":nextstep/{orderId}")]
    public async Task<IActionResult> NextStep([FromRoute] Guid orderId)
    {

        var result = await _mediator.Send(new NextStepOrderCommand(orderId));
        return Ok(result);

    }
}
