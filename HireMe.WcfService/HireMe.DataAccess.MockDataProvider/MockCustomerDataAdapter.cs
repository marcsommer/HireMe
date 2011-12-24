using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition;
using HireMe.DataAccess;
using HireMe.MockData;

namespace HireMe.DataAccess.MockDataProvider
{
  [Export(typeof(ICustomerDal))]
  public class MockCustomerDataAdapter : ICustomerDal
  {
    public CustomerDto Create()
    {
      var data = new CustomerData();
      data.CustomerId = Guid.NewGuid();
      MockDb.Customers.Add(data);
      return CreateDtoFromData(data);
    }
    public void Delete(Guid id)
    {
      MockDb.Customers.Remove(GetCustomer(id));
    }
    public CustomerDto Get(Guid id)
    {
      var custData = GetCustomer(id);
      return CreateDtoFromData(custData);
    }
    public IList<CustomerDto> GetAll()
    {
      var results = from cust in MockDb.Customers
                    select CreateDtoFromData(cust);
      return results.ToList();
      
      //List<CustomerDto> allCustomerDtos = new List<CustomerDto>();
      //foreach (var custData in MockDb.Customers)
      //{
      //  allCustomerDtos.Add(CreateDtoFromData(custData));
      //}
      //return allCustomerDtos;
    }
    public CustomerDto Update(CustomerDto dto)
    {
      var data = GetCustomer(dto.Id);
      data.Name = dto.Name;
      data.EmailAddress = dto.EmailAddress;
      data.ReviewIds = new List<Guid>(dto.ReviewIds);
      return dto;
    }

    public static CustomerDto CreateDtoFromData(CustomerData data)
    {
      return new CustomerDto()
      {
        Id = data.CustomerId,
        Name = data.Name,
        EmailAddress = data.EmailAddress,
        ReviewIds = new List<Guid>(data.ReviewIds)
      };
    }
    private CustomerData GetCustomer(Guid id)
    {
      //For some reason, this is returning all reviews (ignoring where clause?)
      //var results = from data in MockData.MockDb.Customers
      //              where data.CustomerId == id
      //              select data;
      //if (results.Count() != 1)
      //  throw new CustomerDataException();

      //return results.ElementAt(0);

      var results = new List<CustomerData>();
      foreach (var data in MockDb.Customers)
      {
        if (data.CustomerId == id)
          results.Add(data);
      }
      if (results.Count != 1)
        throw new CustomerDataException();

      return results[0];
    }
  }

}

