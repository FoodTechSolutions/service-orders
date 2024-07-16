using DOMAIN.Base;

namespace DOMAIN.Entities;

public class OrderProductIngredient : BaseEntity
{
    public int Quantity { get; set; }

    public static OrderProductIngredient CreateIngredient(int quantity)
    {
        if (quantity < 0) throw new ArgumentException("Quantity cannot be negative!");

        return new OrderProductIngredient
        {
            Quantity = quantity,
        };
    }
}
