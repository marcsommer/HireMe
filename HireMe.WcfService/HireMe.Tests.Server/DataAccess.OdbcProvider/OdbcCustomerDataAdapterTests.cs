using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using HireMe.DataAccess.OdbcProvider;
using HireMe.DataAccess;
using HireMe.WcfService;

namespace HireMe.Tests.Server.DataAccess.OdbcProvider
{
  [TestFixture]
  public class OdbcCustomerDataAdapterTests : IDataAdapterTests
  {
    [Test]
    public void CREATE_NEW_CUSTOMER_DTO()
    {
      OdbcCustomerDataAdapter adapter = new OdbcCustomerDataAdapter();
      var dto = adapter.Create();
    }

    [Test]
    public void GET_CUSTOMER_DTO()
    {
      OdbcCustomerDataAdapter adapter = new OdbcCustomerDataAdapter();
      var dto = adapter.Create();
      dto = adapter.Get(dto.CustomerId);
    }

    [Test]
    public void GET_ALL_CUSTOMER_DTOS()
    {
      OdbcCustomerDataAdapter adapter = new OdbcCustomerDataAdapter();
      IList<CustomerDto> allDtos = adapter.GetAll();
      if (allDtos.Count == 0)
        throw new Exception("GetAll() returned Zero records.  This is expected if there are no records in DB.");
    }

    [Test]
    public void UPDATE_CUSTOMER_DTO()
    {
      var adapter = new OdbcCustomerDataAdapter();
      var dto = adapter.Create();
      var updatedTestName = "UpdatedNameHere";
      var updatedTestEmail = "test123@testtessttessettsetsete.com";
      dto.Name = updatedTestName;
      dto.EmailAddress = updatedTestEmail;

      adapter.Update(dto);
    }

    [Test]
    [ExpectedException(typeof(CustomerDataException))]
    public void DELETE_CUSTOMER_EXPECT_CUSTOMERDATAEXCEPTION()
    {
      var adapter = new OdbcCustomerDataAdapter();
      //CREATE
      var dto = adapter.Create();

      //EDIT/UPDATE
      dto.Name = "NameHere";
      adapter.Update(dto);

      //DELETE
      adapter.Delete(dto.CustomerId);

      //TRY TO GET(ID), SHOULD THROW CUSTOMERDATAEXCEPTION
      adapter.Get(dto.CustomerId);
    }
  }
}
