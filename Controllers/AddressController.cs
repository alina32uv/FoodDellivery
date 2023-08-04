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
    
    [Route("api/Adress")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressRepo addressRepo;
        public AddressController(IAddressRepo addressRepo)
        {
            this.addressRepo = addressRepo;
        }


        [HttpGet("{id}", Name = "AddressById")]
        public async Task<IActionResult> GetAddress(int id)
        {
            var address = await addressRepo.GetAddress(id);
            if (address is null)
                return NotFound();
            return Ok(address);


        }


        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateAddress(int id, [FromBody] AddressForUpdateDto address)
        {
            var dbAddress = await addressRepo.GetAddress(id);
            if (dbAddress is null)
                return NotFound();
            await addressRepo.UpdateAddress(id, address);
            return NoContent();
        }


    }
}
