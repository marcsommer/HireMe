using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HireMe.Tests.Server
{
  public interface IDataAdapterTests
  {
    void CREATE_NEW_CUSTOMER_DTO();
    void GET_CUSTOMER_DTO();
    void GET_ALL_CUSTOMER_DTOS();
    void UPDATE_CUSTOMER_DTO();
    void DELETE_CUSTOMER_EXPECT_CUSTOMERDATAEXCEPTION();
  }
}
