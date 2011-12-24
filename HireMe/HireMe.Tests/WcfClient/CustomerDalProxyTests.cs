using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using HireMe.WcfClient;

namespace HireMe.Tests.WcfClient
{
  [TestFixture]
  public class CustomerDalProxyTests
  {
    [Test]
    public void CREATE_PROXY_ITSELF()
    {
      var proxy = new HireMe.WcfClient.WcfServices.Customer.CustomerDalClient();
    }

    [Test]
    public void PROXY_CREATE_NEW_CUSTOMER()
    {
      CustomerDalProxy proxy = new CustomerDalProxy();
      var dto = proxy.Create();
    }
  }
}
