using API.Requets.Order;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("api/v1/[Controller]")]
public class OrderController : ControllerBase
{
    private readonly IMediator _mediator;

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


    [HttpPost]
    public IActionResult CreateOrderAsync([FromBody] CreateOrderRequest order)
    {
        try
        {
            var result = _mediator.Send(order.ToCommand());

            if (result == null) return BadRequest("Error to create Order");

            return Ok(result);
        }
        catch (Exception ex)
        {
            //_logger.LogError($"Error creating order: {ex.Message}");
            return StatusCode(500, "Internal server error");
        }
    }
}
