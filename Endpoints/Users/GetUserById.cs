using Ardalis.ApiEndpoints;
using FoodDelivery.Interfaces;
using FoodDelivery.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace FoodDelivery.Endpoints.Users
{
    public class GetUserById : EndpointBaseAsync
       .WithoutRequest
        .WithActionResult<UserModel>
    {
        private readonly IUsersRepo _user;

        public GetUserById(IUsersRepo user)
        {
            _user = user;
        }

        [HttpGet("user/{id:int}", Name = "UserById")]
        [SwaggerOperation(Summary = "Get an user by id",
           Description = "Get an user by id",
           OperationId = "User.GetById",
           Tags = new[] { "UserEndpoint" })]

        public override async Task<ActionResult<UserModel>> HandleAsync(CancellationToken cancellationToken = default)
        {
            int id = int.Parse(HttpContext.Request.RouteValues["id"].ToString());

            var user = await _user.GetUser(id);
            if (user is null)
                return null;
            return Ok(user);
        }
    }
}
