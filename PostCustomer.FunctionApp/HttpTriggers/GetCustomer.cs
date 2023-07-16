using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using CQRSInfrastructure.Services.Queries;

namespace PostCustomer.FunctionApp
{
    public class GetCustomer
    {
        private readonly ICustomerQueries _service;

        public GetCustomer(ICustomerQueries service)
        {
            _service = service;
        }

        [FunctionName("GetCustomer")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req, ILogger log)
        {
            var x = await _service.Get();

            return new OkObjectResult(x);
        }
    }
}
