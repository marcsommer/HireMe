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
  public class MockReviewDataAdapterTests : IDataAdapterTests
  {
    [Test]
    public void CREATE_NEW_DTO()
    {
      var adapter = new MockReviewDataAdapter();
      var dto = adapter.Create();
    }

    [Test]
    public void GET_DTO()
    {
      var adapter = new MockReviewDataAdapter();
      var dto = adapter.Get(MockDb.Reviews[0].ReviewId);
    }

    [Test]
    public void GET_ALL_DTOS()
    {
      var adapter = new MockReviewDataAdapter();
      var allDtos = adapter.GetAll();
    }

    [Test]
    public void UPDATE_DTO()
    {
      var adapter = new MockReviewDataAdapter();
      //GET THE FIRST CUSTOMER IN MOCKDB
      var dto = MockReviewDataAdapter.CreateDtoFromData(MockDb.Reviews[0]);

      //SET NEW PROPERTIES
      int newRating = 2;
      string newComments = "These are my NEW NEW NEW COMMENTS";
      dto.Rating = newRating;
      dto.Comments = newComments;
      dto.CustomerId = MockDb.CustId1;//hack:WcfService.Tests test update_dto

      //ADAPTER.UPDATE
      adapter.Update(dto);

      //ASSERT THAT UPDATE OCCURRED
      var checkDto = adapter.Get(dto.Id);
      Assert.AreEqual(dto.Rating, checkDto.Rating);
      Assert.AreEqual(dto.Comments, checkDto.Comments);
      Assert.AreEqual(dto.CustomerId, checkDto.CustomerId);
    }

    [Test]
    [ExpectedException(typeof(ReviewDataException))]
    public void DELETE_ID_EXPECT_TYPEDATAEXCEPTION()
    {
      //GET THE FIRST CUSTOMER IN MOCKDB
      var adapter = new MockReviewDataAdapter();
      ReviewDto dto = MockReviewDataAdapter.CreateDtoFromData(MockDb.Reviews[0]);

      //ADAPTER.DELETE
      adapter.Delete(dto.Id);

      //ATTEMPT GET ON DELETED CUSTOMER ID, SHOULD THROW CUSTOMERDATAEXCEPTION
      adapter.Get(dto.Id);
    }
  }
}
