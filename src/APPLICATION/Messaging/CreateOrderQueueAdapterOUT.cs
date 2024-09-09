using APPLICATION.Configuration;
using APPLICATION.Helpers;
using APPLICATION.Service.Interface;
using DOMAIN;
using DOMAIN.Entities;
using DOMAIN.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using System.Diagnostics;

namespace INFRA.Messaging;

public class CreateOrderQueueAdapterOUT : ICreateOrderQueueAdapterOUT
{
    private IConnection _connection;
    private IModel _channel;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<CreateOrderQueueAdapterOUT> _logger;
    private string RABBIT_HOST;
    private string RABBIT_PORT;
    private string RABBIT_USERNAME;
    private string RABBIT_PASSWORD;
    private readonly IRabbitMqService _rabbitMqService;
    public CreateOrderQueueAdapterOUT(IServiceProvider serviceProvider,
        ILogger<CreateOrderQueueAdapterOUT> logger,
        IConfiguration configuration, IRabbitMqService rabbitMqService)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        RABBIT_HOST = configuration.GetSection("RabbitMqSettings")["HOST"] ?? string.Empty;
        RABBIT_PORT = configuration.GetSection("RabbitMqSettings")["PORT"] ?? string.Empty;
        RABBIT_USERNAME = configuration.GetSection("RabbitMqSettings")["USERNAME"] ?? string.Empty;
        RABBIT_PASSWORD = configuration.GetSection("RabbitMqSettings")["PASSWORD"] ?? string.Empty;

        _rabbitMqService = rabbitMqService;
    }

    public void Publish(DOMAIN.Order order)
    {
        RabbitMqPublishModel<CreateOrderModel> menssage = new RabbitMqPublishModel<CreateOrderModel>
        {
            ExchangeName = EventConstants.CREATE_PRODUCTION_EXCHANGE,
            RoutingKey = string.Empty,
            Message = CreateOrderModel.toModel(order)
        };

        _rabbitMqService.Publish<CreateOrderModel>(menssage);
    }

    public class CreateOrderModel
    {
        public string Order { get; set; }
        public string Customer { get; set; }
        public List<Item> Items { get; set; }

        public class Item
        {
            public string Name { get; set; }
            public List<Ingrediente> Ingredients { get; set; }

            public static List<Item> toModel(List<OrderProduct> orderProducts)
                => orderProducts.Select(ve => new Item
                {
                    Name = "teste",
                    Ingredients = ve.Ingredients.Select(vc => new Ingrediente
                    {
                        Name = "teaa",
                        Price = 100
                    }).ToList()
                }).ToList();
        }

        public class Ingrediente
        {
            public string Name { get; set; }
            public double Price { get; set; }
        }

        public static CreateOrderModel toModel(DOMAIN.Order order)
         => new CreateOrderModel
         {
             Order = order.Id.ToString(),
             Customer = order.CustomerId.ToString(),
             Items = Item.toModel(order.Products)

         };
    }
}
