using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using Caliburn.Micro;
using HireMe.Business;
using HireMe.DataAccess;
using HireMe.WcfClient;

//THIS CODE IS BASED ON THE CODE MADE BY ROB EISENBERG AT http://caliburnmicro.codeplex.com/discussions/218561?ProjectName=caliburnmicro

namespace HireMe.Wpf
{
  public class MefBootstrapper : Bootstrapper<ViewModels.ShellViewModel>
  {
    private CompositionContainer _Container;

    /// <summary>
    /// Configures container.  Initializes Services
    /// </summary>
    protected override void Configure()
    {
      //NEW UP CONTAINER
      _Container = new CompositionContainer();

      //CREATE ASSEMBLY CATALOGS FOR COMPOSITION OF APPLICATION (WPF)
      AssemblyCatalog catThis = new AssemblyCatalog(typeof(MefBootstrapper).Assembly);
      AssemblyCatalog catBusiness = new AssemblyCatalog(typeof(Customer).Assembly);
      AssemblyCatalog catDal = new AssemblyCatalog(typeof(DalManager).Assembly);
      AssemblyCatalog catWcfClient = new AssemblyCatalog(typeof(CustomerDalProxy).Assembly);
      AggregateCatalog catAll = new AggregateCatalog(catThis, catBusiness, catDal, catWcfClient);
      _Container = new CompositionContainer(catAll);

      //ADD BATCH FOR SERVICES TO CONTAINER, INCLUDING CONTAINER ITSELF
      var batch = new CompositionBatch();
      batch.AddExportedValue<IWindowManager>(new WindowManager());
      batch.AddExportedValue<IEventAggregator>(new EventAggregator());
      batch.AddExportedValue(_Container);
      _Container.Compose(batch);

      //INITIALIZE SERVICES
      Services.Initialize(_Container);
    }

    protected override object GetInstance(Type serviceType, string key)
    {
      string contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(serviceType) : key;
      var exports = _Container.GetExportedValues<object>(contract);

      if (exports.Count() > 0)
        return exports.First();

      throw new Exception(string.Format("Could not locate any instances of contract {0}.", contract));
    }

    protected override IEnumerable<object> GetAllInstances(Type serviceType)
    {
      return _Container.GetExportedValues<object>(AttributedModelServices.GetContractName(serviceType));
    }

    protected override void BuildUp(object instance)
    {
      _Container.SatisfyImportsOnce(instance);
    }
  }
}
