using Baman.CrudTest.Domain.Entities;

namespace Baman.CrudTest.Domain.UnitTests;

public class CustomerTests
{
    [Fact]
    public void CreateCustomer_ReturnsValidObject()
    {
        //arrange
        //act
        Customer customer = new Customer();

        //assert
        Assert.NotNull(customer);
        
    }
}