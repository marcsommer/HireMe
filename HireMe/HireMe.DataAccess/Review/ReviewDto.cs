using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HireMe.DataAccess
{
  [Serializable]
  public class ReviewDto
  {
    public Guid Id { get; set;}
    public int Rating { get; set; }
    public string Comments { get; set; }
    public Guid CustomerId { get; set; }
  }
}
