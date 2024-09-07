using APPLICATION.BackgroundServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPLICATION.Service.Interface;

public interface IRabbitMqExampleService
{
    Task ProcessEvent(RabbitMqExampleModel model);
}

