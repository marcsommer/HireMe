using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HireMe.Business.Interfaces
{
  public interface IHaveHeirarchy
  {
    IHaveHeirarchy Parent { get; }
    void SetParent(IHaveHeirarchy parent);
    void AddChild(IHaveHeirarchy child);
    IList<IHaveHeirarchy> Children { get; }
    bool HasChildren { get; }
  }
}
