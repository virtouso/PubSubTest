using Baman.CrudTest.Domain.Entities;
using Baman.CrudTest.Infrastructure.Models;

namespace Baman.CrudTest.Infrastructure.ModelsRepository;

public class CustomerSqlRepository : ICustomerRepository
{
    private CustomerDbContext _customerDbContext;

    public CustomerSqlRepository(CustomerDbContext customerDbContext)
    {
        _customerDbContext = customerDbContext;
    }


    public bool Create(Customer item)
    {
        
        _customerDbContext.Customers.Add(item);
        _customerDbContext.SaveChanges();
        return true;
    }

    public Customer Read(Int64 id)
    {
        return _customerDbContext.Customers.FirstOrDefault(x => x.Id == id);
    }

    public bool Update(Customer item)
    {
        var result = _customerDbContext.Customers.FirstOrDefault(x => x.Email == item.Email);
        if (result == null)
            return false;

        result.FirstName = item.FirstName;
        result.LastName = item.LastName;
        result.BankAccountNumber = item.BankAccountNumber;
        result.PhoneNumber = item.PhoneNumber;
        result.Email = item.Email;

        _customerDbContext.SaveChanges();
        return true;
    }

    public bool Delete(Int64 id)
    {
        var result = _customerDbContext.Customers.FirstOrDefault(x => x.Id == id);
        if (result == null) return false;
        _customerDbContext.Customers.Remove(result);
        _customerDbContext.SaveChanges();
        return true;
    }

    public Customer ReadByEmail(string email)
    {
        return _customerDbContext.Customers.FirstOrDefault(x => x.Email == email);
    }

    public Customer ReadByNameFamilyBirthDate(string firstName, string lastName, DateTime dateOfBirth)
    {
        return _customerDbContext.Customers.FirstOrDefault(x => x.FirstName == firstName && x.LastName == lastName && x.DateOfBirth == dateOfBirth);
    }
}