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
    public Customer()
    {
      ReviewIds = new List<Guid>();
    }

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
        if (!IsLoadingDto)
        {
          NotifyOfPropertyChange(() => Name);
          MarkThisDirty();
        }
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
        if (!IsLoadingDto)
        {
          NotifyOfPropertyChange(() => EmailAddress);
          MarkThisDirty();
        }
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
        if (!IsLoadingDto)
        {
          NotifyOfPropertyChange(() => ReviewIds);
          MarkThisDirty();
        }
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
    /// <summary>
    /// Creates a new Customer object using the dto provided and saves it to DB.  If loadChildren is true,
    /// then this will actively load the Children (Reviews) from the database using the review ids
    /// in dto.ReviewIds.  Otherwise, it merely populates the ReviewIds with the ids.
    /// </summary>
    /// <param name="custDto"></param>
    /// <param name="loadChildren"></param>
    /// <returns></returns>
    public static Customer Create(CustomerDto custDto, bool loadChildren = true)
    {
      if (custDto == null)
        throw new ArgumentNullException("dto");

      Customer newCust = new Customer();
      newCust.LoadFromDto(custDto, loadChildren);
      newCust.MarkThisDirty();
      newCust.Update();
      return newCust;
    }
    public static Customer Create(CustomerDto custDto, params ReviewDto[] reviewDtos)
    {
      if (custDto == null)
        throw new ArgumentNullException("dto");

      //NEW UP AND LOAD CUSTOMER
      Customer newCust = new Customer();

      //CREATE AND ADD CHILD REVIEWS
      foreach (var reviewDto in reviewDtos)
      {
        var review = Review.Create(reviewDto);
        newCust.AddChild(review);
        review.MarkThisDirty();
      }

      //LOAD CUSTOMER
      newCust.LoadFromDto(custDto, true);
      newCust.MarkThisDirty();
      newCust.Update();

      return newCust;
    }
    public static Customer GetCustomer(Guid id)
    {
      var customer = new Customer();
      var custDto = DalManager.CustomerDal.Get(id);
      customer.LoadFromDto(custDto);
      customer.MarkThisClean();
      return customer;
    }
    public static IEnumerable<Customer> GetAll()
    {
      var allDtos = DalManager.CustomerDal.GetAll();
      var allCustomers = from dto in allDtos
                         select Customer.Create(dto);
      return allCustomers;
    }
    
    #endregion

    #region BusinessBase Overrides

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
    protected override void LoadFromDtoImpl(CustomerDto dto, bool loadChildren)
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

        if (loadChildren)
        {
          //POPULATE CHILDREN REVIEWS
          //If we were concerned about over-the-wire performance, we'd have to come up 
          //with something more clever like Review.GetReviews(ReviewIds).
          Children.Clear();
          if (ReviewIds.Count > 0)
          {
            foreach (var reviewId in ReviewIds)
            {
              var review = Review.GetReview(reviewId);
              review.Parent = this;
              AddChild(review);
            }
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
    /// <summary>
    /// If the child is of type Review, then this will add its id to the ReviewIds list
    /// if it is not already there.
    /// </summary>
    /// <param name="child">child to add</param>
    public override void AddChild(Interfaces.IHaveHeirarchy child)
    {
      var review = child as Review;
      if (review != null && !ReviewIds.Contains(review.Id))
        ReviewIds.Add(review.Id);

      base.AddChild(child);
    }
    /// <summary>
    /// Checks to see if Children objects correspond to ids in ReviewIds
    /// </summary>
    /// <returns>Children ids correspond to ReviewIds -> true.  Children ids do not correspond to ReviewIds -> false.  ReviewIds.Count -> null</returns>
    protected override bool? GetChildrenAreLoaded()
    {
      if (ReviewIds.Count == 0)
        return null;

      //IF WE HAVE CHILDREN, CHECK EACH ReviewIds ID HAS CORRESPONDING CHILD OBJECT
      foreach (var id in ReviewIds)
      {
        var results = from child in Children
                      where child is Review && child.Id == id
                      select child;

        if (results.Count() == 0)
          return false;
      }

      return true;
    }
    /// <summary>
    /// Uses the ids in ReviewIds to load the Review instances into the list of children.
    /// </summary>
    public override void LoadChildren()
    {
      Children.Clear();
      foreach (var id in ReviewIds)
      {
        var review = Review.GetReview(id);
        review.Parent = this;
        AddChild(review);
      }
    }
    #endregion
  }
}
