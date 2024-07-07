namespace APPLICATION.Order.GetOrder;

public class GetOrderResponse
{
    public Guid Id { get; init; }


    public static IEnumerable<GetOrderResponse> ToResponse(IEnumerable<DOMAIN.Order> orders)
        => orders.Select(x => new GetOrderResponse
        {
            Id = x.Id
        });
}
