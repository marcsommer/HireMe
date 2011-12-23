using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using HireMe.Wpf;
using System.ComponentModel.Composition.Hosting;
using HireMe.DataAccess;

namespace HireMe.Tests
{
  [SetUpFixture]
  public class SetupTeardownTests
  {
    [SetUp]
    public void SetupTests()
    {
      //This would eventually go elsewhere as different container compositions 
      //would be tested, but since we're going to only have one right now, we have this in the SetupFixture class.
      
      //INITIALIZE CONTAINER
      AssemblyCatalog catProxyDalImplementation = new AssemblyCatalog(typeof(HireMe.WcfClient.CustomerDalProxy).Assembly);
      AggregateCatalog allCatalogs = new AggregateCatalog(catProxyDalImplementation);
      CompositionContainer container = new CompositionContainer(allCatalogs);
      Services.Initialize(container);

      //INITIALIZE DALMANAGER
      DalManager.Initialize(container);
    }

    [TearDown]
    public void TearDownTests()
    {
    }
  }
}
