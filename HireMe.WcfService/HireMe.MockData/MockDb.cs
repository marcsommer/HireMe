using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Odbc;

namespace HireMe.MockData
{
  public static class MockDb
  {
    static MockDb()
    {
      Customers = new List<CustomerData>()
      {
        new CustomerData() { CustomerId = CustId1, 
                             Name="Bob Robertsonbobbington", 
                             EmailAddress=@"goodolebobby123@gmail.com" },

        new CustomerData() { CustomerId = CustId2,
                             Name="Mary Merryweatherby", 
                             EmailAddress=@"sunshineandbutterflies12345@hotmail.com" }
      };

      Reviews = new List<ReviewData>()
      {
        new ReviewData() { ReviewId = ReviewId1,
                           Rating = 5,
                           Comments = "I think Verizon is probably the best company EVER!!!!",
                           CustomerId = CustId1 },
                           
        new ReviewData() { ReviewId = ReviewId2,
                           Rating = 5,
                           Comments = "Verizon RULES!@#%!@#%!@#%!@#%!@@#%!!",
                           CustomerId = CustId2 },
                           
        new ReviewData() { ReviewId = ReviewId3,
                           Rating = 5,
                           Comments = "I LOVE Verizon SO MUCH, I voted TWICE!!!!!????!!!!!??!!!??!?!?!??!?!?",
                           CustomerId = CustId2 },
      };
    }

    public static Guid CustId1 = Guid.Parse("B408A9B6-A411-4730-928C-9E02516E121A");
    public static Guid CustId2 = Guid.Parse("739BC285-4B29-4CAF-8785-5768CE13B5D6");
    public static Guid ReviewId1 = Guid.Parse("2CB9E320-B202-4E21-9207-E974DEAF75F5");
    public static Guid ReviewId2 = Guid.Parse("EFAD1278-16F4-4107-9228-580716022797");
    public static Guid ReviewId3 = Guid.Parse("A201F967-659B-45C1-91FA-306F165E3511");

    public static List<CustomerData> Customers { get; set; }
    public static List<ReviewData> Reviews { get; set; }
  }
}
