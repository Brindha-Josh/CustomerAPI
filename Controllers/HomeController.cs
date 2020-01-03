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
            await HttpContext.SignOutAsync();
            HttpContext.Response.Cookies.Delete(".AspNetCore.Cookies");
            List<Customer> cusList = new List<Customer>();
            //cusList = customerData.GetAllCustomer().ToList();

            cusList = _context.Customer.ToList();
            return View(cusList);
        }
       // [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Customer>> Create(Customer customer)
        {
            await HttpContext.SignOutAsync();
            _context.Customer.Add(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
            //return View(customer);
            //return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);

        }
        //public IActionResult Create([Bind] Customer objCust)
        //{
        //    if(ModelState.IsValid)
        //    {
        //        customerData.InsertCustomer(objCust);
        //        return RedirectToAction("Index");
        //    }
        //    return View(objCust);
        //}
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            HttpContext.Response.Cookies.Delete(".AspNetCore.Cookies");
            if (id==null)
            {

                return NotFound();
            }
            var customer = await _context.Customer.FindAsync(id);
            //  Customer cust = customerData.GetCustomerByID(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(AuthenticationSchemes = "Bearer")]
        //[HttpPut("{id}")]
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
            return View(customer);
           
        }
        //public IActionResult Edit(int? id,[Bind] Customer objcust)
        //{
        //    if (id == null)
        //    {

        //        return NotFound();
        //    }
        //    if(ModelState.IsValid)
        //    {
        //        customerData.UpdateCustomer(objcust);
        //        return RedirectToAction("Index");
        //    }
        //    return View(customerData);
        //}
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
        //public IActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {

        //        return NotFound();
        //    }
        //    Customer cust = customerData.GetCustomerByID(id);
        //    if (cust == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(cust);

        //}
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
           
            //if (customer == null)
            //{
            //    return NotFound();
            //}

            //_context.Customer.Remove(customer);
            //await _context.SaveChangesAsync();

            //return customer;
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
        //public IActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {

        //        return NotFound();
        //    }
        //    Customer cust = customerData.GetCustomerByID(id);
        //    if (cust == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(cust);
        //}
        //[HttpPost,ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public IActionResult DeleteCust(int? id)
        //{
        //    customerData.DeleteCustomer(id);
        //    return RedirectToAction("Index");
        //}
        private bool CustomerExists(int id)
        {
            return _context.Customer.Any(e => e.Id == id);
        }

    }
  
}