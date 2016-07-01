using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using Tier3Lib;

namespace Tier3Host
{          
    class Program
    {
        static void Main(string[] args)
        {                                                                  
            using (ServiceHost serviceHost = new ServiceHost(typeof(Tier3)))
            {
                serviceHost.Open();
                DisplayHostInfo(serviceHost);
                Console.WriteLine("The service is ready.");
                Console.WriteLine("Press the Enter key to terminate service.");
                Console.ReadLine();
            }
        }

        static void DisplayHostInfo(ServiceHost host)
        {                           
            Console.Write("*************** Host Info ***************");

            foreach (ServiceEndpoint se in host.Description.Endpoints)
            {
                Console.WriteLine("\nAddress : {0}", se.Address);
                Console.WriteLine("Binding : {0}", se.Binding.Name);
                Console.WriteLine("Contract: {0}", se.Contract.Name);
            }

            Console.WriteLine("*****************************************\n");
        }
    }
}
