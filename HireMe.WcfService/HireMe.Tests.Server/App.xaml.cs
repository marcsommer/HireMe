using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using HireMe.DataAccess.OdbcProvider;
using HireMe.Tests.Server.DataAccess.OdbcProvider;
using HireMe.Tests.Server.DataAccess.MockDataProvider;
using HireMe.DataAccess;

namespace HireMe.Tests.Server
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    protected override void OnStartup(StartupEventArgs e)
    {
      MockCustomerDataAdapterTests mockTests = new MockCustomerDataAdapterTests();
      mockTests.CREATE_NEW_DTO();

      OdbcCustomerDataAdapterTests odbcTests = new OdbcCustomerDataAdapterTests();
      odbcTests.CREATE_NEW_DTO();
      odbcTests.GET_DTO();
      odbcTests.GET_ALL_DTOS();
      odbcTests.UPDATE_DTO();
      try
      {
        odbcTests.DELETE_ID_EXPECT_TYPEDATAEXCEPTION();
      }
      catch (CustomerDataException cde)
      {
        //expected exception
      }


      base.OnStartup(e);
    }
  }
}
