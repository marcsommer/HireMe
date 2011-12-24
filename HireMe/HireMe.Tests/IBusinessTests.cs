using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HireMe.Tests
{
  public interface IBusinessTests
  {
    void CREATE();
    void GET();
    void UPDATE();
    void DELETE_IMMEDIATELY();
    void COMMIT();
    void TO_DTO();
    void LOAD_FROM_DTO();
  }
}
