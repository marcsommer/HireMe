using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace HireMe.DataAccess
{
  /// <summary>
  /// Manager class that will take care of handling connections to individual 
  /// Dal implementations.
  /// </summary>
  public static class DalManager
  {
    
    public static void Initialize(CompositionContainer container)
    {
      Container = container;
      CustomerDal = Container.GetExportedValue<ICustomerDal>();
    }
    public static CompositionContainer Container { get; set; }

    public static ICustomerDal CustomerDal { get; private set; }
  }
}
