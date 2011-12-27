using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using HireMe.DataAccess.OdbcProvider;
using HireMe.DataAccess;
using HireMe.WcfService;

namespace HireMe.Tests.Server
{
  [TestFixture]
  public class OdbcCustomerDataAdapterTests : IDataAdapterTests
  {
    OdbcCustomerDataAdapter adapter;
    public void SetupTests()
    {
      adapter = new OdbcCustomerDataAdapter();
    }

    [Test]
    public void CREATE_NEW_DTO()
    {
      var dto = adapter.Create();
    }

    [Test]
    public void GET_DTO()
    {
      var dto = adapter.Create();
      dto = adapter.Get(dto.Id);
    }

    [Test]
    public void GET_ALL_DTOS()
    {
      IList<CustomerDto> allDtos = adapter.GetAll();
      if (allDtos.Count == 0)
        throw new Exception("GetAll() returned Zero records.  This is expected if there are no records in DB.");
    }

    [Test]
    public void UPDATE_DTO()
    {
      var dto = adapter.Create();
      var updatedTestName = "UpdatedNameHere";
      var updatedTestEmail = "test123@testtessttessettsetsete.com";
      dto.Name = updatedTestName;
      dto.EmailAddress = updatedTestEmail;

      adapter.Update(dto);
    }

    [Test]
    [ExpectedException(typeof(CustomerDataException))]
    public void DELETE_ID_EXPECT_TYPEDATAEXCEPTION()
    {
      //CREATE
      var dto = adapter.Create();

      //EDIT/UPDATE
      dto.Name = "NameHere";
      adapter.Update(dto);

      //DELETE
      adapter.Delete(dto.Id);

      //TRY TO GET(ID), SHOULD THROW CUSTOMERDATAEXCEPTION
      adapter.Get(dto.Id);
    }

    public void GET_ALL_OBJECTS()
    {
      throw new NotImplementedException();
    }
  }
}
