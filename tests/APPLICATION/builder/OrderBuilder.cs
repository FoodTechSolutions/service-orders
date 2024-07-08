using DOMAIN;

namespace APPLICATION.builder;

public class OrderBuilder
{
    public static Order CreateOrder()
        => new Order(Guid.NewGuid(), 12132, DOMAIN.Enums.OrderStatus.InProgress);
}
