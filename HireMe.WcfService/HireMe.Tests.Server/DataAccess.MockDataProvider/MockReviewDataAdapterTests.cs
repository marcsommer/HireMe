using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using HireMe.DataAccess.MockDataProvider;
using HireMe.MockData;
using HireMe.DataAccess;
using HireMe.Business;

namespace HireMe.Tests.Server
{
  [TestFixture]
  public class MockReviewDataAdapterTests : IDataAdapterTests
  {
    private MockReviewDataAdapter _Adapter;
    [SetUp]
    public void SetupTests()
    {
      MockDb.InitializeData();
      _Adapter = new MockReviewDataAdapter();
    }

    [Test]
    public void CREATE_NEW_DTO()
    {
      var dto = _Adapter.Create();
    }

    [Test]
    public void GET_DTO()
    {
      var dto = _Adapter.Get(MockDb.Reviews[0].ReviewId);
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
      var dto = MockReviewDataAdapter.CreateDtoFromData(MockDb.Reviews[0]);

      //SET NEW PROPERTIES
      int newRating = 2;
      string newComments = "These are my NEW NEW NEW COMMENTS";
      dto.Rating = newRating;
      dto.Comments = newComments;
      dto.CustomerId = MockDb.CustId1;//hack:WcfService.Tests test update_dto

      //ADAPTER.UPDATE
      _Adapter.Update(dto);

      //ASSERT THAT UPDATE OCCURRED
      var checkDto = _Adapter.Get(dto.Id);
      Assert.AreEqual(dto.Rating, checkDto.Rating);
      Assert.AreEqual(dto.Comments, checkDto.Comments);
      Assert.AreEqual(dto.CustomerId, checkDto.CustomerId);
    }

    [Test]
    [ExpectedException(typeof(ReviewDataException))]
    public void DELETE_ID_EXPECT_TYPEDATAEXCEPTION()
    {
      //GET THE FIRST CUSTOMER IN MOCKDB
      ReviewDto dto = MockReviewDataAdapter.CreateDtoFromData(MockDb.Reviews[0]);

      //ADAPTER.DELETE
      _Adapter.Delete(dto.Id);

      //ATTEMPT GET ON DELETED CUSTOMER ID, SHOULD THROW CUSTOMERDATAEXCEPTION
      _Adapter.Get(dto.Id);
    }


    /// <summary>
    /// Get all objects as opposed to Dtos
    /// </summary>
    [Test]
    public void GET_ALL_OBJECTS()
    {
      var allDtos = _Adapter.GetAll();
      foreach (var dto in allDtos)
      {
        var review = Review.Create(dto);
      }
    }
  }
}
