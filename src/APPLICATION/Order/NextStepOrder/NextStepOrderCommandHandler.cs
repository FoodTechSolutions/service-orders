using DOMAIN.Repository;
using INFRA.Repositories;
using MediatR;

namespace APPLICATION.Order.NextStepOrder;

public class NextStepOrderCommandHandler : IRequestHandler<NextStepOrderCommand, NextStepOrderResponse>
{
    private readonly IOrderRepository _orderRepository;
    private readonly ICreateOrderQueueAdapterOUT _createOrderQueueAdapterOUT;

    public NextStepOrderCommandHandler(IOrderRepository orderRepository, ICreateOrderQueueAdapterOUT createOrderQueueAdapterOUT)
    {
        _orderRepository = orderRepository;
        _createOrderQueueAdapterOUT = createOrderQueueAdapterOUT;
    }

    public async Task<NextStepOrderResponse> Handle(NextStepOrderCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByIdAsync(request.OrderId);
        if(request.Status.HasValue)
        {
            order.SetStatus(request.Status.Value);
        }
        else
        {
            order.MoveToNextStep();
        }
        _orderRepository.Update(order);
        _orderRepository.SaveChangesAsync();

        if(request.Status == DOMAIN.Enums.OrderStatus.InProgress)
        {
            _createOrderQueueAdapterOUT.PublishStartProduction(order);
        }

        return NextStepOrderResponse.ToResponse(order);
    }
}
