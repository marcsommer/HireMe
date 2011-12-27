using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using Caliburn.Micro;
using HireMe.Business;

namespace HireMe.Wpf.ViewModels
{
  [Export(typeof(CustomerViewModel))]
  [PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.NonShared)]
  public class CustomerViewModel : Conductor<IScreen>.Collection.AllActive
  {
    //CustomerViewModel.Items contains Customer.Review instances.

    private Customer _Customer;
    public Customer Customer
    {
      get { return _Customer; }
      set
      {
        if (value != _Customer)
        {
          if (_Customer != null)
            _Customer.PropertyChanged -= Customer_PropertyChanged; //unhook
          _Customer = value;
          NotifyOfPropertyChange(() => Customer);
          _Customer.PropertyChanged += Customer_PropertyChanged;
        }
      }
    }

    private void Customer_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
      NotifyOfPropertyChange(() => Customer);
    }
  }
}
