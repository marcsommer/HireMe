using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HireMe.DataAccess;

namespace HireMe.Business
{
  /// <summary>
  /// Customer business class.  Includes Id, Name, EmailAddress, and ReviewIds.
  /// Use static factory methods to CreateNew and Get(retrieve) customer.
  /// Also implements abstract BusinessBase methods for Update/Delete,
  /// as well as loading from a DTO.
  /// </summary>
  public class Customer : BusinessBase<Customer, CustomerDto>
  {
    #region Properties
    
    #region public string Name

    /// <summary>
    /// Customer Name
    /// </summary>
    public string Name
    {
      get { return GetName(); }
      set { SetName(value); }
    }

    protected string _Name;
    protected virtual string GetName()
    {
      return _Name;
    }
    protected virtual void SetName(string value)
    {
      if (value != _Name)
      {
        _Name = value;
        MarkThisDirty();
      }
    }

    #endregion    //Name
    
    #region public string EmailAddress
    
    /// <summary>
    /// Email Address of Customer
    /// </summary>
    public string EmailAddress
    {
      get { return GetEmailAddress(); }
      set { SetEmailAddress(value); }
    }

    protected string _EmailAddress;
    protected virtual string GetEmailAddress()
    {
      return _EmailAddress;
    }
    protected virtual void SetEmailAddress(string value)
    {
      if (value != _EmailAddress)
      {
        _EmailAddress = value;
        MarkThisDirty();
      }
    }

    #endregion    //EmailAddress

    #region public List<Guid> ReviewIds

    /// <summary>
    /// List of ReviewIds of reviews the customer has made.
    /// </summary>
    public List<Guid> ReviewIds
    {
      get { return GetReviewIds(); }
      set { SetReviewIds(value); }
    }

    protected List<Guid> _ReviewIds;
    protected virtual List<Guid> GetReviewIds()
    {
      return _ReviewIds;
    }
    protected virtual void SetReviewIds(List<Guid> value)
    {
      if (value != _ReviewIds)
      {
        _ReviewIds = value;
        MarkThisDirty();
      }
    }

    #endregion        //ReviewIds

    #endregion

    #region Static Factory Methods (CreateNew/Get)
    
    public static Customer CreateNew()
    {
      var customer = new Customer();
      customer.LoadFromDto(DalManager.CustomerDal.Create());
      customer.MarkThisDirty();
      return customer;
    }
    public static Customer GetCustomer(Guid id)
    {
      var customer = new Customer();
      customer.LoadFromDto(DalManager.CustomerDal.Get(id));
      customer.MarkThisClean();
      return customer;
    }
    
    #endregion

    #region Business Base Overrides

    /// <summary>
    /// Updates the Customer object.  No need to check for IsDirty,
    /// that is handled by the BusinessBase class.
    /// </summary>
    protected override void UpdateImpl()
    {
      //Update DB from object, return dto may contain new id
      var dto = DalManager.CustomerDal.Update(ToDto());
      SetIdBackingField(dto.Id);
      MarkThisClean();
    }
    /// <summary>
    /// Deletes this object from the DB.
    /// </summary>
    protected override void DeleteImpl()
    {
      DalManager.CustomerDal.Delete(Id);
      MarkThisDirty();
    }
    /// <summary>
    /// Implements the load from a Dto
    /// </summary>
    /// <param name="dto">CustomerDto with state to load</param>
    protected override void LoadFromDtoImpl(CustomerDto dto)
    {
      BeginLoadFromDto();
      try
      {
        Id = dto.Id;
        Name = dto.Name;
        EmailAddress = dto.EmailAddress;
        if (ReviewIds == null)
          ReviewIds = new List<Guid>();
        else
          ReviewIds.Clear();
        ReviewIds.AddRange(dto.ReviewIds);

        //POPULATE CHILDREN REVIEWS
        //If we were concerned about over-the-wire performance, we'd have to come up 
        //with something more clever like Review.GetReviews(ReviewIds).
        Children.Clear();
        if (ReviewIds.Count > 0)
        {
          foreach (var reviewId in ReviewIds)
          {
            Children.Add(Review.GetReview(reviewId));
          }
        }
      }
      finally
      {
        EndLoadFromDto();
      }
    }
    /// <summary>
    /// Creates CustomerDto from Customer instance values
    /// </summary>
    /// <returns>Newly created CustomerDto</returns>
    public override CustomerDto ToDto()
    {
      return new CustomerDto() { Id = this.Id, 
                                 Name = this.Name, 
                                 EmailAddress = this.EmailAddress, 
                                 ReviewIds = new List<Guid>(this.ReviewIds) };
    }

    #endregion
  }
}
