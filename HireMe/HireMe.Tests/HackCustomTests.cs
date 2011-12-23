using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HireMe.Tests.WcfClient;
using HireMe.Tests.Business;

namespace HireMe.Tests
{
  public class HackCustomTests
  {
    internal HackCustomTests()
    {
      //Hack: NUnit tests are failing for proxy.  It is unable to instantiate the CustomerDalClient class,...
      //as it isn't able to locate the endpoint.  I've tried changing contract name, but that wasn't the problem.  
      //The app.config still isn't being copied to the right place for the NUnit tool (I only have VS 2010 Express, 
      //so I can't run NUnit integrated into VS.  I had to set it up as an external tool that starts its own process 
      //and then starts the startup project here.  So, I'm putting the unit tests here as a workaround to keep things 
      //moving along.  So, for individual tests that do not need to touch the WCF Service, I can use the NUnit external tool.
      //But the tests that DO touch the WCF Service are just run here.  I'll troubleshoot it more later.

      //So, work around for debuggin/breakpoints is to mimic a method here when needed.  It isn't ideal, but 
      //neither is working with Express Editions.
      //Setup by initializing the SetupTearDownTests object
      var setupTearDownInitializer = new SetupTeardownTests();
      setupTearDownInitializer.SetupTests();
      setupTearDownInitializer.TearDownTests();

      RunCustomerTests();
      RunProxyTests();
    }

    private void RunProxyTests()
    {
      var tests = new CustomerDalProxyTests();
      tests.CREATE_PROXY_ITSELF();
      tests.PROXY_CREATE_NEW_CUSTOMER();
    }

    private void RunCustomerTests()
    {
      var tests = new CustomerTests();
      tests.CREATE_CUSTOMER();
    }
  }
}
