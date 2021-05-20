[assembly: Microsoft.Azure.Functions.Extensions.DependencyInjection.FunctionsStartup
    (typeof(NanoservicesFunctionApp.Startup))]

namespace NanoservicesFunctionApp
{
    using Microsoft.Azure.Functions.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection;
    using Nanoservices.Infrastructure.Definitions;
    using Nanoservices.Infrastructure.Repositories;
    using Nanoservices.Infrastructure.Services;

    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddScoped<ICheckOutRepository, CheckOutRepository>();
            builder.Services.AddScoped<ICheckOutService, CheckOutService>();
        }
    }
}
