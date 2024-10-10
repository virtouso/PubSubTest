using Baman.CrudTest.Application.Basic.Enums;
using Baman.CrudTest.Application.Handlers.Query;
using Baman.CrudTest.Application.Models.Queries;
using Baman.CrudTest.Domain.Entities;
using Baman.CrudTest.Infrastructure.ModelsRepository;

namespace Baman.CrudTest.Application.UnitTests;

public class ReadQueryHandlerTests
{

    private readonly ReadCustomerQueryHandler _queryHandler;
    private ICustomerRepository _customerRepository;
    public ReadQueryHandlerTests()
    {
        _customerRepository = new CustomerInMemoryRepository();
        _queryHandler = new ReadCustomerQueryHandler(_customerRepository);
    }


    [Theory]
    [InlineData("1232", false)]
    [InlineData("ali@yahoo.com", true)]
    void Should_Validate_Right_Input(string email, bool valid)
    {
      Assert.Equal(_queryHandler.Validate(new ReadCustomerQuery{Email = email}),valid);
    }


    [Theory]
    [InlineData("mail@yahoo.com", ResponseType.NotFound)]
    [InlineData("m@yahoo.com", ResponseType.Success)]
    void Should_Return_Right_value(string email, ResponseType responseType)
    {
        _customerRepository.Create(new Customer{Email ="m@yahoo.com", Id = 1});
        
        Assert.Equal(_queryHandler.Handle(new ReadCustomerQuery{Email = email},CancellationToken.None).Result.ResponseType,responseType);
        
    }
}