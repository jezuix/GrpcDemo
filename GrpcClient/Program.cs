using Grpc.Core;
using Grpc.Net.Client;
using System;
using System.Threading.Tasks;

namespace GrpcClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5001/");
            var customerClient = new Customer.CustomerClient(channel);
            var customer = await customerClient.GetCustomerInfoAsync(new CustemerLookupModel
            {
                UserId = 2
            });

            Console.WriteLine($"Customer: {customer.FirstName} {customer.LastName}");

            Console.WriteLine();
            Console.WriteLine("New Customer List");
            Console.WriteLine();

            using var call = customerClient.GetNewCustomers(new NewCustomerRequest());
            while (await call.ResponseStream.MoveNext())
            {
                var currentCustomer = call.ResponseStream.Current;

                Console.WriteLine($"New Customer: {currentCustomer.FirstName} {currentCustomer.LastName}: {currentCustomer.EmailAddress}");
            }
            Console.ReadLine();
        }

        //static async Task Main(string[] args)
        //{
        //    using var channel = GrpcChannel.ForAddress("https://localhost:5001/");
        //    var client = new Greeter.GreeterClient(channel);
        //    var reply = await client.SayHelloAsync(new HelloRequest
        //    {
        //        Name = "Rods"
        //    });

        //    Console.WriteLine($"Greetings: {reply.Message}");
        //    Console.ReadLine();
        //}
    }
}
