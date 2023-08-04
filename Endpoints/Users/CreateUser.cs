using Ardalis.ApiEndpoints;
using Azure;
using FoodDelivery.Dto;
using FoodDelivery.Endpoints.FoodOrders;
using FoodDelivery.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FoodDelivery.Endpoints.Users
{
    public class CreateUserRequest
    {

        [FromBody] public UsersForCreationDto CreatedUser { get; set; } = default!;
    }
    public class CreateUser : EndpointBaseAsync
        .WithRequest<CreateUserRequest>
        .WithActionResult<UsersForCreationDto>
    {
        private readonly IUsersRepo _user;

        public CreateUser(IUsersRepo user)
        {
            _user = user;
        }

        [HttpPost("user")]
        [SwaggerOperation(Summary = "Create a new user",
            Description = "Create a new user",
            OperationId = "User.Create",
            Tags = new[] { "UserEndpoint" })]
        public override async Task<ActionResult<UsersForCreationDto>> HandleAsync(CreateUserRequest request, CancellationToken cancellationToken = default)
        {
            var createdUser = await _user.CreateUser(request.CreatedUser);
            return CreatedAtRoute("FoodOrderById", new { id = createdUser.Id }, createdUser);
        }
    }
}
