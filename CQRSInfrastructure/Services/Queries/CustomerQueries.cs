using CQRSInfrastructure.Entities;
using CQRSInfrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSInfrastructure.Services.Queries
{
    public class CustomerQueries: ICustomerQueries
    {
        private readonly ICustomerRepository _repository;

        public CustomerQueries(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<Customer> Get()
        {
            return await _repository.Get();
        }
    }
}
