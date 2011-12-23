using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition.Hosting;
using HireMe.DataAccess.OdbcProvider;

using System.ServiceModel.Description;
using System.ServiceModel;
using HireMe.DataAccess;
using System.Diagnostics;

namespace HireMe.WcfService
{
  class Program
  {
    static void Main(string[] args)
    {
      Trace.Listeners.Add(new ConsoleTraceListener());
      
      InitializeContainer();
      StartCustomerService();

      //Just for the demo.
      Console.WriteLine("Services Running...\r\nPress ENTER to stop services and exit");
      Console.Read();
    }

    private static void InitializeContainer()
    {
      AssemblyCatalog catThis = new AssemblyCatalog(typeof(Program).Assembly);
      AssemblyCatalog catOdbcProvider = new AssemblyCatalog(typeof(CustomerDataAdapter).Assembly);
      AggregateCatalog allCatalogs = new AggregateCatalog(catThis, catOdbcProvider);
      CompositionContainer container = new CompositionContainer(allCatalogs);
      Services.Initialize(container);
    }

    private static void StartCustomerService()
    {
      var baseUri = new Uri(Properties.Resources.CustomerServiceBaseAddress); //http://localhost:8000/HireMe right now
      ServiceHost host = new ServiceHost(typeof(CustomerWcfService), baseUri);
      try
      {
        //ADD CUSTOMER ENDPOINT
        var endpointAddress = Properties.Resources.CustomerServiceRelativeAddress; //CustomerService right now
        host.AddServiceEndpoint(typeof(ICustomerDal), new WSHttpBinding(), endpointAddress);

        //ENABLE METADATA EXCHANGE
        ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
        smb.HttpGetEnabled = true;
        host.Description.Behaviors.Add(smb);
        
        host.Open();
        Trace.WriteLine("Customer Service Started");
      }
      catch (CommunicationException ce)
      {
        Trace.WriteLine(DateTime.Now.ToShortTimeString() + " | " + ce.Message + "\r\n");
        host.Abort();
      }
    }
    
  }
}
