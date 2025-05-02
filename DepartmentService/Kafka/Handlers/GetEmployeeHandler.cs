

namespace EmployeeService.Infrastructure.Kafka.Handlers
{
    /*public class GetEmployeeHandler : IKafkaHandler<KafkaRequest<EmployeeFilterDTO>>
    {
        private readonly IEventProducer _eventProducer;
        public GetEmployeeHandler(IEventProducer eventProducer)
        {
            _eventProducer = eventProducer;
        }
        public async Task HandleAsync(KafkaRequest<EmployeeFilterDTO> message)
        {
            List<Employee> result = await _employeeRepository.GetEmployeesByFilter(message.Filter.ToExpression());
            KafkaResponse<List<Employee>> response = new KafkaResponse<List<Employee>>()
            {
                RequestType = message.RequestType,
                CorrelationId = message.CorrelationId,
                Timestamp = DateTime.UtcNow,
                Filter = result
            };
            Console.WriteLine($"GetEmployeeHandler: {result}");
            await _eventProducer.PublishAsync("EmployeeList", null, null, response);
            

        }
    }*/
}
