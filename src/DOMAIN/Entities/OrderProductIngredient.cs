using DOMAIN.Base;

namespace DOMAIN.Entities;

public class OrderProductIngredient : BaseEntity
{
    public int Quantity { get; set; }

    public static OrderProductIngredient CreateIngredient(Guid Id, int quantity)
    {
        return new OrderProductIngredient
        {
            Quantity = quantity,
            Id = Id,
        };
    }
}
