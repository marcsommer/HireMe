using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HireMe.DataAccess;
using HireMe.WcfClient.WcfServices.Customer;
using System.ComponentModel.Composition;

namespace HireMe.WcfClient
{
  [Export(typeof(HireMe.DataAccess.ICustomerDal))]
  public class CustomerDalProxy : HireMe.DataAccess.ICustomerDal
  {
    public CustomerDalProxy()
    {
      _DalClient = new CustomerDalClient();
    }

    private CustomerDalClient _DalClient { get; set; }

    public CustomerDto Create()
    {
      return _DalClient.Create();
    }

    public CustomerDto Get(Guid id)
    {
      return _DalClient.Get(id);
    }

    public IList<CustomerDto> GetAll()
    {
      return _DalClient.GetAll();
    }

    public CustomerDto Update(CustomerDto dto)
    {
      return _DalClient.Update(dto);
    }

    public void Delete(Guid id)
    {
      _DalClient.Delete(id);
    }
  }
}
