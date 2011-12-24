using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using HireMe.Business;

namespace HireMe.Tests.Business
{
  [TestFixture]
  public class CustomerTests : IBusinessTests
  {
    [Test]
    public void CREATE()
    {
      var cust = Customer.CreateNew();
    }

    [Test]
    public void GET()
    {
      throw new NotImplementedException();
    }

    [Test]
    public void UPDATE()
    {
      throw new NotImplementedException();
    }

    [Test]
    public void DELETE_IMMEDIATELY()
    {
      throw new NotImplementedException();
    }

    [Test]
    public void COMMIT()
    {
      throw new NotImplementedException();
    }

    [Test]
    public void TO_DTO()
    {
      throw new NotImplementedException();
    }

    [Test]
    public void LOAD_FROM_DTO()
    {
      throw new NotImplementedException();
    }
  }
}
