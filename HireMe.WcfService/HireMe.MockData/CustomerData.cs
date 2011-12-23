using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HireMe.MockData
{
  /// <summary>
  /// Represents a customer entry in the mock database.
  /// Data class mirrors CustomerDto in HireMe.DataAccess.
  /// </summary>
  public class CustomerData
  {
    public Guid CustomerId { get; set; }
    public string Name { get; set; }
    public string EmailAddress { get; set; }
    public List<Guid> ReviewIds { get; set; }
  }
}
