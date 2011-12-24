using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HireMe.DataAccess;
using HireMe.WcfClient.WcfServices.Review;
using System.ComponentModel.Composition;

namespace HireMe.WcfClient
{
  [Export(typeof(HireMe.DataAccess.IReviewDal))]
  public class ReviewDalProxy : HireMe.DataAccess.IReviewDal
  {
    public ReviewDalProxy()
    {
      _DalClient = new ReviewDalClient();
    }
    private ReviewDalClient _DalClient { get; set; }

    public ReviewDto Create()
    {
      return _DalClient.Create();
    }
    public ReviewDto Get(Guid id)
    {
      return _DalClient.Get(id);
    }
    public IList<ReviewDto> GetAll()
    {
      return _DalClient.GetAll();
    }
    public ReviewDto Update(ReviewDto dto)
    {
      return _DalClient.Update(dto);
    }
    public void Delete(Guid id)
    {
      _DalClient.Delete(id);
    }
  }
}
