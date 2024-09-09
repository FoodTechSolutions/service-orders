using DOMAIN.Enums;
using MediatR;

namespace APPLICATION.Order.NextStepOrder;

public record NextStepOrderCommand(
    Guid OrderId,
    OrderStatus? Status = null
    ) : IRequest<NextStepOrderResponse>;
