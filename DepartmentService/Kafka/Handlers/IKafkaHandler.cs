

namespace DepartmentService.Kafka.Handlers
{
    public interface IKafkaHandler<T>
    {
        Task HandleAsync(T message);

    }
}
