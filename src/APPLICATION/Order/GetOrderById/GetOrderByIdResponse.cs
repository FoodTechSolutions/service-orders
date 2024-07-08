namespace APPLICATION.Order.GetById;

public class GetOrderByIdResponse
{
    public Guid Id { get; set; }

    public static GetOrderByIdResponse ToResponse(DOMAIN.Order order)
        => new GetOrderByIdResponse { Id = order.Id };
}
