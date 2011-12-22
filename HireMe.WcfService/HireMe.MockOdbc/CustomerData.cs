using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HireMe.MockOdbc
{
  /// <summary>
  /// Represents a customer entry in the mock database.
  /// Data class corresponds to CustomerDto in HireMe.DataAccess (tightly coupled).
  /// </summary>
  public class CustomerData
  {
    public Guid CustomerId { get; set; }
    public string Name { get; set; }
    public string EmailAddress { get; set; }
    public List<Guid> ReviewIds { get; set; }
  }
}
