using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using HireMe.DataAccess.MockDataProvider;
using HireMe.MockData;
using HireMe.DataAccess;

namespace HireMe.Tests.Server
{
  [TestFixture]
  public class MockCustomerDataAdapterTests : IDataAdapterTests
  {
    private MockCustomerDataAdapter _Adapter;

    [SetUp]
    public void SetupTests()
    {
      MockDb.InitializeData();
      _Adapter = new MockCustomerDataAdapter();
    }

    [Test]
    public void CREATE_NEW_DTO()
    {
      var dto = _Adapter.Create();
    }

    [Test]
    public void GET_DTO()
    {
      var dto = _Adapter.Get(MockDb.Customers[0].CustomerId);
    }

    [Test]
    public void GET_ALL_DTOS()
    {
      var allDtos = _Adapter.GetAll();
    }

    [Test]
    public void UPDATE_DTO()
    {
      //GET THE FIRST CUSTOMER IN MOCKDB
      CustomerDto dto = MockCustomerDataAdapter.CreateDtoFromData(MockDb.Customers[0]);

      //SET NEW PROPERTIES
      var newName = "MyNewNameHere";
      var newEmail = "MynewemailAt@Atatatatatatat.com";
      dto.Name = newName;
      dto.EmailAddress = newEmail;

      //ADAPTER.UPDATE
      _Adapter.Update(dto);

      //ASSERT THAT UPDATE OCCURRED
      var checkDto = _Adapter.Get(dto.Id);
      Assert.AreEqual(dto.Name, checkDto.Name);
      Assert.AreEqual(dto.EmailAddress, checkDto.EmailAddress);
    }

    [Test]
    [ExpectedException(typeof(CustomerDataException))]
    public void DELETE_ID_EXPECT_TYPEDATAEXCEPTION()
    {
      //GET THE FIRST CUSTOMER IN MOCKDB
      CustomerDto dto = MockCustomerDataAdapter.CreateDtoFromData(MockDb.Customers[0]);

      //ADAPTER.DELETE
      _Adapter.Delete(dto.Id);

      //ATTEMPT GET ON DELETED CUSTOMER ID, SHOULD THROW CUSTOMERDATAEXCEPTION
      _Adapter.Get(dto.Id);
    }


    public void GET_ALL_OBJECTS()
    {
      throw new NotImplementedException();
    }
  }
}
