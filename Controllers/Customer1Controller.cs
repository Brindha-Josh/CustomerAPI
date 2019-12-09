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

namespace CUSTOMERAPISQL.Controllers
{

    //[Route("api/[controller]")]
    [ApiController]
    public class Customer1Controller : ControllerBase
    {
        private IConfiguration _config;
        public Customer1Controller(IConfiguration config, PersonDBContext context)
        {
            _config = config;
              _context = context;
        }
        //public IActionResult Login(string username, string pass)
        //{
        //    Customer1 login = new Customer1();
        //    login.UserName = username;
        //    login.Password = pass;
        //    IActionResult response = Unauthorized();
        //    var user = AuthenticateUser(login);
        //    if (user != null)
        //    {
        //        var tokenStr = GenerateJSONWebToken(user);
        //        response = Ok(new { token = tokenStr });
        //    }
        //    return response;
        //}
        //private Customer1 AuthenticateUser(Customer1 login)
        //{
        //    Customer1 user = null;
        //    if (login.UserName == "user" && login.Password == "user")
        //    {
        //        user = new Customer1 { UserName = "user",  Password = "user" };
        //    }
        //    return user;
        //}
        private readonly PersonDBContext _context;

        //public Customer1Controller(PersonDBContext context)
        //{
        //    _context = context;
        //}

        // GET: api/Customer1
        [AllowAnonymous]
        [HttpGet]
        [Route("")]
   
        public async Task<ActionResult<IEnumerable<Customer1>>> GetCustomer1()
        {
            return await _context.Customer1.ToListAsync();
        }
       
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
       
        [HttpPut("{id}")]
        [Route("Edit/{id}")]
        [Authorize]
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
        
        [HttpPost]
        [Route("Create")]
        [Authorize]
        public async Task<ActionResult<Customer1>> PostCustomer1(Customer1 customer1)
        {
            _context.Customer1.Add(customer1);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCustomer1", new { id = customer1.Id }, customer1);
           
        }
       
        // DELETE: api/Customer1/5
        [HttpDelete("{id}")]
        [Route("Del/{id}")]
        [Authorize]
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
        //private string GenerateJSONWebToken(Customer1 userinfo)
        //{
        //    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        //    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        //    var claims = new[]
        //    {
        //        new Claim(JwtRegisteredClaimNames.Sub,userinfo.UserName),
        //        //new Claim(JwtRegisteredClaimNames.Email,userinfo.emailAdd),
        //        new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
        //    };
           
        //    var token = new JwtSecurityToken(
        //        issuer: _config["Jwt:Issuer"],
        //        audience: _config["Jwt:Issuer"],
        //        claims,
        //        expires: DateTime.Now.AddMinutes(120),
        //        signingCredentials: credentials);
        //    var encodetoken = new JwtSecurityTokenHandler().WriteToken(token);
        //    return encodetoken;
        //}
    }
}
