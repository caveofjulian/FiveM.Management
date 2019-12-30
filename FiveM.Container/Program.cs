using Microsoft.Extensions.DependencyInjection;
using System;
using FiveM.Integration;

namespace FiveM.Container
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceCollection serviceDescriptors = new ServiceCollection();
            Client Client = new Client(serviceDescriptors);
            Client.RunAsync().GetAwaiter().GetResult();
        }
    }
}
