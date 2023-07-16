using System.Collections.Generic;
using System.Threading.Tasks;
using Azure.Messaging.EventGrid;
using CQRSInfrastructure.Entities;
using Microsoft.Azure.Documents;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace PostCustomer.FunctionApp
{
    public class PublishCustomer
    {
        private readonly EventGridPublisherClient _publisherClient;

        public PublishCustomer(EventGridPublisherClient publisherClient)
        {
            _publisherClient = publisherClient;
        }

        [FunctionName("PublishCustomer")]
        public async Task Run([CosmosDBTrigger(
            databaseName: "suntech-exam-db",
            containerName: "Customer",
            Connection = "CosmosDbConnectionString",
            LeaseContainerName = "leases",
            CreateLeaseContainerIfNotExists = true)] IReadOnlyList<Customer> input,
            ILogger log)
        {
            if (input != null && input.Count > 0)
            {
                log.LogInformation("Documents modified " + input.Count);
                log.LogInformation("First document Id " + input[0].FirstName);
            }


            EventGridEvent gridEvent = new EventGridEvent("CustomerChanges", "Customer.Receive", "1.0", input);

            await _publisherClient.SendEventAsync(gridEvent);
        }
    }
}
