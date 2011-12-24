using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HireMe.Business.Interfaces
{
  public interface IHaveDto<TDto>
  {
    void LoadFromDto(TDto dto);
    TDto ToDto();
  }
}
