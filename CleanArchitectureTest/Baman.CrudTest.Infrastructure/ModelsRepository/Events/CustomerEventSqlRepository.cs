using Baman.CrudTest.Infrastructure.Models;

namespace Baman.CrudTest.Infrastructure.ModelsRepository.Events;

public class CustomerEventSqlRepository:ICustomerEventsRepository
{
    private CustomerDbContext _customerDbContext;

    public CustomerEventSqlRepository(CustomerDbContext customerDbContext)
    {
        _customerDbContext = customerDbContext;
    }


    public bool Create(CustomerEvent item)
    {
        _customerDbContext.CustomerEvents.Add(item);
        _customerDbContext.SaveChanges();
        return true;
    }

    public CustomerEvent Read(Int64 id)
    {
        return _customerDbContext.CustomerEvents.FirstOrDefault(x => x.Id == id);
    }

    public bool Update(CustomerEvent item)
    {
        var result = _customerDbContext.CustomerEvents.FirstOrDefault(x => x.Email == item.Email);
        if (result == null)
            return false;

        result.FirstName = item.FirstName;
        result.LastName = item.LastName;
        result.BankAccountNumber = item.BankAccountNumber;
        result.PhoneNumber = item.PhoneNumber;
        result.Email = item.Email;
        result.EventDate = item.EventDate;
        result.EventType = item.EventType;

        _customerDbContext.SaveChanges();
        return true;
    }

    public bool Delete(Int64 id)
    {
        var result = _customerDbContext.CustomerEvents.FirstOrDefault(x => x.Id == id);
        if (result == null) return false;
        _customerDbContext.CustomerEvents.Remove(result);
        _customerDbContext.SaveChanges();
        return true;
    }
}