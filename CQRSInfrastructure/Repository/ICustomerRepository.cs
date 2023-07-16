using CQRSInfrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSInfrastructure.Repository
{
    public interface ICustomerRepository
    {
        Task<Customer> Get();

        Task<Customer> Create(Customer model);
    }
}
