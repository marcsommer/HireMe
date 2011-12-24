using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HireMe.Business.Interfaces
{
  public interface IBusinessBase<T, TDto> : IHaveId,
                                            IDirtyable,
                                            IDeleteable,
                                            IHaveDto<TDto>,
                                            IUpdateable<T, TDto>,
                                            ICommitable<T, TDto>
  {
  }
}
