using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HireMe.Business.Interfaces
{
  public interface IUpdateable<T,Dto> : IHaveHeirarchy
  {
    BusinessBase<T, Dto> Update(bool updateChildren);
    bool UpdateStarted { get; }
    void UpdateChildren();
  }
}
