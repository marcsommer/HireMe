using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HireMe.Tests
{
  public class ManualTests
  {
    internal ManualTests()
    {
      //Hack: NUnit tests are failing for proxy.  It is unable to instantiate the CustomerDalClient class,...
      //as it isn't able to locate the endpoint.  I've tried changing contract name, but that wasn't the problem.  
      //The app.config still isn't being copied to the right place for the NUnit tool (I only have VS 2010 Express, 
      //so I can't run NUnit integrated into VS).  I had to set it up as an external tool that starts its own process 
      //and then starts the startup project here.  So, I'm putting the unit tests here as a workaround to keep things 
      //moving along.  So, for individual tests that do not need to touch the WCF Service, I can use the NUnit external tool.
      //But the tests that DO touch the WCF Service are just run here.  I'll troubleshoot it more later.

      //So, work around for debuggin/breakpoints is to mimic a method here when needed.  It isn't ideal, but 
      //neither is working with Express Editions.

      //SOLUTION:  Okay, after looking and looking at where and how to copy the app.config file properly so NUnit finds it, 
      //I finally came across http://blogs.msdn.com/b/josealmeida/archive/2004/05/31/loading-config-files-in-nunit.aspx 
      //where they explain that you can run //var location = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile; to 
      //find out where _exactly_(location AND filename) NUnit is looking for the config file.  I copied over the WcfClient.app.config
      //file to this location "HireMe.Tests.config" in the "HireMe.Tests.csproj" directory and it works fine.  Wee dogey.

      //I am still using this class when I want to step through code, as external NUnit tool does not allow me to do this.

      //Setup by initializing the SetupTearDownTests object
      var setupTearDownInitializer = new SetupTeardownTests();
      setupTearDownInitializer.SetupTests();

      try
      {
        RunIBusinessTests(new CustomerMockDbAdapterTests());
        RunIBusinessTests(new ReviewMockDbAdapterTests());
        //RunProxyTests();
        //RunViewModelTests();
      }
      finally
      {
        setupTearDownInitializer.TearDownTests();
      }
    }

    private void RunViewModelTests()
    {
      var tests = new CustomerViewModelTests();
      tests.CUSTOMER_NAME_CHANGE_PROPAGATES_PROPERTY_CHANGED_TO_VIEWMODEL();
    }

    private void RunProxyTests()
    {
      var tests = new CustomerDalProxyTests();
      tests.CREATE_PROXY_ITSELF();
    }

    

    private void RunIBusinessTests(IBusinessTests tests)
    {
      tests.CREATE_NEW();
      tests.CREATE_FROM_DTO();
      tests.GET();
      tests.DELETE_IMMEDIATELY();
      tests.UPDATE();
      tests.COMMIT();
      tests.LOAD_FROM_DTO();
      tests.TO_DTO();
    }
  }
}
