using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HireMe.DataAccess
{
  public interface IDal<TDto> 
  {
    TDto Create();
    TDto Get(Guid id);
    IList<TDto> GetAll();
    TDto Update(TDto dto);
    void Delete(Guid id);
  }
}
