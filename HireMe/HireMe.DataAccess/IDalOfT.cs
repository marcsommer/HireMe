using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HireMe.DataAccess
{
  public interface IDal<T> 
  {
    T Create();
    T Get(Guid id);
    IList<T> GetAll();
    T Update(Guid id);
    void Delete(Guid id);
  }
}
