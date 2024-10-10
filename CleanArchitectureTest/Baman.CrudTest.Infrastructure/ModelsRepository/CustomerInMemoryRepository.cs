using Baman.CrudTest.Domain.Entities;

namespace Baman.CrudTest.Infrastructure.ModelsRepository;

// this class is used for unit testing
public class CustomerInMemoryRepository : ICustomerRepository
{
    private List<Customer> _customers = new();


    public bool Create(Customer item)
    {
        if (_customers.FirstOrDefault(x => x.Id == item.Id) != null)
            return false;
        _customers.Add(item);
        return true;
    }

    public Customer Read(Int64 id)
    {
        return _customers.FirstOrDefault(x => x.Id == id);
    }

    public bool Update(Customer item)
    {
        var result = _customers.FirstOrDefault(x => x.Email == item.Email);

        if (result == null) return false;

        result.Email = item.Email;
        result.FirstName = item.FirstName;
        result.LastName = item.LastName;
        result.BankAccountNumber = item.BankAccountNumber;
        result.PhoneNumber = item.PhoneNumber;
        result.DateOfBirth = item.DateOfBirth;

        return true;
    }

    public bool Delete(Int64 id)
    {
        var result = _customers.FirstOrDefault(x => x.Id == id);
        if (result == null) return false;

        _customers.Remove(result);
        return true;
    }

    public Customer ReadByEmail(string Email)
    {
        return _customers.FirstOrDefault(x => x.Email == Email);
    }

    public Customer ReadByNameFamilyBirthDate(string firstName, string lastName, DateTime dateOfBirth)
    {
        return _customers.FirstOrDefault(x => x.FirstName == firstName && x.LastName == lastName && x.DateOfBirth == dateOfBirth);
    }
}