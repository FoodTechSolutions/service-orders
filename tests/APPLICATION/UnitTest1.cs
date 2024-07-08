using APPLICATION.builder;
using APPLICATION.Fixture;
using DOMAIN;

namespace APPLICATION
{
    public class UnitTest1 : IClassFixture<DatabaseFixture>
    {
        [Fact]
        public void Test1()
        {
            Assert.True(true);
        }


        public IEnumerable<Order> GetOrders()
        {
            yield return OrderBuilder.CreateOrder();
        }
    }
}