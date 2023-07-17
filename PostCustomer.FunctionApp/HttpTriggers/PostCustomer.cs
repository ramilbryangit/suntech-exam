using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using CQRSInfrastructure.Services.Command;
using CQRSInfrastructure.Entities;
using Microsoft.Azure.Cosmos.Serialization.HybridRow;

namespace PostCustomer.FunctionApp
{
    public class PostCustomer
    {
        private readonly ICustomerCommand _service;
        public PostCustomer(ICustomerCommand service)
        {
            _service =  service;
        }

        [FunctionName("PostCustomer")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                dynamic data = JsonConvert.DeserializeObject<Customer>(requestBody);

                var result = await _service.Create(data);

                return new OkObjectResult(result);
            }
            catch (Exception e)
            {

                return new OkObjectResult("Sorry something went wrong, please try again later!");
            }
            

        }
    }
}
