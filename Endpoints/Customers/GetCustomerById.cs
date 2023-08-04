using Ardalis.ApiEndpoints;
using FoodDelivery.Interfaces;
using FoodDelivery.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FoodDelivery.Endpoints.Customers
{
    public class GetCustomerById : EndpointBaseAsync
       .WithoutRequest
        .WithResult<CustomersModel>
    {
        private readonly ICustomerRepo _customer;

        public GetCustomerById(ICustomerRepo customer)
        {
            _customer = customer;
        }
        [HttpGet("customers/{id:int}")]
        [SwaggerOperation(Summary = "Get a customer by id",
            Description = "Get a customer by id",
            OperationId = "Customer.GetById",
            Tags = new[] { "CustomerEndpoint" })]
        public override async Task<CustomersModel> HandleAsync(CancellationToken cancellationToken = default)
        {
            int id = int.Parse(HttpContext.Request.RouteValues["id"].ToString());

            var customer = await _customer.GetCustomer(id);
            if (customer is null)
                return null;
            return customer; 
        }
    }
}
