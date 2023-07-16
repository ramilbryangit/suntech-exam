using CQRSInfrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRSInfrastructure.Services.Command
{
    public interface ICustomerCommand
    {
        Task<Customer> Create(Customer model);
    }
}
