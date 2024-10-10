namespace Baman.CrudTest.Application.Models;

public interface IRequestValidator<T,S>
{
    S Validate(T request);
}