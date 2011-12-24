using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HireMe.Tests.Server
{
  public interface IDataAdapterTests
  {
    void CREATE_NEW_DTO();
    void GET_DTO();
    void GET_ALL_DTOS();
    void UPDATE_DTO();
    void DELETE_ID_EXPECT_TYPEDATAEXCEPTION();
  }
}
