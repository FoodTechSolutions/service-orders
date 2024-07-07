using DOMAIN;
using INFRA.Context;
using INFRA.Repositories.Common;

namespace INFRA.Repositories;

public class OrderRepository : Repository<Order>, IOrderRepository
{
    public OrderRepository(OrderContext context) : base(context) { }
}
