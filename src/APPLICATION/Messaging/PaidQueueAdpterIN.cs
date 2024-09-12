using APPLICATION.Helpers;
using APPLICATION.Order.NextStepOrder;
using INFRA.Messaging;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

public class PaidQueueAdpterIN : BackgroundService
{
    private IConnection _connection;
    private IModel _channel;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<CreateOrderQueueAdapterOUT> _logger;
    private string RABBIT_HOST;
    private string RABBIT_PORT;
    private string RABBIT_USERNAME;
    private string RABBIT_PASSWORD;

    public PaidQueueAdpterIN(IServiceProvider serviceProvider,
        ILogger<CreateOrderQueueAdapterOUT> logger,
        IConfiguration configuration)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        RABBIT_HOST = configuration.GetSection("RabbitMqSettings")["HOST"] ?? string.Empty;
        RABBIT_PORT = configuration.GetSection("RabbitMqSettings")["PORT"] ?? string.Empty;
        RABBIT_USERNAME = configuration.GetSection("RabbitMqSettings")["USERNAME"] ?? string.Empty;
        RABBIT_PASSWORD = configuration.GetSection("RabbitMqSettings")["PASSWORD"] ?? string.Empty;
        CreateConnection();
    }

    private void CreateConnection()
    {
        try
        {
            _connection = new ConnectionFactory
            {
                AmqpUriSslProtocols = System.Security.Authentication.SslProtocols.Tls12,
                Endpoint = new AmqpTcpEndpoint(RABBIT_HOST, int.Parse(RABBIT_PORT)),
                UserName = RABBIT_USERNAME,
                Password = RABBIT_PASSWORD
            }.CreateConnection();

            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(
                exchange: EventConstants.PAID_EXCHANGE,
                type: ExchangeType.Direct);

            _channel.QueueDeclare(
                queue: EventConstants.PAID_QUEUE);

            _channel.QueueBind(
                exchange: EventConstants.PAID_EXCHANGE,
                queue: EventConstants.PAID_QUEUE,
            routingKey: string.Empty);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Houve um erro ao criar a conexão com o RabbitMQ. Erro: {ex.Message}");
        }
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        CreateConsumer();
    }

    private void CreateConsumer()
    {
        var consumer = new EventingBasicConsumer(_channel);
        //_channel.ModelShutdown += async (s, e) => await Channel_Shutdown(s, e);
        consumer.Received += async (s, e) => await Consumer(s, e);
        _channel.BasicQos(0, 20, false);
        _channel.BasicConsume(EventConstants.PAID_QUEUE, false, consumer);
    }

    public async Task Consumer(object sender, BasicDeliverEventArgs e)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var body = e.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            var order = JsonConvert.DeserializeObject<Paid>(message);
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

            var command = new NextStepOrderCommand(order.OrderId, DOMAIN.Enums.OrderStatus.InProgress);
            await mediator.Send(command);

        }
    }

    public class Paid
    {
        public Guid OrderId { get; set; }
    }
}
