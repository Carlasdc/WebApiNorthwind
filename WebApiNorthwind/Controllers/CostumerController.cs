using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApiNorthwind.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace WebApiNorthwind.Controllers
{
    [Route("api/customer")]
    [ApiController]
    public class CostumerController : ControllerBase
    {
        private readonly NorthwindContext _context;
        public CostumerController(NorthwindContext context)
        {
            _context = context;

        }

        //GET /api/Category
        [HttpGet]
        public IEnumerable<Customer> Get()
        {
            //LinqtoEntities Todos los publishers

            List<Customer> customers = (from c in _context.Customers
                                         select c).ToList();
            return customers;

        }

        // GET /api/category/id
        [HttpGet("{id}")]
        public Customer Get(string id)
        {
            var customers = (from c in _context.Customers
                              where c.CustomerId == id
                              select c).SingleOrDefault();
            return customers;
        }

        [HttpGet("{companyName}/{contactName}")]
        public dynamic Get(string companyName, string contactName)
        {
            dynamic customers = (from c in _context.Customers
                                  where c.CompanyName == companyName && c.ContactName == contactName
                                  select new { c.ContactName, c.CompanyName, c.ContactTitle, c.Phone });
            return customers;
        }

        [HttpPost]
        public ActionResult Post(Customer customer)
        {
            //EF Memoria
            _context.Customers.Add(customer);
            //Agregamos en la base
            _context.SaveChanges();
            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Customer customer)
        {
            if (id != customer.CustomerId)
            {
                return BadRequest();
            }
            _context.Entry(customer).State = EntityState.Modified;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult<Customer> Delete(string id)
        {
            // EF
            var customer = _context.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }
            _context.Customers.Remove(customer);
            _context.SaveChanges();

            return NoContent();
        }
    }
}
