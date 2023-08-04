using Ardalis.ApiEndpoints;
using Azure;
using FoodDelivery.Interfaces;
using FoodDelivery.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FoodDelivery.Endpoints.Users
{
    public class GetAllUsers : EndpointBaseAsync
        .WithoutRequest
        .WithActionResult<List<UserModel>>
    {
        private readonly IUsersRepo _user;

        public GetAllUsers(IUsersRepo user)
        {
            _user = user;
        }
        [HttpGet("user")]
        [SwaggerOperation(Summary = "Get all users",
            Description = "Get all users",
            OperationId = "User.GetAll",
            Tags = new[] { "UserEndpoint" })]

        public override async Task<ActionResult<List<UserModel>>> HandleAsync(CancellationToken cancellationToken = default)
        {
            var user = await _user.GetUsers();
            return Ok(user);
        }
    }
}
