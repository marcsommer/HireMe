using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using HireMe.Tests.Business;
using HireMe.Tests.WcfClient;

namespace HireMe.Tests
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    protected override void OnStartup(StartupEventArgs e)
    {
      HackCustomTests tests = new HackCustomTests(); //workaround testing starts in ctor

      base.OnStartup(e);
    }

    
  }
}
