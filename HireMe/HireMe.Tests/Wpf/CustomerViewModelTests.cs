using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using HireMe.Wpf.ViewModels;
using HireMe.Business;
using System.Threading;

namespace HireMe.Tests
{
  [TestFixture]
  public class CustomerViewModelTests
  {
    [Test]
    public void CUSTOMER_NAME_CHANGE_PROPAGATES_PROPERTY_CHANGED_TO_VIEWMODEL()
    {
      bool isPropagatedSuccessfully = false;
      var vm = new CustomerViewModel();
      vm.Customer = Customer.CreateNew();
      vm.PropertyChanged += (s, e) =>
      {
        isPropagatedSuccessfully = true;
      };
      
      //Change Customer's property that should trigger above handler.

      vm.Customer.Name = "newnameheredudeq32451";

      Assert.IsTrue(isPropagatedSuccessfully);
    }
  }
}
