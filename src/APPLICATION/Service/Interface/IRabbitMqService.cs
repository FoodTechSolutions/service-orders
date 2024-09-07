using APPLICATION.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPLICATION.Service.Interface;

public interface IRabbitMqService
{
    void Publish<T>(RabbitMqPublishModel<T> rabbitMqConfig);
}

