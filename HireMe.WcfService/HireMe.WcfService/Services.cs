using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace HireMe.WcfService
{
  public static class Services
  {
    public static void Initialize(CompositionContainer container)
    {
      Container = container;
    }

    public static CompositionContainer Container { get; private set; }
  }
}
