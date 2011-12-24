using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HireMe.Business.Interfaces
{
  public interface IDirtyable : IHaveHeirarchy
  {
    bool IsDirty { get; }
    bool ThisIsDirty { get; }
    void MarkThisDirty();
    void MarkThisClean();
  }
}
