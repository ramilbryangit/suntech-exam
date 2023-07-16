using CQRSInfrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSInfrastructure.Services.Queries
{
    public interface ICustomerQueries
    {
        Task<Customer> Get();
    }
}
