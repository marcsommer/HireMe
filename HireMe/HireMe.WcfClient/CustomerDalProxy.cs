﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HireMe.DataAccess;

namespace HireMe.WcfClient
{
  public class CustomerDalProxy : ICustomerDal
  {
    public CustomerDto Create()
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

    public CustomerDto Update(Guid id)
    {
      throw new NotImplementedException();
    }

    public void Delete(Guid id)
    {
      throw new NotImplementedException();
    }
  }
}