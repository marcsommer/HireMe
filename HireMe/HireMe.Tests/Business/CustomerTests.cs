using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using HireMe.Business;

namespace HireMe.Tests.Business
{
  [TestFixture]
  public class CustomerTests
  {
    [Test]
    public void CREATE_CUSTOMER()
    {
      var customer = Customer.CreateNew();
    }
  }
}
