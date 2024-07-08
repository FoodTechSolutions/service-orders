using INFRA.Context;
using INFRA.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using APPLICATION.Order.GetOrder;
using APPLICATION.Order.CreateOrder;
using APPLICATION.Order.GetByIdAsync;
using APPLICATION.Order.GetById;
using APPLICATION.Order.GetOrdersGroupByStatus;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});


// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IMediator, Mediator>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<OrderContext>(options => options
    .UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IOrderRepository, OrderRepository>();

builder.Services.AddTransient<IRequestHandler<GetOrderQuery, IEnumerable<GetOrderResponse>>, GetOrderQueryHandler>();
builder.Services.AddTransient<IRequestHandler<CreateOrderCommand, CreateOrderResponse>, CreateOrderCommandHandler>();
builder.Services.AddTransient<IRequestHandler<GetOrderByIdQuery, GetOrderByIdResponse>, GetOrderByIdQueryHandler>();
builder.Services.AddTransient<IRequestHandler<GetOrdersGroupByStatusQuery, GetOrdersGroupByStatusResponse>, GetOrdersGroupByStatusQueryHandler>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tech Challenge v1"); });

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
