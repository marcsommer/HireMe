using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using HireMe.DataAccess;

namespace HireMe.MockData
{
  [Export(typeof(ICustomerDal))]
  public class MockDataAdapter : ICustomerDal
  {
    public CustomerDto Create()
    {
      throw new NotImplementedException();
    }

    public void Delete(Guid id)
    {
      throw new NotImplementedException();
    }

    public CustomerDto Get(Guid id)
    {
      throw new NotImplementedException();
    }

    public IList<CustomerDto> GetAll()
    {
      throw new NotImplementedException();
    }

    public CustomerDto Update(CustomerDto dto)
    {
      throw new NotImplementedException();
    }
  }
}
