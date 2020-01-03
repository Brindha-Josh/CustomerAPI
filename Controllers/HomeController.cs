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
        private readonly PersonDBContext _context;
        public HomeController(PersonDBContext context)
        {

            _context = context;

        }

        [AllowAnonymous]
        [HttpGet]
        public  async Task<IActionResult> Index()
        {
            var cusList = await _context.Customer.ToListAsync();
            return View(cusList);
        }


        public IActionResult Create()
        {
            return View();
        }
        
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Customer>> Create(Customer customer)
        {
            
            _context.Customer.Add(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
       
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
                if (!_context.Customer.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
           

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
        public async Task<IActionResult> DeleteCustomerAsync(int? id)
        {
            var customer = await _context.Customer.FindAsync(id);
            _context.Customer.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    }

}