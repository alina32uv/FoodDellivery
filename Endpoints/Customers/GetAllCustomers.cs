using Ardalis.ApiEndpoints;
using Azure;
using FoodDelivery.Interfaces;
using FoodDelivery.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FoodDelivery.Endpoints.Customers
{
    public class GetAllCustomers : EndpointBaseAsync
        .WithoutRequest
        .WithActionResult<List<CustomersModel>>
    {
        private readonly ICustomerRepo _customer;

        public GetAllCustomers(ICustomerRepo customer)
        {
            _customer = customer;
        }
        [HttpGet("customers")]
        [SwaggerOperation(Summary = "Gets all customers",
            Description = "Gets all customers",
            OperationId = "Customer.GetAll",
            Tags = new[] { "CustomerEndpoint" })]
        public override async Task<ActionResult<List<CustomersModel>>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var customers = await _customer.GetCustomers();
            return Ok(customers); 
        }
    }
}
