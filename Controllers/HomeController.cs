using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerMgmt.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CustomerMgmt.Controllers
{
    public class HomeController : Controller
    {
        public CustomerData customerData = new CustomerData();
        public Customer customer = new Customer();
        public HomeController(PersonDBContext context)
        {

            _context = context;

        }
        private readonly PersonDBContext _context;
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<Customer> cusList = new List<Customer>();
            cusList = _context.Customer.ToList();
            return View(cusList);
        }
        // [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
       // [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Customer>> Create(Customer customer)
        {
            //await HttpContext.SignOutAsync();
            _context.Customer.Add(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        //[Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {

                return NotFound();
            }
            var customer = await _context.Customer.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }
       // [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Customer customer)
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
                    return RedirectToAction("Index");
                    throw;
                }
            }
            return RedirectToAction("Index");
            //return View(customer);

        }
        [HttpGet]
        public async Task<ActionResult<Customer>> Details(int id)
        {

            var customer = await _context.Customer.FindAsync(id);

            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);

        }
        public async Task<ActionResult<Customer>> Delete(int? id)
        {
            if (id == null)
            {

                return NotFound();
            }
            var customer = await _context.Customer.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCustAsync(int? id)
        {
            var customer = await _context.Customer.FindAsync(id);
            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        private bool CustomerExists(int id)
        {
            return _context.Customer.Any(e => e.Id == id);
        }

    }

}