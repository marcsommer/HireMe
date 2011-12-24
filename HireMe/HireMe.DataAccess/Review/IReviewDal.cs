using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace HireMe.DataAccess
{
  [ServiceContract]
  public interface IReviewDal : IDal<ReviewDto>
  {
    [OperationContract]
    new ReviewDto Create();

    [OperationContract]
    new ReviewDto Get(Guid id);

    [OperationContract]
    new IList<ReviewDto> GetAll();

    [OperationContract]
    new ReviewDto Update(ReviewDto dto);

    [OperationContract]
    new void Delete(Guid id);
  }
}
