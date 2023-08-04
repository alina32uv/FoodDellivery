using FoodDelivery.Models;
using Microsoft.AspNetCore.Mvc;
using FoodDelivery.Dto;
using Ardalis.ApiEndpoints;
using FoodDelivery.Endpoints.Restaurants;
using FoodDelivery.Interfaces;
using Azure;
using Swashbuckle.AspNetCore.Annotations;

namespace FoodDelivery.Endpoints.Customers
{
    public class UpdateCustomerRequest
    {

        [FromRoute(Name = "id")] public int Id { get; set; }
        [FromBody] public CustomersModel UpdatedCustomer { get; set; } = default!;


    }
    public class UpdateCustomer : EndpointBaseAsync
        .WithRequest<UpdateCustomerRequest>
        .WithResult<CustomersModel>
    {
        private readonly ICustomerRepo _customer;

        public UpdateCustomer(ICustomerRepo customer)
        {
            _customer = customer;
        }
        [HttpPut("customers/{id:int}")]
        [SwaggerOperation(Summary = "Update customer by id",
            Description = "Update customer by id",
            OperationId = "Customer.Update",
            Tags = new[] { "CustomerEndpoint" })]
        public override async Task<CustomersModel> HandleAsync(UpdateCustomerRequest request, CancellationToken cancellationToken = default)
        {
            var customer = await _customer.GetCustomer(request.Id);
            if (customer is null)
            {
                return null;
            }
            customer.FName = request.UpdatedCustomer.FName;
            customer.LName = request.UpdatedCustomer.LName;

            await _customer.UpdateCustomer(request.Id, customer);

            return customer;
        }
    }
}
