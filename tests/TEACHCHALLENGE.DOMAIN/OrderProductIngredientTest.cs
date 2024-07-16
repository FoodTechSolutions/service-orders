using DOMAIN.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOMAIN
{
    public class OrderProductIngredientTest
    {
        [Fact]
        public void CreateIngredient_ShouldInitializeWithGivenValues()
        {
            // Arrange
            var ingredientId = Guid.NewGuid();
            int quantity = 5;

            // Act
            var ingredient = OrderProductIngredient.CreateIngredient(ingredientId, quantity);

            // Assert
            Assert.Equal(ingredientId, ingredient.Id);
            Assert.Equal(quantity, ingredient.Quantity);
        }

        [Fact]
        public void Create_ShouldExceptNegativeQuantity()
        {
            // Arrange
            var ingreditentId = Guid.NewGuid();
            var quantity = -10;

            // Assert
            Assert.Throws<ArgumentException>(() => OrderProductIngredient.CreateIngredient(ingreditentId, quantity));
        }
    }
}
