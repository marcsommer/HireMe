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
  //TODO: Finish fleshing out customerTests to handle more complex scenarios
  [TestFixture]
  public class CustomerMockDbAdapterTests : IBusinessTests
  {
    Guid _NewCustomerId = Guid.Parse(@"73D3252A-036D-4D8B-9061-BEFB6805F657");
    string _NewCustomerName = "Newcome Customerm";
    string _NewCustomerEmail = "newcustomerhereemail@emailcustomerherenew.com";

    Guid _NewReviewId = Guid.Parse(@"91067A89-8251-4AC0-9DAA-1EDBB703CC44");
    int _NewReviewRating = 1;
    string _NewReviewComments = "I think this is a new comment heeeyah.";

    Guid _NewReviewId2 = Guid.Parse(@"67B42D5E-072D-4341-A46A-A750C9E4B61D");
    int _NewReviewRating2 = 2;
    string _NewReviewComments2 = "NEEEEEEEEEEEEEEEEEWSSSSY NEW Comments here.";

    Guid _DeleteCustomerId = Guid.Parse(@"39E511CB-934E-433B-BB8A-0D7F44127529");
    string _DeleteCustomerName = "Delete Meeeename";
    string _DeleteCustomerEmail = @"deletemedeleteme@deletemeemmeemmedelete.com";

    Guid _DeleteReviewId = Guid.Parse(@"BF55E347-CE4D-477A-B3B6-544EE1926FF4");
    int _DeleteRating = 4;
    string _DeleteComments = "I think this review stinks and I should be deleted for it....yay censorship!";

    
    [Test]
    public void CREATE_NEW()
    {
      var cust = Customer.CreateNew();
    }

    [Test]
    public void GET()
    {
      var cust = Customer.GetCustomer(MockDbTestData.CustId1);
      cust = Customer.GetCustomer(MockDbTestData.CustId2);
    }

    [Test]
    public void UPDATE()
    {
      var cust = Customer.GetCustomer(MockDbTestData.CustId1);
      cust.Name = _NewCustomerName;
      cust.EmailAddress = _NewCustomerEmail;
      cust = (Customer)cust.Update();
      var testCust = Customer.GetCustomer(cust.Id);
      Assert.AreEqual(_NewCustomerName, testCust.Name);
    }

    [Test]
    //HACK: ExpectedException is type FaultException.  Need to implement exception handling with WCF
    [ExpectedException(typeof(FaultException))]
    //[ExpectedException(typeof(ReviewDataException))]
    public void DELETE_IMMEDIATELY()
    {
      var custDto = new CustomerDto()
      {
        Id = _DeleteCustomerId,
        Name = _DeleteCustomerName,
        EmailAddress = _DeleteCustomerEmail,
        ReviewIds = new List<Guid>()
        {
          _DeleteReviewId
        }
      };

      var reviewDto = new ReviewDto()
      {
        Id =_DeleteReviewId,
        Rating = _DeleteRating,
        Comments = _DeleteComments,
        CustomerId = _DeleteCustomerId
      };

      //var cust = Customer.Create(custDto, false);
      //var review = Review.Create(revDto);
      //cust.AddChild(review);

      var cust = Customer.Create(custDto, reviewDto);

      cust.Update();

      cust.DeleteImmediately(); //deletes children
      try
      {
        Customer.GetCustomer(cust.Id);
      }
      catch (FaultException fe)
      {
        Review.GetReview(cust.ReviewIds[0]);
        //should throw another FaultException here
      }
    }

    [Test]
    public void COMMIT()
    {
      var cust = Customer.GetCustomer(MockDbTestData.CustId1);
      cust.Name = _NewCustomerName;
      cust.EmailAddress = _NewCustomerEmail;
      Customer updatedCust = (Customer)cust.Commit();
      Customer gottenCust = Customer.GetCustomer(updatedCust.Id);
      Assert.AreEqual(_NewCustomerName, gottenCust.Name);
      Assert.AreEqual(_NewCustomerEmail, gottenCust.EmailAddress);
    }

    [Test]
    public void TO_DTO()
    {
      var cust = Customer.GetCustomer(MockDbTestData.CustId1);
      var dto = cust.ToDto();
      Assert.AreEqual(cust.Name, dto.Name);
      Assert.AreEqual(cust.EmailAddress, dto.EmailAddress);
      for (int i = 0; i < cust.ReviewIds.Count; i++)
      {
        Assert.AreEqual(cust.ReviewIds[i], dto.ReviewIds[i]);
      }

      cust = Customer.GetCustomer(MockDbTestData.CustId2);
      dto = cust.ToDto();
      Assert.AreEqual(cust.Name, dto.Name);
      Assert.AreEqual(cust.EmailAddress, dto.EmailAddress);
      for (int i = 0; i < cust.ReviewIds.Count; i++)
      {
        Assert.AreEqual(cust.ReviewIds[i], dto.ReviewIds[i]);
      }
    }

    [Test]
    public void LOAD_FROM_DTO()
    {

      var dto = new CustomerDto()
      {
        Id = MockDbTestData.CustId1,
        Name = MockDbTestData.CustName1,
        EmailAddress = MockDbTestData.CustEmail1,
        ReviewIds = new List<Guid>()
        {
          MockDbTestData.ReviewId1
        }
      };
      
      var cust = Customer.CreateNew();
      cust.LoadFromDto(dto);

      Assert.AreEqual(dto.Name, cust.Name);
      Assert.AreEqual(dto.EmailAddress, cust.EmailAddress);
      for (int i = 0; i < cust.ReviewIds.Count; i++)
      {
        Assert.AreEqual(cust.ReviewIds[i], dto.ReviewIds[i]);
      }


      dto = new CustomerDto()
      {
        Id = MockDbTestData.CustId2,
        Name = MockDbTestData.CustName2,
        EmailAddress = MockDbTestData.CustEmail2,
        ReviewIds = new List<Guid>()
        {
          MockDbTestData.ReviewId2A,
          MockDbTestData.ReviewId2B
        }
      };

      cust = Customer.CreateNew();
      cust.LoadFromDto(dto);

      Assert.AreEqual(dto.Name, cust.Name);
      Assert.AreEqual(dto.EmailAddress, cust.EmailAddress);
      for (int i = 0; i < cust.ReviewIds.Count; i++)
      {
        Assert.AreEqual(cust.ReviewIds[i], dto.ReviewIds[i]);
      }
    }

    [Test]
    public void PROPERTYCHANGED_TRIGGERED()
    {
      int countChangesRaised = 0;
      var cust = Customer.CreateNew();
      cust.PropertyChanged += (s, e) =>
        {
          countChangesRaised++;
        };
      //1
      cust.Name = "woeirhsidhfiouauuhuwuhdjfk";
      //2
      cust.EmailAddress = "sdfkjskdjf@dskjfsoidwhdkjfs.com";

      int numPropsChanged = 2;
      Assert.AreEqual(numPropsChanged, countChangesRaised);
    }


    public void CREATE_FROM_DTO()
    {
      CustomerDto dto = new CustomerDto() 
      {
        Id = MockDbTestData.CustId1,
        Name=MockDbTestData.CustName1,
        EmailAddress = MockDbTestData.CustEmail1, 
        ReviewIds = new List<Guid>()
        {
          MockDbTestData.ReviewId1
        }
      };

      var cust = Customer.Create(dto);
    }
  }
}
