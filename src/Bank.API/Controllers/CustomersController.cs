using Bank.API.Commands;
using Bank.API.Queries.Customer;
using Bank.Core.Contract.Models;
using Bank.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Bank.API.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}/accounts")]
        [SwaggerOperation(
        Summary = "Get Accounts of Customer",
        Description = "Get Accounts of Customer",
        OperationId = "customers.GetAccounts",
        Tags = new[] { "CustomerEndpoints" })]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResult<List<AccountModel>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ServiceResult<List<AccountModel>>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCustomerAccounts(Guid id)
        {
            var request = new GetAccountsByCustomerQuery() { CustomerId = id };
            var response = await _mediator.Send(request);
            if(response.StatusCode == System.Net.HttpStatusCode.NoContent)
                return NoContent();
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPost]
        [SwaggerOperation(
        Summary = "Creates a new Customer",
        Description = "Creates a new Customer",
        OperationId = "customers.create",
        Tags = new[] { "CustomerEndpoints" })]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResult<CustomerModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResult<CustomerModel>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(CreateCustomerCommand request)
        {
            var response = await _mediator.Send(request);
            return StatusCode((int)response.StatusCode, response);
        }
    }
}
