using Microsoft.Extensions.DependencyInjection;
using API.Configuration;
using MediatR;
using APPLICATION.Order.GetOrdersGroupByStatus;
using Microsoft.AspNetCore.Components.Forms;
using NSubstitute;
using API.Controllers;
using Xunit;
using Moq;
using INFRA.Repositories;
using Microsoft.AspNetCore.Mvc;
using FluentAssertions;
using API.Requets.Order;
using APPLICATION.Order.CreateOrder;
using static API.Requets.Order.CreateOrderRequest;
using APPLICATION.Order.NextStepOrder;
using APPLICATION.Order.GetByIdAsync;
using APPLICATION.Order.GetById;
using APPLICATION.Order.GetOrder;

namespace API;

public class OrderControllerTest
{
    private readonly IMediator _mediator;
    private readonly ServiceProvider _serviceProvider;

    public OrderControllerTest()
    {
        var services = new ServiceCollection();
        services.AddInjectMediator();

        services.AddScoped<IOrderRepository, OrderRepository>();
        _serviceProvider = services.BuildServiceProvider();
        _mediator = _serviceProvider.GetRequiredService<IMediator>();
    }

    [Fact]
    public async Task GetOrdersGroupByStatusAsync_Test()
    {
        var OrderResponse = new GetOrdersGroupByStatusResponse.OrderResponse
        {
            Id = Guid.NewGuid(),
        };
        var lstOrdem = new List<GetOrdersGroupByStatusResponse.OrderResponse>();
        lstOrdem.Add(OrderResponse);

        var ExpectedGetOrdersGroupByStatusResponse = new GetOrdersGroupByStatusResponse
        {
            InProgress = lstOrdem,
            Ready = lstOrdem,
            Received = lstOrdem
        };

        var mockMediator = new Mock<IMediator>();

        mockMediator.Setup(x => x.Send(
                 It.IsAny<GetOrdersGroupByStatusQuery>(), // Qualquer instância de GetOrdersGroupByStatusQuery
                 It.IsAny<CancellationToken>()) // Qualquer CancellationToken
             ).ReturnsAsync(ExpectedGetOrdersGroupByStatusResponse);

        var controller = new OrderController(mockMediator.Object) ;

        IActionResult result = await controller.GetOrdersGroupByStatusAsync();

        result.Should().NotBeNull();
    }

    [Fact]
    public async Task CreateOrderAsync_Test()
    {
        var request = new CreateOrderRequest
        {
            CustomerId = Guid.NewGuid(),
            Discount = 15,
            OrdersProducts = new List<OrderProductRequest>
            {
                new OrderProductRequest()
                {
                    ProductId = Guid.NewGuid(),
                    Quantity = 1,
                    Ingredients =  new List<IngredientRequest>
                    {
                        new IngredientRequest()
                        {
                            Quantity = 1,
                            Id = Guid.NewGuid(),    
                        }
                    }
                }
            }
        };

        var CreateOrderResponse = new CreateOrderResponse
        {
            Id = Guid.NewGuid()
        };

        var mockMediator = new Mock<IMediator>();

        mockMediator.Setup(x => x.Send(
                 request, // Qualquer instância de GetOrdersGroupByStatusQuery
                 It.IsAny<CancellationToken>()) // Qualquer CancellationToken
             ).ReturnsAsync(CreateOrderResponse);

        var controller = new OrderController(mockMediator.Object);

        IActionResult result = await controller.CreateOrderAsync(request);

        result.Should().NotBeNull();
    }

    [Fact]
    public async Task NextStep_Test()
    {
        var id = Guid.NewGuid();
        var command = new NextStepOrderCommand(id);
        var response = new NextStepOrderResponse();

        var CreateOrderResponse = new CreateOrderResponse
        {
            Id = Guid.NewGuid()
        };

        var mockMediator = new Mock<IMediator>();

        mockMediator.Setup(x => x.Send(
                 command, // Qualquer instância de GetOrdersGroupByStatusQuery
                 It.IsAny<CancellationToken>()) // Qualquer CancellationToken
             ).ReturnsAsync(response);

        var controller = new OrderController(mockMediator.Object);

        IActionResult result = await controller.NextStep(id);

        result.Should().NotBeNull();
    }

    [Fact]
    public async Task GetOrderById_Test()
    {
        var id = Guid.NewGuid();
        var command = new GetOrderByIdQuery(id);
        var response = new GetOrderByIdResponse();

        var CreateOrderResponse = new CreateOrderResponse
        {
            Id = Guid.NewGuid()
        };

        var mockMediator = new Mock<IMediator>();

        mockMediator.Setup(x => x.Send(
                 command, // Qualquer instância de GetOrdersGroupByStatusQuery
                 It.IsAny<CancellationToken>()) // Qualquer CancellationToken
             ).ReturnsAsync(response);

        var controller = new OrderController(mockMediator.Object);

        IActionResult result = await controller.GetOrderById(id);

        result.Should().NotBeNull();
    }

    [Fact]
    public async Task GetAsync_Test()
    {
        var id = Guid.NewGuid();
        var request = new GetOrderRequest();
        var command = request.ToQuery();
        var response = new GetOrderResponse();

        var CreateOrderResponse = new CreateOrderResponse
        {
            Id = Guid.NewGuid()
        };
        var lst = new List<GetOrderResponse>
        {
            response
        };

        var mockMediator = new Mock<IMediator>();

        mockMediator.Setup(x => x.Send(
                 command, // Qualquer instância de GetOrdersGroupByStatusQuery
                 It.IsAny<CancellationToken>()) // Qualquer CancellationToken
             ).ReturnsAsync(lst);

        var controller = new OrderController(mockMediator.Object);

        IActionResult result = await controller.GetAsync(request);

        result.Should().NotBeNull();
    }

}
