using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Odbc;

namespace HireMe.MockOdbc
{
  public static class MockDb
  {
    static MockDb()
    {
      Customers = new List<CustomerData>()
      {
        new CustomerData() { CustomerId = Guid.Parse("B408A9B6-A411-4730-928C-9E02516E121A"), 
                             Name="Bob Roberts", EmailAddress=@"goodolebobby123@gmail.com"  },

        new CustomerData() { CustomerId = Guid.Parse("739BC285-4B29-4CAF-8785-5768CE13B5D6"), 
                             Name="Mary Merryweatherby", EmailAddress=@"sunshineandbutterflies12345@hotmail.com"  }
      };
    }

    public static List<CustomerData> Customers { get; set; }

    //public void foo()
    //{
    //  System.Data.Odbc.
    //}
  }
}
