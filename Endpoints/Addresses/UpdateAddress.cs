using Ardalis.ApiEndpoints;
using Azure;
using FoodDelivery.Interfaces;
using FoodDelivery.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FoodDelivery.Endpoints.Addresses
{
    public class UpdateAddressRequest
    {

        
        [FromBody] public AddressModel UpdatedAddress { get; set; } = default!;


    }
    public class UpdateAddress : EndpointBaseAsync
        .WithRequest<UpdateAddressRequest>
        .WithActionResult<AddressModel>
    {
        private readonly IAddressRepo _address;

        public UpdateAddress(IAddressRepo address)
        {
            _address = address;
        }
        [HttpPut("addresses/{id:int}")]
        [SwaggerOperation(Summary = "Update address by id",
           Description = "Update address by id",
           OperationId = "Address.Update",
           Tags = new[] { "AddressEndpoint" })]
        public override async Task<ActionResult<AddressModel>> HandleAsync(UpdateAddressRequest request, CancellationToken cancellationToken = default)
        {
            int id = int.Parse(HttpContext.Request.RouteValues["id"].ToString());

            var address = await _address.GetAddress(id);
            if (address is null)
            {
                return null;
            }
            address.Street = request.UpdatedAddress.Street;
            address.City = request.UpdatedAddress.City;
            address.HouseNumber = request.UpdatedAddress.HouseNumber;
            address.PostalCode = request.UpdatedAddress.PostalCode;

            await _address.UpdateAddress(id, address);

            return Ok(address);
        }
    }
}
