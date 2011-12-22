using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HireMe.DataAccess
{
  [Serializable]
  public class CustomerDto
  {
    public CustomerDto()
    {
      ReviewIds = new List<Guid>();
    }

    public Guid CustomerId { get; set; }
    public string Name { get; set; }
    public string EmailAddress { get; set; }
    public List<Guid> ReviewIds { get; set; }
  }
}
