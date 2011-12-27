using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using HireMe.WcfClient;

namespace HireMe.Tests
{
  [TestFixture]
  public class ReviewDalProxyTests
  {
    [Test]
    public void CREATE_PROXY_ITSELF()
    {
      var proxy = new HireMe.WcfClient.WcfServices.Review.ReviewDalClient();
    }
  }
}
