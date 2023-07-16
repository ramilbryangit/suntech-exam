using CQRSInfrastructure.Repository;
using CQRSInfrastructure.Services.Command;
using CQRSInfrastructure.Services.Queries;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Microsoft.Extensions.Configuration;
using Azure.Messaging.EventGrid;
using Azure;

[assembly: FunctionsStartup(typeof(PostCustomer.FunctionApp.Startup))]
namespace PostCustomer.FunctionApp
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddScoped(s => {
                return new CosmosClientBuilder(Environment.GetEnvironmentVariable("CosmosDbConnectionString")).Build();
            });

            builder.Services.AddScoped(s => {
                var topicEndpoint = Environment.GetEnvironmentVariable("EventGridTopicEndpoint");
                var builder = new EventGridSasBuilder(new Uri(topicEndpoint), DateTimeOffset.Now.AddHours(1));
                var keyCredential = new AzureKeyCredential(Environment.GetEnvironmentVariable("EventGridKey"));
                string sasToken = builder.GenerateSas(keyCredential);

                var sasCredential = new AzureSasCredential(sasToken);
                return new EventGridPublisherClient(
                    new Uri(topicEndpoint),
                    sasCredential);
            });


            builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
            builder.Services.AddTransient<ICustomerQueries, CustomerQueries>();
            builder.Services.AddTransient<ICustomerCommand, CustomerCommand>();
        }
    }
}
