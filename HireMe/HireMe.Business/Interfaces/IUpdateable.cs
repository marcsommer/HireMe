using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HireMe.Business.Interfaces
{
  public interface IUpdateable : IHaveHeirarchy
  {
    IBusinessBase Update(bool updateChildren);
    bool UpdateStarted { get; }
    void UpdateChildren();
  }
}
