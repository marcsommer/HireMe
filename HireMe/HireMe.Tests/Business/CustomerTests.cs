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
  //TODO: Finish fleshing out customerTests to handle more complex scenarios
  [TestFixture]
  public class CustomerTests : IBusinessTests
  {
    string _CustTestName = "TestName HereHere";
    string _CustTestEmail = @"testtestonetwothree@testtestonetestonetest.com";

    int _TestReview1Rating = 3;
    string _TestReview1Id = @"9D749B35-ED9C-42E6-B02D-8EE2BC1E05F6";
    string _TestReview2Id = @"33E7BBD5-B3DD-4166-BBBD-E94E5D477506";

    [Test]
    public void CREATE()
    {
      var cust = Customer.CreateNew();
    }

    [Test]
    public void GET()
    {
      var cust = Customer.CreateNew();
      cust = Customer.GetCustomer(cust.Id);
    }

    [Test]
    public void UPDATE()
    {
      var cust = Customer.CreateNew();
      cust.Name = _CustTestName;
      cust = (Customer)cust.Update();
      var testCust = Customer.GetCustomer(cust.Id);
      Assert.AreEqual(_CustTestName, testCust.Name);
    }

    [Test]
    //HACK: ExpectedException is type FaultException.  Need to implement exception handling with WCF
    [ExpectedException(typeof(FaultException))]
    //[ExpectedException(typeof(ReviewDataException))]
    public void DELETE_IMMEDIATELY()
    {
      var cust = Customer.CreateNew();
      cust.DeleteImmediately();
      Customer.GetCustomer(cust.Id);
    }

    [Test]
    public void COMMIT()
    {
      var cust = Customer.CreateNew();
      cust.Name = _CustTestName;
      Customer updatedCust = (Customer)cust.Commit();
      Customer gottenCust = Customer.GetCustomer(updatedCust.Id);
      Assert.AreEqual(cust.Name, gottenCust.Name);
      
    }

    [Test]
    public void TO_DTO()
    {
      //HACK: CustomerTests.TO_DTO: I'm not sure if I should new up or touch DB using CreateNew().  Right now, I'm touching the DB.
      var cust = Customer.CreateNew();
      cust.Name = _CustTestName;
      cust.EmailAddress = _CustTestEmail;
      cust.ReviewIds.Add(Guid.Parse(_TestReview1Id));
      cust.ReviewIds.Add(Guid.Parse(_TestReview2Id));

      var dto = cust.ToDto();
      Assert.AreEqual(cust.Name, dto.Name);
      Assert.AreEqual(cust.EmailAddress, dto.EmailAddress);
      Assert.AreEqual(cust.ReviewIds[0], dto.ReviewIds[0]);
      Assert.AreEqual(cust.ReviewIds[1], dto.ReviewIds[1]);
    }

    [Test]
    public void LOAD_FROM_DTO()
    {
      //HACK: CustomerTests.LOAD_FROM_DTO: I'm not sure if I should new up or touch DB using CreateNew().  Right now, I'm touching the DB.
      var dto = new CustomerDto();
      dto.Name = _CustTestName;
      dto.EmailAddress = _CustTestEmail;
      var rev1 = Review.CreateNew();
      var rev2 = Review.CreateNew();
      
      dto.ReviewIds.Add(rev1.Id);
      dto.ReviewIds.Add(rev2.Id);

      var cust = Customer.CreateNew();
      cust.LoadFromDto(dto);

      Assert.AreEqual(dto.Name, cust.Name);
      Assert.AreEqual(dto.EmailAddress, cust.EmailAddress);
      Assert.AreEqual(dto.ReviewIds[0], cust.ReviewIds[0]);
      Assert.AreEqual(dto.ReviewIds[1], cust.ReviewIds[1]);

    }
  }
}
