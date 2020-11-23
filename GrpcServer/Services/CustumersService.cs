using Grpc.Core;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GrpcServer.Services
{
    public class CustumersService : Customer.CustomerBase
    {
        private readonly ILogger<CustumersService> _logger;

        public CustumersService(ILogger<CustumersService> logger)
        {
            _logger = logger;
        }

        public override async Task<CustomerModel> GetCustomerInfo(CustemerLookupModel request, ServerCallContext context)
        {
            CustomerModel output = new CustomerModel();

            if (request.UserId == 1)
            {
                output.FirstName = "Jamie";
                output.LastName = "Smith";
            }
            else if (request.UserId == 2)
            {
                output.FirstName = "Rick";
                output.LastName = "Sanches";
            }
            else
            {
                output.FirstName = "Silver";
                output.LastName = "Chair";
            }

            return await Task.FromResult(output);
        }

        public override async Task GetNewCustomers(NewCustomerRequest request, IServerStreamWriter<CustomerModel> responseStream, ServerCallContext context)
        {
            var customers = new List<CustomerModel>
            {
                new CustomerModel
                {
                    FirstName = "Tim",
                    LastName = "Corey",
                    EmailAddress = "tim@corey.com",
                    Age = 41,
                    IsAlive = true
                },
                new CustomerModel
                {
                    FirstName = "Rods",
                    LastName = "Barros",
                    EmailAddress = "rrb@terra.com",
                    Age = 32,
                    IsAlive = true
                },
                new CustomerModel
                {
                    FirstName = "Gandalf",
                    LastName = "Grey",
                    EmailAddress = "gandalf@valarsCommunit.com",
                    Age = 2510,
                    IsAlive = true
                }
            };

            foreach (var customer in customers)
            {
                await Task.Delay(1000);
                await responseStream.WriteAsync(customer);
            }
        }
    }
}
