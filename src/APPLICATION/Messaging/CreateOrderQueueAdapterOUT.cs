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

    public void PublishStartProduction(DOMAIN.Order order)
    {
        RabbitMqPublishModel<StartProduction> menssage = new RabbitMqPublishModel<StartProduction>
        {
            ExchangeName = EventConstants.CREATE_PRODUCTION_EXCHANGE,
            RoutingKey = string.Empty,
            Message = StartProduction.ToModel(order)
        };

        _rabbitMqService.Publish<StartProduction>(menssage);
    }

    public void Publish(DOMAIN.Order order)
    {
        RabbitMqPublishModel<CreateInvoiceModel> menssage = new RabbitMqPublishModel<CreateInvoiceModel>
        {
            ExchangeName = EventConstants.CREATE_INVOICE_EXCHANGE,
            RoutingKey = string.Empty,
            Message = CreateInvoiceModel.ToModel(order)
        };

        _rabbitMqService.Publish<CreateInvoiceModel>(menssage);
    }

    public class StartProduction
    {
        public string Order { get; set; }
        public string Customer { get; set; }
        public List<Item> Items { get; set; }


        public class Item
        {
            public string Name { get; set; }
            public List<OrderProductIngredient> Ingredients { get; set; }

            public static List<Item> ToItem(List<OrderProduct> product)
                => product.Select(x => new Item
                {
                    Ingredients = x.Ingredients.ToList(),
                    Name = x.ProductId.ToString()
                }).ToList();
        }


        public static StartProduction ToModel(DOMAIN.Order order)
          => new StartProduction
          {
              Customer = order.CustomerId.ToString(),
              Order = order.Id.ToString(),
          };
    }


    public record CreateInvoiceModel
    {
        public Guid OrderId { get; set; }
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; }


        public static CreateInvoiceModel ToModel(Order order)
            => new CreateInvoiceModel
            {
                Amount = 150,
                DueDate = DateTime.Now.AddDays(1),
                OrderId = order.Id
            };
    }
}
