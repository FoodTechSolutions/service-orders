namespace DOMAIN.Repository;

public interface ICreateOrderQueueAdapterOUT
{
    void Publish(Order order);
}
