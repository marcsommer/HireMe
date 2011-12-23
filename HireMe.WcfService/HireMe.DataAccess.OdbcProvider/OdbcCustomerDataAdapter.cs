using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Data.Odbc;

namespace HireMe.DataAccess.OdbcProvider
{
  /// <summary>
  /// Implements a data access layer that talks to an odbc data source.
  /// Conventions: Throws exceptions if not found
  /// </summary>
  [Export(typeof(ICustomerDal))]
  public class OdbcCustomerDataAdapter : ICustomerDal
  {
    public CustomerDto Create()
    {
      //CREATE THE DTO
      CustomerDto dto = new CustomerDto() { CustomerId = Guid.NewGuid(), Name = Properties.Resources.DefaultCustomerName };

      //INSERT INTO THE DB
      var connStr = Properties.Resources.ConnectionString;
      #region Odbc Connection Learning Process/NOTES
      //connStr = @"Data Source=.\SQLEXPRESS;Initial Catalog=HireMe;User Id=;Password=;";  //can't find datasource
      //connStr = @"Driver={SQL Server};Server=.\SQLEXPRESS;Database=HireMe;Uid=User;Pwd=;"; //better, now login failed 
      //connStr = @"Driver={SQL Server};Server=.\SQLEXPRESS;Database=HireMe;Uid=User;";//try without pwd=.....
      //connStr = @"Driver={SQL Server};Server=.\SQLEXPRESS;Database=HireMe;Trusted_Connection=yes;";//nope...try trusted connection.
      //connStr = @"Driver={SQL Server};Server=.\SQLEXPRESS;Database=HireMe;Trusted_Connection=yes;";//nope...try trusted connection.
      //connStr = @"Data Source=C:\Users\User\Documents\Visual Studio 2010\Projects\HireMe\HireMe.WcfService\HireMe.DataAccess.OdbcProvider\HireMe.sdf";
      //connStr = @"Driver={SQL Server};Server=.\SQLEXPRESS;Data Source=.\SQLEXPRESS;AttachDbFilename=""C:\Users\User\Documents\Visual Studio 2010\Projects\HireMe\HireMe.WcfService\HireMe.DataAccess.OdbcProvider\DataSource\HireMeDatabase.mdf"";Integrated Security=True;User Instance=True";
      //connStr = @"Driver={SQL Server};Server=.\SQLEXPRESS;AttachDbFilename=""C:\Users\User\Documents\Visual Studio 2010\Projects\HireMe\HireMe.WcfService\HireMe.DataAccess.OdbcProvider\DataSource\HireMeDatabase.mdf"";Integrated Security=True;User Instance=True";
      //connStr = @"Driver={SQL Server};Server=.\SQLEXPRESS;Database=HireMeDatabase;C:\Users\User\Documents\Visual Studio 2010\Projects\HireMe\HireMe.WcfService\HireMe.DataAccess.OdbcProvider\DataSource\HireMeDatabase.mdf"";Integrated Security=True;User Instance=True";
      //connStr = @"Driver={SQL Server};Server=.\SQLEXPRESS;Database=HireMeDatabase.mdf;Integrated Security=True;User Instance=True";
      //connStr = @"FILEDSN=C:\Users\User\Documents\Visual Studio 2010\Projects\HireMe\HireMe.WcfService\HireMe.DataAccess.OdbcProvider\DataSource\HireMeOdbc.dsn;";
      //connStr = @"DSN=HireMeOdbc; Trusted_Connection=yes;Database=HireMe;AttachDbFilename=""C:\HireMe\HireMe.DataSource\HireMe.DataSource\HireMe.mdf""";
      //connStr = @"DSN=HireMeOdbc; Trusted_Connection=yes;"; //does work, so good enough!
      //connStr = @"FILEDSN=C:\HireMe\OdbcDataSource\HireMeOdbc.dsn; Trusted_Connection=yes;";
      //The lessons here are:
      //The AttachDbFilename has to be in a location where the sql server can access it.
      //TODO: Need to test if you can copy mdf file to other location than where it was created.  It seems like this should be possible.
      //Trusted_Connection=yes indicates no user/pass
      //Driver,Server,Database can be included in the DSN config.
      //In DSN config, you must set both the Name to the database name, and the path to the attached file (where it can be accessed).
      //If you even come close to modifying the mdf file, you will have to reconfig the dsn file.
      #endregion
      using (OdbcConnection connection = new OdbcConnection(connStr))
      {
        connection.Open();
        string queryStr = string.Format(@"INSERT INTO {0} " +
                                        @"({1}, {2}, {3}) " +
                                        @"VALUES('{4}','{5}', '{6}');",
                                        Properties.Resources.CustomerTable,
                                        Properties.Resources.CustomerIdColumn, Properties.Resources.NameColumn, Properties.Resources.EmailAddressColumn,
                                        dto.CustomerId, dto.Name, dto.EmailAddress);
        var cmd = connection.CreateCommand();
        cmd.CommandText = queryStr;
        var numRowsAffected = cmd.ExecuteNonQuery();
      }

      //RETURN OUR INSERTED DTO
      return dto;
    }

    public CustomerDto Get(Guid id)
    {
      CustomerDto dto = new CustomerDto();
      var connStr = Properties.Resources.ConnectionString;
      using (OdbcConnection connection = new OdbcConnection(connStr))
      {
        connection.Open();
        string queryStr = string.Format(@"SELECT * " +
                                        @"FROM {0} " +
                                        @"WHERE {1} = '{2}'",
                                        Properties.Resources.CustomerTable,
                                        Properties.Resources.CustomerIdColumn, id.ToString());

        OdbcCommand cmd = new OdbcCommand(queryStr, connection);

        var reader = cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
        if (reader.Read())
        {
          dto.CustomerId = reader.GetGuid(reader.GetOrdinal(Properties.Resources.CustomerIdColumn));
          dto.Name = reader.GetString(reader.GetOrdinal(Properties.Resources.NameColumn));
          dto.EmailAddress = reader.GetString(reader.GetOrdinal(Properties.Resources.EmailAddressColumn));
        }
        else
          throw new CustomerDataException();
      }

      return dto;
    }

    public IList<CustomerDto> GetAll()
    {
      List<CustomerDto> allDtos = new List<CustomerDto>();

      var connStr = Properties.Resources.ConnectionString;
      using (OdbcConnection connection = new OdbcConnection(connStr))
      {
        connection.Open();
        string queryStr = string.Format(@"SELECT * " +
                                        @"FROM {0}",
                                        Properties.Resources.CustomerTable);

        OdbcCommand cmd = new OdbcCommand(queryStr, connection);

        var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
          var dto = new CustomerDto();
          dto.CustomerId = reader.GetGuid(reader.GetOrdinal(Properties.Resources.CustomerIdColumn));
          dto.Name = reader.GetString(reader.GetOrdinal(Properties.Resources.NameColumn));
          dto.EmailAddress = reader.GetString(reader.GetOrdinal(Properties.Resources.EmailAddressColumn));
          allDtos.Add(dto);
        }
      }

      return allDtos;
    }

    public CustomerDto Update(CustomerDto dto)
    {
      var connStr = Properties.Resources.ConnectionString;
      using (OdbcConnection connection = new OdbcConnection(connStr))
      {
        connection.Open();
        string queryStr = string.Format(@"UPDATE {0} " +               //Update Customers table
                                        @"SET {1} = '{2}', " +          //Set Name = dto.Name
                                            @"{3} = '{4}' " +          //Set EmailAddress = dto.EmailAddress
                                        @"WHERE {5} = '{6}'",          //Where Id = 'id'
                                        Properties.Resources.CustomerTable,
                                        Properties.Resources.NameColumn, dto.Name,
                                        Properties.Resources.EmailAddressColumn, dto.EmailAddress,
                                        Properties.Resources.CustomerIdColumn, dto.CustomerId.ToString());

        OdbcCommand cmd = new OdbcCommand(queryStr, connection);

        var numRowsAffected = cmd.ExecuteNonQuery();
      }

      return dto;
    }

    public void Delete(Guid id)
    {
      var connStr = Properties.Resources.ConnectionString;
      using (OdbcConnection connection = new OdbcConnection(connStr))
      {
        connection.Open();
        string queryStr = string.Format(@"DELETE FROM {0} " +          //Delete from Customers table
                                        @"WHERE {1} = '{2}'",          //Where Id = 'id'
                                        Properties.Resources.CustomerTable,
                                        Properties.Resources.CustomerIdColumn, id.ToString());

        OdbcCommand cmd = new OdbcCommand(queryStr, connection);

        var numRowsAffected = cmd.ExecuteNonQuery();
      }
    }
  }
}
