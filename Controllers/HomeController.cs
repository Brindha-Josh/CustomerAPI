using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerMgmt.Models;
using Microsoft.AspNetCore.Mvc;

namespace CustomerMgmt.Controllers
{
    public class HomeController : Controller
    {
 public CustomerData customerData = new CustomerData();
        public IActionResult Index()
        {
            List<Customer> cusList = new List<Customer>();
        cusList = customerData.GetAllCustomer().ToList();
           

            return View(cusList);
                   }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind] Customer objCust)
        {
            if(ModelState.IsValid)
            {
                customerData.InsertCustomer(objCust);
                return RedirectToAction("Index");
            }
            return View(objCust);
        }
        public IActionResult Edit(int? id)
        {
            if (id==null)
            {

                return NotFound();
            }
            Customer cust = customerData.GetCustomerByID(id);
            if(cust==null)
            {
                return NotFound();
            }
            return View(cust);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int? id,[Bind] Customer objcust)
        {
            if (id == null)
            {

                return NotFound();
            }
            if(ModelState.IsValid)
            {
                customerData.UpdateCustomer(objcust);
                return RedirectToAction("Index");
            }
            return View(customerData);
        }
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {

                return NotFound();
            }
            Customer cust = customerData.GetCustomerByID(id);
            if (cust == null)
            {
                return NotFound();
            }
            return View(cust);

        }
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {

                return NotFound();
            }
            Customer cust = customerData.GetCustomerByID(id);
            if (cust == null)
            {
                return NotFound();
            }
            return View(cust);
        }
        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCust(int? id)
        {
            customerData.DeleteCustomer(id);
            return RedirectToAction("Index");
        }
       
    }
}