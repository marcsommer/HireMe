using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using HireMe.DataAccess;
using HireMe.MockData;

namespace HireMe.DataAccess.MockDataProvider
{
  [Export(typeof(IReviewDal))]
  public class MockReviewDataAdapter : IReviewDal
  {
    public ReviewDto Create()
    {
      var data = new ReviewData();
      data.ReviewId = Guid.NewGuid();
      MockDb.Reviews.Add(data);
      return CreateDtoFromData(data);
    }
    public void Delete(Guid id)
    {
      MockDb.Reviews.Remove(GetReview(id));
    }
    public ReviewDto Get(Guid id)
    {
      var reviewData = GetReview(id);
      return CreateDtoFromData(reviewData);
    }
    public IList<ReviewDto> GetAll()
    {
      var results = from revie in MockDb.Reviews
                    select CreateDtoFromData(revie);
      return results.ToList();
    }
    public ReviewDto Update(ReviewDto dto)
    {
      ReviewData data = null;
      var results = from r in MockDb.Reviews
                    where r.ReviewId == dto.Id
                    select r;
      if (results.Count() == 1)
      {
        data = results.First();
        MockDb.Reviews.Remove(data);
      }
      else
        data = new ReviewData();
      data.ReviewId = dto.Id;
      data.Rating = dto.Rating;
      data.Comments = dto.Comments;
      data.CustomerId = dto.CustomerId;

      MockDb.Reviews.Add(data);
      return dto;
    }

    public static ReviewDto CreateDtoFromData(ReviewData data)
    {
      return new ReviewDto()
      {
        Id = data.ReviewId,
        Rating = data.Rating,
        Comments = data.Comments,
        CustomerId = data.CustomerId
      };
    }
    private ReviewData GetReview(Guid id)
    {
      
      var results = new List<ReviewData>();
      foreach (var data in MockDb.Reviews)
      {
        if (data.ReviewId == id)
          results.Add(data);
      }
      if (results.Count != 1)
        throw new ReviewDataException();

      return results[0];
    }
  }

}

