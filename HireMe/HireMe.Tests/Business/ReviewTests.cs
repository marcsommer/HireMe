using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using HireMe.Business;
using HireMe.DataAccess;
using System.ServiceModel;

namespace HireMe.Tests.Business
{
  //TODO: Finish fleshing out ReviewTests to handle more complex scenarios
  [TestFixture]
  public class ReviewTests : IBusinessTests
  {
    string _NewComments = "This is awesome.....NEW COMMENT NEW COMMENT!!!";
    int _NewRating = 3;
    Guid _NewCustomerId = Guid.Parse("21139D1F-5A92-4D0D-A451-758A507D0F87");

    [Test]
    public void CREATE()
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
      review.Comments = _NewComments;
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
      review.Comments = _NewComments;
      Review updatedReview = (Review)review.Commit();
      Review gottenReview = Review.GetReview(updatedReview.Id);
      Assert.AreEqual(review.Comments, gottenReview.Comments);
    }

    [Test]
    public void TO_DTO()
    {
      //HACK: ReviewTests.TO_DTO: I'm not sure if I should new up or touch DB using CreateNew().  Right now, I'm touching the DB.
      var review = Review.CreateNew();
      review.Comments = _NewComments;
      review.Rating = _NewRating;
      review.CustomerId = _NewCustomerId;

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
      dto.Comments = _NewComments;
      dto.Rating = _NewRating;
      var cust = Customer.CreateNew();
      dto.CustomerId = cust.Id;

      
      var review = Review.CreateNew();
      review.LoadFromDto(dto);

      Assert.AreEqual(dto.Comments, review.Comments);
      Assert.AreEqual(dto.Rating, review.Rating);
      Assert.AreEqual(dto.CustomerId, review.CustomerId);

    }
  }
}
