using Baman.CrudTest.Application.Basic;
using Baman.CrudTest.Application.Basic.Enums;
using Baman.CrudTest.Application.Models;
using Baman.CrudTest.Application.Models.Queries;
using Baman.CrudTest.Domain.Entities;
using Baman.CrudTest.Infrastructure.ModelsRepository;
using MediatR;

namespace Baman.CrudTest.Application.Handlers.Query;

public class ReadCustomerQueryHandler : IRequestHandler<ReadCustomerQuery, MetaResponse<Customer>>, IRequestValidator<ReadCustomerQuery, bool>
{

    private ICustomerRepository _customerRepository;

    public ReadCustomerQueryHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<MetaResponse<Customer>> Handle(ReadCustomerQuery request, CancellationToken cancellationToken)
    {
        var valid = Validate(request);
        if (!valid)
            return new MetaResponse<Customer>{Message = "invalid input", ResponseType = ResponseType.BadInput};

        var result = _customerRepository.ReadByEmail(request.Email);
        if (result == null)
            return new MetaResponse<Customer> {ResponseType = ResponseType.NotFound, Message = "customer not found"};

        return new MetaResponse<Customer> {Result = result, ResponseType = ResponseType.Success};
    }

    public bool Validate(ReadCustomerQuery request)
    {
        ReadCustomerQuery.Validator validator = new();
        return validator.Validate(request).IsValid;
    }
}