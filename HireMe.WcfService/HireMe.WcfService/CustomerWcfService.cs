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
  public class CustomerWcfService : ICustomerDal
  {
    public CustomerWcfService()
    {
      Services.Container.SatisfyImportsOnce(this);
    }

    [Import]
    public ICustomerDal CustomerDalImpl { get; set; }

    public CustomerDto Create()
    {
      return CustomerDalImpl.Create();
    }
    public void Delete(Guid id)
    {
      CustomerDalImpl.Delete(id);
    }
    public CustomerDto Get(Guid id)
    {
      throw new NotImplementedException();//debug
      return CustomerDalImpl.Get(id);
    }
    public IList<CustomerDto> GetAll()
    {
      return CustomerDalImpl.GetAll();
    }
    public CustomerDto Update(CustomerDto dto)
    {
      return CustomerDalImpl.Update(dto);
    }
  }
}
