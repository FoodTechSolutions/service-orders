using APPLICATION.BackgroundServices.Models;
using APPLICATION.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APPLICATION.Service;

public class RabbitMqExampleService : IRabbitMqExampleService
{
    public async Task ProcessEvent(RabbitMqExampleModel model)
    {
        Console.WriteLine("Received Message", model);
    }
}

