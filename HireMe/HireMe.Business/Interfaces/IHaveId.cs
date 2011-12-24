using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HireMe.Business.Interfaces
{
  public interface IHaveId
  {
    Guid Id { get; }
  }
}
