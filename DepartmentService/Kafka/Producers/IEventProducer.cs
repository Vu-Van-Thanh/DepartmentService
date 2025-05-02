namespace DepartmentService.Kafka.Producer
{
    public interface IEventProducer
    {
        Task PublishAsync<T>(string topic, int? partition, string? key,T @event);
    }

}
