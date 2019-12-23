using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CustomerMgmt.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
namespace CustomerMgmt.Controllers
{

    [Route("Customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        public CustomerController(PersonDBContext context)
        {

            _context = context;

        }
        private readonly PersonDBContext _context;

        // GET: Customer1
        [AllowAnonymous]
        [HttpGet]
        //[Route("")]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomer()
        {
            return await _context.Customer.ToListAsync();
        }

        // GET: Customer1/5
        //Get Specific Person
        [HttpGet("{id}")]
        // [Route("Details/{id}")]

        [AllowAnonymous]
        public async Task<ActionResult<Customer>> GetCustomer(int id)
        {

            var customer = await _context.Customer.FindAsync(id);

            if (customer== null)
            {
                return NotFound();
            }

            return customer;

        }

        // PUT: Customer1/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPut("{id}")]
        // [Route("Edit/{id}")]
        public async Task<IActionResult> PutCustomer(int id, Customer customer)
        {
            if (id != customer.Id)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: Customer1
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        //[Route("Create")]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            _context.Customer.Add(customer);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);

        }

        // DELETE: Customer1/5
        [HttpDelete("{id}")]
        // [Route("Del/{id}")]

        [Authorize(AuthenticationSchemes = "Bearer")]

        public async Task<ActionResult<Customer>> DeleteCustomer(int id)
        {
            var customer = await _context.Customer.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();

            return customer;
        }

        private bool CustomerExists(int id)
        {
            return _context.Customer.Any(e => e.Id == id);
        }

    }
}
