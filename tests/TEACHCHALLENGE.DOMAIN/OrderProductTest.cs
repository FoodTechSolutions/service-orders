using DOMAIN.Entities;

namespace TEACHCHALLENGE.DOMAIN;

public class OrderProductTest
{
    [Fact]
    public void CreateProduct_ShouldInitializeProductWithGivenValues()
    {
        // Arrange
        var productId = Guid.NewGuid();
        int quantity = 10;

        // Act
        var product = OrderProduct.CreateProduct(productId, quantity);

        // Assert
        Assert.Equal(productId, product.ProductId);
        Assert.Equal(quantity, product.Quantity);
        Assert.Empty(product.Ingredients);
    }

    [Fact]
    public void AddIngredients_ShouldAddIngredientsToProduct()
    {
        // Arrange
        var productId = Guid.NewGuid();
        int quantity = 10;
        var product = OrderProduct.CreateProduct(productId, quantity);

        var ingredients = new List<OrderProductIngredient>
    {
        new OrderProductIngredient { Id = Guid.NewGuid(), Quantity = 1 },
        new OrderProductIngredient { Id = Guid.NewGuid(), Quantity = 2 }
    };

        // Act
        product.AddIngredients(ingredients);

        // Assert
        Assert.NotNull(product.Ingredients);
        Assert.Equal(2, product.Ingredients.Count);
        Assert.Equal(1, product.Ingredients[0].Quantity);
        Assert.Equal(2, product.Ingredients[1].Quantity);
    }

    [Fact]
    public void AddIngredients_ShouldAppendToExistingIngredients()
    {
        // Arrange
        var productId = Guid.NewGuid();
        int quantity = 10;
        var product = OrderProduct.CreateProduct(productId, quantity);

        var initialIngredients = new List<OrderProductIngredient>
    {
        new OrderProductIngredient { Id = Guid.NewGuid(), Quantity = 1 }
    };

        product.AddIngredients(initialIngredients);

        var newIngredients = new List<OrderProductIngredient>
    {
        new OrderProductIngredient { Id = Guid.NewGuid(), Quantity = 2 }
    };

        // Act
        product.AddIngredients(newIngredients);

        // Assert
        Assert.NotNull(product.Ingredients);
        Assert.Equal(2, product.Ingredients.Count);
        Assert.Equal(1, product.Ingredients[0].Quantity);
        Assert.Equal(2, product.Ingredients[1].Quantity);
    }

    [Fact]
    public void Create_ShouldExceptEmptyGuid()
    {
        // Arrange
        var productId = Guid.Empty;
        var quantity = 10;

        // Assert
        Assert.Throws<ArgumentException>(() => OrderProduct.CreateProduct(productId, quantity));
    }

    [Fact]
    public void Create_ShouldExceptNegativeQuantity()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var quantity = -10;

        // Assert
        Assert.Throws<ArgumentException>(() => OrderProduct.CreateProduct(productId, quantity));
    }
}