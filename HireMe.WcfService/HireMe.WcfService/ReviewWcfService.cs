using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HireMe.DataAccess;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.ServiceModel;

namespace HireMe.WcfService
{
  public class ReviewWcfService : IReviewDal
  {
    public ReviewWcfService()
    {
      Services.Container.SatisfyImportsOnce(this);
    }

    [Import]
    public IReviewDal ReviewDalImpl { get; set; }

    public ReviewDto Create()
    {
      return ReviewDalImpl.Create();
    }
    public void Delete(Guid id)
    {
      ReviewDalImpl.Delete(id);
    }
    public ReviewDto Get(Guid id)
    {
      return ReviewDalImpl.Get(id);
    }
    public IList<ReviewDto> GetAll()
    {
      return ReviewDalImpl.GetAll();
    }
    public ReviewDto Update(ReviewDto dto)
    {
      return ReviewDalImpl.Update(dto);
    }
  }
}
