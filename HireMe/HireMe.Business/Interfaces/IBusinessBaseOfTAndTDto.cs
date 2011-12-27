using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using Caliburn.Micro;

namespace HireMe.Business.Interfaces
{
  public interface IBusinessBase<T, TDto> : IBusinessBase,
                                            IHaveId,
                                            IDirtyable,
                                            IDeleteable,
                                            IHaveDto<TDto>,
                                            IUpdateable,
                                            ICommitable<T, TDto>
  {
  }
}
