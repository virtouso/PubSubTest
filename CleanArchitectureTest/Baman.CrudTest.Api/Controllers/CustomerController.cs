using Baman.CrudTest.Application.Basic.Enums;
using Baman.CrudTest.Application.Models.Commands;
using Baman.CrudTest.Application.Models.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Baman.CrudTest.Api.Controllers;

[ApiController]
[ApiVersion("1.0")]
public class CustomerController : Controller
{
    private readonly IMediator _mediator;

    public CustomerController(IMediator mediator)
    {
        this._mediator = mediator;
    }


    [Route("v{version:apiVersion}/[controller]/[action]")]
    [HttpPost]
    public async Task<IActionResult> Create(CreateCustomerCommand request)
    {
        var result = await _mediator.Send(request);

        if (result.ResponseType != ResponseType.Success)
            return BadRequest(result);

        return Ok(result);
    }


    [Route("v{version:apiVersion}/[controller]/[action]")]
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] ReadCustomerQuery request)
    {
        var result = await _mediator.Send(request);

        if (result.ResponseType == ResponseType.BadInput)
            return BadRequest(result);
        if (result.ResponseType == ResponseType.NotFound)
            return NotFound(result);
        return Ok(result);
    }


    [Route("v{version:apiVersion}/[controller]/[action]")]
    [HttpPut]
    public async Task<IActionResult> Update(UpdateCustomerCommand request)
    {
        var result = await _mediator.Send(request);

        if (result.ResponseType == ResponseType.BadInput)
            return BadRequest(result);
        if (result.ResponseType == ResponseType.NotFound)
            return NotFound(result);
        return Ok(result);
    }

    [Route("v{version:apiVersion}/[controller]/[action]")]
    [HttpDelete]
    public async Task<IActionResult> Delete(DeleteCustomerCommand request)
    {
        var result = await _mediator.Send(request);
        if (result.ResponseType == ResponseType.NotFound)
            return NotFound(result);
        return Ok(result);
    }
}