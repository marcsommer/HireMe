using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HireMe.Business;

namespace HireMe.Wpf.ViewModels
{
  public class ReviewViewModel : ViewModelBase
  {
    private Review _Review;
    public Review Review
    {
      get { return _Review; }
      set
      {
        if (value != _Review)
        {
          if (_Review != null)
            _Review.PropertyChanged -= Review_PropertyChanged; //unhook
          _Review = value;
          NotifyOfPropertyChange(() => Review);
          _Review.PropertyChanged += Review_PropertyChanged;
        }
      }
    }

    private void Review_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
      NotifyOfPropertyChange(() => Review);
    }
  }
}
