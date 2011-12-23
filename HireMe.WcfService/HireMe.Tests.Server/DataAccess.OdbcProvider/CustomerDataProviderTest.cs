using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using HireMe.DataAccess.OdbcProvider;
using HireMe.DataAccess;

namespace HireMe.Tests.Server.DataAccess.OdbcProvider
{
  [TestFixture]
  public class CustomerDataProviderTest
  {
    [Test]
    public void CREATE_NEW_CUSTOMER_DTO()
    {
      CustomerDataAdapter adapter = new CustomerDataAdapter();
      var dto = adapter.Create();
    }

    [Test]
    public void GET_CUSTOMER_DTO()
    {
      CustomerDataAdapter adapter = new CustomerDataAdapter();
      var dto = adapter.Create();
      dto = adapter.Get(dto.CustomerId);
    }

    [Test]
    public void GET_ALL_CUSTOMER_DTOS()
    {
      CustomerDataAdapter adapter = new CustomerDataAdapter();
      IList<CustomerDto> allDtos = adapter.GetAll();
      if (allDtos.Count == 0)
        throw new Exception("GetAll() returned Zero records.  This is expected if there are no records in DB.");
    }

    [Test]
    public void UPDATE_CUSTOMER_DTO()
    {
      var adapter = new CustomerDataAdapter();
      var dto = adapter.Create();
      var updatedTestName = "UpdatedNameHere";
      var updatedTestEmail = "test123@testtessttessettsetsete.com";
      dto.Name = updatedTestName;
      dto.EmailAddress = updatedTestEmail;

      adapter.Update(dto);
    }
  }
}
