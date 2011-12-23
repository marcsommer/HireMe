using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using HireMe.DataAccess.MockDataProvider;
using HireMe.MockData;
using HireMe.DataAccess;

namespace HireMe.Tests.Server.DataAccess.MockDataProvider
{
  [TestFixture]
  public class MockCustomerDataAdapterTests : IDataAdapterTests
  {
    [Test]
    public void CREATE_NEW_CUSTOMER_DTO()
    {
      var adapter = new MockCustomerDataAdapter();
      var dto = adapter.Create();
    }

    [Test]
    public void GET_CUSTOMER_DTO()
    {
      var adapter = new MockCustomerDataAdapter();
      var dto = adapter.Get(MockDb.Customers[0].CustomerId);
    }

    [Test]
    public void GET_ALL_CUSTOMER_DTOS()
    {
      var adapter = new MockCustomerDataAdapter();
      var allDtos = adapter.GetAll();
    }

    [Test]
    public void UPDATE_CUSTOMER_DTO()
    {
      var adapter = new MockCustomerDataAdapter();
      //GET THE FIRST CUSTOMER IN MOCKDB
      CustomerDto dto = MockCustomerDataAdapter.CreateDtoFromData(MockDb.Customers[0]);

      //SET NEW PROPERTIES
      var newName = "MyNewNameHere";
      var newEmail = "MynewemailAt@Atatatatatatat.com";
      dto.Name = newName;
      dto.EmailAddress = newEmail;

      //ADAPTER.UPDATE
      adapter.Update(dto);

      //ASSERT THAT UPDATE OCCURRED
      var checkDto = adapter.Get(dto.CustomerId);
      Assert.AreEqual(dto.Name, checkDto.Name);
      Assert.AreEqual(dto.EmailAddress, checkDto.EmailAddress);
    }

    [Test]
    [ExpectedException(typeof(CustomerDataException))]
    public void DELETE_CUSTOMER_EXPECT_CUSTOMERDATAEXCEPTION()
    {
      //GET THE FIRST CUSTOMER IN MOCKDB
      var adapter = new MockCustomerDataAdapter();
      CustomerDto dto = MockCustomerDataAdapter.CreateDtoFromData(MockDb.Customers[0]);

      //ADAPTER.DELETE
      adapter.Delete(dto.CustomerId);

      //ATTEMPT GET ON DELETED CUSTOMER ID, SHOULD THROW CUSTOMERDATAEXCEPTION
      adapter.Get(dto.CustomerId);
    }
  }
}
