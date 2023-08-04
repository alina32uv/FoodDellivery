using Ardalis.ApiEndpoints;
using Azure;
using FoodDelivery.Dto;
using FoodDelivery.Endpoints.Restaurants;
using FoodDelivery.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FoodDelivery.Endpoints.Customers
{
    public class CreateCustomerRequest
    {

        [FromBody] public CustomersForCreationDto CreatedCustomer { get; set; } = default!;


    }

    public class CreateCustomer : EndpointBaseAsync
        .WithRequest<CreateCustomerRequest>
        .WithActionResult<CustomersForCreationDto>
    {
        private readonly ICustomerRepo _customer;

        public CreateCustomer(ICustomerRepo customer)
        {
            _customer = customer;
        }

        [HttpPost("customers")]
        [SwaggerOperation(Summary = "Create customer",
            Description = "Create customer",
            OperationId = "Customer.Create",
            Tags = new[] { "CustomerEndpoint" })]

        public override async Task<ActionResult<CustomersForCreationDto>> HandleAsync(CreateCustomerRequest request, CancellationToken cancellationToken = default)
        {

            var createdCustomer = await _customer.CreateCustomer(request.CreatedCustomer);
            return CreatedAtRoute("RestaurantById", new { id = createdCustomer.Id }, createdCustomer);
        }
    }
}
