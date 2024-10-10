using Baman.CrudTest.Application.Basic;
using Baman.CrudTest.Domain.Entities;
using FluentValidation;
using MediatR;

namespace Baman.CrudTest.Application.Models.Queries;

public class ReadCustomerQuery :IRequest<MetaResponse<Customer>>
{
    public string Email { get; set; }


    public class Validator : AbstractValidator<ReadCustomerQuery>
    {
        public Validator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
        }
    }
}