using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FoodDelivery.Interfaces;
using Microsoft.Identity.Client;
using FoodDelivery.Models;
using FoodDelivery.Dto;
using FoodDelivery.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace FoodDelivery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepo usersRepo;

        public UsersController(IUsersRepo usersRepo)
        {
            this.usersRepo = usersRepo;
        }


        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            try
            {
                var users = await usersRepo.GetUsers();
                return Ok(users);



            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }



        }

       /* [HttpGet("{id}", Name = "UserById")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await usersRepo.GetUser(id);
            if (user is null)
                return NotFound();
            return Ok(user);


        }*/
        [Authorize(Roles = "admin")]
        [HttpPost ("AddUser")]
        public async Task<ActionResult> CreateUser([FromBody] UsersForCreationDto user)
        {
            var createdUser = await usersRepo.CreateUser(user);
            return CreatedAtRoute("UserById", new { id = createdUser.Id }, createdUser);

        }
    }
}
