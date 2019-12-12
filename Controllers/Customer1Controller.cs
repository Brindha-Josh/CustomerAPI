using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CUSTOMERAPISQL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
namespace CUSTOMERAPISQL.Controllers
{

    //[Route("api/[controller]")]
    [ApiController]
    public class Customer1Controller : ControllerBase
    {
       
        public Customer1Controller(PersonDBContext context)
        {
            
              _context = context;
           
        }
       

        private readonly PersonDBContext _context;
       
        //public Customer1Controller(PersonDBContext context)
        //{
        //    _context = context;
        //}
        [AllowAnonymous]
        // GET: api/Customer1
        [HttpGet]
        [Route("")]
        
        public async Task<ActionResult<IEnumerable<Customer1>>> GetCustomer1()
        {
            return await _context.Customer1.ToListAsync();
        }
        //[AllowAnonymous]
        // GET: api/Customer1/5
        //Get Specific Person
        [HttpGet("{id}")]
        
        [Route("Details/{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Customer1>> GetCustomer1(int id)
        {
            
            var customer1 = await _context.Customer1.FindAsync(id);

            if (customer1 == null)
            {
                return NotFound();
            }

            return customer1;
            
        }

        // PUT: api/Customer1/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [Authorize]
        [HttpPut("{id}")]
        [Route("Edit/{id}")]
        public async Task<IActionResult> PutCustomer1(int id, Customer1 customer1)
        {
            if (id != customer1.Id)
            {
                return BadRequest();
            }

            _context.Entry(customer1).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Customer1Exists(id))
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

        // POST: api/Customer1
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [Authorize]
        [HttpPost]
        [Route("Create")]
        public async Task<ActionResult<Customer1>> PostCustomer1(Customer1 customer1)
        {
            _context.Customer1.Add(customer1);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer1", new { id = customer1.Id }, customer1);
           
        }
        [Authorize]
        // DELETE: api/Customer1/5
        [HttpDelete("{id}")]
        [Route("Del/{id}")]
        public async Task<ActionResult<Customer1>> DeleteCustomer1(int id)
        {
            var customer1 = await _context.Customer1.FindAsync(id);
            if (customer1 == null)
            {
                return NotFound();
            }

            _context.Customer1.Remove(customer1);
            await _context.SaveChangesAsync();

            return customer1;
        }

        private bool Customer1Exists(int id)
        {
            return _context.Customer1.Any(e => e.Id == id);
        }
       
    }
}
