using Baman.CrudTest.Application.Handlers.Command;
using Baman.CrudTest.Application.Models.Commands;
using Baman.CrudTest.Domain.Entities;
using Baman.CrudTest.Infrastructure.ModelsRepository;
using Baman.CrudTest.Infrastructure.ModelsRepository.Events;

namespace Baman.CrudTest.Application.UnitTests;

public class UpdateCommandHandlerTests
{
     private readonly UpdateCustomerCommandHandler _commandHandler;
    private ICustomerRepository _customerRepository;
    private readonly ICustomerEventsRepository _customerEventsRepository;

    public UpdateCommandHandlerTests()
    {
        _customerEventsRepository = new CustomerEventInMemoryRepository();
        _customerRepository = new CustomerInMemoryRepository();
        _commandHandler = new UpdateCustomerCommandHandler(_customerRepository,_customerEventsRepository);
    }


    [Theory]
    [InlineData("", "ali", "2024561111", "a@y.com", "1231231231",false)]
    [InlineData("", "ali", "2024561111", "c@y.com", "1231231231",true)]
    public void Should_Invalidate_NotFound(string name, string family, string phoneNumber, string email, string bankNumber, bool validate)
    {
        //arrange
        _customerRepository.Create(new Customer
        {
            Id = 1
            , FirstName = "c"
            , Email = "c@y.c"
            , BankAccountNumber = "111222333"
            , LastName = "het"
            , PhoneNumber = "123123123"
            , DateOfBirth = new DateTime(1990, 2, 2)
        });

        //act
        var result = _commandHandler.Validate(new UpdateCustomerCommand
        {
            BankAccountNumber = bankNumber
            , PhoneNumber = phoneNumber
            , DateOfBirth = new DateTime(1991, 2, 2)
            , FirstName = name
            , Email = email
            , LastName = family
        });

        //assert

        Assert.Equal(result.Result, false);
    }

  
    [Theory]
    [InlineData("", "ali", "2024561111", "a@y.com", "1231231231",false)]
    [InlineData("ali", "ali", "2024561111", "a@y.com", "1231231231",true)]
    [InlineData("ali", "ali", "2024561111", "ay.com", "1231231231",false)]
    [InlineData("ali", "ali", "123123123", "a@y.com", "1231231231",false)]
    void Should_Validate_Right_Input(string name, string family, string phoneNumber, string email, string bankNumber, bool validate)
    {
        //arrange

        _customerRepository.Create(new Customer{Email = "a@y.com" });
        
        //act
       var result= _commandHandler.Validate(new UpdateCustomerCommand
        {
            BankAccountNumber = bankNumber
            , PhoneNumber = phoneNumber
            , DateOfBirth = new DateTime(1990, 2, 2)
            , FirstName = name
            , Email = email
            , LastName = family
        });
        
        
        //assert
        Assert.Equal(result.Result,validate);
    }


    [Theory]
    [InlineData("ali", "ali", "2024561111", "a@y.com", "1231231231",true)]
    public void Should_Update_When_Right(string name, string family, string phoneNumber, string email, string bankNumber, bool valid)
    {
        //arrage
        
        _customerRepository.Create(new Customer{Email = "a@y.com" });
        
        //act
        var result= _commandHandler.Handle(new UpdateCustomerCommand
        {
            BankAccountNumber = bankNumber
            , PhoneNumber = phoneNumber
            , DateOfBirth = new DateTime(1990, 2, 2)
            , FirstName = name
            , Email = email
            , LastName = family
        }, CancellationToken.None);
        
        //assert

        var find = _customerRepository.ReadByEmail(email);
        Assert.Equal(find.FirstName== name && find.LastName== family && find.BankAccountNumber== bankNumber,valid );
    }

}