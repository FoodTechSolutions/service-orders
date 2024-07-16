using INFRA.Context;
using INFRA.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Xunit.Abstractions;

namespace API
{
    public class ProgramTests
    {

        private readonly ITestOutputHelper _output;

        public ProgramTests(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void ConfigureServices_AddsDependencies()
        {
            // Arrange
            var services = new ServiceCollection();
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            // Replace with your actual assembly name
            var assemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            var xmlFile = $"{assemblyName}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

            services.AddControllers();

            services.AddDbContext<OrderContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IOrderRepository, OrderRepository>();

            // Act
            var serviceProvider = services.BuildServiceProvider();

            // Assert
            Assert.NotNull(serviceProvider.GetRequiredService<OrderContext>());
            Assert.NotNull(serviceProvider.GetRequiredService<IOrderRepository>());
        }

        [Fact]
        public void Configure_SwaggerSetup()
        {
            // Arrange
            var services = new ServiceCollection();
            var builder = services.AddSwaggerGen();

            // Act
            // Configure SwaggerGen, similar to how it's done in Program.cs

            // Assert
            Assert.NotNull(builder);
            // Add more assertions as needed for Swagger configuration
        }
    }
}