using CQRSInfrastructure.Entities;
using CQRSInfrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSInfrastructure.Services.Command
{
    public class CustomerCommand : ICustomerCommand
    {
        private readonly ICustomerRepository _repository;

        public CustomerCommand(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<Customer> Create(Customer model)
        {
            return await _repository.Create(model);
        }
    }
}
