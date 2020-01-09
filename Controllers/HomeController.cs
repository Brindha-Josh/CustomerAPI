using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using CustomerMgmt.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.SqlClient;


namespace CustomerMgmt.Controllers
{
    public class HomeController : Controller
    {
        private readonly DbCountry db = new DbCountry();
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
            DataSet ds = db.GetCountry();
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                list.Add(new SelectListItem { Text = dr["CountryName"].ToString(), Value = dr["CountryName"].ToString() });
            }
            ViewBag.CountryList = list;
            //List<string> CountryList = new List<string>();
            //CultureInfo[] CInfoList = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            //foreach (CultureInfo CInfo in CInfoList)
            //{
            //    RegionInfo R = new RegionInfo(CInfo.LCID);
            //    if (!(CountryList.Contains(R.EnglishName)))
            //    {
            //        CountryList.Add(R.EnglishName);
            //    }
            //}

            //CountryList.Sort();
            //ViewBag.CountryList = CountryList;
            return View();
        }
        public JsonResult GetCityList(string CountryName)
        {
            DataSet ds = db.GetCity(CountryName);
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                list.Add(new SelectListItem { Text = dr["CityName"].ToString(), Value = dr["CityName"].ToString() });
            }
            return Json(list);
        }
       // [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<Customer>> Create(Customer customer)
        {
            
            _context.Customer.Add(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

       // [Authorize]
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