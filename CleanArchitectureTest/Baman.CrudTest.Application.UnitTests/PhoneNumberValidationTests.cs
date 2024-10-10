using Baman.CrudTest.Application.Helpers;

namespace Baman.CrudTest.Application.UnitTests;

public class PhoneNumberValidationTests
{
    public PhoneNumberValidationTests()
    {
    }


    [Fact]
    public void PhoneValidatorInstance_Not_returnNUll()
    {
        var phoneValidator = new PhoneValidator();

        Assert.NotNull(phoneValidator);
    }

    [Theory]
    [InlineData("12344",5,10, true)]
    [InlineData("1234",5,10, false)]
    [InlineData("12312312312",5,10, false)]
    public void PhoneValidator_Return_True_If_valid_Length(string phoneNumber, int min, int max, bool validResult)
    {
        var phoneValidator = new PhoneValidator();

        bool valid = phoneValidator.Validate("", 5, 10);
        Assert.True(validResult);
    }
}