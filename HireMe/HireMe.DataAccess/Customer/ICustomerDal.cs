using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace HireMe.DataAccess
{
  [ServiceContract]
  public interface ICustomerDal : IDal<CustomerDto>
  {
    [OperationContract]
    new CustomerDto Create();

    [OperationContract]
    new CustomerDto Get(Guid id);

    [OperationContract]
    new IList<CustomerDto> GetAll();

    [OperationContract]
    new CustomerDto Update(CustomerDto dto);

    [OperationContract]
    new void Delete(Guid id);
  }
}
