using Application.Common.Factories;
using Application.Common.Factories.Interfaces;
using Application.Common.Viewmodels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebApi.Controllers;
using Wegister.WebApi;
using Xunit;

namespace Presentation.IntegrationTests
{
    public class CustomersControllerTests
    {
        private readonly ILogger<CustomersController> _logger;
        private readonly CustomersController _sut;

        public CustomersControllerTests()
        {
            IConfiguration configuration;
            IWebHostBuilder webHostBuilder;
            IServiceCollection services;

            var startup = new Startup(configuration, webHostBuilder);
            startup.ConfigureServices(services);

            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .BuildServiceProvider();
            var logger = serviceProvider.GetService<ILoggerFactory>().CreateLogger<CustomersController>();

            var customerFactory = new CustomerFactory();

            _sut = new CustomersController(logger, customerFactory);
        }

        [Fact]
        public void CustomerCreate_ShouldCreateCustomer()
        {
            //Arrange
            var customer = new CustomerVm
            {
                Name = "Mark Richards",
                Email = "m.richards@yahoo.com",
                EmailToSendHoursTo = "administration@yahoo.com",
                Place = "Washington",
                Street = "Park ln. avenue", 
                HouseNumber = "392",
                ZipCode = "231248"
            };

            //Act
            var actionresult = _sut.CreateCustomer(customer);

            //Assert
            var result = actionresult.Result as OkObjectResult;
            var st = result.Value;
        }
    }
}
