using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DepartmentService.Kafka.Consumers.Manual
{
    public  interface IEventConsumer
    {
        Task<T> ConsumeAsync<T>(string topic, CancellationToken cancellationToken = default);
    }
}
