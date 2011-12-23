using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using HireMe.DataAccess.OdbcProvider;
using HireMe.Tests.Server.DataAccess.OdbcProvider;

namespace HireMe.Tests.Server
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    protected override void OnStartup(StartupEventArgs e)
    {
      CustomerDataProviderTest test = new CustomerDataProviderTest();
      test.CREATE_NEW_CUSTOMER_DTO();
      test.GET_CUSTOMER_DTO();
      test.GET_ALL_CUSTOMER_DTOS();


      base.OnStartup(e);
    }
  }
}
