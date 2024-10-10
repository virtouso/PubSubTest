namespace Baman.CrudTest.Infrastructure.Models;

public class CustomerEvent
{
    public Int64 Id { get; set; }// unique event id
    
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string BankAccountNumber { get; set; }
    
    public DateTime EventDate { get; set; }
    public EventType EventType { get; set; }
    
}

public enum EventType
{
    Create, Update,Delete
}