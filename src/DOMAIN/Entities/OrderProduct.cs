using DOMAIN.Base;
using System.Xml.Linq;

namespace DOMAIN.Entities;

public class OrderProduct : BaseEntity
{
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }
    public List<OrderProductIngredient> Ingredients { get; set; }

    public static OrderProduct CreateProduct(Guid productId, int quantity)
    {
        if(productId == Guid.Empty) throw new ArgumentException("Id cannot be empty guid!");

        if (quantity < 0) throw new ArgumentException("Quantity must be greater than 0!");


        return new OrderProduct { ProductId = productId, Quantity = quantity };
    }

    public OrderProduct AddIngredients(IEnumerable<OrderProductIngredient> ingredients)
    {
        if (Ingredients is null) Ingredients = new List<OrderProductIngredient>();
        Ingredients.AddRange(ingredients);
        return this;
    }
}
