using Bank.API.Commands;
using Bank.API.Queries.Account;
using Bank.Core.Contract.Models;
using Bank.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Bank.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
        Summary = "Get Account Detail",
        Description = "Get Account Detail",
        OperationId = "accounts.GetById",
        Tags = new[] { "AccountEndpoints" })]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResult<AccountModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResult<AccountModel>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid id)
        {
            var request = new GetAccountByIdQuery() { AccountId = id };
            var response = await _mediator.Send(request);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPost]
        [SwaggerOperation(
        Summary = "Creates a new Account for Customer",
        Description = "Creates a new Account for Customer",
        OperationId = "accounts.create",
        Tags = new[] { "AccountEndpoints" })]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResult<AccountModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResult<AccountModel>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(CreateCustomerAccountCommand request)
        {
            var response = await _mediator.Send(request);
            return StatusCode((int)response.StatusCode, response);
        }

        [HttpPut]
        [SwaggerOperation(
        Summary = "Update balance Account ",
        Description = "Update balance Account, Withdrawl or withdraw money",
        OperationId = "accounts.patch",
        Tags = new[] { "AccountEndpoints" })]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ServiceResult<AccountModel>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ServiceResult<AccountModel>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(UpdateAccountBalanceCommand request)
        {
            var response = await _mediator.Send(request);
            return StatusCode((int)response.StatusCode, response);
        }

    }
}
