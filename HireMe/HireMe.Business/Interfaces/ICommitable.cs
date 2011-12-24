using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HireMe.Business.Interfaces
{
  public interface ICommitable<T, TDto> : IHaveHeirarchy
  {
    BusinessBase<T, TDto> Commit(bool commitChildren);
  }
}
