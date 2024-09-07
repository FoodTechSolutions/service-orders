using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPLICATION.Configuration;

public class RabbitMqPublishModel<T>
{
    public string ExchangeName { get; set; }
    public string RoutingKey { get; set; }
    public T Message { get; set; }
}

