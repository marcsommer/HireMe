using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using System.Data.Odbc;

namespace HireMe.DataAccess.OdbcProvider
{
  [Export(typeof(IReviewDal))]
  [PartCreationPolicy(System.ComponentModel.Composition.CreationPolicy.Shared)]
  public class OdbcReviewDataAdapter : IReviewDal
  {
    public ReviewDto Create()
    {
      //CREATE THE DTO
      ReviewDto dto = new ReviewDto() 
      {
        Id = Guid.NewGuid(), 
        Rating = int.Parse(Properties.Resources.DefaultReviewRating), 
        Comments = Properties.Resources.DefaultReviewComments,
        CustomerId = Guid.Empty
      };

      //INSERT INTO THE DB
      var connStr = Properties.Resources.ConnectionString;
      using (OdbcConnection connection = new OdbcConnection(connStr))
      {
        connection.Open();
        string queryStr = string.Format(@"INSERT INTO {0} " +
                                        @"({1}, {2}, {3}, {4}) " +
                                        @"VALUES('{5}','{6}', '{7}','{8}');",
                                        Properties.Resources.ReviewTable,
                                        Properties.Resources.ReviewIdColumn, Properties.Resources.ReviewRatingColumn, 
                                        Properties.Resources.ReviewCommentsColumn, Properties.Resources.ReviewCustomerIdColumn,
                                        dto.Id, dto.Rating, 
                                        dto.Comments, dto.CustomerId);
        var cmd = connection.CreateCommand();
        cmd.CommandText = queryStr;
        var numRowsAffected = cmd.ExecuteNonQuery();
      }

      //RETURN OUR INSERTED DTO
      return dto;
    }

    public void Delete(Guid id)
    {
      var connStr = Properties.Resources.ConnectionString;
      using (OdbcConnection connection = new OdbcConnection(connStr))
      {
        connection.Open();
        string queryStr = string.Format(@"DELETE FROM {0} " +          //Delete from Reviews table
                                        @"WHERE {1} = '{2}'",          //Where Id = 'id'
                                        Properties.Resources.ReviewTable,
                                        Properties.Resources.ReviewIdColumn, id.ToString());

        OdbcCommand cmd = new OdbcCommand(queryStr, connection);

        var numRowsAffected = cmd.ExecuteNonQuery();
      }
    }

    public ReviewDto Get(Guid id)
    {
      ReviewDto dto = new ReviewDto();
      var connStr = Properties.Resources.ConnectionString;
      using (OdbcConnection connection = new OdbcConnection(connStr))
      {
        connection.Open();
        string queryStr = string.Format(@"SELECT * " +
                                        @"FROM {0} " +
                                        @"WHERE {1} = '{2}'",
                                        Properties.Resources.ReviewTable,
                                        Properties.Resources.ReviewIdColumn, id.ToString());

        OdbcCommand cmd = new OdbcCommand(queryStr, connection);

        var reader = cmd.ExecuteReader(System.Data.CommandBehavior.SingleResult);
        if (reader.Read())
        {
          dto.Id = reader.GetGuid(reader.GetOrdinal(Properties.Resources.ReviewIdColumn));
          dto.Rating = reader.GetInt32(reader.GetOrdinal(Properties.Resources.ReviewRatingColumn));
          dto.Comments = reader.GetString(reader.GetOrdinal(Properties.Resources.ReviewCommentsColumn));
          dto.CustomerId = reader.GetGuid(reader.GetOrdinal(Properties.Resources.ReviewCustomerIdColumn));
        }
        else
          throw new ReviewDataException();
      }

      return dto;
    }

    public IList<ReviewDto> GetAll()
    {
      List<ReviewDto> allDtos = new List<ReviewDto>();

      var connStr = Properties.Resources.ConnectionString;
      using (OdbcConnection connection = new OdbcConnection(connStr))
      {
        connection.Open();
        string queryStr = string.Format(@"SELECT * " +
                                        @"FROM {0}",
                                        Properties.Resources.ReviewTable);

        OdbcCommand cmd = new OdbcCommand(queryStr, connection);

        var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
          var dto = new ReviewDto();
          dto.Id = reader.GetGuid(reader.GetOrdinal(Properties.Resources.ReviewIdColumn));
          dto.Rating = reader.GetInt32(reader.GetOrdinal(Properties.Resources.ReviewRatingColumn));
          dto.Comments = reader.GetString(reader.GetOrdinal(Properties.Resources.ReviewCommentsColumn));
          dto.CustomerId = reader.GetGuid(reader.GetOrdinal(Properties.Resources.ReviewCustomerIdColumn)); 
          allDtos.Add(dto);
        }
      }

      return allDtos;
    }

    public ReviewDto Update(ReviewDto dto)
    {
      var connStr = Properties.Resources.ConnectionString;
      using (OdbcConnection connection = new OdbcConnection(connStr))
      {
        connection.Open();
        string queryStr = string.Format(@"UPDATE {0} " +               //Update Reviews table
                                        @"SET {1} = '{2}', " +         //Set Rating = dto.Rating
                                            @"{3} = '{4}' " +          //Set Comments = dto.Comments
                                            @"{5} = '{6}' " +          //Set CustomerId = dto.CustomerId
                                        @"WHERE {7} = '{8}'",          //Where Id = 'id'
                                        Properties.Resources.ReviewTable,
                                        Properties.Resources.ReviewRatingColumn, dto.Rating,
                                        Properties.Resources.ReviewCommentsColumn, dto.Comments,
                                        Properties.Resources.ReviewCustomerIdColumn, dto.CustomerId,
                                        Properties.Resources.ReviewIdColumn, dto.Id);

        OdbcCommand cmd = new OdbcCommand(queryStr, connection);

        var numRowsAffected = cmd.ExecuteNonQuery();
      }

      return dto;
    }
  }
}
