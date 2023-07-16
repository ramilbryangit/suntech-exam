using CQRSInfrastructure.Entities;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSInfrastructure.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly Database _database;

        protected Container _container;

        public CustomerRepository(CosmosClient client)
        {
            _database = client.GetDatabase(Environment.GetEnvironmentVariable("CosmosDbName"));
            _container = _database.GetContainer("Customer");
        }

        public async Task<Customer> Get()
        {
            return await _container.ReadItemAsync<Customer>("9cf7dda1-d136-415b-8a01-26808ec12db0", new PartitionKey("replace_with_new_document_id"));
        }

        public async Task<Customer> Create(Customer model)
        {
            return await _container.CreateItemAsync<Customer>(model, new PartitionKey(model.LastName));
        }
    }
}
