using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CQRSInfrastructure.Entities
{
    public class Customer
    {
        public string id => $"{Guid.NewGuid()}";

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string BirthdayInEpoch { get; set; }

        public string Email { get; set; }   
    }
}
