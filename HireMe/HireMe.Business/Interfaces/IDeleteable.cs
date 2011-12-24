using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HireMe.Business.Interfaces
{
  public interface IDeleteable : IHaveHeirarchy
  {
    bool IsMarkedForDeletion { get; }
    bool DeleteStarted { get; }

    void Delete(bool deleteChildren);
    void DeleteImmediately(bool deleteChildrenImmediately);
  }
}
