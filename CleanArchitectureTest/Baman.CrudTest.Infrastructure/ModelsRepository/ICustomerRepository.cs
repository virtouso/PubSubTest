using Baman.CrudTest.Domain.Entities;

namespace Baman.CrudTest.Infrastructure.ModelsRepository;

public interface ICustomerRepository: IRepository<Customer>
{
    Customer ReadByEmail(string Email);
    Customer ReadByNameFamilyBirthDate(string firstName,string lastName, DateTime dateOfBirth);
}