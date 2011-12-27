using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using HireMe.Business;
using HireMe.DataAccess;
using System.ServiceModel;

namespace HireMe.Tests
{
  //TODO: Finish fleshing out ReviewTests to handle more complex scenarios
  [TestFixture]
  public class ReviewMockDbAdapterTests : IBusinessTests
  {
    Guid _TestId = Guid.Parse(@"481291C7-3CB3-4C4F-974F-718494D1CA4A");
    string _TestComments = "This is awesome.....NEW COMMENT NEW COMMENT!!!";
    int _TestRating = 3;
    Guid _TestCustomerId = Guid.Parse("21139D1F-5A92-4D0D-A451-758A507D0F87");

    [Test]
    public void CREATE_NEW()
    {
      var review = Review.CreateNew();
    }

    [Test]
    public void GET()
    {
      var review = Review.CreateNew();
      review = Review.GetReview(review.Id);
    }

    [Test]
    public void UPDATE()
    {
      var review = Review.CreateNew();
      review.Comments = _TestComments;
      review.Update();
    }

    [Test]
    //HACK: ExpectedException is type FaultException.  Need to implement exception handling with WCF
    [ExpectedException(typeof(FaultException))]
    //[ExpectedException(typeof(ReviewDataException))]
    public void DELETE_IMMEDIATELY()
    {
      var review = Review.CreateNew();
      review.DeleteImmediately();
      Review.GetReview(review.Id);
    }

    [Test]
    public void COMMIT()
    {
      var review = Review.CreateNew();
      review.Comments = _TestComments;
      Review updatedReview = (Review)review.Commit();
      Review gottenReview = Review.GetReview(updatedReview.Id);
      Assert.AreEqual(review.Comments, gottenReview.Comments);
    }

    [Test]
    public void TO_DTO()
    {
      //HACK: ReviewTests.TO_DTO: I'm not sure if I should new up or touch DB using CreateNew().  Right now, I'm touching the DB.
      var review = Review.CreateNew();
      review.Comments = _TestComments;
      review.Rating = _TestRating;
      review.CustomerId = _TestCustomerId;

      var dto = review.ToDto();
      Assert.AreEqual(review.Comments, dto.Comments);
      Assert.AreEqual(review.Rating, dto.Rating);
      Assert.AreEqual(review.CustomerId, dto.CustomerId);
    }

    [Test]
    public void LOAD_FROM_DTO()
    {
      //HACK: ReviewTests.LOAD_FROM_DTO: I'm not sure if I should new up or touch DB using CreateNew().  Right now, I'm touching the DB.
      var dto = new ReviewDto();
      dto.Comments = _TestComments;
      dto.Rating = _TestRating;
      var cust = Customer.CreateNew();
      dto.CustomerId = cust.Id;

      
      var review = Review.CreateNew();
      review.LoadFromDto(dto);

      Assert.AreEqual(dto.Comments, review.Comments);
      Assert.AreEqual(dto.Rating, review.Rating);
      Assert.AreEqual(dto.CustomerId, review.CustomerId);

    }

    [Test]
    public void PROPERTYCHANGED_TRIGGERED()
    {
      int countChangesRaised = 0;
      var review = Review.CreateNew();
      review.PropertyChanged += (s, e) =>
      {
        countChangesRaised++;
      };
      //1
      review.Rating = 4;
      //2
      review.Comments = "sdiuweifjskdjfoijwef";

      int numPropsChanged = 2;
      Assert.AreEqual(numPropsChanged, countChangesRaised);
    }


    [Test]
    public void CREATE_FROM_DTO()
    {
      var dto = new ReviewDto()
      {
        Id = _TestId,
        Comments = _TestComments, 
        Rating = _TestRating,
        CustomerId = _TestCustomerId
      };

      var review = Review.Create(dto);
    }
  }
}
