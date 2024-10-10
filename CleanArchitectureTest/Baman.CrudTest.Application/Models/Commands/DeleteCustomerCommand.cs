using Baman.CrudTest.Application.Basic;
using MediatR;

namespace Baman.CrudTest.Application.Models.Commands;

public class DeleteCustomerCommand :  IRequest<MetaResponse<bool>>
{
   public  Int64 Id { get; set; } 
}