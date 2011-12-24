using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HireMe.DataAccess;

namespace HireMe.Business
{
  /// <summary>
  /// Review business class.  Includes Id, Name, EmailAddress, and ReviewIds.
  /// Use static factory methods to CreateNew and Get(retrieve) review.
  /// Also implements abstract BusinessBase methods for Update/Delete,
  /// as well as loading from a DTO.
  /// </summary>
  public class Review : BusinessBase<Review, ReviewDto>
  {
    #region Properties

    //public Guid ReviewId { get; set; }
    //public int Rating { get; set; }
    //public string Comments { get; set; }
    //public Guid CustomerId { get; set; }

    #region public int Rating

    /// <summary>
    /// Integer representing a customer's review
    /// </summary>
    public int Rating
    {
      get { return GetRating(); }
      set { SetRating(value); }
    }

    protected int _Rating;
    protected virtual int GetRating()
    {
      return _Rating;
    }
    protected virtual void SetRating(int value)
    {
      if (value != _Rating)
      {
        _Rating = value;
        if (!IsLoadingDto)
          MarkThisDirty();
      }
    }

    #endregion    //Rating

    #region public string Comments

    /// <summary>
    /// Comments filled out by a customer in a review
    /// </summary>
    public string Comments
    {
      get { return GetComments(); }
      set { SetComments(value); }
    }

    protected string _Comments;
    protected virtual string GetComments()
    {
      return _Comments;
    }
    protected virtual void SetComments(string value)
    {
      if (value != _Comments)
      {
        _Comments = value;
        if (!IsLoadingDto)
          MarkThisDirty();
      }
    }

    #endregion    //Comments

    #region public Guid CustomerId

    /// <summary>
    /// Id of Customer Object (foreign key) who made this review.
    /// </summary>
    public Guid CustomerId
    {
      get { return GetCustomerId(); }
      set { SetCustomerId(value); }
    }

    protected Guid _CustomerId;
    protected virtual Guid GetCustomerId()
    {
      return _CustomerId;
    }
    protected virtual void SetCustomerId(Guid value)
    {
      if (value != _CustomerId)
      {
        _CustomerId = value;
        if (!IsLoadingDto)
          MarkThisDirty();
      }
    }

    #endregion    //CustomerId

    #endregion

    #region Static Factory Methods (CreateNew/Get)

    public static Review CreateNew()
    {
      var review = new Review();
      review.LoadFromDto(DalManager.ReviewDal.Create());
      review.MarkThisDirty();
      return review;
    }
    public static Review GetReview(Guid id)
    {
      var review = new Review();
      review.LoadFromDto(DalManager.ReviewDal.Get(id));
      review.MarkThisClean();
      return review;
    }

    #endregion

    /// <summary>
    /// Updates the Review object.  No need to check for IsDirty,
    /// that is handled by the BusinessBase class.
    /// </summary>
    protected override void UpdateImpl()
    {
      //Update DB from object, return dto may contain new id
      var dto = DalManager.ReviewDal.Update(ToDto());
      SetIdBackingField(dto.Id);
      MarkThisClean();
    }
    /// <summary>
    /// Deletes this object from the DB.
    /// </summary>
    protected override void DeleteImpl()
    {
      DalManager.ReviewDal.Delete(Id);
      MarkThisDirty();
    }
    /// <summary>
    /// Implements the load from a Dto
    /// </summary>
    /// <param name="dto">ReviewDto with state to load</param>
    protected override void LoadFromDtoImpl(ReviewDto dto)
    {
      Id = dto.Id;
      Rating = dto.Rating;
      Comments = dto.Comments;
      CustomerId = dto.CustomerId;
      Parent = Customer.GetCustomer(dto.CustomerId);
    }
    /// <summary>
    /// Creates ReviewDto from Review instance values
    /// </summary>
    /// <returns>Newly created ReviewDto</returns>
    public override ReviewDto ToDto()
    {
      return new ReviewDto() { Id = this.Id,
                               Rating = this.Rating,
                               Comments = this.Comments,
                               CustomerId = this.CustomerId };
    }
  }
}
