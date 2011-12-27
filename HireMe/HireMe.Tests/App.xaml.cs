using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace HireMe.Tests
{
  /// <summary>
  /// Interaction logic for App.xaml
  /// </summary>
  public partial class App : Application
  {
    protected override void OnStartup(StartupEventArgs e)
    {
      ManualTests tests = new ManualTests(); //workaround testing starts in ctor

      base.OnStartup(e);
    }

    
  }
}
