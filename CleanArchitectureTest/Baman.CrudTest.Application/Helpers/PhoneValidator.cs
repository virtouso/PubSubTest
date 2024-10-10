namespace Baman.CrudTest.Application.Helpers;

public class PhoneValidator
{
    public bool Validate(string phoneNumber, int min, int max)
    {
        return phoneNumber.Length >= min && phoneNumber.Length <= max;
    }
}