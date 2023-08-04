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
    [Route("api/customers")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepo customerRepo;

        public CustomerController(ICustomerRepo customerRepo)
        {
            this.customerRepo = customerRepo;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            try
            {
                var customers = await customerRepo.GetCustomers();
                return Ok(customers);



            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }



        }

        [AllowAnonymous]
        [HttpGet("{id}", Name = "CustomerById")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var customer = await customerRepo.GetCustomer(id);
            if (customer is null)
                return NotFound();
            return Ok(customer);


        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> CreateCustomer([FromBody] CustomersForCreationDto customer)
         {
             var createdCustomer = await customerRepo.CreateCustomer(customer);
             return CreatedAtRoute("CustomerById", new { id = createdCustomer.Id }, createdCustomer);

         }
        [AllowAnonymous]
        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] CustomerForUpdateDto customer)
        {
            var dbCustomer = await customerRepo.GetCustomer(id);
            if (dbCustomer is null)
                return NotFound();
            await customerRepo.UpdateCustomer(id, customer);
            return NoContent();
        }
    }
}
