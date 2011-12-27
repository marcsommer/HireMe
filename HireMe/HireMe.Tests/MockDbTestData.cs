using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HireMe.Tests
{
  /// <summary>
  /// this is the same data as in MockDb in Server solution.
  /// </summary>
  public static class MockDbTestData
  {
    //TEST CUSTOMER 1 (1 REVIEW)
    public static Guid CustId1 = Guid.Parse("B408A9B6-A411-4730-928C-9E02516E121A");
    public static string CustName1 = "Bob Robertsonbobbington";
    public static string CustEmail1 = @"goodolebobby123@gmail.com";
    public static Guid ReviewId1 = Guid.Parse("2CB9E320-B202-4E21-9207-E974DEAF75F5");
    public static int ReviewRating1 = 5;
    public static string ReviewComments1 = "I think Verizon is probably the best company EVER!!!!";

    //TEST CUSTOMER 2 (2 REVIEWS)
    public static Guid CustId2 = Guid.Parse("739BC285-4B29-4CAF-8785-5768CE13B5D6");
    public static string CustName2 = "Mary Merryweatherby";
    public static string CustEmail2 = @"sunshineandbutterflies12345@hotmail.com";

    public static Guid ReviewId2A = Guid.Parse("EFAD1278-16F4-4107-9228-580716022797");
    public static int ReviewRating2A = 5;
    public static string ReviewComments2A = "Verizon RULES!@#%!@#%!@#%!@#%!@@#%!!";

    public static Guid ReviewId2B = Guid.Parse("A201F967-659B-45C1-91FA-306F165E3511");
    public static int ReviewRating2B = 5;
    public static string ReviewComments2B = "I LOVE Verizon SO MUCH, I voted TWICE!!!!!????!!!!!??!!!??!?!?!??!?!?";

  }
}
