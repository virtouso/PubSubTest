using Baman.CrudTest.Infrastructure.Models;

namespace Baman.CrudTest.Infrastructure.ModelsRepository.Events;

public class CustomerEventInMemoryRepository:ICustomerEventsRepository
{
    private List<CustomerEvent> _customers = new();


    public bool Create(CustomerEvent item)
    {
        if (_customers.FirstOrDefault(x => x.Id == item.Id) != null)
            return false;
        _customers.Add(item);
        return true;
    }

    public CustomerEvent Read(Int64 id)
    {
        return _customers.FirstOrDefault(x => x.Id == id);
    }

    public bool Update(CustomerEvent item)
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

    
}