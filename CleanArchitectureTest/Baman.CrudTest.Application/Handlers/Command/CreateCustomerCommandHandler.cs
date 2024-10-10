using Baman.CrudTest.Application.Basic;
using Baman.CrudTest.Application.Basic.Enums;
using Baman.CrudTest.Application.Models;
using Baman.CrudTest.Application.Models.Commands;
using Baman.CrudTest.Domain.Entities;
using Baman.CrudTest.Infrastructure.Models;
using Baman.CrudTest.Infrastructure.ModelsRepository;
using Baman.CrudTest.Infrastructure.ModelsRepository.Events;
using MediatR;

namespace Baman.CrudTest.Application.Handlers.Command;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, MetaResponse<bool>>
    , IRequestValidator<CreateCustomerCommand, MetaResponse<bool>>
{
    private ICustomerRepository _customerRepository;
    private ICustomerEventsRepository _customerEventsRepository;

    public CreateCustomerCommandHandler(ICustomerRepository customerRepository, ICustomerEventsRepository customerEventsRepository)
    {
        _customerRepository = customerRepository;
        _customerEventsRepository = customerEventsRepository;
    }


    public async Task<MetaResponse<bool>> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var validationResult = Validate(request);
        if (!validationResult.Result)
            return validationResult;

        // could use automapper
        Customer newCustomer = new Customer
        {
            Email = request.Email
            , DateOfBirth = request.DateOfBirth
            , FirstName = request.FirstName
            , LastName = request.LastName
            , PhoneNumber = request.PhoneNumber
            , BankAccountNumber = request.BankAccountNumber
        };

        _customerRepository.Create(newCustomer);

        _customerEventsRepository.Create(new CustomerEvent
        {
            Email = request.Email
            , DateOfBirth = request.DateOfBirth
            , FirstName = request.FirstName
            , LastName = request.LastName
            , PhoneNumber = request.PhoneNumber
            , BankAccountNumber = request.BankAccountNumber
            , EventDate = DateTime.UtcNow
            , EventType = EventType.Create
        });


        return new MetaResponse<bool> { ResponseType = ResponseType.Success, Result = true };
    }


    public MetaResponse<bool> Validate(CreateCustomerCommand request)
    {
        CreateCustomerCommand.Validator validator = new();

        if (!validator.Validate(request).IsValid)
            return new MetaResponse<bool> { ResponseType = ResponseType.BadInput, Message = "invalid input sent", Result = false };


        var emailResult = _customerRepository.ReadByEmail(request.Email);
        if (emailResult != null)
            return new MetaResponse<bool> { ResponseType = ResponseType.EmailExist, Message = "email exist", Result = false };


        var nameAndBirthResult = _customerRepository.ReadByNameFamilyBirthDate(request.FirstName, request.LastName, request.DateOfBirth);
        if (nameAndBirthResult != null)
            return new MetaResponse<bool>
            {
                ResponseType = ResponseType.SameNameAndBirth, Message = "user with same name and family exist", Result = false
            };


        return new MetaResponse<bool> { ResponseType = ResponseType.Success, Result = true };
    }
}