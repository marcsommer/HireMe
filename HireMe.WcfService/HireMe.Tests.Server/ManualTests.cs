using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HireMe.Tests.Server
{
  public class ManualTests
  {
    internal void RunTests()
    {
      RunDataAdapterTests(new MockCustomerDataAdapterTests());
      RunDataAdapterTests(new MockReviewDataAdapterTests());

      //OdbcProvider
      //WcfService
    }

    private void RunDataAdapterTests(IDataAdapterTests tests)
    {
      tests.SetupTests();
      tests.CREATE_NEW_DTO();
      tests.SetupTests();
      tests.GET_DTO();
      tests.SetupTests();
      tests.GET_ALL_DTOS();
      try
      {
        tests.SetupTests();
        tests.DELETE_ID_EXPECT_TYPEDATAEXCEPTION();
      }
      catch (DataAccess.DataException de)
      {
        //expected
      }
      tests.SetupTests();
      tests.UPDATE_DTO();
    }
  }
}
