using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HireMe.MockData
{
  /// <summary>
  /// Represents a review entry in the mock database.
  /// Data class mirrors ReviewDto in HireMe.DataAccess.
  /// </summary>
  [Serializable]
  public class ReviewData
  {
    public Guid ReviewId { get; set; }
    public int Rating { get; set; }
    public string Comments { get; set; }
    public Guid CustomerId { get; set; }
  }
}
