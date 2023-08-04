using Ardalis.ApiEndpoints;
using Azure;
using FoodDelivery.Interfaces;
using FoodDelivery.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FoodDelivery.Endpoints.Addresses
{
    public class GetAddressById : EndpointBaseAsync
       .WithoutRequest
        .WithActionResult<AddressModel>
    {
        private readonly IAddressRepo _address;

        public GetAddressById(IAddressRepo address)
        {
            _address = address;
        }
        [HttpGet("addresses/{id:int}")]
        [SwaggerOperation(Summary = "Get address by id",
            Description = "Get address by id",
            OperationId = "Address.GetById",
            Tags = new[] { "AddressEndpoint" })]
        public override async Task<ActionResult<AddressModel>> HandleAsync(CancellationToken cancellationToken = default)
        {
            int id = int.Parse(HttpContext.Request.RouteValues["id"].ToString());

            var address = await _address.GetAddress(id);
            if (address is null)
                return null;
            return address;
        }
    }
}
