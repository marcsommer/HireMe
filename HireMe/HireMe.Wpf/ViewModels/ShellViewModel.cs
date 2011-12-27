using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using HireMe.Business;
using Caliburn.Micro;

namespace HireMe.Wpf.ViewModels
{
  [Export(typeof(ShellViewModel))]
  [PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.Shared)]
  public class ShellViewModel : Conductor<IScreen>.Collection.AllActive
  {
    public ShellViewModel()
    {
      IEnumerable<Customer> allCustomers = Customer.GetAll();
      foreach (var cust in allCustomers)
      {
        var vm = Services.Container.GetExportedValue<CustomerViewModel>();
        vm.Customer = cust;
        vm.LoadReviews();
        Items.Add(vm);
      }
      //var results = from c in allCustomers
      //               select new CustomerViewModel() { Customer = c };
      //Items.AddRange(results);
    }
  }
}
