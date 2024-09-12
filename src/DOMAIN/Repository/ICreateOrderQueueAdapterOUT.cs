namespace DOMAIN.Repository;

public interface ICreateOrderQueueAdapterOUT
{
    void Publish(Order order);
    void PublishStartProduction(DOMAIN.Order order);
}
